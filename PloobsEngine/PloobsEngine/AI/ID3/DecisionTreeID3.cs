#if WINDOWS
using System;
using System.Collections;
using System.Data;

//AUTHOR: Roosevelt dos Santos Júnior

namespace PloobsEngine.IA
{
    /// <summary>
    /// Classe que representa um atributo utilizado na classe de decisão
    /// </summary>
    public class Attribute
    {
        ArrayList mValues;
        string mName;
        object mLabel;

        /// <summary>
        /// Inicializa uma nova instância de uma classe Atribute
        /// </summary>
        /// <param name="name">Indica o nome do atributo</param>
        /// <param name="values">Indica os valores possíveis para o atributo</param>
        public Attribute(string name, string[] values)
        {
            mName = name;
            mValues = new ArrayList(values);
            mValues.Sort();
        }

        public Attribute(object Label)
        {
            mLabel = Label;
            mName = string.Empty;
            mValues = null;
        }

        /// <summary>
        /// Indica o nome do atributo
        /// </summary>
        public string AttributeName
        {
            get
            {
                return mName;
            }
        }

        /// <summary>
        /// Retorna um array com os valores do atributo
        /// </summary>
        public string[] values
        {
            get
            {
                if (mValues != null)
                    return (string[])mValues.ToArray(typeof(string));
                else
                    return null;
            }
        }

        /// <summary>
        /// Indica se um valor é permitido para este atributo
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool isValidValue(string value)
        {
            return indexValue(value) >= 0;
        }

        /// <summary>
        /// Retorna o índice de um valor
        /// </summary>
        /// <param name="value">Valor a ser retornado</param>
        /// <returns>O valor do índice na qual a posição do valor se encontra</returns>
        public int indexValue(string value)
        {
            if (mValues != null)
                return mValues.BinarySearch(value);
            else
                return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (mName != string.Empty)
            {
                return mName;
            }
            else
            {
                return mLabel.ToString();
            }
        }
    }

    /// <summary>
    /// Classe que representará a arvore de decisão montada;
    /// </summary>
    public class TreeNode
    {
        private ArrayList mChilds = null;
        private Attribute mAttribute;

        /// <summary>
        /// Inicializa uma nova instância de TreeNode
        /// </summary>
        /// <param name="attribute">Atributo ao qual o node está ligado</param>
        public TreeNode(Attribute attribute)
        {
            if (attribute.values != null)
            {
                mChilds = new ArrayList(attribute.values.Length);
                for (int i = 0; i < attribute.values.Length; i++)
                    mChilds.Add(null);
            }
            else
            {
                mChilds = new ArrayList(1);
                mChilds.Add(null);
            }
            mAttribute = attribute;
        }

        /// <summary>
        /// Adiciona um TreeNode filho a este treenode no galho de nome indicicado pelo ValueName
        /// </summary>
        /// <param name="treeNode">TreeNode filho a ser adicionado</param>
        /// <param name="ValueName">Nome do galho onde o treeNode é criado</param>
        public void AddTreeNode(TreeNode treeNode, string ValueName)
        {
            int index = mAttribute.indexValue(ValueName);
            mChilds[index] = treeNode;
        }

        /// <summary>
        /// Retorna o nro total de filhos do nó
        /// </summary>
        public int totalChilds
        {
            get
            {
                return mChilds.Count;
            }
        }

        /// <summary>
        /// Retorna o nó filho de um nó
        /// </summary>
        /// <param name="index">Indice do nó filho</param>
        /// <returns>Um objeto da classe TreeNode representando o nó</returns>
        public TreeNode getChild(int index)
        {
            return (TreeNode)mChilds[index];
        }

        /// <summary>
        /// Atributo que está conectado ao Nó
        /// </summary>
        public Attribute attribute
        {
            get
            {
                return mAttribute;
            }
        }

        /// <summary>
        /// Retorna o filho de um nó pelo nome do galho que leva até ele
        /// </summary>
        /// <param name="branchName">Nome do galho</param>
        /// <returns>O nó</returns>
        public TreeNode getChildByBranchName(string branchName)
        {
            int index = mAttribute.indexValue(branchName);
            return (TreeNode)mChilds[index];
        }
    }

    /// <summary>
    /// Classe que implementa uma árvore de Decisão usando o algoritmo ID3
    /// </summary>
    public class DecisionTreeID3
    {
        private DataTable mSamples;
        private int mTotalPositives = 0;
        private int mTotal = 0;
        private string mTargetAttribute = "result";
        private double mEntropySet = 0.0;

