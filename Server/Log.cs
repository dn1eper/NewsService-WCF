using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Log
    {
        private static string now => DateTime.Now.ToString() + "   ";

        public static void ServiceStartInfo(string url)
        {
            Console.WriteLine(string.Concat(
                now,
                "Сервис запущен по адресу: ",
                url
            ));
        }

        public static void LoadingNewsInfo(int newsCount, int servicesCount)
        {
            Console.WriteLine(string.Concat(
                now,
                "Новости обновлены. В базе ",
                newsCount,
                " новостей из ",
                servicesCount,
                " источников"
            ));
        }

        public static void SubscribeInfo(int id, string category, bool success)
        {
            Console.WriteLine(string.Concat(
                now,
                "Пользовтель ",
                id,
                success ? " подписался " : " пытался подписаться ",
                "на тему рассылки: ",
                category
            ));
        }

        public static void MailingInfo(int id, string category, bool success)
        {
            Console.WriteLine(string.Concat(
                now,
                "Пользовтелю ",
                id,
                success ? " отправлена статья " : " не удалось отправить статью ",
                "по теме ",
                category
            ));
        }
    }
}
