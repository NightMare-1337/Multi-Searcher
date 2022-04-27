using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace multi_searcher
{
    public partial class Form1 : Form
    {
        Form2 form2 = new Form2();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string URL = "";

            richTextBox1.Clear();

            for (int i = 1; i <= (Setting.SiteAmounts / 10); i++)
            {

                if (Setting.Sitespecify)
                {
                    URL = "https://www.google.com/search?q=site:" + Setting.Sitename + " \"" + textBox1.Text + "\"" + "&start=" + (Setting.SiteAmounts - 10).ToString();
                }
                else
                {
                    URL = "https://www.google.com/search?q=" + textBox1.Text + "&start=0" + (i * 10 - 10).ToString();
                }

                var req = (HttpWebRequest)WebRequest.Create(URL);

                string html = "";

                using (var res = (HttpWebResponse)req.GetResponse())
                using (var resSt = res.GetResponseStream())
                using (var sr = new StreamReader(resSt, Encoding.UTF8))
                {
                    html = sr.ReadToEnd();
                }

                MatchCollection matche = Regex.Matches(html, "q=https://(.*?)&amp;sa");

                foreach (Match m in matche)
                {
                    var WriteURL = Regex.Match(m.Value.ToString(), "https://(.*?)&").Value.ToString();

                    if (!WriteURL.Contains("https://accounts.google.com") && !WriteURL.Contains("https://support.google.com") && !WriteURL.Contains("https://www.google.com/preferences"))
                    {
                        richTextBox1.Text += WriteURL.Trim('&');
                        richTextBox1.Text += "\n";
                    }
                }
            }
        }

        private Point mousePoint;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Url list";
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.FileName = @"urllist.txt";
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                File.AppendAllText(saveFileDialog.FileName, richTextBox1.Text + Environment.NewLine);
            }
        }
    }
}