using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuthDelivery
{
    public class User
    {
        [JsonProperty(PropertyName = "email")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }

    class Program
    {
        
        public static string Authorize(string email, string password, Uri authorizeServer)
        {
            var Token = string.Empty;
            var httpRequest = (HttpWebRequest)WebRequest.Create(authorizeServer.AbsoluteUri);
            httpRequest.Method = "POST"; 
            httpRequest.ContentType = "application/json; charset=utf-8";

            var user = new User { Username = email, Password = password };
            var jsonUser = JsonConvert.SerializeObject(user);
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream())) { streamWriter.Write(jsonUser); }

            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    Token = streamReader.ReadToEnd();
                    Console.WriteLine(Token);                  
                }                 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var TokenSplited = Token.Split('"');
            return TokenSplited[3];          
        }
       public static string GetData(Uri Apiurl, string token)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(Apiurl.AbsoluteUri);
            httpRequest.Headers["Authorization"] = "Bearer " + token;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
          
            return httpResponse.StatusCode.ToString();
        }
       

        static void Main(string[] args)
        {
            // Authorize and get access token from server
                var AccessToken =  Authorize("User@mail.com", "user", new Uri("https://localhost:44395/api/Authenticate/login"));           

            // Request Token to Resource server and Get data 
                var StatusCode = GetData(new Uri("http://localhost:44840/api/History"), AccessToken);
            
            
            Console.WriteLine(StatusCode);
            Console.ReadLine();

        }

   
    }
}

