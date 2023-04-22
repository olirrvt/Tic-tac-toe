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
                    Console.ForegroundColor = ConsoleColor.DarkRed;
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

            // Verificando se os nomes não são iguais

            if (player2 == player1)
            {
                bool nomeValido = false;

                do
                {   
                  Console.WriteLine("O nome não pode ser igual ao do Player 1, digite outro!");
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

        static void PvcFacil()
        {
            string[,] tabela = new string[3, 3];
            string opcao;
            string jogadorAtual;
            string player;
            string corUsuario;
            string computador = "Wall-E";
            bool jogarNovamente = false;
            bool jogadaValida;
            int linha, coluna;

            Console.Clear();

            // Indetificando o Jogador

            Console.WriteLine("Qual é o nome do Player?");
            player = Console.ReadLine();

            Console.Clear();

            // Cor do Jogador

            Console.WriteLine("Escolha a sua cor:");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Azul");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Vermelho");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Verde");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Roxo");
            Console.ResetColor();

            corUsuario = Console.ReadLine().ToLower();

            Console.Clear();

            jogadorAtual = player;

            do
            {
                // Limpa a tabela
                for (int i = 0; i < tabela.GetLength(0); i++)
                {
                    for(int j = 0; j < tabela.GetLength(1); j++)
                    {
                        tabela[i, j] = " ";
                    }
                }

                while (true)
                {
                    Console.Clear();
                    ImprimeTabela(tabela);

                    // Mostra de quem é a vez de jogar e verifica se não é o robô

                    Console.Write("Jogador atual: ");

                    if(jogadorAtual == player)
                    {
                        mudaCor(corUsuario);
                    }
                    else
                    {
                        mudaCor("vermelho");
                    }

                    Console.WriteLine(jogadorAtual);
                    Console.ResetColor();

                    // Verificando se é a vez do Player

                    if(jogadorAtual == player)
                    {
                        // Pedindo as coordenadas e verificando se são válidas

                        do 
                        {
                            jogadaValida = true;

                            Console.Write("Digite a linha: ");
                            linha = int.Parse(Console.ReadLine()) - 1;
                            Console.Write("Digite a Coluna: ");
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
                         tabela[linha, coluna] = "X";
                    }
                    else
                    {
                        // Jogada do Computador

                        Random random = new Random();

                        do
                        {
                            jogadaValida = true;

                            int randomLinha = random.Next(0,3);
                            int randomColuna = random.Next(0,3);

                            linha = randomLinha;
                            coluna = randomColuna;

                            if(linha < 0 || linha >= tabela.GetLength(0) || coluna < 0 || coluna >= tabela.GetLength(1))
                            {
                                jogadaValida = false;
                            }
                            else if (tabela[linha, coluna] != " ")
                            {
                                jogadaValida = false;
                            }

                        } while (!jogadaValida);

                        // Realiza jogada do robô
                        tabela[linha, coluna] = "O";
                    }

                    // Verifica se teve um vencedor

                    if(VerificaVencedor(tabela, jogadorAtual, player))
                    {
                        Console.Clear();
                        ImprimeTabela(tabela);

                        if(jogadorAtual == player)
                        {
                            mudaCor(corUsuario);
                        }
                        else
                        {
                            mudaCor("vermelho");
                        }

                        Console.WriteLine($"O jogador {jogadorAtual} venceu!");
                        Console.ResetColor();
                        break;
                    }

                    // Verificação de empate

                    bool empate = true;

                    for (int i = 0; i < tabela.GetLength(0); i++)
                    {
                        for(int j = 0; j < tabela.GetLength(1); j++)
                        {
                            if (tabela[i,j] == " ")
                            {
                                empate = false;
                                break;
                            }
                        }

                        if(!empate)
                        {
                            break;
                        }
                    }

                    if(empate)
                    {
                        Console.Clear();
                        ImprimeTabela(tabela);
                        Console.WriteLine("O jogo terminou em empate");
                        break;
                    }

                    // Troca de vez

                    jogadorAtual = jogadorAtual == player ? computador : player;

                }

                // Perguntar se o usuário deseja jogar novamente
            
                Console.WriteLine("Deseja jogar novamente?");
                opcao = Console.ReadLine().ToUpper();

                jogarNovamente = opcao == "N" ? false : true;

            } while (jogarNovamente);

        }

        static void JogadaDaIA(string[,] tabela, string simboloComputador, string simboloJogador)
        {
            bool jogadaFeita = false;
            int[] jogada;

            // Verificar se o jogador pode vencer na próxima jogada e impedir

            jogada = VerificarJogadaVencedora(tabela, simboloJogador);
            if (jogada != null)
            {
                tabela[jogada[0], jogada[1]] = simboloComputador;
                jogadaFeita = true;
                return;
            }

            // Verificar se o computador pode vencer na próxima jogada e jogar para vencer

            jogada = VerificarJogadaVencedora(tabela, simboloComputador);
            if (jogada != null)
            {
                tabela[jogada[0], jogada[1]] = simboloComputador;
                jogadaFeita = true;
                return;
            }

            // Verificar se o jogador pode vencer em duas jogadas e bloquear

            jogada = VerificarJogadaBloqueio(tabela, simboloComputador, simboloJogador);
            if (jogada != null)
            {
                tabela[jogada[0], jogada[1]] = simboloComputador;
                jogadaFeita = true;
                return;
            }

            // Verificar a jogada mais vantajosa para o computador

            jogada = VerificarJogadaVantajosa(tabela, simboloComputador, simboloJogador);
            if (jogada != null)
            {
                tabela[jogada[0], jogada[1]] = simboloComputador;
                jogadaFeita = true;
                return;
            }

            // Jogar em qualquer posição livre, caso não haja jogada vantajosa

            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                for (int j = 0; j < tabela.GetLength(1); j++)
                {
                    if (tabela[i, j] == " ")
                    {
                        tabela[i, j] = simboloComputador;
                        jogadaFeita = true;
                        return;
                    }
                }
            }
        }

        static int[] VerificarJogadaVencedora(string[,] tabela, string simbolo)
        {
            // Verificar linhas

            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                if (tabela[i, 0] == simbolo && tabela[i, 1] == simbolo && tabela[i, 2] == " ")
                {
                    return new int[] { i, 2 };
                }
                else if (tabela[i, 0] == simbolo && tabela[i, 2] == simbolo && tabela[i, 1] == " ")
                {
                    return new int[] { i, 1 };
                }
                else if (tabela[i, 1] == simbolo && tabela[i, 2] == simbolo && tabela[i, 0] == " ")
                {
                    return new int[] { i, 0 };
                }
            }

            // Verificar colunas

            for (int j = 0; j < tabela.GetLength(1); j++)
            {
                if (tabela[0, j] == simbolo && tabela[1, j] == simbolo && tabela[2, j] == " ")
                {
                    return new int[] { 2, j };
                }
                else if (tabela[0, j] == simbolo && tabela[2, j] == simbolo && tabela[1, j] == " ")
                {
                    return new int[] { 1, j };
                }
                else if (tabela[1, j] == simbolo && tabela[2, j] == simbolo && tabela[0, j] == " ")
                {
                    return new int[] { 0, j };
                }
            }

            // Verificar colunas

            for (int j = 0; j < tabela.GetLength(1); j++)
            {
                if (tabela[0, j] == simbolo && tabela[1, j] == simbolo && tabela[2, j] == " ")
                {
                    return new int[] { 2, j };
                }
                else if (tabela[0, j] == simbolo && tabela[2, j] == simbolo && tabela[1, j] == " ")
                {
                    return new int[] { 1, j };
                }
                else if (tabela[1, j] == simbolo && tabela[2, j] == simbolo && tabela[0, j] == " ")
                {
                    return new int[] { 0, j };
                }
            }

            // Verificar diagonais

            if (tabela[0, 0] == simbolo && tabela[1, 1] == simbolo && tabela[2, 2] == " ")
            {
                return new int[] { 2, 2 };
            }
            else if (tabela[0, 0] == simbolo && tabela[2, 2] == simbolo && tabela[1, 1] == " ")
            {
                return new int[] { 1, 1 };
            }
            else if (tabela[1, 1] == simbolo && tabela[2, 2] == simbolo && tabela[0, 0] == " ")
            {
                return new int[] { 0, 0 };
            }
            else if (tabela[0, 2] == simbolo && tabela[1, 1] == simbolo && tabela[2, 0] == " ")
            {
                return new int[] { 2, 0 };
            }
            else if (tabela[0, 2] == simbolo && tabela[2, 0] == simbolo && tabela[1, 1] == " ")
            {
                return new int[] { 1, 1 };
            }
            else if (tabela[1, 1] == simbolo && tabela[2, 0] == simbolo && tabela[0, 2] == " ")
            {
                return new int[] { 0, 2 };
            }

            return null; // Retorna null se não houver jogada vencedora
        }

        static int[] VerificarJogadaBloqueio(string[,] tabela, string simboloComputador, string simboloJogador)
        {
            // Verificar linhas

            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                if (tabela[i, 0] == simboloJogador && tabela[i, 1] == simboloJogador && tabela[i, 2] == " ")
                {
                    return new int[] { i, 2 };
                }

            }

            // Verificar colunas

            for (int j = 0; j < tabela.GetLength(1); j++)
            {
                if (tabela[0, j] == simboloJogador && tabela[1, j] == simboloJogador && tabela[2, j] == " ")
                {
                    return new int[] { 2, j };
                }
                else if (tabela[0, j] == simboloJogador && tabela[2, j] == simboloJogador && tabela[1, j] == " ")
                {
                    return new int[] { 1, j };
                }
                else if (tabela[1, j] == simboloJogador && tabela[2, j] == simboloJogador && tabela[0, j] == " ")
                {
                    return new int[] { 0, j };
                }
            }

            // Verificar diagonais

            if (tabela[0, 0] == simboloJogador && tabela[1, 1] == simboloJogador && tabela[2, 2] == " ")
            {
                return new int[] { 2, 2 };
            }
            else if (tabela[0, 0] == simboloJogador && tabela[2, 2] == simboloJogador && tabela[1, 1] == " ")
            {
                return new int[] { 1, 1 };
            }
            else if (tabela[1, 1] == simboloJogador && tabela[2, 2] == simboloJogador && tabela[0, 0] == " ")
            {
                return new int[] { 0, 0 };
            }
            else if (tabela[0, 2] == simboloJogador && tabela[1, 1] == simboloJogador && tabela[2, 0] == " ")
            {
                return new int[] { 2, 0 };
            }
            else if (tabela[0, 2] == simboloJogador && tabela[2, 0] == simboloJogador && tabela[1, 1] == " ")
            {
                return new int[] { 1, 1 };
            }
            else if (tabela[1, 1] == simboloJogador && tabela[2, 0] == simboloJogador && tabela[0, 2] == " ")
            {
                return new int[] { 0, 2 };
            }

            // Retornar nulo se não houver jogada vencedora
            return null;
        }

        static int[] VerificarJogadaVantajosa(string[,] tabela, string simboloComputador, string simboloJogador)
        {
            // Verificar linhas

            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                int countComputador = 0;
                int countJogador = 0;
                int countVazio = 0;
                int[] posicaoVazio = null;

                for (int j = 0; j < tabela.GetLength(1); j++)
                {
                    if (tabela[i, j] == simboloComputador)
                    {
                        countComputador++;
                    }
                    else if (tabela[i, j] == simboloJogador)
                    {
                        countJogador++;
                    }
                    else
                    {
                        countVazio++;
                        posicaoVazio = new int[] { i, j };
                    }
                }

                if (countComputador == 2 && countVazio == 1)
                {
                    return posicaoVazio;
                }

                if (countJogador == 2 && countVazio == 1)
                {
                    return posicaoVazio;
                }
            }

            // Verificar colunas

            for (int j = 0; j < tabela.GetLength(1); j++)
            {
                int countComputador = 0;
                int countJogador = 0;
                int countVazio = 0;
                int[] posicaoVazio = null;

                for (int i = 0; i < tabela.GetLength(0); i++)
                {
                    if (tabela[i, j] == simboloComputador)
                    {
                        countComputador++;
                    }
                    else if (tabela[i, j] == simboloJogador)
                    {
                        countJogador++;
                    }
                    else
                    {
                        countVazio++;
                        posicaoVazio = new int[] { i, j };
                    }
                }

                if (countComputador == 2 && countVazio == 1)
                {
                    return posicaoVazio;
                }

                if (countJogador == 2 && countVazio == 1)
                {
                    return posicaoVazio;
                }
            }

            // Verificar diagonal principal

            int countComputadorDP = 0;
            int countJogadorDP = 0;
            int countVazioDP = 0;
            int[] posicaoVazioDP = null;

            for (int i = 0; i < tabela.GetLength(0); i++)
            {
                if (tabela[i, i] == simboloComputador)
                {
                    countComputadorDP++;
                }
                else if (tabela[i, i] == simboloJogador)
                {
                    countJogadorDP++;
                }
                else
                {
                    countVazioDP++;
                    posicaoVazioDP = new int[] { i, i };
                }
            }

            if (countComputadorDP == 2 && countVazioDP == 1)
            {
                return posicaoVazioDP;
            }

            if (countJogadorDP == 2 && countVazioDP == 1)
            {
                return posicaoVazioDP;
            }

            // Verificar diagonal secundária

            int countComputadorDS = 0;
            int countJogadorDS = 0;
            int count = 0;
            int[] posicaoVazioDS = null;

            for (int i = 0, j = tabela.GetLength(1) - 1; i < tabela.GetLength(0); i++, j--)
            {
                if (tabela[i, j] == simboloComputador)
                {
                    countComputadorDS++;
                }
                else if (tabela[i, j] == simboloJogador)
                {
                    countJogadorDS++;
                }
                else
                {
                    count++;
                    posicaoVazioDS = new int[] { i, j };
                }
            }

            if (countComputadorDS == 2 && count == 1)
            {
                return posicaoVazioDS;
            }

            if (countJogadorDS == 2 && count == 1)
            {
                return posicaoVazioDS;
            }

            return null; // caso não tenha jogada vantajosa, retorna null
        }

        static void PvcDificil()
        {
            string[,] tabela = new string[3, 3];
            string opcao;
            string jogadorAtual;
            string player;
            string corUsuario;
            string computador = "Wall-E";
            bool jogarNovamente = false;
            bool jogadaValida;
            int linha, coluna;

            Console.Clear();

            // Indetificando o Jogador

            Console.WriteLine("Qual é o nome do Player?");
            player = Console.ReadLine();

            Console.Clear();

            // Cor do Jogador

            Console.WriteLine("Escolha a sua cor:");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Azul");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Vermelho");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Verde");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Roxo");
            Console.ResetColor();

            corUsuario = Console.ReadLine().ToLower();

            Console.Clear();

            jogadorAtual = player;

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

                while (true)
                {
                    Console.Clear();
                    ImprimeTabela(tabela);

                    // Mostra de quem é a vez de jogar e verifica se não é o robô

                    Console.Write("Jogador atual: ");

                    if (jogadorAtual == player)
                    {
                        mudaCor(corUsuario);
                    }
                    else
                    {
                        mudaCor("vermelho");
                    }

                    Console.WriteLine(jogadorAtual);
                    Console.ResetColor();

                    // Verificando se é a vez do Player

                    if (jogadorAtual == player)
                    {
                        // Pedindo as coordenadas e verificando se são válidas

                        do
                        {
                            jogadaValida = true;

                            Console.Write("Digite a linha: ");
                            linha = int.Parse(Console.ReadLine()) - 1;
                            Console.Write("Digite a Coluna: ");
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
                        tabela[linha, coluna] = "X";
                    }
                    else
                    {
                        // Jogada do Computador

                        string simboloComputador = "O";
                        string simboloJogador = "X";

                        // Função para realizar a jogada da IA
                        if(jogadorAtual != player)
                        {
                            JogadaDaIA(tabela, simboloComputador, simboloJogador);
                        }

                    }

                    // Verifica se teve um vencedor

                    if (VerificaVencedor(tabela, jogadorAtual, player))
                    {
                        Console.Clear();
                        ImprimeTabela(tabela);

                        if (jogadorAtual == player)
                        {
                            mudaCor(corUsuario);
                        }
                        else
                        {
                            mudaCor("vermelho");
                        }

                        Console.WriteLine($"O jogador {jogadorAtual} venceu!");
                        Console.ResetColor();
                        break;
                    }

                    // Verificação de empate

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
                        Console.WriteLine("O jogo terminou em empate");
                        break;
                    }

                    // Troca de vez

                    jogadorAtual = jogadorAtual == player ? computador : player;

                }

                // Perguntar se o usuário deseja jogar novamente

                Console.WriteLine("Deseja jogar novamente?");
                opcao = Console.ReadLine().ToUpper();

                jogarNovamente = opcao == "N" ? false : true;

            } while (jogarNovamente);
        }

        static void PvCmenu()
        {
            string[,] tabela = new string[3, 3];
            string jogadorAtual;
            string player;

            int opMenu;

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Escolha uma das dificuldades abaixo:");
            Console.ResetColor();

            Console.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1- Nível Fácil ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" 2- Nível Difícil");
            Console.ResetColor();

            Console.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Para continuar escolha a dificuldade para jogar contra a IA:");
            Console.ResetColor();

            Console.WriteLine(" ");

            opMenu = int.Parse(Console.ReadLine());

            switch (opMenu)
            {
                case 1:
                    PvcFacil();
                    break;

                case 2:
                    PvcDificil();
                    break;

                case 3:
                    PvP();
                    break;
            }
        }

        static void Main(string[] args)
        {
            int opMenu;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(" C#: BEM VINDO AO GAME! ");
            Console.WriteLine(" ");
          
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(" 1- Player vs Player ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" 2- Player vs Computer ");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" 3- Exit");
            Console.ResetColor();

            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
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
                    PvCmenu();
                break;

                case 3:
                    System.Environment.Exit(0);
                break;
            }
        }
    }
}
