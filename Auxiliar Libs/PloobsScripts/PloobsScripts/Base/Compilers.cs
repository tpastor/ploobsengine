using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.CodeDom;

namespace PloobsScripts
{
    public static class Compilers
    {
        public static Assembly GenerateAssembly(out String errors, String[] references, params CodeCompileUnit[] GenerateClassCode)
        {
            CSharpCodeProvider csp = new CSharpCodeProvider();

            CompilerParameters cp = new CompilerParameters();
            foreach (var item in references)
            {
                cp.ReferencedAssemblies.Add(item);
            }

            cp.WarningLevel = 3;
            cp.IncludeDebugInformation = false;
            cp.CompilerOptions = "/target:library /optimize";
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            CompilerResults results = csp.CompileAssemblyFromDom(cp, GenerateClassCode);
            if (results.Errors.HasErrors)
            {
                string errorMessage = "";
                errorMessage = results.Errors.Count.ToString() + " Errors:";
                for (int x = 0; x < results.Errors.Count; x++)
                {
                    errorMessage = errorMessage + "\r\nLine: " +
                        results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;
                }
                errors = errorMessage;
            }
            else
            {
                errors = null;
            }
            return results.CompiledAssembly;
        }

        public static Assembly GenerateAssembly(Generator GenerateClassCode, out String errors)
        {
            CSharpCodeProvider csp = new CSharpCodeProvider();

            CompilerParameters cp = new CompilerParameters();
            foreach (var item in GenerateClassCode.References)
            {
                cp.ReferencedAssemblies.Add(item);
            }

            cp.WarningLevel = 3;
            cp.IncludeDebugInformation = false;
            cp.CompilerOptions = "/target:library /optimize";
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            CompilerResults results = csp.CompileAssemblyFromDom(cp,GenerateClassCode.CodeCompileUnit);
            if (results.Errors.HasErrors)
            {
                string errorMessage = "";
                errorMessage = results.Errors.Count.ToString() + " Errors:";
                for (int x = 0; x < results.Errors.Count; x++)
                {
                    errorMessage = errorMessage + "\r\nLine: " +
                        results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;
                }
                errors = errorMessage;
            }
            else
            {
                errors = null;
            }
            return results.CompiledAssembly;
        }
        
        public static Assembly GenerateAssembly(String classCode,ICollection<String> references , out String errors,String outputFilePath = null, bool inMemory = true)
        {
            CSharpCodeProvider csp = new CSharpCodeProvider();            

            CompilerParameters cp = new CompilerParameters();
            if (outputFilePath != null)
            {
                cp.OutputAssembly = Environment.CurrentDirectory + outputFilePath;
            }
            else
            {
                cp.OutputAssembly = Environment.CurrentDirectory + "\\TestClass.dll";
            }
            foreach (var item in references)
            {
                cp.ReferencedAssemblies.Add(item);    
            }            
            
            cp.WarningLevel = 3;
            cp.IncludeDebugInformation = false;            
            cp.CompilerOptions = "/target:library /optimize";
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = inMemory;
            CompilerResults results = csp.CompileAssemblyFromSource(cp, classCode);
            if (results.Errors.HasErrors)
            {
                string errorMessage = "";
                errorMessage = results.Errors.Count.ToString() + " Errors:";
                for (int x = 0; x < results.Errors.Count; x++)
                {
                    errorMessage = errorMessage + "\r\nLine: " +
                        results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;
                }
                errors = errorMessage;
            }
            else
            {
                errors = null;
            }
            if (errors != null)
            {
                return null;
            }
            else
            {
                return results.CompiledAssembly;
            }
        }

    }
}
