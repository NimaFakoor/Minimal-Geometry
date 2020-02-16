using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    class IncidenceMatrix
    {
        public IncidenceMatrix()
        {
            MIN_POINT_PER_LINE = 2;
            MAX_LINES_ANDWEIGHT = 2;
        }
        public IncidenceMatrix(int minPointPerLine)
        {
            MIN_POINT_PER_LINE = minPointPerLine;
            MAX_LINES_ANDWEIGHT = 2;
        }
        public IncidenceMatrix(int minPointPerLine, int maxLinesAndWeight)
        {
            MIN_POINT_PER_LINE = minPointPerLine;
            MAX_LINES_ANDWEIGHT = maxLinesAndWeight;
        }

        public bool checkAxiom3()
        {
            int MIN_POINT_ZERO_ANDWEIGHT = 3;
            for (int i = 0; i < Lines.Length - MIN_POINT_ZERO_ANDWEIGHT + 1; i++)
            {
                for (int j = i + 1; j < Lines.Length - MIN_POINT_ZERO_ANDWEIGHT + 2; j++)
                {
                    for (int k = j + 1; k < Lines.Length - MIN_POINT_ZERO_ANDWEIGHT + 3; k++)
                    {
                        if (Lines[i].And(Lines[j]).AndWeight(Lines[k]) == 0)
                            return true;
                    }
                }
            }
            return false;
        }

        public bool checkAxiom1()
        {
            List<Selection> Points = new List<Selection>();
            bool[] point = new bool[Lines.Length];
            for (int i = 0; i < Lines[0].codeword.Length; i++)
            {
                for (int j = 0; j < Lines.Length; j++)
                {
                    point[j] = Lines[j].codeword[i];
                }
                Points.Add(new Selection(point));
            }
            for (int i = 0; i < Points.Count - 1; i++)
            {
                for (int j = i + 1; j < Points.Count; j++)
                {
                    if (Points[i].AndWeight(Points[j]) == 0)
                        return false;
                }
            }
            return true;
        }

        public Selection[] Lines { get; set; }

        public int MIN_POINT_PER_LINE;
        public int MAX_LINES_ANDWEIGHT;
        public Selection[] generate(int points)
        {
            Selection selection = new Selection(points, MIN_POINT_PER_LINE);
            List<Selection> lines = new List<Selection>();
            lines.Add(new Selection(selection.codeword));
        NotGoodLine: while (selection.next())
            {
                foreach (Selection line in lines)
                {
                    if (selection.AndWeight(line) >= MAX_LINES_ANDWEIGHT)
                    {
                        goto NotGoodLine;
                    }
                }
                lines.Add(new Selection(selection.codeword));
            }
            Lines = lines.ToArray();
            return lines.ToArray();
        }

    }

    class Selection
    {
        int[] index;
        public bool[] codeword { get; set; }

        public Selection(int length, int ones)
        {
            codeword = new bool[length];
            index = new int[ones];
            for (int i = 0; i < ones; i++)
            {
                codeword[i] = true;
                index[i] = i;
            }
        }
        public Selection(bool[] codeword)
        {
            this.codeword = new bool[codeword.Length];
            for (int i = 0; i < codeword.Length; i++)
            {
                this.codeword[i] = codeword[i];
            }
        }

        public bool next()
        {
            int i = index.Length - 1;
            while (i != -1 && index[i] == codeword.Length - index.Length + i)
            {
                i--;
            }
            if (i > -1)
            {
                codeword[index[i]] = false;
                index[i]++;
                codeword[index[i]] = true;
                for (int j = index[i] + 1; j < codeword.Length; j++)
                {
                    codeword[j] = false;
                }
                for (int j = i + 1; j < index.Length; j++)
                {
                    index[j] = index[j - 1] + 1;
                    codeword[index[j]] = true;
                }
                return true;
            }
            return false;
        }

        public int AndWeight(Selection arg)
        {
            int count = 0;
            for (int i = 0; i < codeword.Length; i++)
            {
                if (codeword[i] && arg.codeword[i] == true)
                {
                    count++;
                }
            }
            return count;
        }

        public Selection And(Selection selection)
        {
            Selection temp = new Selection(codeword);
            for (int i = 0; i < codeword.Length; i++)
            {
                temp.codeword[i] = selection.codeword[i] && codeword[i];
            }
            return temp;
        }
    }
}
