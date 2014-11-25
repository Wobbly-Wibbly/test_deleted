using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.IO;


namespace Words
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Clear();
            Program.checkConfigs();
            Server.Debug(Environment.UserName, "Words is launched v2");
            Server.Debug(Environment.UserName, "OS Version: " + System.Environment.OSVersion.ToString());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string server = Config.Current.AppSettings.Settings["ServerUrl"].Value;
            string email = Config.User.AppSettings.Settings["email"].Value;

            string userAnswer = "N/A";
            bool correct = false;
            int i = 1;
            Server.Debug(Environment.UserName, "DEBUG 5");
            while (true)
            {
                Server.Debug(Environment.UserName, "Request(" + i++ + ") ...");
                correct = false;
                ServerResponse response = Server.getQuestion(email);
                if (response.Code == ResponseCodes.REQUEST_ERROR)
                {
                    MessageBox.Show("Please check your internet connection or try again later.");
                    break;
                }
                Words.Forms.Question dialog = new Forms.Question(response.Question.ToString());
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (response.Answer.ToString() == dialog.Value)
                    {
                        correct = true;
                        System.Threading.Thread.Sleep(50000);
                    }
                    else
                    {
                        MessageBox.Show("INCORRECT! correct answer is: " + response.Answer.ToString());
                    }
                }
            }
            //Application.Run(new Forms.Question(server + email));
        }

        public static void checkConfigs()
        {
            if (Config.Current.AppSettings.Settings["ServerUrl"] == null)
            {
                Config.Current.AppSettings.Settings.Add("ServerUrl", "http://192.168.0.102/words");
                Config.Current.Save();
                Server.Debug(Environment.UserName, "Server url set");
            }
            if (Config.User.AppSettings.Settings["email"] == null)
            {
                Words.Forms.Question dialog = new Forms.Question("Unknown email. Please enter your email!", Environment.UserName);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Server.Debug(Environment.UserName, "validate email... user input is $" + dialog.Value + "$");
                    Config.User.AppSettings.Settings.Add("email", dialog.Value);
                    Config.User.Save();
                    Server.Debug(Environment.UserName, "email set");
                }
                else
                {
                    Server.Debug(Environment.UserName, "validate email... user input is nothing... exit");
                    return;
                }
            }
        }
    }
}
