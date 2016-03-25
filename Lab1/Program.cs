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

            while (true) {
                int choice = ShowMainMenu();
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
                        do {
                            Console.Write("Введите имя типа: ");
                            t = Console.ReadLine();
                        } while (!types.Keys.Contains(t));
                        ShowInfo(types[t]);                        
                        break;
                    default: break;
                }
            }                
        }

        public static void ShowInfo(Type t) {
            ShowTypeInfo(t);
        }

        public static int ShowMainMenu() {
            Console.WriteLine(
                @"Выберете необходимое действие:
                1 – Общая информация по типам
                2 – Выбрать из списка
                3 – Ввести имя типа
                4 – Параметры консоли
                0 - Выход из программы");
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

        public static void SelectType() {
            Console.WriteLine(
                @"Выберете тип из списка:
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
            while (true) {
                int choice = Int32.Parse(Console.ReadLine());
                switch (choice) {
                    case 0:                        
                        Console.Clear();
                        return;
                    case 1:
                        ShowTypeInfo(typeof(uint));
                        return;
                    case 2:
                        ShowTypeInfo(typeof(int));
                        return;
                    case 3:
                        ShowTypeInfo(typeof(long));
                        return;
                    case 4:
                        ShowTypeInfo(typeof(float));
                        return;
                    case 5:
                        ShowTypeInfo(typeof(double));
                        return;
                    case 6:
                        ShowTypeInfo(typeof(char));
                        return;
                    case 7:
                        ShowTypeInfo(typeof(string));
                        return;
                    case 8:
                        ShowTypeInfo(typeof(MyClass));
                        return;
                    case 9:
                        ShowTypeInfo(typeof(MyStruct));
                        return;
                    default: break;
                }
            }
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
            while (true) {
                char ch = Char.Parse(Console.ReadLine());
                switch (ch) {
                    case 'M':
                        break;
                    case '0':
                        Console.Clear();
                        return;
                    default: break;
                }
            }            
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
