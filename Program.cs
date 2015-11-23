using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace KillIgHk
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Looking for igfxHK processes...");
            //as seen @ http://stackoverflow.com/a/116098/120427
            var retry = 0;
            var processes = new Process[0];
            while (retry < 5)
            {
                processes = Process.GetProcessesByName("igfxHK");
                if (processes.Length == 0)
                {
                    retry++;
                    Console.WriteLine("Couldn't find any. Waiting a couple seconds and retrying (retry nr. {0}).", retry);
                    Thread.Sleep(2000);
                    continue;
                }
                break;
            }
            if (processes.Length == 0)
                Console.WriteLine("Couldn't find any. Nothing done.");

            foreach (var p in processes)
            {
                Console.WriteLine("Got a igfxHK process. Trying to kill it.");
                try
                {
                    p.Kill();
                    p.WaitForExit(1000);//just wait for a second not something that I want in the background waiting indefinitely
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("I think I got it. Go use your keyboard shortcuts!!!");
                }
                catch (Win32Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    //just loggin since I just want to try (!) killing it
                    Console.WriteLine("Error killing igfxHK.\n{0}", ex);
                }
                catch (InvalidOperationException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    //just loggin since I just want to try (!) killing it
                    Console.WriteLine("Error killing igfxHK.\n{0}", ex);
                }
            }
        }
    }
}
