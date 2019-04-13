using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    public static class RssService
    {
        public enum State { NotInitialized, Stoped, Runing, Error }
        public static TimeSpan delayLoading = new TimeSpan(0, 10, 0);
        public static State Status = State.NotInitialized;

        private static string[] newsServices = {
            "https://www.theguardian.com/international/rss",
            "https://www.theguardian.com/us/rss",
            "https://www.theguardian.com/uk/rss"
        };
        private static CancellationTokenSource cancellationGettingNews;
        private static Task getNewsTask;
        private static ConcurrentBag<Article> news;

        /// <summary>
        /// Начать периодическую загрузку новостей из информационных порталов.
        /// </summary>
        public static void Start()
        {
            if (Status != State.Runing)
            {
                news = new ConcurrentBag<Article>();
                cancellationGettingNews = new CancellationTokenSource();
                getNewsTask = Task.Factory.StartNew(loadingNews, cancellationGettingNews.Token);
                Status = State.Runing;
            }
        }

        /// <summary>
        /// Остановить периодическую загрузки новостей из информационный порталов.
        /// </summary>
        public static void Stop()
        {
            if (Status == State.Runing)
            {
                cancellationGettingNews.Cancel();
                Status = State.Stoped;
            }
        }

        /// <summary>
        /// Получить все статьи указанной тематики.
        /// </summary>
        /// <param name="category">Тема статей</param>
        public static List<Article> GetArticles(string category)
        {
            if (Status != State.NotInitialized)
            {
                return news.Where(article => article.IsCategory(category.ToLower())).ToList<Article>();
            }
            return new List<Article>();
        }

        private static void loadingNews()
        {
            while (!cancellationGettingNews.Token.IsCancellationRequested)
            {
                foreach (string service in newsServices)
                {
                    using(XmlReader reader = XmlReader.Create(service))
                    {
                        while (reader.ReadToFollowing("item"))
                        {
                            XmlNode node = new XmlDocument().ReadNode(reader);
                            Article article = new Article()
                            {
                                Title = node.SelectSingleNode("title").InnerText,
                                Description = node.SelectSingleNode("description").InnerText,
                                Link = node.SelectSingleNode("link").InnerText,
                                Date = Convert.ToDateTime(node.SelectSingleNode("pubDate").InnerText),
                                Categories = new List<string>()
                            };

                            foreach (XmlNode item in node.SelectNodes("category"))
                            {
                                article.Categories.Add(item.InnerText.ToLower());
                            }
                            
                            lock (news)
                            {
                                if (!news.Contains(article))
                                {
                                    news.Add(article);
                                }
                            }
                        }
                    }
                }
                Log.LoadingNewsInfo(news.Count, newsServices.Length);
                getNewsTask.Wait(delayLoading);
            }
        }

        
    }
}
