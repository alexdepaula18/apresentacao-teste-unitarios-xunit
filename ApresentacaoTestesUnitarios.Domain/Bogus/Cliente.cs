namespace ApresentacaoTestesUnitarios.Domain.Bogus
{
    public interface IClienteRepository
    {
        void Salvar(Cliente cliente);
    }

    public class ClienteRepository : IClienteRepository
    {
        public void Salvar(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }

    public enum Sexo
    {
        Masculino,
        Feminino
    }

    public class Cliente
    {
        private readonly IClienteRepository _repository;

        public Cliente(IClienteRepository repository) => _repository = repository;

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public Sexo Sexo { get; set; }

        public string WhatsApp { get; set; }

        public Endereco Endereco { get; set; }

        public void Salvar()
        {
            _repository.Salvar(this);
        }
    }

    public class Endereco
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
    }
}
