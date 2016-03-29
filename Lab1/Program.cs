using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab1 {
    class Program {        
        static void Main(string[] args) {
            Dictionary<string, Type> types = new Dictionary<string, Type>();
            types.Add("int", typeof(int));
            types.Add("uint", typeof(uint));
            types.Add("string", typeof(string));
            types.Add("float", typeof(float));
            types.Add("double", typeof(double));
            types.Add("long", typeof(long));
            types.Add("char", typeof(string));
            int choice = 4;
            do {
                try {
                    choice = ShowMainMenu();
                }
                catch (FormatException) { }
                switch (choice) {
                    case 0:
                        Exit();
                        break;
                    case 1:
                        ShowAllTypeInfo();
                        break;
                    case 2:
                        SelectType();
                        break;
                    case 3:
                        string t = "";
                        do
                        {
                            Console.Write("Введите имя типа: ");
                            t = Console.ReadLine();
                        } while (!types.Keys.Contains(t));
                        ShowInfo(types[t]);
                        break;
                    case 4:
                        ChangeConsoleView();
                        break;
                    default: break;
                }
            } while (choice != 0);
        }

        public static void ShowInfo(Type t) {
            ShowTypeInfo(t);
        }

        public static int ShowMainMenu() {
            Console.WriteLine(
                @"Выберите необходимое действие:
                1 – Общая информация по типам
                2 – Выбрать из списка
                3 – Ввести имя типа
                4 – Параметры консоли
                0 - Выход из программы");
            Console.Write("Введите Ваш выбор: ");
            return Int32.Parse(Console.ReadLine());
        }

        public static void Exit() {
            Console.Clear();
            Environment.Exit(0);
        }

        public static void ShowAllTypeInfo() {
            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            foreach (Assembly asm in refAssemblies)
                types.AddRange(asm.GetTypes());
            TypeInfo tInfo = new TypeInfo(types);
            Console.WriteLine(
                @"Общая информация по типам
                Подключенные сборки: " + refAssemblies.Length + @"
                Всего типов по всем подключенным сборкам: " + types.Count + @"
                Ссылочные типы: " + tInfo.refTypesCount + @"
                Значимые типы: " + tInfo.valueTypesCount + @"
                Типы-интерфейсы: " + tInfo.interfacesCount + @"
                Тип с максимальным числом методов: " + tInfo.maxMethodsType + @"
                Самое длинное название метода: " + tInfo.maxMethodName + @"
                Метод с наибольшим числом аргументов: " + tInfo.maxMethodArgs);
        }        

        public static void ChangeConsoleView() {
            int choice = 5;            
            Console.WriteLine(
                @"Выберите цвет фона консоли:
                1 – жёлтый
                2 – синий
                3 – чёрный
                4 - очистить консоль 
                0 – Выход в главное меню");
            Console.Write("Введите Ваш выбор: ");
            do {
                try {
                    choice = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException) { }
                switch (choice) {
                    case 0:
                        Console.Clear();
                        break;
                    case 1:
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        break;
                    case 2:
                        Console.BackgroundColor = ConsoleColor.Blue;
                        break;
                    case 3:
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Неправильный выбор!");
                        break;
                }
            } while (choice != 0 && choice > 5);
        }

        public static void SelectType() {
            int choice = 10;
            Console.WriteLine(
                @"Выберите тип из списка:
                1 – uint
                2 – int
                3 – long
                4 – float
                5 – double
                6 – char
                7 - string
                8 – MyClass
                9 – MyStruct
                0 – Выход в главное меню");
            Console.Write("Введите Ваш выбор: ");
            do {
                try {
                    choice = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException) { }
                
                switch (choice) {
                    case 0:
                        Console.WriteLine("-------------------");
                        return;
                    case 1:
                        ShowTypeInfo(typeof(uint));
                        break;
                    case 2:
                        ShowTypeInfo(typeof(int));
                        break;
                    case 3:
                        ShowTypeInfo(typeof(long));
                        break;
                    case 4:
                        ShowTypeInfo(typeof(float));
                        break;
                    case 5:
                        ShowTypeInfo(typeof(double));
                        break;
                    case 6:
                        ShowTypeInfo(typeof(char));
                        break;
                    case 7:
                        ShowTypeInfo(typeof(string));
                        break;
                    case 8:
                        ShowTypeInfo(typeof(MyClass));
                        break;
                    case 9:
                        ShowTypeInfo(typeof(MyStruct));
                        break;
                    default:
                        Console.WriteLine("Неправильный выбор!");
                        break;
                }
            } while (choice != 0 && choice > 9);
        }

        public static void ShowTypeInfo(Type t) {          
            Console.WriteLine(
                    @"Информация по типу: " + t.FullName + @"
                Значимый тип: " + (t.IsValueType ? "+" : "-") + @"
                Пространство имен: " + t.Namespace + @"
                Сборка: " + t.Assembly.GetName().Name + @"
                Общее число элементов: " + t.GetMembers().Length + @"
                Число методов: " + t.GetMethods().Length + @"
                Число свойств: " + t.GetProperties().Length + @"
                Число полей: " + t.GetFields().Length + @"
                Список полей: " + printFields(t) + @"
                Список свойств: " + printProperties(t) + @"
                Нажмите ‘M’ для вывода дополнительной информации по
                методам:
                Нажмите ‘0’ для выхода в главное меню");
            Console.Write("Введите Ваш выбор: ");
            char ch = '0';
            do {
                try {
                    ch = Char.Parse(Console.ReadLine());
                } catch (FormatException) {
                    Console.WriteLine("Неправильный выбор!");
                }
                switch (ch) {
                    case 'M':
                        getExtendedInfo(t);
                        break;
                    case '0':
                        Console.WriteLine("-------------------");
                        return;
                    default:
                        Console.WriteLine("Неправильный выбор!");
                        break;
                }
            } while (ch != '0' && ch.ToString().Length > 1);
        }

        public static void getExtendedInfo(Type t) {
            Console.WriteLine("{0,-25}  {1,15}  {2,15}", "Название", "Число перегрузок", "Количество аргументов");
            Console.WriteLine("-----------------------------------------------------------------");
            Dictionary <string, int> dic = new Dictionary<string, int>();
            
            foreach (var method in t.GetMethods()) {
                if (dic.Keys.Contains(method.Name))
                    dic[method.Name]++;
                else
                    dic.Add(method.Name, 1);
            }

            foreach (var m in dic.Keys) {
                Console.WriteLine("{0,-25}{1,15}{2,15}", m, dic[m].ToString(), getParamsCount(t, m, dic[m]));
            }
        }

        public static string getParamsCount(Type t, string mn, int reloads) {
            int[] pars = (from m in t.GetMethods() where m.Name == mn select m.GetParameters().Length).ToArray();
            int minArgs = pars[0];
            int maxArgs = pars[0];
            for (int i = 1; i < pars.Length; i++) {                
                if (pars[i] > maxArgs)                  
                    maxArgs = pars[i];
                if (pars[i] < minArgs)
                    minArgs = pars[i];
            }
            return (minArgs != maxArgs && reloads > 1) ? (minArgs + ".." + maxArgs) : minArgs.ToString();
        }               

        public static string printFields(Type t) {
            string s = "";
            foreach (var f in t.GetFields())
                s += f.Name + ", ";            
            return (s.Length == 0) ? "-" : s.Remove(s.Length - 2);
        }

        public static string printProperties(Type t) {
            string s = "";
            foreach (var f in t.GetProperties())
                s += f.Name + ", ";
            return (s.Length == 0) ? "-" : s.Remove(s.Length - 2);
        }

    }
    struct MyStruct { }
    class MyClass { }
}
