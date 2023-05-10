namespace ApresentacaoTestesUnitarios.Domain.xUnit
{
    public class ContaBancaria
    {
        public decimal Saldo { get; set; }

        public decimal SacarDinheiro(decimal valorSaque)
        {
            if (Saldo <= 0)
            {
                throw new Exception("Saldo insuficiente para saque.");
            }

            if (valorSaque >= 700)
            {
                throw new Exception("Valor de saque excede o limite permitido.");
            }

            return Saldo -= valorSaque;
        }
    }
}
