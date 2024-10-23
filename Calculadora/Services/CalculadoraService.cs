using Calculadora.Models;
using Calculadora.Services.Interfaces;

namespace Calculadora.Services
{
    public class CalculadoraService : ICalculadoraService
    {
        public Resposta Somar(List<float> valores)
        {
            return new Resposta()
            {
                Sucesso = true,
                Message = new List<string>()
                {
                    $"O resultado da soma dos valores é: {valores.Sum(x => x)}"
                }
            };
        }

        public Resposta Subtrair(float x, float y)
        {
            return new Resposta()
            {
                Sucesso = true,
                Message = new List<string>()
                {
                    $"O resultado da subtração de {x} por {y} é: {x - y}"
                }
            };
        }

        public Resposta Divisao(float x, float y)
        {
            if (y == 0)
            {
                return new Resposta()
                {
                    Sucesso = false,
                    Message = new()
                    {
                        "Não é possível dividir por 0"
                    }
                };
            }

            return new Resposta()
            {
                Sucesso = true,
                Message = new()
                {
                   $"O resultado da divisão de {x} por {y} é: {x / y}"
                }
            };
        }

        public Resposta Multiplicacao(float x, float y)
        {
            return new Resposta()
            {
                Sucesso = true,
                Message = new()
                {
                    $"O resultado da multiplicação de {x} por {y} é: {x * y}"
                }
            };
        }

        public Resposta RaizQuadrada(int x)
        {
            // Verifica se o número é negativo
            if (x < 0)
            {
                return new Resposta()
                {
                    Sucesso = false,
                    Message = new()
            {
                "O número não pode ser negativo."
            }
                };
            }

            int i = 0;
            while (i * i <= x)
            {
                if (i * i == x)
                {
                    return new Resposta()
                    {
                        Sucesso = true,
                        Message = new()
                {
                    $"A raiz de {x} é {i}!"
                }
                    };
                }

                i++;
            }

            // Se não for exato, calcular a raiz aproximada com método de Newton
            var raizNaoExata = RaizQuadradaNaoExata(x);

            return new Resposta()
            {
                Sucesso = raizNaoExata.Sucesso,
                Message = new List<string>()
                {
                    "Ops, parece que esse valor não possui uma raiz exata. "
                    + raizNaoExata.Message.FirstOrDefault()
                }
            };
        }

        public Resposta RaizQuadradaNaoExata(double x, double precisao = 0.00001)
        {
            if (x < 0)
            {
                return new Resposta()
                {
                    Sucesso = false,
                    Message = new()
                    {
                        "O número não pode ser negativo."
                    }
                };
            }

            double estimativa = x / 2;
            double diferenca;

            do
            {
                double novaEstimativa = (estimativa + x / estimativa) / 2;
                diferenca = Math.Abs(novaEstimativa - estimativa);
                estimativa = novaEstimativa;
            }
            while (diferenca > precisao);

            return new Resposta()
            {
                Sucesso = true,
                Message = new()
                {
                    $"A raiz mais próxima de {x} é: {estimativa}"
                }
            };
        }

        public Resposta CalculoPersonalizado(string expressao)
        {
            var valores = new Stack<double>();   // Pilha para números
            var operadores = new Stack<char>();  // Pilha para operadores
            int i = 0;

            while (i < expressao.Length)
            {
                if (char.IsWhiteSpace(expressao[i]))
                {
                    i++; // Ignorar espaços em branco
                    continue;
                }

                // Se for um número, ler o número inteiro e empilhar
                if (char.IsDigit(expressao[i]))
                {
                    double valor = 0;

                    while (i < expressao.Length && char.IsDigit(expressao[i]))
                    {
                        valor = valor * 10 + (expressao[i] - '0'); // Converte char para número
                        i++;
                    }

                    valores.Push(valor);
                }
                // Se for um parêntese de abertura
                else if (expressao[i] == '(')
                {
                    operadores.Push(expressao[i]);
                    i++;
                }
                // Se for um parêntese de fechamento, resolver tudo dentro dos parênteses
                else if (expressao[i] == ')')
                {
                    while (operadores.Peek() != '(')
                    {
                        valores.Push(AplicarOperador(operadores.Pop(), valores.Pop(), valores.Pop()));
                    }
                    operadores.Pop(); // Remove o parêntese de abertura '('
                    i++;
                }
                // Se for um operador
                else if (IsOperador(expressao[i]))
                {
                    // Resolver operações pendentes se o operador atual tiver menor ou igual precedência
                    while (operadores.Count > 0 && Precedencia(operadores.Peek()) >= Precedencia(expressao[i]))
                    {
                        valores.Push(AplicarOperador(operadores.Pop(), valores.Pop(), valores.Pop()));
                    }

                    operadores.Push(expressao[i]);
                    i++;
                }
            }

            // Aplicar os operadores restantes
            while (operadores.Count > 0)
            {
                valores.Push(AplicarOperador(operadores.Pop(), valores.Pop(), valores.Pop()));
            }

            // O valor final é o resultado
            var resultado = valores.Pop();

            return new Resposta()
            {
                Sucesso = true,
                Message = new()
                {
                    $"O resultado é: {resultado}"
                }
            };
        }

        static double AplicarOperador(char operador, double b, double a)
        {
            return operador switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
                _ => throw new ArgumentException("Operador inválido")
            };
        }

        static bool IsOperador(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        static int Precedencia(char operador)
        {
            return operador switch
            {
                '+' or '-' => 1,
                '*' or '/' => 2,
                _ => 0
            };
        }
    }
}

