using System;

namespace Implementaciones
{
    class EditDistance1
    {
        static Random r = new Random();

        static void MainEditDistance1(string[] args)
        {
            Test(LevenshteinDistance);
            return;

            int count_test = 10;
            while (count_test > 0)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Enter first string");
                string firstStr = Console.ReadLine();
                Console.WriteLine("Enter second string");
                string secondStr = Console.ReadLine();

                Console.WriteLine("Distancia = " + EditDistance(firstStr, secondStr));
                count_test--;
                Console.WriteLine("-------------------------------");
            }
        }

        private static int EditDistance(string firstStr, string secondStr)
        {
            //return RecursiveEditDistance(firstStr, secondStr);
            //return DynamicEditDistance(firstStr, secondStr);
            return LevenshteinDistance(firstStr, secondStr);
        }

        //Dynamic method
        private static int LevenshteinDistance(string s, string t)
        {
            /* Distancia de Levenshtein, distancia de edición, o distancia entre palabras, al número mínimo de operaciones 
             * requeridas para transformar una cadena de caracteres en otra. Se entiende por operación, bien una inserción, 
             * eliminación o la sustitución de un carácter. Esta distancia recibe ese nombre en honor al científico ruso 
             * Vladimir Levenshtein, quien se ocupara de esta distancia en 1965. Es útil en programas que determinan cuán 
             * similares son dos cadenas de caracteres, como es el caso de los correctores de ortografía.

             * Por ejemplo, la distancia de Levenshtein entre "casa" y "calle" es de 3 porque se necesitan al menos tres 
             * ediciones elementales para cambiar uno en el otro.

             *    casa → cala (sustitución de 's' por 'l')
             *    cala → calla (inserción de 'l' entre 'l' y 'a')
             *    calla → calle (sustitución de 'a' por 'e')

             * Se le considera una generalización de la distancia de Hamming, que se usa para cadenas de la misma longitud y que 
             * solo considera como operación la sustitución. Hay otras generalizaciones de la distancia de Levenshtein, como la 
             * distancia de Damerau-Levenshtein, que consideran el intercambio de dos caracteres como una operación
             */

            // d es una tabla con m+1 renglones y n+1 columnas
            int costo = 0;
            int sl = s.Length;
            int tl = t.Length;
            int[,] d = new int[sl + 1, tl + 1];

            // Verifica que exista algo que comparar
            if (tl == 0) return sl;
            if (sl == 0) return tl;

            // Llena la primera columna y la primera fila.
            for (int i = 0; i <= sl; d[i, 0] = i++) ;
            for (int j = 0; j <= tl; d[0, j] = j++) ;

            // recorre la matriz llenando cada unos de los pesos.
            // i columnas, j renglones
            for (int i = 1; i <= sl; i++)
                // recorre para j
                for (int j = 1; j <= tl; j++)
                {
                    // si son iguales en posiciones equidistantes el peso es 0
                    // de lo contrario el peso suma uno.
                    costo = (s[i - 1] == t[j - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1,    //Eliminacion
                                                 d[i, j - 1] + 1),   //Inserccion 
                                        d[i - 1, j - 1] + costo);    //Sustitucion
                }

            return d[sl, tl];
        }

        //Recursive method
        private static int RecursiveEditDistance(string firstStr, string secondStr)
        {
            return RecursiveEditDistance(firstStr, secondStr, 0, 0);
        }
        private static int RecursiveEditDistance(string firstStr, string secondStr, int f, int s)
        {
            if (f == firstStr.Length && s == secondStr.Length)
                return 0;
            //Eliminación o inserción
            else if (f == firstStr.Length)
                return 1 + RecursiveEditDistance(firstStr, secondStr, f, s + 1);
            //Eliminación o inserción
            else if (s == secondStr.Length)
                return 1 + RecursiveEditDistance(firstStr, secondStr, f + 1, s);
            else if (firstStr[f] == secondStr[s])
                return RecursiveEditDistance(firstStr, secondStr, f + 1, s + 1);
            else
            {
                return 1 + Math.Min(
                                    RecursiveEditDistance(firstStr, secondStr, f, s + 1),
                                    Math.Min(
                                            RecursiveEditDistance(firstStr, secondStr, f + 1, s + 1),
                                            RecursiveEditDistance(firstStr, secondStr, f + 1, s)
                                            )
                                );
            }
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write(matrix[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void Test(DEditDistance metod)
        {
            int testCount = 1000;
            int wrong = 0;

            int TimeM = 0;
            int TimeR = 0;

            int min = 0;
            int seg = 0;
            int miliseg = 0;

            for (int l = 0; l < testCount; l++)
            {
                int c1 = r.Next(1, 9);
                string p1 = "";
                for (int i = 0; i < c1; i++)
                    p1 += (char)r.Next(60, 123);

                int c2 = r.Next(1, 9);
                string p2 = "";
                for (int i = 0; i < c2; i++)
                    p2 += (char)r.Next(60, 123);

                #region Print

                string print = "";

                int start = Environment.TickCount;
                int metodResult = metod.Invoke(p1, p2);
                int end = Environment.TickCount;

                min = (end - start) / 60000;
                seg = ((end - start) % 60000) / 1000;
                miliseg = ((end - start) % 60000) % 1000;
                string methodTime = min + ":" + seg + ":" + miliseg;
                print += "Method[Result: " + metodResult + " Time: " + methodTime + "]    ";

                TimeM += end - start;

                start = Environment.TickCount;
                int recursiveResult = RecursiveEditDistance(p1, p2);
                end = Environment.TickCount;

                min = (end - start) / 60000;
                seg = ((end - start) % 60000) / 1000;
                miliseg = ((end - start) % 60000) % 1000;
                string recursiveTime = min + ":" + seg + ":" + miliseg;
                print += "Recursive[Result: " + recursiveResult + " Time: " + recursiveTime + "]";

                TimeR += end - start;

                #endregion

                if (metodResult != recursiveResult)
                    wrong++;

                #region Print

                Console.WriteLine(p1 + "     " + p2);
                Console.WriteLine(print);
                Console.WriteLine("-------------------------------------------------------------");

                #endregion
            }

            min = (TimeM) / 60000;
            seg = ((TimeM) % 60000) / 1000;
            miliseg = ((TimeM) % 60000) % 1000;
            string methodTimeMean = min + ":" + seg + ":" + miliseg;

            min = (TimeR) / 60000;
            seg = ((TimeR) % 60000) / 1000;
            miliseg = ((TimeR) % 60000) % 1000;
            string recursiveTimeMean = min + ":" + seg + ":" + miliseg;

            Console.WriteLine("Wrong Results: " + wrong + "/" + testCount + "Time Mean[Method: " + methodTimeMean + " Recursive: " + recursiveTimeMean + "]");
        }

        private delegate int DEditDistance(string firstStr, string secondStr);
    }
}
