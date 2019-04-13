using System;
using System.ServiceModel;
using System.Windows.Forms;
using Client.Extensions;
using Client.Services;

namespace Client
{
    public partial class MainForm : Form
    {
        private NewsServiceClient client;
        public static ListBox ListBox;

        public MainForm()
        {
            InstanceContext newsServiceContext = new InstanceContext(new NewsServiceCallback());
            client = new NewsServiceClient(newsServiceContext);
            InitializeComponent();
            ListBox = listBox;
        }

        ~MainForm()
        {
            client.Close();
        }

        private async void OnSubscribe(object sender, EventArgs e)
        {
            string category = categoryTextBox.Text;
            string infoMessage = "Вы подписались на рассылку по теме " + category;
            if (!await client.SubscribeAsync(category))
            {
                infoMessage = "Не удалось подписаться на рассылку по теме " + category;
            }
            MessageBox.Show(infoMessage, "Info");
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = !categoryTextBox.Text.IsEmpty() && categoryTextBox.Text.Length > 2;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnSubscribe(sender, new EventArgs());
            }
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            Article article = ((Article)listBox.SelectedItem);
            webBrowser.DocumentText = article.Description;
        }
    }

    class NewsServiceCallback : INewsServiceCallback
    {
        void INewsServiceCallback.SendArticle(Article article)
        {
            MainForm.ListBox.Items.Add(new ListBoxArticle(article));
        }
    }

    public class ListBoxArticle : Article
    {
        public ListBoxArticle(Article article)
        {
            Description = article.Description;
            Title = article.Title;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