        /// <summary>
        /// Retorna o total de amostras positivas em uma tabela de amostras
        /// </summary>
        /// <param name="samples">DataTable com as amostras</param>
        /// <returns>O nro total de amostras positivas</returns>
        private int countTotalPositives(DataTable samples)
        {
            int result = 0;

            foreach (DataRow aRow in samples.Rows)
            {
                if ((bool)aRow[mTargetAttribute] == true)
                    result++;
            }

            return result;
        }

        /// <summary>
        /// Calcula a entropia dada a seguinte fórmula
        /// -p+log2p+ - p-log2p-
        /// 
        /// onde: p+ é a proporção de valores positivos
        ///		  p- é a proporção de valores negativos
        /// </summary>
        /// <param name="positives">Quantidade de valores positivos</param>
        /// <param name="negatives">Quantidade de valores negativos</param>
        /// <returns>Retorna o valor da Entropia</returns>
        private double calcEntropy(int positives, int negatives)
        {
            int total = positives + negatives;
            double ratioPositive = (double)positives / total;
            double ratioNegative = (double)negatives / total;

            if (ratioPositive != 0)
                ratioPositive = -(ratioPositive) * System.Math.Log(ratioPositive, 2);
            if (ratioNegative != 0)
                ratioNegative = -(ratioNegative) * System.Math.Log(ratioNegative, 2);

            double result = ratioPositive + ratioNegative;

            return result;
        }

        /// <summary>
        /// Varre tabela de amostras verificando um atributo e se o resultado é positivo ou negativo
        /// </summary>
        /// <param name="samples">DataTable com as amostras</param>
        /// <param name="attribute">Atributo a ser pesquisado</param>
        /// <param name="value">valor permitido para o atributo</param>
        /// <param name="positives">Conterá o nro de todos os atributos com o valor determinado com resultado positivo</param>
        /// <param name="negatives">Conterá o nro de todos os atributos com o valor determinado com resultado negativo</param>
        private void getValuesToAttribute(DataTable samples, Attribute attribute, string value, out int positives, out int negatives)
        {
            positives = 0;
            negatives = 0;

            foreach (DataRow aRow in samples.Rows)
            {
                if (((string)aRow[attribute.AttributeName] == value))
                    if ((bool)aRow[mTargetAttribute] == true)
                        positives++;
                    else
                        negatives++;
            }
        }

        /// <summary>
        /// Calcula o ganho de um atributo
        /// </summary>
        /// <param name="samples">The samples.</param>
        /// <param name="attribute">Atributo a ser calculado</param>
        /// <returns>
        /// O ganho do atributo
        /// </returns>
        private double gain(DataTable samples, Attribute attribute)
        {
            string[] values = attribute.values;
            double sum = 0.0;

            for (int i = 0; i < values.Length; i++)
            {
                int positives, negatives;

                positives = negatives = 0;

                getValuesToAttribute(samples, attribute, values[i], out positives, out negatives);

                double entropy = calcEntropy(positives, negatives);
                sum += -(double)(positives + negatives) / mTotal * entropy;
            }
            return mEntropySet + sum;
        }

        /// <summary>
        /// Retorna o melhor atributo.
        /// </summary>
        /// <param name="samples">The samples.</param>
        /// <param name="attributes">Um vetor com os atributos</param>
        /// <returns>
        /// Retorna o que tiver maior ganho
        /// </returns>
        private Attribute getBestAttribute(DataTable samples, Attribute[] attributes)
        {
            double maxGain = 0.0;
            Attribute result = null;

            foreach (Attribute attribute in attributes)
            {
                double aux = gain(samples, attribute);
                if (aux > maxGain)
                {
                    maxGain = aux;
                    result = attribute;
                }
            }
            return result;
        }

