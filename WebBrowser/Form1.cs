using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WebBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = ("Обновлено: "+DateTime.Now.ToString());

            string url = Properties.Settings.Default.url;
            int timeout = Properties.Settings.Default.timeout;

            webBrowser1.PreviewKeyDown -= webBrowser1_PreviewKeyDown;
            webBrowser1.PreviewKeyDown += webBrowser1_PreviewKeyDown;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.Url = new Uri(url);
            
            //webBrowser1.Cli

            if (Properties.Settings.Default.FormState == "normal")
            {
                WindowState = FormWindowState.Normal;
                Location = Properties.Settings.Default.FormLocation;
                Size = Properties.Settings.Default.FormSize;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                StartPosition = FormStartPosition.CenterScreen;
                Size = new Size(640, 480);
            }

            timer1.Interval = timeout;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = true;
            timer1.Start();
        }

        void timer1_Tick(object sender, EventArgs e)
        {            
            string url = Properties.Settings.Default.url;
            webBrowser1.Update();
            webBrowser1.Navigate(url);

            this.Text = ("Обновлено: " + DateTime.Now.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormLocation = Location;
            Properties.Settings.Default.FormSize = Size;
            if (WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.FormState = "max";
            }
            else
            {
                Properties.Settings.Default.FormState = "normal";
            }
            Properties.Settings.Default.Save();           
        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    e.IsInputKey = true;
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
                    break;

                case Keys.F12:
                    e.IsInputKey = true;
                    if (this.FormBorderStyle == FormBorderStyle.Sizable)
                    {
                        this.FormBorderStyle = FormBorderStyle.None;
                    }
                    else
                    {
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
