using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PloobsScripts
{
    public class ScriptParsed
    {
        public ScriptParsed()
        {
            newCode = String.Empty;
            references = new List<string>();
            usingStatements = new List<string>();
            functions = String.Empty;
        }

        internal String newCode ;
        public String MethodCode
        {
            get
            {
                return newCode;
            }
        }

        internal List<String> references;
        public List<String> References
        {
            get
            {
                return references;
            }
        }



        internal  List<String> usingStatements;

        public List<String> UsingStatements
        {
            get
            {
                return usingStatements;
            }
        }

        internal  String functions ;

        public String AuxiliarFunctionsCode
        {
            get
            {
                return functions;
            }
        }
    }

    public static class Parser
    {
        public static ScriptParsed ParseScriptFile(String scriptPath)
        {
            String text = File.ReadAllText(scriptPath);
            return ParseScriptCode(text);           
        }

        public static ScriptParsed ParseScriptCode(String code)
        {
            ScriptParsed ScriptParsed = new ScriptParsed();
            bool insideFunction = false;
            int isusing = 0;
            int brackets = 0;
            foreach (var item2 in code.Split('\n'))
            {
                String iter = item2.Replace("\r", "");
                foreach (var item in iter.Split(' '))
                {

                    if (isusing == 1)
                    {
                        if (item.Contains(';'))
                        {
                            ScriptParsed.usingStatements.Add(item.Replace(";", ""));
                            isusing = 0;
                            continue;
                        }
                        else
                        {
                            ScriptParsed.usingStatements.Add(item);
                            isusing = 2;
                            continue;
                        }
                    }
                    if (isusing == 2)
                    {
                        isusing = 0;
                        continue;
                    }

                    if (item == "using")
                    {
                        isusing = 1;
                        continue;
                    }

                    if (item.ToUpperInvariant() == "FUNCTION:")
                    {
                        insideFunction = true;
                        brackets = -1;
                        continue;
                    }

                    if (insideFunction)
                    {
                        if (brackets == -1)
                        {
                            if (item == "{")
                            {
                                brackets = 0;
                            }
                        }

                        if (item == "{")
                        {
                            brackets++;
                        }

                        if (item == "}")
                        {
                            brackets--;
                        }

                        if (brackets == 0)
                        {
                            insideFunction = false;
                        }

                        ScriptParsed.functions += item + " ";
                    }
                    else
                    {
                        ScriptParsed.newCode += item + " ";
                    }
                }
                if (insideFunction)
                {
                    ScriptParsed.functions += "\n";
                }
                else
                {
                    ScriptParsed.newCode += "\n";
                }
            }
            return ScriptParsed;
        }
        
    }
}
