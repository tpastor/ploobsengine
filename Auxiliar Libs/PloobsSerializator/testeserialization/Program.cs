using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsSerializator;

namespace testeserialization
{
    class Program
    {
        static void Main(string[] args)
        {
            teste intern = new testeserialization.teste()
            {
                fieldString = "testestring",
                propertyInt = 12
            };

            teste teste = new testeserialization.teste()
            {
                fieldString = "internal",
                propertyInt = 13,
                teste2 = intern
            };

            testeenum testeenum = new testeserialization.testeenum()
            {
                enumteste = enumteste.opt2
            };

            //SerializatorMachine.Serialize(intern, "teste.xml");
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(testeenum, "teste.xml", Encoding.Default);
            object ob = SerializatorMachine.Desserialize<testeenum>("teste.xml");

            tests t = new tests();
            t.teste1();
            t.teste2();
            t.teste3();
            t.teste4();
            t.teste5();
            t.teste6();
            t.teste7();
            t.teste8();
            t.teste9();

        }
    }

    public enum enumteste
    {
        opt1,opt2,opt3
    }

    public class testeenum
    {
        [PloobsSerialize]
        public enumteste[] arr = new enumteste[] { enumteste.opt1, enumteste.opt3};

        [PloobsSerialize]
        public int[] arr2 = new int[] { 1, 2, 3, 4, 5 };

        [PloobsSerialize]        
        public enumteste enumteste;

        [PloobsSerialize]        
        public float fteste = 10;

        [PloobsSerialize]        
        public long longtest = 10;

        [PloobsSerialize]
        public short shortest = 10;
        
    }

    internal class teste
    {
        public teste()
        {
            
        }

        [PloobsSerialize]        
        public string fieldString;

        [PloobsSerialize]
        public int propertyInt
        {
            get;
            set;
        }

        [PloobsSerialize]
        public teste teste2;
    }
        
    internal class testefdsf
    {
        public testefdsf()
        {
            testedsgfa = "dsfa";
        }

        public string testedsgfa;
                
        public int dasfa
        {
            get;
            set;
        }
    }

}
