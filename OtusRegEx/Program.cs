using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace OtusRegEx
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string html = "";
            //string urlAddress = "https://stackoverflow.com/questions/16642196/get-html-code-from-website-in-c-sharp";
            Console.Write("Enter site:");
            string urlAddress = Console.ReadLine();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                html = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }

            Console.WriteLine();
            Console.WriteLine("Show finding url adress: ");
            Console.WriteLine();
            string pattern = @"href\s*=\s*['""]?(?<a>[^\s'"">]+)[\s'"">]";
            Match m;
            try
            {
                m = Regex.Match(html, pattern);
                while (m.Success)
                {
                    Console.WriteLine($"{m.Groups["a"]} at {m.Groups["a"].Index}");
                    m = m.NextMatch();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}