using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace healthcheck
{
    public class Program
    {
        private const int Alive = 0;
        private const int Dead = -1;
        public static async Task<int> Main(string[] args)
        {
            if (!HasUrlArg(args)) return Dead;
            var result = await IsAlive(args[0]);
            switch (result)
            {
                case Alive:
                    Console.WriteLine("Alive");
                    break;
                case Dead:
                    Console.WriteLine("Dead");
                    break;
                default:
                    Console.WriteLine("Unknown Result: Reporting Dead");
                    result = Dead;
                    break;
            }
            return result;
        }

        private static bool HasUrlArg(string[] args)
        {
            if (args.Length.Equals(1)) return true;
            Console.WriteLine("USAGE: dotnet healthcheck {url}");
            return false;
        }

        private static async Task<int> IsAlive(string url)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(url);
                return response.StatusCode.Equals(HttpStatusCode.OK) ? Alive : Dead;
            }
            catch (Exception)
            {
                return Dead;
            }

        }
    }
}
