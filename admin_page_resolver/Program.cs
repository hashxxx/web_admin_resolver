using System;
using System.Net;
using System.IO;


namespace SharpPanel
{
    public class Program
    {

        public static string Banner = @"      ██████╗███████╗███████╗███████╗    ███████╗██████╗     " + Environment.NewLine +
           @"     ██╔════╝╚══███╔╝╚════██║██╔════╝    ██╔════╝██╔══██╗     " + Environment.NewLine +
           @"     ██║       ███╔╝     ██╔╝███████╗    ███████╗██████╔╝     " + Environment.NewLine +
           @"     ██║      ███╔╝     ██╔╝ ╚════██║    ╚════██║██╔═══╝      " + Environment.NewLine +
           @"     ╚██████╗███████╗   ██║  ███████║    ███████║██║          " + Environment.NewLine +
           @"      ╚═════╝╚══════╝   ╚═╝  ╚══════╝    ╚══════╝╚═╝          " + Environment.NewLine;


        public static string Text = @"" + Environment.NewLine +
            @"  ┌ INFORMATION ────────────────────────────┐" + Environment.NewLine +
            @"  │ [Author] H                              │" + Environment.NewLine +
            @"  │ [Version] 1.0.0                         │" + Environment.NewLine +
            @"  │ [How to use] Set the valid url          │" + Environment.NewLine +
            @"  │ [Website] github.com/hashxxx            │" + Environment.NewLine +
            @"  └─────────────────────────────────────────┘" + Environment.NewLine;


        public static void Main(string[] args)
        {
            Console.Title = "[CZ75 SP] Web admin finder - github.com/hashxxx";
            Console.ResetColor();
            Console.Clear();


            if (!CheckInternet())
            {
                Console.WriteLine(Banner);
                Console.WriteLine(Text);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection Error! Please check your internet and try again");
                Console.ReadKey();
                return;
            }

            if (!File.Exists("List.txt"))
            {
                File.WriteAllText("List.txt", admin_page_resolver.Properties.Resources.List);
            }

            Console.WriteLine(Banner);
            Console.WriteLine(Text);

            string url = null;
            if (args.Length == 1)
            {
                url = args[0];
            }
            else
            {
                Console.WriteLine("\nEnter URL:");
                url = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nurl is empty!\n");
                Console.ReadKey();
                return;
            }

            if (!url.EndsWith("/")) url += "/";
            if (!url.ToLower().StartsWith("http")) url = "http://" + url;
            Console.WriteLine("\n");

            foreach (string line in File.ReadAllLines("List.txt"))
            {
                if (UrlIsValid(url + line))
                {
                    
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("I've finished searching, now look at the results");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static bool CheckInternet()
        {
            return UrlIsValid("https://google.com/");
        }

        private static bool UrlIsValid(string url)
        {
            bool urlExists = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 10000;
                request.Method = WebRequestMethods.Http.Head;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK) urlExists = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nURL: {url}    [FOUND]");
                Console.ResetColor();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"URL: {url}    [NOT FOUND]");
                Console.ResetColor();
            }
            return urlExists;
        }
    }
}
