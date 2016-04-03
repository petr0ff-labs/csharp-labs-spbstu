using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2 {
    class Program {
        static void Main(string[] args) {
            int choice = 5;
            Matrix m = null;
            do {
                try {
                    choice = ShowMainMenu();
                }
                catch (FormatException) { }
                switch (choice) {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        m = Matrix.InitMatrix();
                        break;
                    case 2:
                        m = SelectOperation(m);
                        break;
                    case 3:
                        if (m == null)
                            Console.WriteLine("Сначала введите матрицу!");
                        else
                            Console.WriteLine(m.ToString());
                        break;
                    default: break;
                }
            } while (choice != 0);
        }
        
        public static int ShowMainMenu() {
            Console.WriteLine(
                @"Работа с матрицами
                -----------------------
                1 – Ввод матрицы
                2 – Операции
                3 – Вывод результатов                
                0 - Выход из программы
                -----------------------");
            Console.Write("Введите Ваш выбор: ");
            return Int32.Parse(Console.ReadLine());
        }

        public static Matrix SelectOperation(Matrix matrix) {
            Matrix m = matrix;
            int choice = 15;
            do {
                Console.WriteLine(
                    @"Работа с матрицами
                -----------------------
                1 – Транспонирование матрицы
                2 - Посчитать след матрицы
                3 – Является ли матрица квадратной?
                4 – Является ли матрица нулевой?                
                5 - Является ли матрица симметричной?
                6 - Является ли матрица единичной?
                7 - Является ли матрица диагональной?
                8 - Сложить матрицы                
                9 - Вычесть матрицы
                10 - Умножить матрицы
                11 - Создать единичную матрицу
                12 - Создать нулевую матрицу
                0 - Вернуться в главное меню
                -----------------------");
                Console.Write("Введите Ваш выбор: ");
                String res = Console.ReadLine();
                while (!Int32.TryParse(res, out choice)) {
                    Console.Write("Неправильный формат, введите еще раз: ");
                    res = Console.ReadLine();
                }
                switch (choice) {
                    case 0:
                        return m;
                    case 1:
                        if (Matrix.AlertOnNull(m))
                            return m;
                        m = m.Transpose();
                        Console.WriteLine("Транспонированная матрица:");
                        Console.WriteLine(m.ToString());
                        break;
                    case 2:
                        if (Matrix.AlertOnNull(m))
                            return m;
                        try {
                            double trace = m.Trace();
                            Console.WriteLine("След матрицы равен " + trace);
                        } catch (Exception) {
                            Console.WriteLine("Матрица должна быть квадратной!");
                        }
                        break;
                    case 3:
                        if (Matrix.AlertOnNull(m))
                            return m;
                        Console.WriteLine("Матрица ");
                        Console.Write(m.ToString());
                        res = m.isSquared() ? "Квадратная" : "Не квадратная";
                        Console.WriteLine(res);
                        break;
                    case 4:
                        if (Matrix.AlertOnNull(m))
                            return m;
                        Console.WriteLine("Матрица ");
                        Console.Write(m.ToString());
                        res = m.isEmpty() ? "Нулевая" : "Не нулевая";
                        Console.WriteLine(res);
                        break;
                    case 5:
                        if (Matrix.AlertOnNull(m))
                            return m;
                        Console.WriteLine("Матрица ");
                        Console.Write(m.ToString());
                        res = m.isSymmetric() ? "Симметрична" : "Не симметрична";
                        Console.WriteLine(res);
                        break;
                    case 6:
                        if (Matrix.AlertOnNull(m))
                            return m;
                        Console.WriteLine("Матрица ");
                        Console.Write(m.ToString());
                        res = m.isUnity() ? "Единичная" : "Не единичная";
                        Console.WriteLine(res);
                        break;
                    case 7:
                        if (Matrix.AlertOnNull(m))
                            return m;
                        Console.WriteLine("Матрица ");
                        Console.Write(m.ToString());
                        res = m.isDiagonal() ? "Диагональная" : "Не диагональная";
                        Console.WriteLine(res);
                        break;
                    case 8:
                        if (!Matrix.AlertOnNull(m)) {
                            m = Matrix.InitOperation(m, 8);
                        }
                        break;
                    case 9:
                        if (!Matrix.AlertOnNull(m)) {
                            m = Matrix.InitOperation(m, 9);
                        }
                        break;
                    case 10:
                        if (!Matrix.AlertOnNull(m)) {
                            m = Matrix.InitOperation(m, 10);
                        }
                        break;
                    case 11:
                        Console.Write("Введите размер матрицы: ");
                        res = Console.ReadLine();
                        int size;
                        while (!Int32.TryParse(res, out size)) {
                            Console.Write("Неправильный формат, введите еще раз: ");
                            res = Console.ReadLine();
                        }
                        m = Matrix.GetUnity(size);
                        break;
                    case 12:
                        Console.Write("Введите размер матрицы: ");
                        res = Console.ReadLine();
                        while (!Int32.TryParse(res, out size)) {
                            Console.Write("Неправильный формат, введите еще раз: ");
                            res = Console.ReadLine();
                        }
                        m = Matrix.GetEmpty(size);
                        break;
                    default: break;
                }
            } while (choice != 0);
            return m;
        }

        
    }
}

