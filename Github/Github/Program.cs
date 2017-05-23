using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
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
                    if (ValidarCredenciais(usuarioMe, senhaMe, client))
                    {
                        Console.Write("Informe um usuário para busca: ");
                        var usuarioBusca = Console.ReadLine(); //Obtém usuário

                        Console.WriteLine("Buscando dados");

                        var resultadoBusca = BuscaUsuario(usuarioBusca, client);

                        if (resultadoBusca != null)
                            foreach (var result in resultadoBusca.Items)
                            {
                                Console.WriteLine(
                                    "------------------------------------------------------------------------------");
                                Console.WriteLine("Id: {0}\nRepositório: {1}\n", result.Id, result.Name);
                            }
                        else
                            Console.WriteLine(string.Format("Dados de usuário não encontrado."));
                    }
                }
                else
                {
                    Console.WriteLine("Informe seu usuário e senha");
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.ReadLine();
            }
        }

        private static bool ValidarCredenciais(string usuario, string senha, GitHubClient client)
        {
            var basicAuth = new Credentials(usuario, senha);
            client.Credentials = basicAuth;

            return client.User.Get(usuario).Result != null ? true : false;
        }

        private static SearchRepositoryResult BuscaUsuario(string usuarioBusca, GitHubClient client)
        {
            var searchResults = client.Search.SearchRepo(new SearchRepositoriesRequest()
            {
                User = usuarioBusca
            }).Result;

            return searchResults;
        }
    }
}
