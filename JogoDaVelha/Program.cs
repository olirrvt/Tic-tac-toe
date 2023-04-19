using System;

namespace JogoDaVelha
{
    internal class Program
    {
        static void ImprimeTabela(string[,] tabela)
        {
            // Imprime a numeração das colunas
            Console.Write("  ");
            for (int j = 0; j < tabela.GetLength(1); j++)
            {
                Console.Write($" {j + 1} ");
            }
            Console.WriteLine();

            // Imprime a numeração das linhas
            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < tabela.GetLength(1); j++)
                {
                    Console.Write($" {tabela[i, j]} ");
                }
                Console.WriteLine();

                if (i < tabela.GetLength(0) - 1)
                {
                    for (int j = 0; j < tabela.GetLength(1); j++)
                    {
                        Console.Write("---");
                        // Verifica se o elemento é o ultimo da coluna
                        if (j < tabela.GetLength(1) - 1)
                        {
                            Console.Write("|");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        static bool VerificaVencedor(string[,] tabela, string jogador)
        {
            // Verifica as linhas
            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                bool linhaCompleta = true;
                for (int j = 0; j < tabela.GetLength(1); j++)
                {
                    if (tabela[i, j] != jogador)
                    {
                        linhaCompleta = false;
                        break;
                    }
                }
                if (linhaCompleta)
                {
                    return true;
                }
            }

            // Verifica as colunas
            for (int j = 0; j < tabela.GetLength(1); j++)
            {
                bool colunaCompleta = true;
                for (int i = 0; i < tabela.GetLength(0); i++)
                {
                    if (tabela[i, j] != jogador)
                    {
                        colunaCompleta = false;
                        break;
                    }
                }
                if (colunaCompleta)
                {
                    return true;
                }
            }

            // Verifica a diagonal principal

            bool diagonalCompleta1 = true;
            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                if (tabela[i, i] != jogador)
                {
                    diagonalCompleta1 = false;
                    break;
                }
            }
            if (diagonalCompleta1)
            {
                return true;
            }

            // Verifica a diagonal secundária

            bool diagonalCompleta2 = true;
            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                if (tabela[i, tabela.GetLength(0) - i - 1] != jogador)
                {
                    diagonalCompleta2 = false;
                    break;
                }
            }
            if (diagonalCompleta2)
            {
                return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            string[,] tabela = new string[3, 3];
            string opcao;
            string jogadorAtual = "X";
            bool jogadaValida;
            bool jogarNovamente = true;
            int linha, coluna;

            do
            {
                // Limpa a tabela

                for (int i = 0; i < tabela.GetLength(0); i++)
                {
                    for (int j = 0; j < tabela.GetLength(1); j++)
                    {
                        tabela[i, j] = " ";
                    }
                }

                // Loop do jogo

                while (true)
                {
                    Console.Clear();
                    ImprimeTabela(tabela);

                    // Mostra o próximo jogador

                    Console.WriteLine($"Jogador atual: {jogadorAtual}");

                    // Pede as coordenadas da jogada e verifica se é válida

                    do
                    {
                        jogadaValida = true;
                        Console.Write("Digite a linha: ");
                        linha = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Digite a coluna: ");
                        coluna = int.Parse(Console.ReadLine()) - 1;

                        // Se as coordenadas não existir volta o loop
                        if (linha < 0 || linha >= tabela.GetLength(0) || coluna < 0 || coluna >= tabela.GetLength(1))
                        {
                            Console.WriteLine("Coordenadas inválidas! Tente novamente.");
                            jogadaValida = false;
                        }
                        // Se as coordenadas tiverem marcadas volta o loop
                        else if (tabela[linha, coluna] != " ")
                        {
                            Console.WriteLine("Esta posição já está ocupada! Tente novamente.");
                            jogadaValida = false;
                        }
                    } while (!jogadaValida);

                    // Realiza a jogada

                    tabela[linha, coluna] = jogadorAtual;

                    // Verifica se houve um vencedor

                    if (VerificaVencedor(tabela, jogadorAtual))
                    {
                        Console.Clear();
                        ImprimeTabela(tabela);
                        Console.WriteLine($"O jogador {jogadorAtual} venceu!");
                        break;
                    }

                    // Verifica se houve empate

                    bool empate = true;

                    for (int i = 0; i < tabela.GetLength(0); i++)
                    {
                        for (int j = 0; j < tabela.GetLength(1); j++)
                        {
                            if (tabela[i, j] == " ")
                            {
                                empate = false;
                                break;
                            }
                        }
                        if (!empate)
                        {
                            break;
                        }
                    }

                    if (empate)
                    {
                        Console.Clear();
                        ImprimeTabela(tabela);
                        Console.WriteLine("O jogo terminou em empate!");
                        break;
                    }

                    // Troca o jogador atual verificando se ele já jogou
                    jogadorAtual = jogadorAtual == "X" ? "O" : "X";
                }

                // Pergunta ao usuário se ele quer jogar novamente
                Console.WriteLine("Você deseja jogar novamente?");
                opcao = Console.ReadLine().ToUpper();

                jogarNovamente = opcao == "N" ? false : true;

            } while (jogarNovamente);
        }
    }
}