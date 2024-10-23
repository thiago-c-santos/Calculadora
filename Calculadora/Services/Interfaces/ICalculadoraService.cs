using Calculadora.Models;

namespace Calculadora.Services.Interfaces
{
    public interface ICalculadoraService
    {
        public Resposta Somar(List<float> valores);

        public Resposta Subtrair(float x, float y);

        public Resposta Divisao(float x, float y);

        public Resposta RaizQuadrada(int x);

        public Resposta RaizQuadradaNaoExata(double x, double precisao = 0.00001);

        public Resposta CalculoPersonalizado(string expressao);
    }
}
