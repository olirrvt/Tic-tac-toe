using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace JogoDaVelha
{
    internal class Program
    {
        static void ImprimeTabela(string[,] tabela)
        {
            Console.WriteLine("    1   2   3  ");
            Console.WriteLine("   ");
            Console.WriteLine($" 1  {tabela[0, 0]} | {tabela[0, 1]} | {tabela[0, 2]}");
            Console.WriteLine("   -----------");
            Console.WriteLine($" 2  {tabela[1, 0]} | {tabela[1, 1]} | {tabela[1, 2]}");
            Console.WriteLine("   -----------");
            Console.WriteLine($" 3  {tabela[2, 0]} | {tabela[2, 1]} | {tabela[2, 2]}");
            Console.WriteLine("   ");
        }

        static void mudaCor(string cor)
        {
            switch(cor)
            {
                // Azul
                // Vermelho
                // Verde
                // Roxo

                case "azul":
                    Console.ForegroundColor = ConsoleColor.Blue;
                break;
                
                case "vermelho":
                    Console.ForegroundColor = ConsoleColor.Red;
                break; 
                
                case "verde":
                    Console.ForegroundColor = ConsoleColor.Green;
                break;
                
                case "roxo":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                break;

            }
        }

        static bool VerificaVencedor(string[,] tabela, string jogadorAtual, string player1)
        {
            string verificaJogador;

            verificaJogador = jogadorAtual == player1 ? "X" : "O";

            // Verifica as linhas
            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                bool linhaCompleta = true;
                for (int j = 0; j < tabela.GetLength(1); j++)
                {
                    if (tabela[i, j] != verificaJogador)
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
                    if (tabela[i, j] != verificaJogador)
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

            // Verifica as diagonais
            bool diagonalCompleta1 = true;
            bool diagonalCompleta2 = true;
            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                if (tabela[i, i] != verificaJogador)
                {
                    diagonalCompleta1 = false;
                }
                if (tabela[i, tabela.GetLength(0) - i - 1] != verificaJogador)
                {
                    diagonalCompleta2 = false;
                }
            }
            if (diagonalCompleta1 || diagonalCompleta2)
            {
                return true;
            }

            return false;
        }

        static void PvP()
        {
            string[,] tabela = new string[3, 3];
            string opcao;
            string jogadorAtual;
            string player1;
            string player2;
            string corUsuario1;
            string corUsuario2;
            bool jogadaValida;
            bool jogarNovamente = true;
            int linha, coluna;

            Console.Clear();

            // Identificando o Player 1

            Console.WriteLine("Qual é o nome do Player 1?");
            player1 = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Escolha sua cor:");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Azul");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Vermelho");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Verde");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Roxo");
            Console.ResetColor();

            corUsuario1 = Console.ReadLine().ToLower();

            Console.Clear();

            // Identificando o Player 2

            Console.WriteLine("Qual é o nome do Player 2?");
            player2 = Console.ReadLine();

            if (player2 == player1)
            {
                bool nomeValido = false;

                do
                {   
                  Console.WriteLine("O nome não pode ser igual ao do Player 1, tente outro!");
                  player2 = Console.ReadLine();

                  if(player2 != player1)
                  {
                     nomeValido = true;
                  }

                } while (!nomeValido);
            }

            Console.Clear();

            Console.WriteLine("Escolha sua cor:");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Azul");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Vermelho");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Verde");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Roxo");
            Console.ResetColor();

            corUsuario2 = Console.ReadLine().ToLower();

            Console.Clear();

            // Identificando o jogador

            jogadorAtual = player1;

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

                    Console.Write($"Jogador atual: ");

                    if ( jogadorAtual == player1)
                    {
                        mudaCor(corUsuario1);
                    }
                    else
                    {
                        mudaCor(corUsuario2);
                    }

                    Console.WriteLine(jogadorAtual);
                    Console.ResetColor();

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

                    tabela[linha, coluna] = jogadorAtual == player1 ? "X" : "O";

                    // Verifica se houve um vencedor

                    if (VerificaVencedor(tabela, jogadorAtual, player1))
                    {
                        Console.Clear();
                        ImprimeTabela(tabela);

                        // Verifica a cor do usuário

                        if(jogadorAtual == player1)
                        {
                            mudaCor(corUsuario1);
                        }
                        else
                        {
                            mudaCor(corUsuario2);
                        }

                        Console.WriteLine($"O jogador {jogadorAtual} venceu!");
                        Console.ResetColor();
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

                    jogadorAtual = jogadorAtual == player1 ? player2 : player1;
                }

                // Pergunta ao usuário se ele quer jogar novamente
                Console.WriteLine("Você deseja jogar novamente?");
                opcao = Console.ReadLine().ToUpper();

                jogarNovamente = opcao == "N" ? false : true;

            } while (jogarNovamente);
        }

        static void PvC()
        {

        }

        static void Main(string[] args)
        {
            int opMenu;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" C#: BEM VINDO AO GAME! ");
            Console.WriteLine(" ");
          
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(" 1- Player vs Player ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" 2- Player vs Computer ");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" 3- Exit");
            Console.ResetColor();

            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" Escolha uma das opções acima para começar! ");
            Console.ResetColor();
            Console.WriteLine(" ");

            opMenu = int.Parse(Console.ReadLine());

            switch(opMenu) 
            {
                case 1:
                    PvP();
                break;

                case 2:
                    PvC();
                break;

                case 3:
                    System.Environment.Exit(0);
                break;
            }
        }
    }
}
