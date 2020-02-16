using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.Clear();


            for (int axiom2 = 4; axiom2 < 5; axiom2++)
            {
                IncidenceMatrix incidenceMatrix = new IncidenceMatrix(axiom2);
                for (int points = 8; points < 15; points++)
                {
                    incidenceMatrix.generate(points);
                    Console.WriteLine("num points " + points + "\tnum lines " + incidenceMatrix.Lines.Length);
                    MatrixWriter.Write(incidenceMatrix.Lines);
                    if (incidenceMatrix.checkAxiom3())
                    {
                        Console.WriteLine("Axiom 3 OK.");
                        if (incidenceMatrix.checkAxiom1())
                        {
                            Console.WriteLine("Axiom 1 OK.");
                            MatrixWriter.WriteFile(incidenceMatrix.Lines);
                        }
                        else
                            Console.WriteLine("Axiom 1 Failed.");
                    }
                    else
                        Console.WriteLine("Axiom 3 Failed.");

                }
            }
            Console.ReadKey();
    
        }
    }
}
