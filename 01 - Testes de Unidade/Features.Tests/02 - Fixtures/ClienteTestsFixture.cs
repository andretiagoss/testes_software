using System;
using Features.Clientes;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteCollection))]

    //A coleção é criado para compartilhar código e ficar disponivel para todos os testes que estiverem rodando dentro da coleção.
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    {}

    //Criação da classe fixture que será compartilhada para outras classes que pertencerem a coleção ClienteCollection
    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Eduardo",
                "Pires",
                DateTime.Now.AddYears(-30),
                "edu@edu.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public Cliente GerarClienteInValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "edu2edu.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public void Dispose()
        {
        }
    }
}