using System;


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