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

                Console.Write("Seu Usuário: ");
                var usuarioMe = Console.ReadLine();
                Console.Write("Sua Senha: ");
                var senhaMe = Console.ReadLine();

                if (usuarioMe != "" && senhaMe != "")
                {
                    var basicAuth = new Credentials(usuarioMe, senhaMe);
                    client.Credentials = basicAuth;

                    Console.Write("Informe um usuário para busca: ");
                    var usuarioBusca = Console.ReadLine(); //Obtém usuário

                    Console.WriteLine("Buscando dados");
                    var searchResults = client.Search.SearchRepo(new SearchRepositoriesRequest()
                    {
                        User = usuarioBusca
                    }).Result;

                    if (searchResults != null)
                        foreach (var result in searchResults.Items)
                        {
                            Console.WriteLine(
                                "------------------------------------------------------------------------------");
                            Console.WriteLine(string.Format("Id: {0}\nRepositório: {1}\n", result.Id, result.Name));
                        }
                    else
                        Console.WriteLine(string.Format("Dados de usuário não encontrado."));
                }
                else
                {
                    Console.WriteLine("Informe seu usuário e senha");
                }
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
