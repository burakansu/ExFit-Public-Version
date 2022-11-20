using DatabaseLayer;
using ExFit.Data;
using System.Data;

namespace FunctionLayer.Stats_Manager.Regression
{
    // Doğrusal(Lineer) Regresyon eğrisi çizer.

    public class LinearCurve
    {
        private Context context;
        public LinearCurve(Context _context)
        {
            context = _context;
        }

        Double a, b, Xi_Avg, Yi_Avg, SSA = 0;
        Double[] Xi, Yi, Yi_Tilda;
        int n;
        void Avg_Y()
        {
            Double Counter = 0;
            for (int i = 0; i < n; i++)
            {
                Counter += Yi[i];
            }
            Yi_Avg = (Counter / n);
        }
        void Avg_X()
        {
            Double Counter = 0;
            for (int i = 0; i < n; i++)
            {
                Counter += Xi[i];
            }
            Xi_Avg = (Counter / n);
        }
        void Method_a()
        {
            a = Yi_Avg - (b * Xi_Avg);
        }
        void Method_b()
        {
            Avg_X();
            Avg_Y();
            Double Nominator = 0;
            Double Denominator = 0;
            for (int i = 0; i < n; i++)
            {
                Nominator += (Xi[i] - Xi_Avg) * (Yi[i] - Yi_Avg);
                double sum = (Xi[i] - Xi_Avg);
                Denominator += (Double)Math.Pow(sum, 2);
            }
            b = Nominator / Denominator;
        }
        void Method_SSA()
        {
            for (int i = 0; i < n; i++)
            {
                SSA += (Double)Math.Pow((Yi[i] - a - (b * Xi[i])), 2);
            }
        }
        public Double[] Curve(int ID, int Total)
        {
            if (Total >= 9)
            {
                Yi_Tilda = new Double[Total];
                return Yi_Tilda;
            }
            int[] Array = context.MemberMeazurements.Where(x => x.Member_ID == ID).Select(x => x.Weight).ToArray();

            n = Array.Count();
            Yi_Tilda = new Double[Total];
            Xi = new Double[12];
            Yi = new Double[n];
            if (n > 0)
            {
                for (int j = 0; j < 12; j++)
                {
                    Xi[j] = j + 1;
                }
                for (int i = 0; i < n; i++)
                {
                    Yi[i] = Convert.ToDouble(Array[i]);
                }
            }
            Method_b();
            Method_a();
            Method_SSA();
            for (int i = 0; i < Total; i++)
            {
                Yi_Tilda[i] = Yi_Avg - (b * Xi_Avg) + (b * Xi[i]);
            }
            return Yi_Tilda;
        }
    }
}
