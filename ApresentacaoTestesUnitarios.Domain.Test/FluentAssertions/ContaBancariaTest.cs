using ApresentacaoTestesUnitarios.Domain.NSustitute;
using FluentAssertions;
using NSubstitute;

namespace ApresentacaoTestesUnitarios.Domain.Test.FluentAssertions
{
    public class ContaBancariaTest
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
            saldoRestante.Should().Be(500);
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

            // Act
            Action action = () => contaBancaria.SacarDinheiro(numeroConta, valorSaque);

            // Assert
            action.Should()
                    .Throw<Exception>()
                    .WithMessage("Saldo insuficiente para saque.");

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

            // Act
            Action action = () => contaBancaria.SacarDinheiro(numeroConta, valorSaque);

            // Assert
            action.Should()
                    .Throw<Exception>()
                    .WithMessage("Valor de saque excede o limite permitido.");

            repository.Received(1).ConsultarSaldo(numeroConta);
        }
    }
}
