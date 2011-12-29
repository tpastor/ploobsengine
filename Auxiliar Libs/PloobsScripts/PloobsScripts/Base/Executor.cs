using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PloobsScripts
{
    public static class Executor
    {
        public static List<MethodInfo> GetExposedMethods(Assembly assembly, String typeName)
        {
            List<MethodInfo> infos = new List<MethodInfo>(); 
            Type type = assembly.GetType(typeName);
            foreach (var item in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                 object[] attribs = item.GetCustomAttributes(typeof(Expose), true);
                 if (attribs != null && attribs.Count() > 0)
                 {
                     infos.Add(item);
                 }            
            }
            return infos;
        }

        public static object CallExposedMethods(object obj, String methodName, object[] parameters, bool checkIfExposed = false)
        {
            if (checkIfExposed)
            {
                MethodInfo mi = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (mi.GetCustomAttributes(typeof(Expose), true).Count() > 0)
                {
                    return mi.Invoke(obj, parameters);
                }
                else
                {
                    throw new Exception("method not exposed");
                }
            }
            else
            {
                return obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Invoke(obj, parameters);
            }
        }

        public static  T BindTypeFromAssembly<T>(Assembly assembly, Type type) where T : class
        {
            return (T) assembly.CreateInstance(type.FullName);            
        }

        public static T BindTypeFromAssembly<T>(Assembly assembly, String typeName) where T : class
        {
            return (T)assembly.CreateInstance(typeName);
        }

        public static void Execute(Generator classCode, Assembly Assembly, String methodName, object[] param = null)
        {
            Type type = Assembly.GetType(classCode.TypeName);
            object obj = Assembly.CreateInstance(classCode.TypeName);
            MethodInfo mi = type.GetMethod(methodName);
            mi.Invoke(obj, param);          
        }

        public static void Execute(String typeName , Assembly Assembly, String methodName, object[] param = null)
        {
            Type type = Assembly.GetType(typeName);
            object obj = Assembly.CreateInstance(typeName);
            MethodInfo mi = type.GetMethod(methodName);
            mi.Invoke(obj, param);
        }

        public static void Execute(Generator classCode, Assembly Assembly, object[] constructorArgs, String methodName, object[] param = null)
        {
            Type type = Assembly.GetType(classCode.TypeName);
            object obj = Assembly.CreateInstance(classCode.TypeName, false, BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, null, constructorArgs, null, null);
            MethodInfo mi = type.GetMethod(methodName);
            mi.Invoke(obj, param);
        }

        public static  void ExecuteAssemblyInNewDomain(String DomainName, Assembly assembly)
        {
            AppDomainSetup ads = new AppDomainSetup();
            ads.ShadowCopyFiles = "true";
            AppDomain newDomain = AppDomain.CreateDomain(DomainName);
            newDomain.ExecuteAssembly(assembly.FullName);
        }
    }
}
