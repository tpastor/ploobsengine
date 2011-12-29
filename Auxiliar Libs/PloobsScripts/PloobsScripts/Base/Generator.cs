using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;

namespace PloobsScripts
{
    public class Generator
    {
        public String[] References
        {
            get;
            private set;
        }

        String CodeNamespace;
        String[] imports;
        public String TypeName
        {
            private set;
            get;
        }

        public CodeCompileUnit CodeCompileUnit
        {
            get;
            private set;
        }

        CodeTypeDeclaration derived;

        public Generator(String[] references, String[] imports, String CodeNamespace)
        {
            this.References = references;
            this.CodeNamespace = CodeNamespace;
            this.imports = imports;
        }

        public void GenerateField(String name,Type type ,MemberAttributes MemberAttributes = MemberAttributes.Public)
        {
            if (CodeCompileUnit == null)
                throw new Exception("Call Generate Class Before");

            CodeMemberField CodeMemberField = new CodeMemberField();
            CodeMemberField.Attributes = MemberAttributes;
            CodeMemberField.Type = new CodeTypeReference(type);
            CodeMemberField.Name = name;
            derived.Members.Add(CodeMemberField);
        }

        public void GenerateMethod(String MethodName,String body, Type returnType, MemberAttributes MemberAttributes = MemberAttributes.Public, params KeyValuePair<Type,String>[] parameters)
        {
            if (CodeCompileUnit == null)
                throw new Exception("Call Generate Class Before");

            CodeMemberMethod derivedMethod = new CodeMemberMethod();
            derivedMethod.Attributes = MemberAttributes;            
            derivedMethod.Name = MethodName;
            derivedMethod.ReturnType = new CodeTypeReference(returnType);

            foreach (var item in parameters)
	        {
		        CodeParameterDeclarationExpression CodeParameterDeclarationExpression = new CodeParameterDeclarationExpression(item.Key,item.Value);
                derivedMethod.Parameters.Add(CodeParameterDeclarationExpression);                
	        }

            CodeSnippetStatement code = new CodeSnippetStatement(body);
            derivedMethod.Statements.Add(code);
            derived.Members.Add(derivedMethod);

        }

        public void GenerateClass(String className, String baseClass = null)
        {           

            CodeCompileUnit = new CodeCompileUnit();

            foreach (var item in References)
            {
                CodeCompileUnit.ReferencedAssemblies.Add(item);
            }

            CodeNamespace customEntityRoot = new CodeNamespace(CodeNamespace);//Create a namespace
            CodeCompileUnit.Namespaces.Add(customEntityRoot);


            foreach (var item in imports)
            {
                customEntityRoot.Imports.Add(new CodeNamespaceImport(item));
            }

            derived = new CodeTypeDeclaration(className);

            customEntityRoot.Types.Add(derived);

            CodeConstructor derivedClassConstructor = new CodeConstructor();
            derivedClassConstructor.Attributes = MemberAttributes.Public;

            derived.Members.Add(derivedClassConstructor);

            if(baseClass!= null)
                derived.BaseTypes.Add(new CodeTypeReference(baseClass));

            TypeName = CodeNamespace + "." + className;
        }

        public void GenerateConstructor(String body, params KeyValuePair<Type, String>[] parameters)
        {
            if (CodeCompileUnit == null)
                throw new Exception("Call Generate Class Before");

            CodeConstructor derivedClassConstructor = new CodeConstructor();
            derivedClassConstructor.Attributes = MemberAttributes.Public;            

            foreach (var item in parameters)
            {
                CodeParameterDeclarationExpression CodeParameterDeclarationExpression = new CodeParameterDeclarationExpression(item.Key, item.Value);
                derivedClassConstructor.Parameters.Add(CodeParameterDeclarationExpression);
            }

            CodeSnippetStatement code = new CodeSnippetStatement(body);
            derivedClassConstructor.Statements.Add(code);

            derived.Members.Add(derivedClassConstructor);            
        }


        public String GetCode()
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeGenerator codeGenerator = codeProvider.CreateGenerator();

            StringBuilder generatedCode = new StringBuilder();
            StringWriter codeWriter = new StringWriter(generatedCode);
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            codeGenerator.GenerateCodeFromCompileUnit(CodeCompileUnit, codeWriter, options);            
            return generatedCode.ToString();            

        }

        public static String GenerateFullClass(String[] references, String[] imports, String CodeNamespace,String className, string methodBody, String method = "Method")
        {
                CodeCompileUnit unit = new CodeCompileUnit();

                if (references != null)
                {
                    foreach (var item in references)
                    {
                        unit.ReferencedAssemblies.Add(item);
                    }
                }

                CodeNamespace customEntityRoot = new CodeNamespace(CodeNamespace);//Create a namespace
                unit.Namespaces.Add(customEntityRoot);

                if (imports != null)
                {
                    foreach (var item in imports)
                    {
                        customEntityRoot.Imports.Add(new CodeNamespaceImport(item));
                    }
                }

                CodeTypeDeclaration derived = new CodeTypeDeclaration(className);

                customEntityRoot.Types.Add(derived);

                CodeConstructor derivedClassConstructor = new CodeConstructor();
                derivedClassConstructor.Attributes = MemberAttributes.Public;

                derived.Members.Add(derivedClassConstructor);

                
                CodeMemberMethod derivedMethod = new CodeMemberMethod();
                derivedMethod.Attributes = MemberAttributes.Public ; 
                derivedMethod.Name = method;
                derivedMethod.ReturnType = new CodeTypeReference(typeof(void));

                CodeSnippetStatement code = new CodeSnippetStatement(methodBody);
                derivedMethod.Statements.Add(code);
                derived.Members.Add(derivedMethod);

                CSharpCodeProvider codeProvider = new CSharpCodeProvider();
                ICodeGenerator codeGenerator = codeProvider.CreateGenerator();
                
                StringBuilder generatedCode = new StringBuilder();
                StringWriter codeWriter = new StringWriter(generatedCode);
                CodeGeneratorOptions options = new CodeGeneratorOptions();
                options.BracingStyle = "C"; 
                codeGenerator.GenerateCodeFromCompileUnit(unit, codeWriter, options);
                return generatedCode.ToString();            
        }

    }
}
