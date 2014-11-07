using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebServer
{
    class Program
    {
        private static string s_path;

        static void Main(string[] args)
        {
            s_path = Environment.CurrentDirectory;
            Console.WriteLine("Path is : {0}", s_path);
            WebServer ws = new WebServer(SendResponse, "http://localhost:8080/");
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }

        private static Response SendResponse(HttpListenerRequest request)
        {
            string physicalRequest = request.Url.AbsolutePath.Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar);
            string physicalFile = Path.Combine(s_path, physicalRequest);
            Console.WriteLine("Serving : {0}", physicalRequest);
            using (MemoryStream ms = new MemoryStream())
            using (FileStream fs = File.OpenRead(physicalFile))
            {
                byte[] buffer = new byte[4096];
                int read;
                while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return new Response()
                {
                    Content = ms.ToArray()
                };
            }
        }
    }
}
