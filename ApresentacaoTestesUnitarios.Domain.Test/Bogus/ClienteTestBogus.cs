using ApresentacaoTestesUnitarios.Domain.Bogus;
using Bogus;
using Bogus.Extensions.Brazil;
using NSubstitute;
using static Bogus.DataSets.Name;

namespace ApresentacaoTestesUnitarios.Domain.Test.Bogus
{
    public class ClienteTestBogus
    {
        [Fact]
        public void Salvar_DeveChamarMetodoSalvarDoRepositorio()
        {
            // Arrange
            var repository = Substitute.For<IClienteRepository>();
            repository.Salvar(Arg.Any<Cliente>());

            var cliente = new Cliente(repository);

            cliente.Nome = "João";
            cliente.Sobrenome = "Silva";
            cliente.Cpf = "123456789";
            cliente.Sexo = Sexo.Masculino;
            cliente.WhatsApp = "987654321";

            cliente.Endereco = new Endereco
            {
                Cep = "12345-678",
                Logradouro = "Rua Teste",
                Numero = "123",
                Bairro = "Centro",
                Uf = "SP",
                Cidade = "São Paulo"
            };

            // Act
            cliente.Salvar();

            // Assert
            repository.Received(1).Salvar(cliente);
        }

        [Fact]
        public void Salvar_DeveChamarMetodoSalvarDoRepositorio_ComBogus()
        {
            // Arrange
            var repository = Substitute.For<IClienteRepository>();
            repository.Salvar(Arg.Any<Cliente>());

            var cliente = new Cliente(repository);

            var faker = new Faker("pt_BR");

            var sexo = faker.PickRandom<Sexo>();
            
            cliente.Nome = faker.Name.FirstName((Gender)(int)sexo);
            cliente.Sobrenome = faker.Name.LastName((Gender)(int)sexo);
            cliente.Cpf = faker.Person.Cpf();
            cliente.Sexo = faker.PickRandom<Sexo>();
            cliente.WhatsApp = faker.Person.Phone;

            cliente.Endereco = new Endereco
            {
                Cep = faker.Address.ZipCode(),
                Logradouro = faker.Address.StreetName(),
                Numero = faker.Random.Int(1, 100).ToString(),
                Bairro = faker.Address.SecondaryAddress(),
                Uf = faker.Address.StateAbbr(),
                Cidade = faker.Address.City()
            };

            // Act
            cliente.Salvar();

            // Assert
            repository.Received(1).Salvar(cliente);
        }
    }
}
