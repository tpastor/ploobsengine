using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Engine
{
    public static class PloobsDispatcher
    {
        private struct idata
        {
            public Delegate del;
            public object[] parameters;
        }

        private  static object lockobj = new object();
        private static List<idata> list = new List<idata>();
        public static void Dispatch(Delegate function, params object[] parameter)
        {
            idata idata = new idata();
            idata.del = function;
            idata.parameters = parameter;
            lock (lockobj)
            {                
                list.Add(idata);
            }
        }

        internal static void Update()
        {
            lock (lockobj)
            {
                foreach (var item in list)
                {
                    item.del.DynamicInvoke(item.parameters);
                }
                list.Clear();
            }
        }
    }
}
