using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multi_searcher
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();

            Setting.Sitespecify = checkBox1.Checked;
            Setting.Sitename = textBox1.Text;
            Setting.SiteAmounts = (int)numericUpDown1.Value;
        }
    }
}
