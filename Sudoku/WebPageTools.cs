using System;
using System.Net;
using System.IO;

namespace Sudoku
{
    public class WebPageTools
    {
        public static T ParseWebPage<T>(string sURL, Func<string, T> func)
        {

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            //WebProxy myProxy = new WebProxy("myproxy", 80);
            //myProxy.BypassProxyOnLocal = true;

            //wrGETURL.Proxy = WebProxy.GetDefaultProxy();
            //wrGETURL.Proxy = myProxy;


            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            string result = "";

            StreamReader objReader = new StreamReader(objStream);

            using (objReader)
            {

                string sLine = "";

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        result += sLine+"\r\n";
                }

            }

            return func(result);
        }
    }
}
