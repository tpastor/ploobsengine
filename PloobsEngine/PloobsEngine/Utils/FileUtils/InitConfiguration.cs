#if !WINRT
#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
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

            //para nao soltar exception se nao encontrar a key
            string conf = null;
            keyConf.TryGetValue(confName, out conf);
            return conf;
                
        }


    }
}
#endif