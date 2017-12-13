using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FnLabType = System.Single;

namespace FS_Lab4
{
    static class FSLab4
    {
        static private FnLabType number = (FnLabType)0.0;

        public static FnLabType[,] LoadFromTextFile(string name, int size)
        {
            FnLabType[,] result = new FnLabType[size, size];

            if (File.Exists(name))
            {
                using (var file = File.Open(name, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string s = reader.ReadLine();
                        Console.WriteLine(s);

                        int i = 0;
                        while (!reader.EndOfStream)
                        {
                            int j = 0;
                            s = reader.ReadLine();
                            s = s + " ";
                            s = s.TrimStart(' ');
                            while (s.Length != 0)
                            {
                                FnLabType number;
                                string snumber = s.Substring(0, s.IndexOf(" "));
                                snumber = snumber.Replace('.', ',');
                                FnLabType.TryParse(snumber, out number);
                                s = s.Remove(0, s.IndexOf(" ") + 1);
                                s = s.TrimStart(' ');

                                result[i, j] = number;
                                j = j + 1;
                            }
                            i = i + 1;
                        }
                    }
                }

                return result;
            }
            else
            {
                Console.WriteLine("Что-то там ...");
                return result;
            }
        }

        // Определяет количество значимых цифр после запятой
        public static int MeaningNumbers(FnLabType input)
        {
            FnLabType number = (FnLabType)0.0;
            string s = "";
            int eps = 0;
            if (number.GetType().ToString() == "System.Double")
            {
                s = Math.Abs(input).ToString("F15"); // s = A[i,i] c 15-ю цифрами после запятой
                s = s.Substring(0, 16); // всего в FnLabType 15 значачих цифр. + 1 запятая. Берем из s только 15 значащих цифр
                eps = 15 - s.IndexOf(","); // отсюда получаем кол-во значащих цифр после запятой. 15 минус положение "," 
                if (Math.Truncate(Math.Abs(input)) == (FnLabType)0) eps++;
            }
            else if (number.GetType().ToString() == "System.Single")
            {
                s = Math.Abs(input).ToString("F7"); // s = A[i,i] c 7-ю цифрами после запятой
                s = s.Substring(0, 8); // всего в FnLabType 7 значачих цифр. + 1 запятая. Берем из s только 7 значащих цифр
                eps = 7 - s.IndexOf(","); // отсюда получаем кол-во значащих цифр после запятой. 7 минус положение "," 
                if (Math.Truncate(Math.Abs(input)) == (FnLabType)0) eps++;
            }    
            return eps;
        }

        public static void SaveMatrixToTextFile(string name, FnLabType[,] matrix, int collength, int rowlength)
        {
            using (var file = File.Open(name, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        for (int i = 0; i <= collength - 1; i++)
                        {
                            for (int j = 0; j <= rowlength - 1; j++)
                            {
                                int nums = MeaningNumbers(matrix[i, j]) ;
                                string s = matrix[i, j].ToString("F" + nums);
                                while (s.Length <= 20) s = " "+ s;
                                writer.Write(s);
                                writer.Write("\t");
                            }
                            writer.WriteLine();
                        }
                    }
                }
        }

        static void Main(string[] args)
        {
            int n;
            FnLabType[,] A; // Матрица A

            // Запрос ввода размера системы
            while (true)
            {
                Console.Write("Введите размер системы: ");
                if (int.TryParse(Console.ReadLine(), out n))
                    break;
            }

            A = LoadFromTextFile("input.txt", n); Console.WriteLine();
            SaveMatrixToTextFile("output.txt", A, n, n);

        }

    }
}
