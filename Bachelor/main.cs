using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Research.Science.Data;
using Microsoft.Research.Science.Data.Imperative;

namespace TMP
{
    class main
    {
        static void Main(string[] args)
        {
            //double X = 2.0;
            int Count = 0, Count_T = 1, Count_Parts_T;
            string[] More_Phi, Function_U, Function_Part_U;
            double Step_On_Phi, Step_On_T;
            int Int_Step;
            HeatEquation My_Class = new HeatEquation();

            Console.WriteLine("Please, Write Lenght of X (use \",\" as separator)");
            string Str_X = Console.ReadLine();
            double Lenght_X;
            if (Double.TryParse(Str_X, out Lenght_X))
                My_Class.Lenght_X = Lenght_X;
            Console.WriteLine("Please, Write Lenght of T (use \",\" as separator)");
            string Str_T = Console.ReadLine();
            double Lenght_T;
            if (Double.TryParse(Str_T, out Lenght_T))
                My_Class.Lenght_T = Lenght_T;
            Console.WriteLine("Please, Write count steps on grid");
            string Str_Grid = Console.ReadLine();
            My_Class.Grid = Convert.ToInt32(Str_Grid);
            Console.WriteLine("Please, Write max steps for reconstruction function Pfi:");
            string Str_Count_Steps_On_Phi = Console.ReadLine();
            My_Class.Count_Steps_On_Pfi = Convert.ToInt32(Str_Count_Steps_On_Phi);
            Console.WriteLine("Please, Write count steps to see on Pfi:");
            string TMP_Count_Pfi = Console.ReadLine();
            Count = Convert.ToInt32(TMP_Count_Pfi);
            /*Console.WriteLine("Please, Write count steps to see on T:");
            string TMP_Count_T = Console.ReadLine();
            Count_T = Convert.ToInt32(TMP_Count_T);*/
            Console.WriteLine("Please, Write count parts to see on T:");
            string TMP_Count_Parts_T = Console.ReadLine();
            Count_Parts_T = Convert.ToInt32(TMP_Count_Parts_T);


            My_Class.Calculate();

            More_Phi = new string[Count];
            Function_U = new string[Count_T];
            Function_Part_U = new string[Count_Parts_T];

            Step_On_Phi = Convert.ToInt32(Str_Count_Steps_On_Phi) / Count;
            Int_Step = Convert.ToInt32(Math.Truncate(Step_On_Phi));
            Step_On_T = My_Class.Lenght_T / Count_Parts_T;

            var dataset = DataSet.Open("msds:memory");
            if (!dataset.Any(v => v.Name == "x"))
                dataset.Add<double[]>("x");

            double[] x = new double[My_Class.Count_Parts_For_X];
            for (int i = 0; i < My_Class.Count_Parts_For_X; i++)
                x[i] = i * (My_Class.Lenght_X / My_Class.Count_Parts_For_X);

           

            for (int i = 0; i < Count; i++)
            {
                if ((i + 1) * Int_Step != 10)
                {
                    More_Phi[i] = "P(x) for " + (i + 1) * Int_Step + " iterations";
                    if (!dataset.Any(v => v.Name == More_Phi[i]))
                        dataset.Add<double[]>(More_Phi[i]);
                }
            }
            
           // var dataset_2 = DataSet.Open("msds:memory");
            

            string VarPfi = "Pfi(x)";
            if (!dataset.Any(v => v.Name == VarPfi))
                dataset.Add<double[]>(VarPfi);


            string VarP_X = "P(x) for " + My_Class.Count_Steps_On_Pfi + " iterations";
            if (!dataset.Any(v => v.Name == VarP_X))
                dataset.Add<double[]>(VarP_X); 

            string VarP_X_0 = "P(x) for 1 iteration";
            if (!dataset.Any(v => v.Name == VarP_X_0))
                dataset.Add<double[]>(VarP_X_0);

            for (int i = 0; i < Count_Parts_T; i++)
            {
                Function_Part_U[i] = "u(x, t = " + (i + 1) * Step_On_T + ")";
                if (!dataset.Any(v => v.Name == Function_Part_U[i]))
                    dataset.Add<double[]>(Function_Part_U[i]);
            }

            for (int i = 0; i < Count_T; i++)
            {
                Function_U[i] = "u(x, t = " + (i + 1) * My_Class.Lenght_T + ")";
                if (!dataset.Any(v => v.Name == Function_U[i]))
                    dataset.Add<double[]>(Function_U[i]);
            }

            double[] p_x = new double[My_Class.Count_Parts_For_X];
            double[] pfi = new double[My_Class.Count_Parts_For_X];
            

            for (int i = 0; i < My_Class.Count_Parts_For_X; i++)
            {
                /*double[][] TMP_Result = new double[My_Class.Count_Parts_For_T + 1][];
                for (int j = 0; j < My_Class.Count_Parts_For_T + 1; j++)
                {
                    TMP_Result[j] = new double[My_Class.Count_Parts_For_X];
                }

                TMP_Result = My_Class.First_Tridiagonal_Matrix_Algorithm(My_Class.Lenght_X, My_Class.Lenght_T);

                for (int j = 0; j < My_Class.Count_Parts_For_X; j++)
                {
                    p_x[j] += TMP_Result[My_Class.Count_Parts_For_T][j];
                }*/
                pfi[i] = My_Class.Function_Pfi(i * My_Class.Lenght_OF_One_Step_On_X);
                   
            }
            dataset.PutData<double[]>("x", x);
            dataset.PutData<double[]>(VarPfi, pfi);
            for (int j = 0; j < Count_T; j++)
            {
                if (j != 0)
                {
                    My_Class.Lenght_T = Lenght_T * (j + 1);
                    My_Class.Calculate();
                }
                for (int i = 0; i < My_Class.Count_Parts_For_T + 1; i++)
                {
                    dataset.PutData<double[]>(Function_U[j], My_Class.Result[i]);

                }
            }

            for (int j = 0; j < Count_Parts_T; j++)
            {
                for (int i = 0; i < (My_Class.Count_Parts_For_T / Count_Parts_T) * (j +1 ) + 1; i++)
                {
                    dataset.PutData<double[]>(Function_Part_U[j], My_Class.Result[i]);

                }
            }

            p_x = My_Class.P_x(10);
            double[] p_x_0 = My_Class.P_x(1);
           // for (int i = 0; i < My_Class.Count_Parts_For_X; i++)
             //   p_x_0[i] = My_Class.Function_Pfi(i * My_Class.Lenght_OF_One_Step_On_X);
            dataset.PutData<double[]>(VarP_X, p_x);
            dataset.PutData<double[]>(VarP_X_0, p_x_0);

            for (int i = 0; i < Count; i++)
            {
                if ((i + 1) * Int_Step != 10)
                {
                    double[] p_x_more = new double[My_Class.Count_Parts_For_X];
                    p_x_more = My_Class.P_x((i + 1) * Int_Step);
                    dataset.PutData<double[]>(More_Phi[i], p_x_more);
                }
            }

            dataset.View();

        }

        public void Read_And_Count_Info()
        {

        }

    }
}
