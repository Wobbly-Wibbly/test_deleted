using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Words
{
    enum Request
    {
        Random
    }

    enum Answer
    {
        Correct,
        Wrong,
        None
    }

    static class Server
    {
        public static bool debug = true;

        public static void Debug(string email, string data)
        {
            ServerResponse response = null;
            response = Server.makeRequest(Config.Current.AppSettings.Settings["ServerUrl"].Value + "/debug.php", "email=" + email + "&data=" + data, true);
        }
        public static ServerResponse getQuestion(string email)
        {
            return Server.makeRequest(Config.Current.AppSettings.Settings["ServerUrl"].Value + "/api.php", "email=" + email, true);
        }
        private static ServerResponse makeRequest(string URL, string parameters, bool noDebug = false)
        {
            ServerResponse response = new ServerResponse();
            WebClient client = new WebClient();
            ServicePointManager.Expect100Continue = true;
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            string json = "null";
            int retries = 3;
            while (true)
            {
                try
                {
                    json = client.UploadString(URL, parameters);
                    response = JsonConvert.DeserializeObject<ServerResponse>(json);
                    break; // success!
                }
                catch (Exception e)
                {
                    if (--retries == 0)
                    {
                        response.Code = ResponseCodes.REQUEST_ERROR;
                        response.Data = null;
                        response.Message = e.Message;

                        if (noDebug == false)
                        {
                            Server.Debug(Environment.UserName, "Can't send request. Error description: " + e.Message + "; URL: " + URL + "; params: " + parameters + "; response: " + json);
                        }
                        else
                        {
                            Log.Write("Can't send request. Error description: " + e.Message + "; URL: " + URL + "; params: " + parameters + "; response: " + json);
                        }

                        return response;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }

            return response;
        }
    }
}
