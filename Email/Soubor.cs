using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class Soubor
    {
        public string[] Cesty(string Cesty)
        {
            List<string> Pole = new List<string>();

            if (!File.Exists(Cesty))
            { Console.WriteLine("Cesty" + Cesty + "--- NENALEZENA"); return []; }

            var fo = File.OpenText(Cesty);
            while (!fo.EndOfStream)
            {
                var Radek = fo.ReadLine();
                if (Directory.Exists(Radek))
                    Pole.Add(Radek);
            }
            fo.Close();
            fo.Dispose();
            return Pole.ToArray();
        }

        public string[] CestyEmail(string Cesty)
        {
            List<string> Pole = new List<string>();

            if (!File.Exists(Cesty))
            { Console.WriteLine("Cesty" + Cesty + "--- NENALEZENA"); return []; }

            var fo = File.OpenText(Cesty);
            while (!fo.EndOfStream)
            {
                var Radek = fo.ReadLine();
                Pole.Add(Radek);
            }
            fo.Close();
            fo.Dispose();
            return Pole.ToArray();
        }

        public string[] Cesty(string[] Cesty)
        {
            List<string> Pole = new List<string>();

            foreach (var item in Cesty)
            {
                string invar = item.ToLowerInvariant();
                //v kopírovaném adresaři hledej soubor s cestou kam kopírovat
                //Předpoklad budou se kopírovat i podsložky
                if (!File.Exists(invar))
                   return [];
                var fo = File.OpenText(invar);
                while (!fo.EndOfStream)
                {
                    var Radek = fo.ReadLine();
                    if(Radek.Trim().Length < 1 ) continue;
                    if (Directory.Exists(Radek.ToLowerInvariant()))
                        Pole.Add(Radek);
                    //else {
                        //Console.WriteLine($"Aresar {Radek} musí existovat.");
                    //}
                }
                fo.Close();
                fo.Dispose();
            }
            return Pole.ToArray();
        }


        public string[] GetRelativniCesty(string baseDirectory, string[] absolutePaths)
        {
            List<string> relativePaths = new List<string>();
            foreach (string absolutePath in absolutePaths)
            {
                string relativePath = Path.GetRelativePath(baseDirectory, absolutePath);
                relativePaths.Add(relativePath);
            }
            return relativePaths.ToArray();
        }

        /// <summary>Seznam souborů</summary>
        public System.IO.FileInfo[] SeznamSouboru(string Cesta)
        {
            System.IO.DirectoryInfo Dir = new System.IO.DirectoryInfo(Cesta);
            System.IO.FileInfo[] SeznamSou = Dir.GetFiles();  //seznam souborů
            return SeznamSou;
        }

        /// <summary>Seznam Adresařů</summary>
        public System.IO.DirectoryInfo[] SeznamAdresaru(string Cesta)
        {
            System.IO.DirectoryInfo Dir = new System.IO.DirectoryInfo(Cesta);
            System.IO.DirectoryInfo[] SeznamAdr = Dir.GetDirectories();  //seznam adresářů
            return SeznamAdr;
        }

        /// <summary> Vrátí pole všech souborů včetně těch v podložkách. </summary>
        /// <returns></returns>
        public string[] GetAllFilesInFolder(string folderPath)
        {
            List<string> filesList = new List<string>();
            TraverseDirectory(folderPath, filesList);
            return filesList.ToArray();
        }

        void TraverseDirectory(string folderPath, List<string> filesList)
        {
            // Získání všech souborů v aktuální složce
            string[] files = Directory.GetFiles(folderPath);
            filesList.AddRange(files);

            // Získání všech podložek v aktuální složce
            string[] directories = Directory.GetDirectories(folderPath);
            foreach (string directory in directories)
            {
                // Rekurzivní procházení každé podložky
                TraverseDirectory(directory, filesList);
            }
        }
    }
}
