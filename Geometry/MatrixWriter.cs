using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    class MatrixWriter
    {
        static int callstack = 0;
        // File class has some problems in VS2015 so we omitted writing to files!  
        //***************************************************************//
        // thats becuse of your .NetFreamworke version :|  so you can cheng it in app config file ... debug done ;)   n.f
        internal static void WriteFile(Selection[] lines)
        {
            string[] stringLines = new string[lines.Length];
            string line;
            for (int i = 0; i < lines.Length; i++)
            {
                line = "";
                for (int j = 0; j < lines[0].codeword.Length; j++)
                {
                    line += (lines[i].codeword[j] == true) ? '0' : '1';
                    line += ' ';
                }
                stringLines[i] = line;
            }
            string path = Directory.GetCurrentDirectory();
            File.WriteAllLines(path + callstack + ".txt", stringLines);
            callstack++;
        }

        internal static void Write(Selection[] lines)
        {
            foreach (Selection item in lines)
            {
                for (int i = 0; i < item.codeword.Length; i++)
                {
                    Console.Write((item.codeword[i] == false) ? '0' : '1');
                }
                Console.WriteLine();
            }
        }
    }
}
