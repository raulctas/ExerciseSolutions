using System;

namespace COJ
{
    class Program1288
    {
        static void Main1288(string[] args)
        {
            int casos = int.Parse(Console.ReadLine());
            while (casos != 0)
            {
                string numero = Console.ReadLine();
                int suma = 0;
                int ultimo_digito = int.Parse(numero[numero.Length - 1].ToString());
                for (int i = 0; i < numero.Length; i++)
                    suma += int.Parse(numero[i].ToString());
                if (suma % 3 == 0 && ultimo_digito % 2 == 0)
                    Console.WriteLine("YES");
                else
                    Console.WriteLine("NO");
                casos--;
            }
        }
    }
}
