using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Global Configurations of the app
    /// Not used yet
    /// </summary>
    public class InitConfiguration
    {
        private static IDictionary<String,String> keyConf = null;

        /// <summary>
        /// Pega uma configuracao do arquivo de configuracao.
        /// confs sao do tipo confname = value
        /// </summary>
        /// <param name="confName">confname</param>
        /// <returns>value</returns>
        public static String getConf(String confName)
        {
            if (keyConf == null)
            {
                keyConf = BundleHandler.getBundle(PrincipalConstants.DefaultInitConfFileName);
            }

            ///para nao soltar exception se nao encontrar a key
            string conf = null;
            keyConf.TryGetValue(confName, out conf);
            return conf;
                
        }


    }
}
