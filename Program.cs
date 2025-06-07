using System;


namespace MasmorraRPG
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            int habilidadeHeroi = Rolard6() + 6;
            int sorteHeroi = Rolard6() + Rolard6() + 12;
            int energiaHeroi = Rolard6() + 6;

            Console.WriteLine("Bem-vindo à Masmorra da Perdição!");
            Console.WriteLine($"Seu herói - Habilidade: {habilidadeHeroi}, Sorte: {sorteHeroi}, Energia: {energiaHeroi}");
            Console.WriteLine("Prepare-se para enfrentar criaturas terríveis!\n");

            var criaturas = new[]
            {
                new Criatura("Lobo Cinzento", 3, 3),
                new Criatura("Lobo Branco", 3, 3),
                new Criatura("Goblin", 5, 4),
                new Criatura("Orc Vesgo", 5, 5),
                new Criatura("Orc Barbudo", 5, 5),
                new Criatura("Zumbi Manco", 6, 7),
                new Criatura("Zumbi Balofo", 6, 7),
                new Criatura("Troll", 8, 7),
                new Criatura("Ogro", 8, 9),
                new Criatura("Ogro Furioso", 10, 9),
                new Criatura("Necromante Maligno", 12, 12)
            };

            foreach (var criatura in criaturas)
            {
                if (energiaHeroi <= 0) break;

                Console.WriteLine($"\nVocê encontrou um {criatura.Nome} (Habilidade: {criatura.Habilidade}, Energia: {criatura.Energia})");
                
                while (criatura.Energia > 0 && energiaHeroi > 0)
                {
                    Console.WriteLine($"\nSeu status - Energia: {energiaHeroi}, Sorte: {sorteHeroi}");
                    Console.WriteLine($"{criatura.Nome} - Energia: {criatura.Energia}");

                    int forcaHeroi = habilidadeHeroi + Rolard6() + Rolard6();
                    int forcaCriatura = criatura.Habilidade + Rolard6() + Rolard6();

                    Console.WriteLine($"Sua força de ataque: {forcaHeroi} vs {criatura.Nome}: {forcaCriatura}");

                    if (forcaHeroi > forcaCriatura)
                    {
                        Console.WriteLine($"Você acerta o {criatura.Nome}!");
                        int dano = 2;
                        
                        if (sorteHeroi > 0)
                        {
                            Console.Write("Deseja testar sua sorte para causar mais dano? (s/n): ");
                            if (Console.ReadLine().ToLower() == "s")
                            {
                                bool sorte = TestarSorte(sorteHeroi);
                                sorteHeroi--;
                                
                                if (sorte)
                                {
                                    dano = 4;
                                    Console.WriteLine("Você teve sorte! Causa 4 de dano.");
                                }
                                else
                                {
                                    dano = 1;
                                    Console.WriteLine("Você teve azar! Causa apenas 1 de dano.");
                                }
                            }
                        }
                        
                        criatura.Energia -= dano;
                        Console.WriteLine($"Você causa {dano} de dano ao {criatura.Nome}!");
                    }
                    else if (forcaCriatura > forcaHeroi)
                    {
                        Console.WriteLine($"O {criatura.Nome} acerta você!");
                        int dano = 2;
                        
                        if (sorteHeroi > 0)
                        {
                            Console.Write("Deseja testar sua sorte para reduzir o dano? (s/n): ");
                            if (Console.ReadLine().ToLower() == "s")
                            {
                                bool sorte = TestarSorte(sorteHeroi);
                                sorteHeroi--;
                                
                                if (sorte)
                                {
                                    dano = 1;
                                    Console.WriteLine("Você teve sorte! Recebe apenas 1 de dano.");
                                }
                                else
                                {
                                    dano = 3;
                                    Console.WriteLine("Você teve azar! Recebe 3 de dano.");
                                }
                            }
                        }
                        
                        energiaHeroi -= dano;
                        Console.WriteLine($"Você sofre {dano} de dano!");
                    }
                    else
                    {
                        Console.WriteLine("Ambos erram os ataques!");
                    }
                }

                if (energiaHeroi <= 0)
                {
                    Console.WriteLine("\nVocê foi derrotado! Fim do jogo.");
                    return;
                }
                else
                {
                    Console.WriteLine($"\nVocê derrotou o {criatura.Nome}!");
                }
            }

            Console.WriteLine("\nParabéns! Você derrotou todas as criaturas da masmorra e venceu o jogo!");
        }

        static int Rolard6()
        {
            return random.Next(1, 7);
        }

        static bool TestarSorte(int sorteAtual)
        {
            int resultado = Rolard6() + Rolard6();
            Console.WriteLine($"Teste de sorte: {resultado} (precisa ser ≤ {sorteAtual})");
            return resultado <= sorteAtual;
        }
    }

    class Criatura
    {
        public string Nome { get; }
        public int Habilidade { get; }
        public int Energia { get; set; }

        public Criatura(string nome, int habilidade, int energia)
        {
            Nome = nome;
            Habilidade = habilidade;
            Energia = energia;
        }
    }
};
