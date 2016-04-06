using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2 {
    class Matrix {
        int n;
        int m;
        double[,] data;        

        public Matrix(int nRows, int nCols) {
            this.n = nRows;
            this.m = nCols;
            this.data = new double[nRows, nCols];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    data[i, j] = 0.0;
        }

        public Matrix(): this(2, 2) {}

        public Matrix(double[,] initData) {
            this.n = initData.GetLength(0);
            this.m = initData.GetLength(1);
            this.data = initData;
        }

        public int Rows {
            get { return this.n; }
        }

        public int Columns {
            get { return this.m; }
        }

        public int Size {
            get {
                if (!IsSquared)
                    throw new Exception("Матрица должна быть квадратной!");
                return this.n * this.m;
            }
        }

        double[,] Data {
            get { return this.data; }
            set { this.data = value; }
        }

        double this[int i, int j] {
            get { return this.data[i, j]; }      
            set { this.data[i, j] = value; }      
        }

        public bool IsEmpty {
            get {
                for (int i = 0; i < this.Rows; i++)
                    for (int j = 0; j < this.Columns; j++)
                        if (this[i, j] != 0)
                            return false;
                return true;
            }
        }

        public bool IsSquared {
            get {
                return (this.n == this.m);
            }
        }

        public bool IsSymmetric {
            get {
                if (!IsSquared)
                    return false;
                for (int i = 0; i < this.Rows; i++)
                    for (int j = 0; j < this.Columns; j++)
                        if (this[i, j] != this[j, i])
                            return false;

                return true;
            }
        }

        public bool IsDiagonal {
            get {
                if (!IsSquared)
                    return false;
                for (int i = 0; i < this.Rows; i++)
                    for (int j = 0; j < this.Columns; j++)
                        if (i != j && this[i, j] != 0)
                            return false;
            
                return true;
            }
        }

        public bool IsUnity {
            get {
                for (int i = 0; i < this.Rows; i++)
                    for (int j = 0; j < this.Columns; j++)
                        if (this[i, j] != 1)
                            return false;

                return true;
            }
        }

        public static bool isEqual(Matrix m1, Matrix m2) {
            return (m1.Rows == m2.Rows && m1.Columns == m2.Columns);
        }

        public static Matrix operator+(Matrix m1, Matrix m2) {
            if (!isEqual(m1, m2))
                throw new Exception("Размеры матриц не совпадают!");
            Matrix m = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++) {
                for (int j = 0; j < m1.Columns; j++) {
                    m[i, j] = m1[i, j] + m2[i, j];
                }
            }                
            return m;
        }

        public static Matrix operator-(Matrix m1, Matrix m2) {
            if (!isEqual(m1, m2))
                throw new Exception();
            Matrix m = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++) {
                for (int j = 0; j < m1.Columns; j++) {
                    m[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return m;
        }

        public static Matrix operator*(Matrix m1, double d) {
            Matrix m = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++) {
                for (int j = 0; j < m1.Columns; j++) {
                    m[i, j] = m1[i, j] * d;
                }
            }
            return m;
        }

        public static Matrix operator*(Matrix m1, Matrix m2) {
            if (m1.Columns != m2.Rows)
                throw new Exception("Количество строк второй матрицы должно быть равным количеству столбцов первой матрицы!");
            Matrix m = new Matrix(m1.Rows, m2.Columns);
            for (int i = 0; i < m.Rows; i++) {
                for (int j = 0; j < m.Columns; j++) {
                    for (int k = 0; k < m1.Columns; k++) {
                        m[i, j] = m[i, j] + m1[i, k] * m2[k, j];
                    }
                }
            }
            return m;
        }

        public static explicit operator Matrix(double[,] arr) {
            return new Matrix(arr);
        }

        public Matrix Transpose() {
            Matrix m = new Matrix(Columns, Rows);
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    m[j, i] = this[i, j];

            return m;
        }

        public double Trace() {
            if (!IsSquared)
                throw new Exception("Матрица должна быть квадратной!");
            double sum = 0.0;
            if (IsEmpty)
                return sum;
            if (IsUnity)
                return 1 * Rows;
            for (int i = 0; i < Rows; i++)
                if (i < Columns) 
                    sum += this[i, i];          
                       
            return sum;
        }

        public override string ToString() {
            string s = "";
            for (int i = 0; i < this.Rows; i++) {
                for (int j = 0; j < this.Columns; j++) { 
                    s += (this[i, j] + " ");
                }
                s += "\n";
            }
            return s;
        }

        public static Matrix GetUnity(int size) {
            Matrix m = new Matrix(size / 2, size / 2);
            for (int i = 0; i < size / 2; i++)
                for (int j = 0; j < size / 2; j++)
                    m[i, j] = 1;
            return m;
        }

        public static Matrix GetEmpty(int size) {
            return new Matrix(size / 2, size / 2);
        }

        public static Matrix Parse(string s) {
            if (!s.Contains(", "))
                throw new FormatException();
            string[] arr = s.Replace(", ", ",").Split(',');
            Matrix m = new Matrix(arr.Length, arr[0].Split(' ').Length);
            for (int i = 0; i < m.Rows; i++) {
                if (!arr[i].Contains(' '))
                    throw new FormatException();
                string[] arr_i = arr[i].Split(' ');
                for (int j = 0; j < m.Columns; j++) {
                    try {
                        m[i, j] = Double.Parse(arr_i[j].ToString());
                    } catch (FormatException) {
                        throw;
                    }
                }
            }
            return m;
        }

        public static bool TryParse(string s, out Matrix m) {
            try {
                m = Parse(s);
                return true;
            } catch (FormatException) {
                m = null;
                return false;
            }
        }
        
        public static Matrix InitMatrix() {            
            Console.Write("Введите матрицу в формате '1 2 3, 4 5 6': ");
            String res = Console.ReadLine();
            Matrix m;
            while (!Matrix.TryParse(res, out m)) {
                Console.Write("Неправильный формат матрицы, введите еще раз: ");
                res = Console.ReadLine();
            }
            m = Matrix.Parse(res);
            return m;
        }

        public static bool AlertOnNull(Matrix m) {
            if (m == null) {
                Console.WriteLine("Сначала введите матрицу!!");
                return true;
            }
            else
                return false;
        }

        public static Matrix InitOperation(Matrix matrix, int op) {
            Matrix m = matrix;
            Console.WriteLine("У Вас создана матрица: ");
            Console.WriteLine(m.ToString());
            Console.Write("Провести операцию ней (0) или создать новые матрицы (1)?: ");
            string ch = Console.ReadLine();
            while (ch != "0" && ch != "1") {
                Console.Write("Неправильно! Введите еще раз: ");
                ch = Console.ReadLine();
            }
            if (ch == "0") {
                try {
                    if (op == 8)
                        m = m + Matrix.InitMatrix();
                    else if (op == 9)
                        m = m - Matrix.InitMatrix();
                    else if (op == 10)
                        m = m * Matrix.InitMatrix();
                }
                catch (Exception) {
                    Console.WriteLine("Неправильные размеры матриц!");
                }
            }
            else {
                try {
                    if (op == 8)
                        m = Matrix.InitMatrix() + Matrix.InitMatrix();
                    else if (op == 9)
                        m = Matrix.InitMatrix() - Matrix.InitMatrix();
                    else if (op == 10)
                        m = Matrix.InitMatrix() * Matrix.InitMatrix();
                }
                catch (Exception) {
                    Console.WriteLine("Неправильные размеры матриц!");
                }
            }
            Console.WriteLine("Результат операции:");
            Console.WriteLine(m.ToString());
            return m;
        }
    }
}

