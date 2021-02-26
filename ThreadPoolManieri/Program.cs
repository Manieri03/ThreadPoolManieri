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
        static string s;
        static List<string> nomi = new List<string>();
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("nomi.txt");
            string file = "nomi.txt";
            if (File.Exists(file))
            {
                
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    nomi.Add(line);
                }

                //stampo tutti i nomi
                foreach (string nome in nomi)
                {
                    Console.WriteLine(nome);
                }

                Console.Write("\nInserisci qui un nome e cognome da ricercare: ");
                s = Console.ReadLine();



                Stopwatch crono = new Stopwatch();

                Console.WriteLine("Esecuzione Thread Pool in corso...");

                crono.Start();
                ThreadPoolUtilizzo("Mario Rossi", nomi);
                crono.Stop();
                
                Console.WriteLine("Tempo impiegato Thread Pool: " + crono.ElapsedTicks.ToString());
                crono.Reset();


                Console.WriteLine("Esecuzione Thread in corso...");

                crono.Start();
                ThreadUtilizzo("Mario Rossi", nomi);
                crono.Stop();

                Console.WriteLine("Tempo impiegato Thread: " + crono.ElapsedTicks.ToString()); 
                
                Console.ReadLine();
            }
        }


        private static void ThreadUtilizzo(string s, List<string> nomi)
        {
            for(int i=0; i < 10; i++)
            {
                Thread t1 = new Thread(Ricerca);
                t1.Start();
            }
            
        }


        static void ThreadPoolUtilizzo(string s, List<string> nomi)
        {
            for (int i = 0; i <= 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Ricerca)); //errore  
            }
        }

        static void Ricerca(object callback)
        {
            bool trovato = false;
            int i;
            for (i = 0; i < 100; i++)
            {
                if (s.ToLower().Trim() == nomi[i].ToLower().Trim())
                    trovato = true;
                    
            }

            if (trovato == false)
            {
                Console.WriteLine($"{s} non è stato trovato");
            }
            else
            {
                Console.WriteLine($"{s} è stato trovato ed è in posizione {i}");
            }
            
        }

    }
}
