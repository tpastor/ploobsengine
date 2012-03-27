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

namespace PloobsEngine

{
    /// <summary>
    /// Here we have all the importants Engine Constants
    /// </summary>
    public class PrincipalConstants
    {
        //public const String DefaultFactoriesListFileName = "defaultFactoriesListFileName";

        /// <summary>
        /// Where to search for the init conf file
        /// This funcionality is not on yet
        /// </summary>
        public const String DefaultInitConfFileName = "Content/confs\\initconf.txt";
        ///// <summary>
        ///// Script Message sender name
        ///// </summary>
        //public const String ScriptMessage = "Message";
        /// <summary>
        /// Id used when an event send a message
        /// </summary>
        public const int EventSenderId = -5;
        /// <summary>
        /// Invalid id constant
        /// </summary>
        public const int InvalidId = -10;
        /// <summary>
        /// The messegeDeliver component id
        /// </summary>
        public const int MessageDeliverId = -50;
        /// <summary>
        /// Message recieved when the reciever is not found
        /// </summary>
        public const string RecieverNotFound= "Not Found Message";
        /// <summary>
        /// the combined image alias (Post processing)
        /// </summary>
        public const String CombinedImage = "FinalImage";
        /// <summary>
        /// The current image alias (Post Processing)
        /// </summary>
        public const String CurrentImage= "CurrentImage";
        /// <summary>
        /// extra render target name
        /// </summary>
        public const String extra1RT = "LIGHTOCCLUSIONRT";
        /// <summary>
        /// Color (diffuse) render target name
        /// </summary>
        public const String colorRT = "COLORRT";
        /// <summary>
        /// Normal render target name
        /// </summary>
        public const String normalRt = "NORMALRT";
        /// <summary>
        /// Light Render target name
        /// </summary>
        public const String lightRt = "LIGHTRT";
        /// <summary>
        /// Depth Render target name
        /// </summary>
        public const String DephRT = "DEPHRT";
        /// <summary>
        /// Separator character used when parsing files
        /// </summary>
        public const string SEPARATOR_CHARACTER = "%";

        /// <summary>
        /// Sampler Register for Diffuse Texture
        /// </summary>
        public const int DiffuseRegister = 0;
        /// <summary>
        /// Sampler Register for Normal Texture
        /// </summary>
        public const int NormalRegister = 1;
        /// <summary>
        /// Sampler Register for Specular Texture
        /// </summary>
        public const int SpecularRegister = 2;
        /// <summary>
        /// Sampler Register for Glow Texture
        /// </summary>
        public const int GlowRegister = 3;
        /// <summary>
        /// Sampler Register for Environment Texture
        /// </summary>
        public const int EnvironmentRegister = 4;
        /// <summary>
        /// Sampler Register for Heigh Texture
        /// </summary>
        public const int HeighRegister = 5;
    }
}
