using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace Github
{
    class Program
    {
        static void Main(string[] args)
        {
            Guit();
        }

        public static void Guit()
        {
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("octokit"));
                //Necessário informar usuário e senha do github
                var basicAuth = new Credentials("jeanbasoni", "a1s2d3f4");
                client.Credentials = basicAuth;

                Console.Write("Informe um usuário: ");
                var usuario = Console.ReadLine();//Obtém usuário

                Console.WriteLine("Buscando dados");
                var searchResults = client.Search.SearchRepo(new SearchRepositoriesRequest()
                {
                    User = usuario
                }).Result;

                if (searchResults != null)
                    foreach (var result in searchResults.Items)
                    {
                        Console.WriteLine("------------------------------------------------------------------------------");
                        Console.WriteLine(string.Format("Id: {0}\nRepositório: {1}\n", result.Id, result.Name));
                    }
                else
                    Console.WriteLine(string.Format("Dados de usuário não encontrado."));

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ayyyy... problemas");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
