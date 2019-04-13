using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    [ServiceContract(CallbackContract = typeof(INewsServiceCallback))]
    public interface INewsService
    {
        /// <summary>
        /// Подписаться на новостную рассылку по теме.
        /// </summary>
        /// <param name="category">Тема новостной рассылки</param>
        [OperationContract(IsOneWay = false)]
        bool Subscribe(string category);
    }

    public interface INewsServiceCallback
    {
        /// <summary>
        /// Отправить подписчику статью.
        /// </summary>
        /// <param name="article"></param>
        [OperationContract(IsOneWay = true)]
        void SendArticle(Article article);
    }

    [ServiceBehavior]
    public class NewsService : INewsService
    {
        public TimeSpan delayMailing = new TimeSpan(0, 0, 10);
        public CancellationTokenSource cancellationMailing;

        private ConcurrentDictionary<INewsServiceCallback, Dictionary<string, int>> subscribers;
        private Task mailingTask;

        public NewsService()
        {
            subscribers = new ConcurrentDictionary<INewsServiceCallback, Dictionary<string, int>>();
            cancellationMailing = new CancellationTokenSource();
            mailingTask = Task.Factory.StartNew(mailing, cancellationMailing.Token);
        }

        ~NewsService()
        {
            cancellationMailing.Cancel();
        }

        /// <summary>
        /// Подписаться на рассылку новостей по указанной тематике.
        /// </summary>
        /// <param name="category">Тема рассылки</param>
        public bool Subscribe(string category)
        {
            INewsServiceCallback subscriber = OperationContext.Current.GetCallbackChannel<INewsServiceCallback>();
            bool hasCategory = RssService.GetArticles(category).Count > 0;

            if (hasCategory)
            {    
                Dictionary<string, int> subCategories = subscribers.GetOrAdd(subscriber, new Dictionary<string, int>());
                subCategories.Add(category, 0);
            }
            Log.SubscribeInfo(subscriber.GetHashCode(), category, hasCategory);
            return hasCategory;
        }

        private void mailing()
        {
            while (!cancellationMailing.Token.IsCancellationRequested)
            {
                List<Tuple<INewsServiceCallback, string>> info = new List<Tuple<INewsServiceCallback, string>>();

                // Проход по всем клиентам
                foreach (KeyValuePair<INewsServiceCallback, Dictionary<string, int>> sub in subscribers)
                {
                    // Проход по всем подпискам клиента
                    foreach (KeyValuePair<string, int> subInfo in sub.Value)
                    {
                        List<Article> articles = RssService.GetArticles(subInfo.Key);
                        if (articles.Count > 0 && articles.Count > subInfo.Value)
                        {
                            // Пытаемся отправить пользователю статью
                            try
                            {
                                sub.Key.SendArticle(articles[subInfo.Value]);
                                Log.MailingInfo(sub.Key.GetHashCode(), subInfo.Key, true);
                                info.Add(new Tuple<INewsServiceCallback, string>(sub.Key, subInfo.Key));
                            }
                            catch (TimeoutException e)
                            {
                                Log.MailingInfo(sub.Key.GetHashCode(), subInfo.Key, false);
                                // Удаляем пользователя из рассылки
                                subscribers.TryRemove(sub.Key, out Dictionary<string, int> s);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Необработаная ошибка " + e.GetType().ToString() + " " + e.Message);
                            }
                        }
                    }
                }

                foreach (Tuple<INewsServiceCallback, string> i in info)
                {
                    subscribers[i.Item1][i.Item2]++;
                }
                
                mailingTask.Wait(delayMailing);
            }
        }
    }
}
