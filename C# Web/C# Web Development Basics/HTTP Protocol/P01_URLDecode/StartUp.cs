namespace P01_URLDecode
{
    using System;
    using System.Net;

    public class StartUp
    {
        public static void Main()
        {
            while (true)
            {
                Console.Write("Enter URL: ");
                var inputUrl = Console.ReadLine();
                var decodedUrl = WebUtility.UrlDecode(inputUrl);
                Console.Write("Decoded URL: ");
                Console.WriteLine(decodedUrl);
            }
        }
    }
}
