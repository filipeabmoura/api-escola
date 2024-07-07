namespace api_escola.Model
{
    public class Escola
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public int NumeroDeTurmas { get; set; }

        public Escola(string? nome, int numeroDeTurmas)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            NumeroDeTurmas = numeroDeTurmas;
        }
    }
}
