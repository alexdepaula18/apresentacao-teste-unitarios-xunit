namespace ApresentacaoTestesUnitarios.Domain.NSustitute
{
    public interface IContaBancariaRepository
    {
        decimal ConsultarSaldo(int numeroConta);
    }

    public class ContaBancariaRepository : IContaBancariaRepository
    {
        public decimal ConsultarSaldo(int numeroConta)
        {
            throw new NotImplementedException();
        }
    }

    public class ContaBancaria
    {
        private readonly IContaBancariaRepository _repository;
        public ContaBancaria(IContaBancariaRepository repository) => _repository = repository;

        public decimal SacarDinheiro(int numeroConta, decimal valorSaque)
        {
            decimal saldo = _repository.ConsultarSaldo(numeroConta);

            if (saldo <= 0)
            {
                throw new Exception("Saldo insuficiente para saque.");
            }

            if (valorSaque >= 700)
            {
                throw new Exception("Valor de saque excede o limite permitido.");
            }

            return saldo -= valorSaque;
        }
    }
}