        /// <summary>
        /// Retorna true caso todos os exemplos da amostragem são positivos
        /// </summary>
        /// <param name="samples">DataTable com as amostras</param>
        /// <param name="targetAttribute">Atributo (coluna) da tabela a qual será verificado</param>
        /// <returns>True caso todos os exemplos da amostragem são positivos</returns>
        private bool allSamplesPositives(DataTable samples, string targetAttribute)
        {
            foreach (DataRow row in samples.Rows)
            {
                if ((bool)row[targetAttribute] == false)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Retorna true caso todos os exemplos da amostragem são negativos
        /// </summary>
        /// <param name="samples">DataTable com as amostras</param>
        /// <param name="targetAttribute">Atributo (coluna) da tabela a qual será verificado</param>
        /// <returns>True caso todos os exemplos da amostragem são negativos</returns>
        private bool allSamplesNegatives(DataTable samples, string targetAttribute)
        {
            foreach (DataRow row in samples.Rows)
            {
                if ((bool)row[targetAttribute] == true)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Retorna uma lista com todos os valores distintos de uma tabela de amostragem
        /// </summary>
        /// <param name="samples">DataTable com as amostras</param>
        /// <param name="targetAttribute">Atributo (coluna) da tabela a qual será verificado</param>
        /// <returns>Um ArrayList com os valores distintos</returns>
        private ArrayList getDistinctValues(DataTable samples, string targetAttribute)
        {
            ArrayList distinctValues = new ArrayList(samples.Rows.Count);

            foreach (DataRow row in samples.Rows)
            {
                if (distinctValues.IndexOf(row[targetAttribute]) == -1)
                    distinctValues.Add(row[targetAttribute]);
            }

            return distinctValues;
        }

        /// <summary>
        /// Retorna o valor mais comum dentro de uma amostragem
        /// </summary>
        /// <param name="samples">DataTable com as amostras</param>
        /// <param name="targetAttribute">Atributo (coluna) da tabela a qual será verificado</param>
        /// <returns>Retorna o objeto com maior incidência dentro da tabela de amostras</returns>
        private object getMostCommonValue(DataTable samples, string targetAttribute)
        {
            ArrayList distinctValues = getDistinctValues(samples, targetAttribute);
            int[] count = new int[distinctValues.Count];

            foreach (DataRow row in samples.Rows)
            {
                int index = distinctValues.IndexOf(row[targetAttribute]);
                count[index]++;
            }

            int MaxIndex = 0;
            int MaxCount = 0;

            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] > MaxCount)
                {
                    MaxCount = count[i];
                    MaxIndex = i;
                }
            }

            return distinctValues[MaxIndex];
        }

        /// <summary>
        /// Monta uma árvore de decisão baseado nas amostragens apresentadas
        /// </summary>
        /// <param name="samples">Tabela com as amostragens que serão apresentadas para a montagem da árvore</param>
        /// <param name="targetAttribute">Nome da coluna da tabela que possue o valor true ou false para 
        /// validar ou não uma amostragem</param>
        /// <returns>A raiz da árvore de decisão montada</returns>
        private TreeNode internalMountTree(DataTable samples, string targetAttribute, Attribute[] attributes)
        {
            if (allSamplesPositives(samples, targetAttribute) == true)
                return new TreeNode(new Attribute(true));

            if (allSamplesNegatives(samples, targetAttribute) == true)
                return new TreeNode(new Attribute(false));

            if (attributes.Length == 0)
                return new TreeNode(new Attribute(getMostCommonValue(samples, targetAttribute)));

            mTotal = samples.Rows.Count;
            mTargetAttribute = targetAttribute;
            mTotalPositives = countTotalPositives(samples);

            mEntropySet = calcEntropy(mTotalPositives, mTotal - mTotalPositives);

            Attribute bestAttribute = getBestAttribute(samples, attributes);

            TreeNode root = new TreeNode(bestAttribute);

            DataTable aSample = samples.Clone();

            foreach (string value in bestAttribute.values)
            {
                // Seleciona todas os elementos com o valor deste atributo				
                aSample.Rows.Clear();

                DataRow[] rows = samples.Select(bestAttribute.AttributeName + " = " + "'" + value + "'");

                foreach (DataRow row in rows)
                {
                    aSample.Rows.Add(row.ItemArray);
                }
                // Seleciona todas os elementos com o valor deste atributo				

                // Cria uma nova lista de atributos menos o atributo corrente que é o melhor atributo				
                ArrayList aAttributes = new ArrayList(attributes.Length - 1);
                for (int i = 0; i < attributes.Length; i++)
                {
                    if (attributes[i].AttributeName != bestAttribute.AttributeName)
                        aAttributes.Add(attributes[i]);
                }
                // Cria uma nova lista de atributos menos o atributo corrente que é o melhor atributo

                if (aSample.Rows.Count == 0)
                {
                    return new TreeNode(new Attribute(getMostCommonValue(aSample, targetAttribute)));
                }
                else
                {
                    DecisionTreeID3 dc3 = new DecisionTreeID3();
                    TreeNode ChildNode = dc3.mountTree(aSample, targetAttribute, (Attribute[])aAttributes.ToArray(typeof(Attribute)));
                    root.AddTreeNode(ChildNode, value);
                }
            }

            return root;
        }


        /// <summary>
        /// Monta uma árvore de decisão baseado nas amostragens apresentadas
        /// </summary>
        /// <param name="samples">Tabela com as amostragens que serão apresentadas para a montagem da árvore</param>
        /// <param name="targetAttribute">Nome da coluna da tabela que possue o valor true ou false para
        /// validar ou não uma amostragem</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns>
        /// A raiz da árvore de decisão montada
        /// </returns>
        public TreeNode mountTree(DataTable samples, string targetAttribute, Attribute[] attributes)
        {
            mSamples = samples;
            return internalMountTree(mSamples, targetAttribute, attributes);
        }
    }

}
#endif