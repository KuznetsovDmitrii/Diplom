﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMP
{
    class HeatEquation
    {
        private int grid;
        private double lenght_X;
        private double lenght_T;
        private int count_Steps_On_Pfi;
        public int Count_Parts_For_X;
        public int Count_Parts_For_T;
        public double Lenght_OF_One_Step_On_X;
        public double Lenght_OF_One_Step_On_T;
        public double[][] Result;
       // private double Tolerance;
        private double MU_1, MU_2, Psi, Pfi;
        private double Multiplayer_A;
        private double[] Source_Function;

        public double Lenght_X
        {
            get
            {
                return lenght_X;
            }
            set
            {
                lenght_X = value;
                Lenght_OF_One_Step_On_X = lenght_X / Count_Parts_For_X;
            }
        }

        public double Lenght_T
        {
            get
            {
                return lenght_T;
            }
            set
            {
                lenght_T = value;
                Lenght_OF_One_Step_On_T = lenght_T / Count_Parts_For_T;
            }
        }

        public int Grid
        {
            get
            {
                return grid;
            }
            set
            {
                grid = value;
                Count_Parts_For_T = grid;
                Count_Parts_For_X = grid;
                Lenght_OF_One_Step_On_T = lenght_T / Count_Parts_For_T;
                Lenght_OF_One_Step_On_X = lenght_X / Count_Parts_For_X;
            }
        }

        public int Count_Steps_On_Pfi
        {
            get
            {
                return count_Steps_On_Pfi;
            }
            set
            {
                count_Steps_On_Pfi = value;
            }
        }

        public HeatEquation(int Grid = 1000, double Lenght_X = 12.0, double Lenght_T = 4.0, double Lenght_OF_One_Step_On_X = 0.0,
            double Lenght_OF_One_Step_On_T = 0.0, int Count_Steps_On_Pfi = 10, double Multiplayer_A = 1, double MU_1 = 0.0, double MU_2 = 0.0,
            double Psi = 0.0, double Pfi = 0.0)
        {
            this.Grid = Grid;
            this.lenght_X = Lenght_X;
            this.lenght_T = Lenght_T;
            this.Count_Steps_On_Pfi = Count_Steps_On_Pfi; 
            this.Count_Parts_For_X = Grid; 
            this.Count_Parts_For_T = Grid; 
           // this.Tolerance = Tolerance;
            this.Multiplayer_A = Multiplayer_A;
            this.MU_1 = MU_1;
            this.MU_2 = MU_2; 
            this.Psi = Psi; 
            this.Pfi = Pfi;
            if (Lenght_OF_One_Step_On_X == 0.0)
               this.Lenght_OF_One_Step_On_X = Lenght_X / Count_Parts_For_X;
            else
                this.Lenght_OF_One_Step_On_X = Lenght_OF_One_Step_On_X;
            if (Lenght_OF_One_Step_On_T == 0.0)
                this.Lenght_OF_One_Step_On_T = Lenght_T / Count_Parts_For_X;
            else
                this.Lenght_OF_One_Step_On_T = Lenght_OF_One_Step_On_T;
        }

        public double Function_Pfi(double X)
        {
            double Pfi = 0.0;
            //Pfi = 2 - Math.Abs(X - 2) + Math.Abs(X - 4) ;
            //Pfi = Lenght_X / 2 - Math.Abs(X - Lenght_X / 2);
            //Pfi = 2 * Math.Pow(Math.E, -1 * 2 * Math.Abs(X - 2)) - 6 * Math.Pow(Math.E, -1 * 2 * Math.Abs(X - 6));
            //Pfi = X * X * X *X * X * (Lenght_X - X) * (Lenght_X - X) * (Lenght_X - X);
            // Pfi = Math.Sin(15 * Math.PI * X / Lenght_X);
            //Pfi = 15 * X * X * (Lenght_X - X) * (Lenght_X - X) * Math.Pow(Math.E,((-3) * (X - 4) * (X - 4))) - -
            //  5 * X * X * (Lenght_X - X) * (Lenght_X - X) * Math.Pow(Math.E, ((-3) * (X - 8) * (X - 8)));
            //Pfi = 15 * X * X * (Lenght_X - X) * (Lenght_X - X) * Math.Pow(Math.E, ((-3) * (X - 8) * (X - 8))) - 
            //5 * X * X * (Lenght_X - X) * (Lenght_X - X) * Math.Pow(Math.E, ((-3) * (X - 12) * (X - 12))) -
            //10 * X * X * (Lenght_X - X) * (Lenght_X - X) * Math.Pow(Math.E, ((-3) * (X - 4) * (X - 4))); 
            /*if ((X <= Lenght_X / 4) || ( X >= Lenght_X * 3 / 4))
               Pfi = 0;
           else
               Pfi = 3;*/
            Pfi = 1;

             //Pfi = Math.Sin(Math.PI * X) + 0.2 * Math.Sin(10 * Math.PI * X);
             //Pfi = (Lenght_X - X) * (Lenght_X - X) * Math.Sin(X) - (Lenght_X - X) * (Lenght_X - X) * Math.Sin((5 * Math.PI * X) / Lenght_X)/ 100;
            return Pfi;
        }

       /* public double F_x(double X)
        {
            double f_x = 0.0;

            if (X + Lenght_OF_One_Step_On_X > Lenght_X)
            {
                f_x = (-1) * Multiplayer_A * (Function_Pfi(Lenght_X) - 2 * Function_Pfi(X) +
                    Function_Pfi(X - Lenght_OF_One_Step_On_X)) / (Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X);
            }
            else if (X - Lenght_OF_One_Step_On_X < 0)
            {
                f_x = (-1) * Multiplayer_A * (Function_Pfi(X + Lenght_OF_One_Step_On_X) - 2 * Function_Pfi(X) +
                    Function_Pfi(0)) / (Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X);
            }
            else
            {
                f_x = (-1) * Multiplayer_A * (Function_Pfi(X + Lenght_OF_One_Step_On_X) - 2 * Function_Pfi(X) +
                    Function_Pfi(X - Lenght_OF_One_Step_On_X)) / (Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X);
            }
            return f_x;
        }*/

        /*public double F_x(double X)
        {
            double f_x = 0.0;
            if (X + 2 * Lenght_OF_One_Step_On_X > Lenght_X)
            {
                f_x = (-1) * Multiplayer_A * ((Function_Pfi(2 * Lenght_X  - X) + 16 * Function_Pfi(X + Lenght_OF_One_Step_On_X) - 30 *
                    Function_Pfi(X) + 16 * Function_Pfi(X - Lenght_OF_One_Step_On_X) - Function_Pfi(X - 2 * Lenght_OF_One_Step_On_X)) /
                    (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }
            else if (X + Lenght_OF_One_Step_On_X > Lenght_X)
            {
                f_x = (-1) * Multiplayer_A * ((Function_Pfi(2 * Lenght_X - X - Lenght_OF_One_Step_On_X) - 16 * Function_Pfi(2 * Lenght_X - X) - 30 *
                    Function_Pfi(X) + 16 * Function_Pfi(X - Lenght_OF_One_Step_On_X) - Function_Pfi(X - 2 * Lenght_OF_One_Step_On_X)) /
                    (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }
            else if (X - 2 * Lenght_OF_One_Step_On_X < 0)
            {
                f_x = (-1) * Multiplayer_A * (((-1) * Function_Pfi(X + 2 * Lenght_OF_One_Step_On_X) + 16 * Function_Pfi(X + Lenght_OF_One_Step_On_X) - 30 *
                   Function_Pfi(X) + 16 * Function_Pfi(X - Lenght_OF_One_Step_On_X) + Function_Pfi((-1)*X + 2 * Lenght_OF_One_Step_On_X)) /
                   (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }
            else if (X - Lenght_OF_One_Step_On_X < 0)
            {
                f_x = (-1) * Multiplayer_A * (((-1) * Function_Pfi(X + 2 * Lenght_OF_One_Step_On_X) + 16 * Function_Pfi(X + Lenght_OF_One_Step_On_X) - 30 *
                   Function_Pfi(X) - 16 * Function_Pfi((-1)*X + Lenght_OF_One_Step_On_X) + Function_Pfi((-1) * X + 2 * Lenght_OF_One_Step_On_X)) /
                   (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }
            else
            {
                f_x = (-1) * Multiplayer_A * (((-1) * Function_Pfi(X + 2 * Lenght_OF_One_Step_On_X) + 16 * Function_Pfi(X + Lenght_OF_One_Step_On_X) - 30 *
                   Function_Pfi(X) + 16 * Function_Pfi(X - Lenght_OF_One_Step_On_X) - Function_Pfi(X - 2 * Lenght_OF_One_Step_On_X)) /
                   (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }

            return f_x;
        }*/

        public double F_x(double X)
        {
            double f_x = 0.0;
            if (X + 2 * Lenght_OF_One_Step_On_X > Lenght_X)
            {
                X = X - 2 * Lenght_OF_One_Step_On_X;
                f_x = (-1) * Multiplayer_A * (((-1) * Function_Pfi(X + 2 * Lenght_OF_One_Step_On_X) + 16 * Function_Pfi(X + Lenght_OF_One_Step_On_X) - 30 *
                  Function_Pfi(X) + 16 * Function_Pfi(X - Lenght_OF_One_Step_On_X) - Function_Pfi(X - 2 * Lenght_OF_One_Step_On_X)) /
                  (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }
            else if (X + Lenght_OF_One_Step_On_X > Lenght_X)
            {
                X = X - Lenght_OF_One_Step_On_X;
                f_x = (-1) * Multiplayer_A * (((-1) * Function_Pfi(X + 2 * Lenght_OF_One_Step_On_X) + 16 * Function_Pfi(X + Lenght_OF_One_Step_On_X) - 30 *
                  Function_Pfi(X) + 16 * Function_Pfi(X - Lenght_OF_One_Step_On_X) - Function_Pfi(X - 2 * Lenght_OF_One_Step_On_X)) /
                  (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }
            else if (X - 2 * Lenght_OF_One_Step_On_X < 0)
            {
                X = X + 2 * Lenght_OF_One_Step_On_X;
                f_x = (-1) * Multiplayer_A * (((-1) * Function_Pfi(X + 2 * Lenght_OF_One_Step_On_X) + 16 * Function_Pfi(X + Lenght_OF_One_Step_On_X) - 30 *
                  Function_Pfi(X) + 16 * Function_Pfi(X - Lenght_OF_One_Step_On_X) - Function_Pfi(X - 2 * Lenght_OF_One_Step_On_X)) /
                  (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }
            else if (X - Lenght_OF_One_Step_On_X < 0)
            {
                X = X + Lenght_OF_One_Step_On_X;
                f_x = (-1) * Multiplayer_A * (((-1) * Function_Pfi(X + 2 * Lenght_OF_One_Step_On_X) + 16 * Function_Pfi(X + Lenght_OF_One_Step_On_X) - 30 *
                  Function_Pfi(X) + 16 * Function_Pfi(X - Lenght_OF_One_Step_On_X) - Function_Pfi(X - 2 * Lenght_OF_One_Step_On_X)) /
                  (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }
            else
            {
                f_x = (-1) * Multiplayer_A * (((-1) * Function_Pfi(X + 2 * Lenght_OF_One_Step_On_X) + 16 * Function_Pfi(X + Lenght_OF_One_Step_On_X) - 30 *
                   Function_Pfi(X) + 16 * Function_Pfi(X - Lenght_OF_One_Step_On_X) - Function_Pfi(X - 2 * Lenght_OF_One_Step_On_X)) /
                   (12 * Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            }

            return f_x;
        }

        public double[][] First_Tridiagonal_Matrix_Algorithm(double X, double T)
        {
            double[][] Result_First;
            int TMP_Count_Parts_For_T;
            TMP_Count_Parts_For_T = (int)(T / Lenght_OF_One_Step_On_T);
            Result_First = new double[TMP_Count_Parts_For_T + 1][];
            for (int i = 0; i < TMP_Count_Parts_For_T + 1; i++)
                Result_First[i] = new double[Count_Parts_For_X];

            double TMP_A = ((-1) * Multiplayer_A * Lenght_OF_One_Step_On_T) / (Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X);
            double TMP_B = 1 + ((2 * Multiplayer_A * Lenght_OF_One_Step_On_T) / (Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X));
            double TMP_C = ((-1) * Multiplayer_A * Lenght_OF_One_Step_On_T) / (Lenght_OF_One_Step_On_X * Lenght_OF_One_Step_On_X);
            double TMP_Ksi;

            double[] Alfa = new double[Count_Parts_For_X];
            double[] Betta = new double[Count_Parts_For_X];

            for (int j = 0; j < Count_Parts_For_X; j++)
                Result_First[0][j] = F_x(j * Lenght_OF_One_Step_On_X);

            for (int i = 0; i < TMP_Count_Parts_For_T; i++)
            {
                Alfa[0] = 0;
                Betta[0] = 0;

                for (int j = 1; j < Count_Parts_For_X; j++)
                {
                    TMP_Ksi = Result_First[i][j - 1];
                    Alfa[j] = (-1) * TMP_A / (TMP_B + TMP_C * Alfa[j - 1]);
                    Betta[j] = (TMP_Ksi - TMP_C * Betta[j - 1]) / (TMP_B + TMP_C * Alfa[j - 1]);
                }

                Result_First[i + 1][Count_Parts_For_X - 1] = 0;

                for (int j = Count_Parts_For_X - 2; j >= 0; j--)
                {
                    Result_First[i + 1][j] = Alfa[j + 1] * Result_First[i + 1][j + 1] + Betta[j + 1];
                }
            }
            return Result_First;
        }

        public double[] P_x(int сount_Steps_On_Pfi)
        {
            double[] p_x = new double[Count_Parts_For_X];

            for (int j = 0; j < Count_Parts_For_X; j++)
            {
                p_x[j] = F_x(j * Lenght_OF_One_Step_On_X);
            }

            for (int i = 1; i <= сount_Steps_On_Pfi; i++)
            {
                int TMP_Count_Parts_For_T;
                TMP_Count_Parts_For_T = (int)((i * lenght_T) / Lenght_OF_One_Step_On_T);
                double[][] TMP_Result = new double[TMP_Count_Parts_For_T + 1][];

                for (int j = 0; j < Count_Parts_For_T + 1; j++)
                {
                    TMP_Result[j] = new double[Count_Parts_For_X];
                }

                TMP_Result = First_Tridiagonal_Matrix_Algorithm(Lenght_X, lenght_T * i);

                for (int j = 0; j < Count_Parts_For_X; j++)
                {
                    p_x[j] += TMP_Result[TMP_Count_Parts_For_T][j];
                }

            }
            return p_x;
        }

        public double[][] Second_Tridiagonal_Matrix_Algorithm(double X, double T)
        {
            double[][] Result_Second;
            double TMP_Step_T, TMP_Step_X;

            Result_Second = new double[Count_Parts_For_T + 1][];
            for (int i = 0; i < Count_Parts_For_T + 1; i++)
                Result_Second[i] = new double[Count_Parts_For_X];

            TMP_Step_T = T / Count_Parts_For_T;
            TMP_Step_X = X / Count_Parts_For_X;

            double TMP_A = ((-1) * Multiplayer_A * TMP_Step_T) / (TMP_Step_X * TMP_Step_X);
            double TMP_B = 1 + (2 * Multiplayer_A * TMP_Step_T) / (TMP_Step_X * TMP_Step_X);
            double TMP_C = ((-1) * Multiplayer_A * TMP_Step_T) / (TMP_Step_X * TMP_Step_X);
            double TMP_Ksi;

            double[] Alfa = new double[Count_Parts_For_X];
            double[] Betta = new double[Count_Parts_For_X];

            Source_Function = P_x(Count_Steps_On_Pfi);

            for (int i = 0; i < Count_Parts_For_X - 1; i++)
            {
                Result_Second[0][i] = 0;
            }

            for (int i = 0; i < Count_Parts_For_T; i++)
            {
                Alfa[0] = 0;
                Betta[0] = 0;
                for (int j = 1; j < Count_Parts_For_X - 1; j++)
                {
                    TMP_Ksi = Result_Second[i][j] + TMP_Step_T * Source_Function[j];
                    Alfa[j] = (-1) * TMP_A / (TMP_B + TMP_C * Alfa[j - 1]);
                    Betta[j] = (TMP_Ksi - TMP_C * Betta[j - 1]) / (TMP_B + TMP_C * Alfa[j - 1]);
                }

                Result_Second[i + 1][Count_Parts_For_X - 1] = 0;

                for (int j = Count_Parts_For_X - 2; j >= 0; j--)
                {
                    Result_Second[i + 1][j] = Alfa[j] * Result_Second[i + 1][j + 1] + Betta[j];
                }
            }
            return Result_Second;
        }

        public void Calculate()
        {
            double[] My_Tolerance = new double[Count_Parts_For_X];
            Result = new double[Count_Parts_For_T + 1][];
            for (int i = 0; i < Count_Parts_For_T + 1; i++)
                Result[i] = new double[Count_Parts_For_X];

            Result = Second_Tridiagonal_Matrix_Algorithm(Lenght_X, lenght_T);

            for (int i = 0; i < Count_Parts_For_X; i++)
            {
                My_Tolerance[i] = Result[Count_Parts_For_T][i] - Function_Pfi(i * Lenght_OF_One_Step_On_X);
            }

            //for (int i = 0; i < Count_Parts_For_X; i++)
            //    Console.WriteLine(My_Tolerance[i]);
        }
    }
}
