using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsSerializator;

namespace testeserialization
{
    public class tests
    {
        public void teste1()
        {
            testtypes testtypes = new testeserialization.testtypes();
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(testtypes, "teste1.xml", Encoding.Default);
            testtypes ob = SerializatorMachine.Desserialize<testtypes>("teste1.xml") ;
            System.Diagnostics.Debug.Assert(ob.str == "t1");
            System.Diagnostics.Debug.Assert(ob.fl == 1);
            System.Diagnostics.Debug.Assert(ob.it == 1);
            System.Diagnostics.Debug.Assert(ob.sh == 1);
            System.Diagnostics.Debug.Assert(ob.ln == 1);
            System.Diagnostics.Debug.Assert(ob.dl == 1);
            System.Diagnostics.Debug.Assert(ob.ch == 'a');
        }

        public void teste2()
        {
            teststruct tt = new teststruct();
            tt.t = 10;
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(tt, "teste2.xml", Encoding.Default);
            teststruct ob = (teststruct) SerializatorMachine.Desserialize("teste2.xml");
            System.Diagnostics.Debug.Assert(ob.t== 10);
        }

        public void teste3()
        {
            t1 t1 = new t1()
            {
                tt1 = "t1",
                t2 = new t2()
                {
                    tt2 = "t2",
                    t3 = new t3()
                    {
                        tt3 = "t3"
                    }
                }
            };
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(t1, "teste3.xml", Encoding.Default);
            t1 ob = SerializatorMachine.Desserialize<t1>("teste3.xml");
            System.Diagnostics.Debug.Assert(ob.tt1 == "t1");
            System.Diagnostics.Debug.Assert(ob.t2.tt2 == "t2");
            System.Diagnostics.Debug.Assert(ob.t2.t3.tt3 == "t3");
        }

        public void teste4()
        {
            partialserialize s = new partialserialize();
            s.ser = 10;
            s.noser = 20;
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(s, "teste4.xml", Encoding.Default);
            partialserialize ob = SerializatorMachine.Desserialize<partialserialize>("teste4.xml");
            System.Diagnostics.Debug.Assert(ob.noser != 20);
            System.Diagnostics.Debug.Assert(ob.noser == 0);
            System.Diagnostics.Debug.Assert(ob.ser == 10);
        }

        public void teste5()
        {
            array array = new array();
            array.ar = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; 
            array.str = new string[] { "asfadsfa", "a" };
            array.en = new ens[] { ens.a, ens.c };
            array.ss1 = new strc[] { new strc() { a = 1 }, new strc() { a = 5 } };
            array.ss = new strc2[] { new strc2() { a = 1 }, new strc2() { a = 5 } };
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(array, "teste5.xml", Encoding.Default);
            array ob = SerializatorMachine.Desserialize<array>("teste5.xml");
            System.Diagnostics.Debug.Assert(ob.ar.Count() == 9);
            System.Diagnostics.Debug.Assert(ob.ar[1] == 2);
            System.Diagnostics.Debug.Assert(ob.str[1] == "a");
            System.Diagnostics.Debug.Assert(ob.en[1] == ens.c);
            System.Diagnostics.Debug.Assert(ob.ss1[1].a == 5);
            System.Diagnostics.Debug.Assert(ob.ss[1].a == 5);

        }

        public void teste6()
        {
            extend ex = new extend();
            ex.a = "teste";
            ex.strc2 = new strc2() { a = 4 };
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(ex, "teste6.xml", Encoding.Default);
            extend ob = SerializatorMachine.Desserialize<extend>("teste6.xml");
            System.Diagnostics.Debug.Assert(ob.a== "teste");
            System.Diagnostics.Debug.Assert(ob.strc2.a == 4);
        }

        public void teste7()
        {
            testeextend2 testeextend2 = new testeextend2();
            testeextend2.t = 4;
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(testeextend2, "teste7.xml", Encoding.Default);
            testeextend2 ob = SerializatorMachine.Desserialize<testeextend2>("teste7.xml");
            System.Diagnostics.Debug.Assert(ob.t2 == 8);
        }

