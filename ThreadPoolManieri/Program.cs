using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolManieri
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("nomi.txt");
            string file = "nomi.txt";
            if (File.Exists(file))
            {
                List<string> nomi = new List<string>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    nomi.Add(line);
                }

                //stampo tutti i nomi
                foreach (string s in nomi)
                {
                    Console.WriteLine(s);
                }

                Console.Write("\nInserisci qui un nome e cognome da ricercare: ");
                string nome = Console.ReadLine();



                Stopwatch crono = new Stopwatch();

                Console.WriteLine("Esecuzione Thread Pool in corso...");

                crono.Start();
                ThreadPoolUtilizzo("Mario Rossi", nomi);
                crono.Stop();

                Console.WriteLine("Tempo impiegato Thread Pool" + crono.ElapsedTicks.ToString());
                crono.Reset();


                Console.WriteLine("Esecuzione Thread in corso...");

                crono.Start();
                ThreadUtilizzo("Mario Rossi", nomi);
                crono.Stop();

                Console.WriteLine("Tempo impiegato Thread" + crono.ElapsedTicks.ToString());
                
                Console.ReadLine();
            }
        }


        private static void ThreadUtilizzo(string s, List<string> nomi)
        {
            for(int i=0; i < 10; i++)
            {
                Thread t1 = new Thread(() => Ricerca(s, nomi));
                t1.Start();
            }
            
        }


        private static void ThreadPoolUtilizzo(string s, List<string> nomi)
        {
            for (int i = 0; i <= 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Ricerca(s, nomi))); //errore 
            }
        }

        static string Ricerca(string s, List <string> nomi)
        {
            int i;
            for (i = 0; i < 100; i++)
            {
                if (s == nomi[i])
                    return $"{s} è stato trovato ed è in posizione {i}";
            }
            return $"{s} non è stato trovato";
        }

    }
}
