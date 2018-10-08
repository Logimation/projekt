using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Support;

using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;


//using System.Collections.Specialized;

//using HtmlAgilityPack;


namespace web_consol
{
    class Program
    {
        static void Main(string[] args)
        {

            
            var t = Task.Run(() => postRequest());
            t.Wait();
            

            //find form action login id=loginform
            string temp = Console.ReadLine();

        }

            
        static async void postRequest()
        {
            WebClient wc = new WebClient();
            Stream myStream = wc.OpenRead("https://gm02.secomea.com/app");

            string html = "";
            using (StreamReader sr = new StreamReader(myStream))
            {
                while (!html.Contains("</html>"))
                {
                    html += sr.ReadLine();
                }
            }
            var startPos = html.IndexOf("<form action=");
            var stopPos = html.IndexOf(" name=\"loginform\"");
            var result = html.Substring(startPos + 14, (stopPos - startPos) - 15);
            //Postrequest("https://ptsv2.com/t/uwi7j-1538125150/post");
            
            using (var client = new HttpClient())
            {
                /*
                var values = new Dictionary<string, string>
                {
                { "test", "test" },
                { "user", "hello" },
                { "pass", "world" }
                };
                */

                string tests = "user=test&pass=dfgdsgfr";
                StringContent content = new StringContent(tests);

                /*
                IEnumerable<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("login_pass","hello"),
                    new KeyValuePair<string, string>("login_user","world")
                };*/

                /* forsøg
                Login jonas = new Login("test", "test2");
                var values = jonas.ToString();
                
                //StringEntity se = new StringEntity(values);
                

                //var content = new StringContent (values);

                
                */
                //var content = "user=test&pass=";
                //var content = new FormUrlEncodedContent(values);
                //var response = await client.PostAsync("https://gm02.secomea.com/app/" + result, content);
                var response = await client.PostAsync("https://ptsv2.com/t/uwi7j-1538125150/post", content);
                //var response = await client.PostAsync("http://httpbin.org/post", content);
            
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content.Headers);
                Console.WriteLine(response);
            }
        }

        static async void Postrequest(string url)
        {
            /*
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("login_user","hello"),
                    new KeyValuePair<string, string>("login_pass","world")
                };*/



            var queries = new Dictionary<string, string>
                {
                { "user", "hello" },
                { "pass", "world" }
                };
            HttpContent q = new FormUrlEncodedContent(queries);


            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {

                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders headers = content.Headers;
                        Console.WriteLine(headers);
                        Console.WriteLine(mycontent);
                    }
                }

            }
        }

        static async void Getrequest(string url)
        {

            using (HttpClient client = new HttpClient())
            {

                using (HttpResponseMessage response = await client.GetAsync(url))
                {

                    HttpContent content = response.Content;

                    string mycontent = await content.ReadAsStringAsync();
                    Console.WriteLine(mycontent);

                }

            }
        }

    }
    public class Login {
        public string user { get; set; }
        public string pass { get; set; }
        public Login(string user1, string pass1) {
            user = user1;
            pass = pass1;
        }
    }

            /*
            Console.WriteLine(html.Remove(0, 4));

            Console.ReadLine();
            
            IWebDriver webdriver = new ChromeDriver();
            webdriver.Url = "https://gm02.secomea.com/app";

            IWebElement searchText = webdriver.FindElement(By.Name("user"));
            searchText.SendKeys("test");
            searchText = webdriver.FindElement(By.Name("pass"));
            searchText.SendKeys("pass");
            searchText.Submit();

            //webdriver.Quit();
            */
    
}

/*
 * //https://stackoverflow.com/questions/5401501/how-to-post-data-to-specific-url-using-webclient-in-c-sharp //
 * string URL = "FILE_URL_PATH";
WebClient webClient = new WebClient();

System.Collections.Specialized.NameValueCollection formData = new System.Collections.Specialized.NameValueCollection();
formData["Username"] = "myusername";
formData["Password"] = "mypassword";
byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
string Result = Encoding.UTF8.GetString(responseBytes);
*/
