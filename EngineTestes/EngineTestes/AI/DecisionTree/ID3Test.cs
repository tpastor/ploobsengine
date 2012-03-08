#if WINDOWS
using System.Collections.Generic;
using PloobsEngine.IA;
using System.Data;

namespace EngineTestes.AI.DecisionTree
{
    public class ID3Test
    {
        static void TesteID3()
        {
            Attribute ceu = new Attribute("ceu", new string[] { "sol", "nublado", "chuva" });
            Attribute temperatura = new Attribute("temperatura", new string[] { "alta", "baixa", "suave" });
            Attribute humidade = new Attribute("humidade", new string[] { "alta", "normal" });
            Attribute vento = new Attribute("vento", new string[] { "sim", "nao" });

            Attribute[] attributes = new Attribute[] { ceu, temperatura, humidade, vento };

            DataTable samples = getDataTable();

            DecisionTreeID3 id3 = new DecisionTreeID3();
            TreeNode root = id3.mountTree(samples, "result", attributes);

            printNode(root, "");

        }

        static DataTable getDataTable()
        {
            DataTable result = new DataTable("samples");
            DataColumn column = result.Columns.Add("ceu");
            column.DataType = typeof(string);

            column = result.Columns.Add("temperatura");
            column.DataType = typeof(string);

            column = result.Columns.Add("humidade");
            column.DataType = typeof(string);

            column = result.Columns.Add("vento");
            column.DataType = typeof(string);

            column = result.Columns.Add("result");
            column.DataType = typeof(bool);

            result.Rows.Add(new object[] { "sol", "alta", "alta", "nao", false }); //D1 sol alta alta não N
            result.Rows.Add(new object[] { "sol", "alta", "alta", "sim", false }); //D2 sol alta alta sim N
            result.Rows.Add(new object[] { "nublado", "alta", "alta", "nao", true }); //D3 nebulado alta alta não P
            result.Rows.Add(new object[] { "chuva", "alta", "alta", "nao", true }); //D4 chuva alta alta não P
            result.Rows.Add(new object[] { "chuva", "baixa", "normal", "nao", true }); //D5 chuva baixa normal não P
            result.Rows.Add(new object[] { "chuva", "baixa", "normal", "sim", false }); //D6 chuva baixa normal sim N
            result.Rows.Add(new object[] { "nublado", "baixa", "normal", "sim", true }); //D7 nebulado baixa normal sim P
            result.Rows.Add(new object[] { "sol", "suave", "alta", "nao", false }); //D8 sol suave alta não N
            result.Rows.Add(new object[] { "sol", "baixa", "normal", "nao", true }); //D9 sol baixa normal não P
            result.Rows.Add(new object[] { "chuva", "suave", "normal", "nao", true }); //D10 chuva suave normal não P
            result.Rows.Add(new object[] { "sol", "suave", "normal", "nao", true }); //D11 sol suave normal sim P
            result.Rows.Add(new object[] { "nublado", "suave", "alta", "sim", true }); //D12 nebulado suave alta sim P
            result.Rows.Add(new object[] { "nublado", "alta", "normal", "nao", true }); //D13 nebulado alta normal não P
            result.Rows.Add(new object[] { "chuva", "suave", "alta", "sim", false }); //D14 chuva suave alta sim N

            return result;

        }

        public static void printNode(TreeNode root, string tabs)
        {
            System.Console.WriteLine(tabs + '|' + root.attribute + '|');

            if (root.attribute.values != null)
            {
                for (int i = 0; i < root.attribute.values.Length; i++)
                {
                    System.Console.WriteLine(tabs + "\t" + "<" + root.attribute.values[i] + ">");
                    TreeNode childNode = root.getChildByBranchName(root.attribute.values[i]);
                    printNode(childNode, "\t" + tabs);
                }
            }
        }


    }
}
#endif