using Microsoft.Extensions.Configuration;
using System.IO;

namespace NerdStore.BDD.Tests.Config
{
    //Classe onde se vai obter os dados de um arquivo de configuração (appsettings.json).
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper()
        {
            //Na propriedade "Copy to Output Directory" do arquivo appsettings.json selecione a opção "Copy Always" 
            //para copiar esse arquivo no diretório do Build.
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string VitrineUrl => _config.GetSection("VitrineUrl").Value;
        public string ProdutoUrl => $"{DomainUrl}{_config.GetSection("ProdutoUrl").Value}";
        public string CarrinhoUrl => $"{DomainUrl}{_config.GetSection("CarrinhoUrl").Value}";
        public string DomainUrl => _config.GetSection("DomainUrl").Value;
        public string RegisterUrl => $"{DomainUrl}{_config.GetSection("RegisterUrl").Value}";
        public string LoginUrl => $"{DomainUrl}{_config.GetSection("LoginUrl").Value}";
        public string WebDrivers => $"{_config.GetSection("WebDrivers").Value}";
        public string FolderPath => Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        public string FolderPicture => $"{FolderPath}{_config.GetSection("FolderPicture").Value}";
    }
}

