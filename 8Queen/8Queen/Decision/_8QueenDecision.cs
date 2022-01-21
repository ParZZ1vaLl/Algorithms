using System.Text;

namespace _8Queen
{
    internal class _8QueenDecision: IDecision<byte[]>
    {
        public _8QueenDecision(byte[] data)
        {
            Data = data;
        }
        
        public byte[] Data { get; set; }

        public bool isValid
        {
            get
            {
                for(int i = 0; i < 8; i++)
                {
                    for (int j = i + 1; j < 8; j++)
                    {
                        //на основной диагонали i - j всегда одинаковы
                        if (i - Data[i] == j - Data[j])
                            return false;
                        //на побочной одинакова сумма i и j
                        if (i + Data[i] == j + Data[j])
                            return false;
                    }
                }
                return true;
            } 
        }

        public int heuristic
        {
            get
            {
                int count = 0;
                for (int i = 0; i < 8; i++)
                {
                    int j = Data[i];

                    Func<int, (int, int)>[] getIJ =
                    {
                        step => { return (i+step, j+step); }, 
                        step => { return (i-step, j-step); }, 
                        step => { return (i+step, j-step); }, 
                        step => { return (i-step, j+step); }
                    };

                    foreach (var g in getIJ)
                    {
                        for (int step = 1; 
                            g(step).Item1 >= 0 && g(step).Item1 < 8 && 
                            g(step).Item2 >= 0 && g(step).Item2 < 8;
                            step++)
                            if (Data[g(step).Item1] == g(step).Item2)
                            {
                                count++;
                                break;
                            }
                    }
                }
                return count;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < Data[i]; j++)
                    sb.Append("|_");
                sb.Append("|Q");
                for (int j = 0; j < 8 - Data[i]; j++)
                    sb.Append("|_");
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