        public void teste8()
        {
            {
                List<String> l = new List<string>();
                l.Add("thiago");
                l.Add("pastor");
                SerializatorMachine SerializatorMachine = new SerializatorMachine();
                SerializatorMachine.Serialize(l, "teste8.xml", Encoding.Default);
                List<string> ob = SerializatorMachine.Desserialize<List<string>>("teste8.xml");
                System.Diagnostics.Debug.Assert(ob[0] == "thiago");
                System.Diagnostics.Debug.Assert(ob[1] == "pastor");
            }
            {
                List<int> l = new List<int>();
                l.Add(2);
                l.Add(3);
                SerializatorMachine SerializatorMachine = new SerializatorMachine();
                SerializatorMachine.Serialize(l, "teste8.xml", Encoding.Default);
                List<int> ob = SerializatorMachine.Desserialize<List<int>>("teste8.xml");
                System.Diagnostics.Debug.Assert(ob[0] == 2);
                System.Diagnostics.Debug.Assert(ob[1] == 3);
            }

            {
                List<enumteste> l = new List<enumteste>();
                l.Add(enumteste.opt1);
                l.Add(enumteste.opt3);
                SerializatorMachine SerializatorMachine = new SerializatorMachine();
                SerializatorMachine.Serialize(l, "teste8.xml", Encoding.Default);
                List<enumteste> ob = SerializatorMachine.Desserialize<List<enumteste>>("teste8.xml");
                System.Diagnostics.Debug.Assert(ob[0] == enumteste.opt1);
                System.Diagnostics.Debug.Assert(ob[1] == enumteste.opt3);
            }
        }

        public void teste9()
        {
            Dictionary<String,int> l = new Dictionary<string,int>();
            l.Add("abc",1);
            l.Add("abcdef", 2);
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(l, "teste9.xml", Encoding.Default);
            Dictionary<String, int> ob = SerializatorMachine.Desserialize<Dictionary<String, int>>("teste9.xml");
            System.Diagnostics.Debug.Assert(ob["abc"] == 1);
            System.Diagnostics.Debug.Assert(ob["abcdef"] == 2);
        }

    }

    public class testeextend2 : ICustomDeserializable
    {
        [PloobsSerialize]
        public int t;

        public int t2;

        public void Deserialize(DeSerializerProxy DeSerializerProxy)
        {
            System.Diagnostics.Debug.Assert(t == 4);
            System.Diagnostics.Debug.Assert(t2 == 0);
            t2 = t * 2;
        }
    }

    public class extend : ICustomSerializable
    {        
        public object Deserialize(DeSerializerProxy DeSerializerProxy)
        {
            extend extend = new extend();
            extend.a = DeSerializerProxy.DeSerialize("teste") as string;
            extend.strc2 = DeSerializerProxy.DeSerialize("teste2") as strc2;
            return extend;            
        }

        public string a;
        public strc2 strc2;

        public void Serialize(SerializerProxy SerializerProxy)
        {
            SerializerProxy.Serialize("teste", a);
            SerializerProxy.Serialize("teste2", strc2);
        }
    }

    public class array
    {
        [PloobsSerialize]
        public strc2[] ss;

        [PloobsSerialize]
        public int[] ar;

        [PloobsSerialize]
        public String[] str ;

        [PloobsSerialize]
        public ens[] en;
               

        [PloobsSerialize]
        public strc[] ss1;        
    }

    [PloobsSerializeAll]
    public class strc2
    {
        public int a;
    }

    [PloobsSerializeAll]
    public struct strc
    {
        public int a;
    }


    public enum ens
    {
        a,b,c
    }


    public class partialserialize
    {
        [PloobsSerialize]
        public int ser;
        public int noser;
    }

    [PloobsSerializeAll]
    public class t1
    {
        public string tt1;
        public t2 t2;
    }
    [PloobsSerializeAll]
    public class t2
    {
        public string tt2;
        public t3 t3;
    }
    [PloobsSerializeAll]
    public class t3
    {
        public string tt3;
    }


    [PloobsSerializeAll]
    public struct teststruct
    {
        public int t;        
    }

    [PloobsSerializeAll]
    public class testtypes
    {
        public testtypes()
        {
            str = "t1";
            fl = 1;
            it = 1;
            sh = 1;
            ln = 1;
            dl = 1;
            ch = 'a';

            pstr = "t2";
            pfl = 2;
            pit = 2;
            psh = 2;
            pln = 2;
            pdl = 2;
            pch = 'b';


            ppstr = "t3";
            ppfl = 3;
            ppit = 3;
            ppsh = 3;
            ppln = 3;
            ppdl = 3;
            ppch = 'c';

        }

        public String str;
        public float fl;
        public int it;
        public short sh;
        public long ln;
        public double dl;
        public char ch;


        private String pstr;
        private float pfl;
        private int pit;
        private short psh;
        private long pln;
        private double pdl;
        private char pch;


        protected String ppstr;
        protected float ppfl;
        protected int ppit;
        protected short ppsh;
        protected long ppln;
        protected double ppdl;
        protected char ppch;
    }
}
