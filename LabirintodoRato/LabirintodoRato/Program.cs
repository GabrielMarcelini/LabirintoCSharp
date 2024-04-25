using System;
using System.Collections.Generic;

namespace LabirintoCSharp
{
    internal class Program
    {
        private const int limit = 15;

        static void Main(string[] args)
        {
            char[,] labirinto = new char[limit, limit];
            criaLabirinto(labirinto);
            mostrarLabirinto(labirinto, limit, limit);
            buscarQueijo(labirinto, 1, 1);
            Console.ReadKey();
        }


        static void mostrarLabirinto(char[,] array, int l, int c)
        {
            for (int i = 0; i < l; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < c; j++)
                {
                    Console.Write($" {array[i, j]} ");
                }
            }
            Console.WriteLine();
        }


        static void criaLabirinto(char[,] meuLab)
        {
            Random random = new Random();
            for (int i = 0; i < limit; i++)
            {
                for (int j = 0; j < limit; j++)
                {
                    meuLab[i, j] = random.Next(4) == 1 ? '|' : '.';
                }
            }


            for (int i = 0; i < limit; i++)
            {
                meuLab[0, i] = '*';
                meuLab[limit - 1, i] = '*';
                meuLab[i, 0] = '*';
                meuLab[i, limit - 1] = '*';
            }


            int x = random.Next(limit);
            int y = random.Next(limit);
            meuLab[x, y] = 'Q';
        }

        static bool buscarQueijo(char[,] meuLab, int startX, int startY)
        {
            Stack<(int, int)> pilha = new Stack<(int, int)>();

            // Adiciona a posição inicial à pilha
            pilha.Push((startX, startY));

            while (pilha.Count > 0)
            {
                // Obtém a posição atual
                var (x, y) = pilha.Peek();

                // Verifica se chegou ao queijo
                if (meuLab[x, y] == 'Q')
                {
                    Console.WriteLine("Queijo encontrado!");
                    return true; // Retorna verdadeiro para indicar que o queijo foi encontrado
                }

                // Marca a posição como visitada
                meuLab[x, y] = 'v';

                // Verifica as direções possíveis (cima, baixo, esquerda, direita) e adiciona as posições válidas à pilha
                bool movimentoFeito = false;
                if (x > 0 && meuLab[x - 1, y] != '*' && meuLab[x - 1, y] != 'v' && meuLab[x -1, y] != '|')
                {
                    pilha.Push((x - 1, y)); // Cima
                    movimentoFeito = true;
                }
                else if (x < limit - 1 && meuLab[x + 1, y] != '*' && meuLab[x + 1, y] != 'v' && meuLab[x + 1, y] != '|')
                {
                    pilha.Push((x + 1, y)); // Baixo
                    movimentoFeito = true;
                }
                else if (y > 0 && meuLab[x, y - 1] != '*' && meuLab[x, y - 1] != 'v' && meuLab[x, y - 1] != '|')
                {
                    pilha.Push((x, y - 1)); // Esquerda
                    movimentoFeito = true;
                }
                else if (y < limit - 1 && meuLab[x, y + 1] != '*' && meuLab[x, y + 1] != 'v' && meuLab[x, y + 1] != '|')
                {
                    pilha.Push((x, y + 1)); // Direita
                    movimentoFeito = true;
                }

                // Se não houver movimento possível, retrocede
                if (!movimentoFeito)
                {
                    pilha.Pop();
                }

                // Se chegou a um beco sem saída, marca a posição como tal
                if (!movimentoFeito && pilha.Count > 0)
                {
                    var (anteriorX, anteriorY) = pilha.Peek();
                    meuLab[anteriorX, anteriorY] = 'x';
                }


                System.Threading.Thread.Sleep(200);
                Console.Clear();
                mostrarLabirinto(meuLab, limit, limit);
            }
            Console.WriteLine("Não foi possível encontrar o queijo.");
            return false; // Retorna falso para indicar que o queijo não foi encontrado
        }
    }
}