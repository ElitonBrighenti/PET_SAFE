using Microsoft.EntityFrameworkCore;

namespace PetSafe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inicializando o banco de dados e inserindo dados
            using (var context = new AppDbContext())
            {
                // Garantir que o banco de dados é criado
                context.Database.EnsureCreated();

                // Adicionar um usuário
                var usuario = new Usuario
                {
                    Nome = "Maria Santos",
                    Email = "maria@example.com",
                    Senha = "password123",
                    Telefone = "999888777",
                    Endereco = "Rua XYZ, 123",
                    DataCriacao = DateTime.UtcNow
                };

                // Adicionar um pet para o usuário
                var pet = new Pet
                {
                    Nome = "LEci",
                    Raca = "Aleatorio",
                    Idade = 4,
                    Peso = 35.7M,
                    DataCadastro = DateTime.UtcNow,
                    Usuario = usuario // Associando o pet ao usuário
                };

                try
                {
                    // Salvando no banco de dados
                    context.Usuarios.Add(usuario);
                    context.Pets.Add(pet);
                    context.SaveChanges();
                    Console.WriteLine("SALVOU NO BANCO");
                }catch (Exception ex) 
                { 
                    Console.WriteLine(ex.ToString());
                }
                // Consultar dados inseridos
                var usuarios = context.Usuarios.Include(u => u.Pets).ToList();
                foreach (var user in usuarios)
                {
                    Console.WriteLine($"Usuário: {user.Nome}, Pet: {user.Pets.FirstOrDefault()?.Nome}");
                }
            }
        }
    }
}
