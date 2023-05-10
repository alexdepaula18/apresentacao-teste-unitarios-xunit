using ApresentacaoTestesUnitarios.Domain.NSustitute;
using NSubstitute;

namespace ApresentacaoTestesUnitarios.Domain.Test.NSubstitute
{
    public class ContaBancariaUnitTestNSubstitute
    {
        [Fact]
        public void SacarDinheiro_SaldoSuficiente_SaqueRealizado()
        {
            // Arrange
            var repository = Substitute.For<IContaBancariaRepository>();
            repository.ConsultarSaldo(Arg.Any<int>()).Returns(1000);

            var contaBancaria = new ContaBancaria(repository);
            int numeroConta = 123;
            decimal valorSaque = 500;

            // Act
            decimal saldoRestante = contaBancaria.SacarDinheiro(numeroConta, valorSaque);

            // Assert
            Assert.Equal(500, saldoRestante);
            repository.Received(1).ConsultarSaldo(numeroConta);
        }

        [Fact]
        public void SacarDinheiro_SaldoInsuficiente_ExcecaoLancada()
        {
            // Arrange
            var repository = Substitute.For<IContaBancariaRepository>();
            repository.ConsultarSaldo(Arg.Any<int>()).Returns(0);

            var contaBancaria = new ContaBancaria(repository);
            int numeroConta = 123;
            decimal valorSaque = 500;

            // Act and Assert
            var excecao = Assert.Throws<Exception>(() 
                => contaBancaria.SacarDinheiro(numeroConta, valorSaque));
            Assert.Equal("Saldo insuficiente para saque.", excecao.Message);
            repository.Received(1).ConsultarSaldo(numeroConta);
        }

        [Fact]
        public void SacarDinheiro_ValorExcedeLimite_ExcecaoLancada()
        {
            // Arrange
            var repository = Substitute.For<IContaBancariaRepository>();
            repository.ConsultarSaldo(Arg.Any<int>()).Returns(1000);

            var contaBancaria = new ContaBancaria(repository);
            int numeroConta = 123;
            decimal valorSaque = 800;

            // Act and Assert
            var excecao = Assert.Throws<Exception>(() => contaBancaria.SacarDinheiro(numeroConta, valorSaque));
            Assert.Equal("Valor de saque excede o limite permitido.", excecao.Message);
            repository.Received(1).ConsultarSaldo(numeroConta);
        }
    }
}
