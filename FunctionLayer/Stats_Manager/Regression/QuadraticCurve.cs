﻿using ExFit.Data;
using System.Data;

namespace FunctionLayer.Stats_Manager.Regression
{
    // Üstel(Quadratic) Regresyon eğrisi çizer.

    public class QuadraticCurve
    {
        private Context context;
        public QuadraticCurve(Context _context)
        {
            context = _context;
        }

        Double[] Xi, Yi, Yi_Tilda, Z;
        Double a,b,c,d;
        int n;
        private void Method_B ()
        {
            Double T = 0;
            Double Xi2 = 0;
            for (int i = 0; i < n; i++)
            {
                T += Xi[i] * Math.Log10(Yi[i]);
                Z[i] = Math.Log10(Yi[i]);
                Xi2 += Math.Pow(Xi[i], 2);
            }

            b = (n * T - Xi.Sum() * Z.Sum()) / (n * Xi2 - Math.Pow(Xi.Sum(), 2));
        }
        private void Method_A ()
        {
            a = (Z.Sum() / Z.Length) - b * (Xi.Sum() / Xi.Length);
            c = Math.Pow(10, a);
            d = Math.Pow(10, b);
        }
        public Double[] Curve(int ID,int Total)
        {
            int[] Array = context.MemberMeazurements.Where(x => x.Member_ID == ID).Select(x => x.Weight).ToArray();

            n = Array.Count();
            Yi_Tilda = new Double[Total];
            Xi = new Double[12];
            Yi = new Double[n];
            Z = new Double[n];
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
            Method_B();
            Method_A();
            for (int i = 0; i < Total; i++)
            {
                Yi_Tilda[i] = c * Math.Pow(d, i + (12 - Total));
            }
            return Yi_Tilda;
        }
    }
}
