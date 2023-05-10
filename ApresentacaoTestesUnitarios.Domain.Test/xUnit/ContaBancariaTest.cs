using ApresentacaoTestesUnitarios.Domain.xUnit;

namespace ApresentacaoTestesUnitarios.Domain.Test.xUnit
{
    public class ContaBancariaUnitTestFact
    {
        [Fact]
        public void SacarDinheiro_SaldoSuficiente_SaqueRealizado()
        {
            // Arrange
            ContaBancaria conta = new ContaBancaria();
            conta.Saldo = 1000;

            // Act
            conta.SacarDinheiro(500);

            // Assert
            Assert.Equal(500, conta.Saldo);
        }

        [Fact]
        public void SacarDinheiro_SaldoInsuficiente_ExcecaoLancada()
        {
            // Arrange
            ContaBancaria conta = new ContaBancaria();
            conta.Saldo = 0;

            // Act and Assert
            var excecao = Assert.Throws<Exception>(() => conta.SacarDinheiro(500));
            Assert.Equal("Saldo insuficiente para saque.", excecao.Message);
        }

        [Fact]
        public void SacarDinheiro_ValorExcedeLimite_ExcecaoLancada()
        {
            // Arrange
            ContaBancaria conta = new ContaBancaria();
            conta.Saldo = 1000;

            // Act and Assert
            var excecao = Assert.Throws<Exception>(() => conta.SacarDinheiro(800));
            Assert.Equal("Valor de saque excede o limite permitido.", excecao.Message);
        }
    }


    public class ContaBancariaUnitTestTheory
    {
        [Theory]
        [InlineData(1000, 699.99, 300.01)]
        [InlineData(50, 49.9, 0.1)]
        [InlineData(200.5, 77.77, 122.73)]
        [InlineData(0.12, 0.11, 0.01)]
        public void SacarDinheiro_SaldoSuficiente_SaqueRealizado(decimal saldo, decimal valorSaque, decimal saldoFinal)
        {
            // Arrange
            ContaBancaria conta = new ContaBancaria();
            conta.Saldo = saldo;

            // Act
            conta.SacarDinheiro(valorSaque);

            // Assert
            Assert.Equal(saldoFinal, conta.Saldo);
        }
    }
}