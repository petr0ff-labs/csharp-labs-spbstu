using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class TypeInfo
    {
        public string maxMethodName;
        public string maxMethodArgs;
        public int interfacesCount;
        public int valueTypesCount;
        public int refTypesCount;
        public string maxMethodsType;

        public TypeInfo(List<Type> l)
        {
            Type tMaxMethods = l[0];
            int argsCount = 0;
            maxMethodArgs = "";
            maxMethodName = "";

            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].IsInterface)
                    interfacesCount++;
                if (!l[i].IsClass)
                    valueTypesCount++;
                if (l[i].IsClass)
                    refTypesCount++;
                if (l[i].IsInterface)
                    interfacesCount++;
                if (l[i].GetMethods().Length > tMaxMethods.GetMethods().Length)
                    tMaxMethods = l[i];
                for (int j = 0; j < l[i].GetMethods().Length; j++)
                {
                    if (l[i].GetMethods()[j].GetParameters().Length > argsCount)
                    {
                        argsCount = l[i].GetMethods()[j].GetParameters().Length;
                        maxMethodArgs = l[i].GetMethods()[j].Name;
                    }
                    if (l[i].GetMethods()[j].Name.Length > maxMethodName.Length)
                        maxMethodName = l[i].GetMethods()[j].Name;
                }
            }
            maxMethodsType = tMaxMethods.Name + "(" + tMaxMethods.GetMethods().Length + ")";
            maxMethodArgs = maxMethodArgs + " (" + argsCount + ")";
            maxMethodName = maxMethodName + " (" + maxMethodName.Length + ")";
        }        
    }
}
