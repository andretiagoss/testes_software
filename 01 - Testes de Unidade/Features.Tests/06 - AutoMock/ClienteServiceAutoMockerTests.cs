using System.Linq;
using System.Threading;
using Features.Clientes;
using MediatR;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceAutoMockerTests
    {
        readonly ClienteTestsBogusFixture _clienteTestsBogus;

        public ClienteServiceAutoMockerTests(ClienteTestsBogusFixture clienteTestsFixture)
        {
            _clienteTestsBogus = clienteTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            
            var mocker = new AutoMocker();
            //Cria uma instancia diretamente da classe concreta e não da interface.
            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            //Obtem o objeto concreto da interface IClienteRepository e verifica se o método Adicionar foi chamado ao menos uma vez.
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente),Times.Once);
            //Obtem o objeto concreto da interface IMediator e verifica se o método Publish foi chamado ao menos uma vez.
            mocker.GetMock<IMediator>().Verify(m=>m.Publish(It.IsAny<INotification>(),CancellationToken.None),Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteInvalido();
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            //Obtem o objeto concreto da interface IClienteRepository e o configura (setup) a retornar alguma coisa.
            mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clienteTestsBogus.ObterClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert 
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c=>!c.Ativo) > 0);
        }
    }
}