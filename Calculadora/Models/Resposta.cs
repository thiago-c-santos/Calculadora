namespace Calculadora.Models
{
    public class Resposta
    {
        public bool Sucesso { get; set; }

        public required List<string> Message { get; set; }
    }
}
