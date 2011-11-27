////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Importers                                        //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: CursorImporter.cs                            //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 15/02/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Cursor file importer.                                     //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System;
////////////////////////////////////////////////////////////////////////////

#endregion


namespace TomShane.Neoforce.Importers
{


	#region //// Importer //////////

    internal static class AssemblyHelper
    {
        private static readonly string windowsPublicKeyTokens = "0c21691816f8c6d0";
        //private static readonly string windowsPublicKeyTokens = "7ebcd2ab87b5cecb";
        //private static readonly string xboxPublicKeyTokens = "121fbd51268a0405";

        private static readonly string[] assemblySplitter = { ", " };

        internal static string GetRuntimeReader(Type type, TargetPlatform targetPlatform)
        {
            // Type full name
            string typeFullName = type.FullName;

            // Assembly name tokenized
            string fullAssemblyName = type.Assembly.FullName;
            string[] assemblyTokens = fullAssemblyName.Split(assemblySplitter, StringSplitOptions.None);

            return
                //typeFullName + ", " + assemblyTokens[0] + ", " + assemblyTokens[1] + ", " +
                //    assemblyTokens[2] + ", " + GetAssemblyPublicKey(targetPlatform);
                typeFullName + ", " + "PloobsEngineDebug" + ", " + assemblyTokens[1] + ", " +
                    assemblyTokens[2] + ", " + GetAssemblyPublicKey(targetPlatform);

        }

        internal static string GetAssemblyPublicKey(TargetPlatform targetPlatform)
        {
            string publicKey = "PublicKeyToken=";

            publicKey += windowsPublicKeyTokens;

            //switch (targetPlatform)
            //{
            //    case TargetPlatform.Windows:
            //        publicKey += windowsPublicKeyTokens;
            //        break;

            //    case TargetPlatform.Xbox360:
            //        //publicKey += xboxPublicKeyTokens;
            //        throw new Exception("XBOX NOT SUPPORTED");
            //        break;

            //    default:
            //        throw new ArgumentException("targetPlatform");
            //}

            return publicKey;
        }
    }


	public class CursorFile
	{
		public byte[] Data = null;
	}

	////////////////////////////////////////////////////////////////////////////
	[ContentImporter(".cur", DisplayName = "Cursor - Neoforce Controls")]
	class CursorImporter : ContentImporter<CursorFile>
	{
		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////
		public override CursorFile Import(string filename, ContentImporterContext context)
		{
			CursorFile cur = new CursorFile();
			cur.Data = File.ReadAllBytes(filename);
			return cur;
		}
		////////////////////////////////////////////////////////////////////////////

		#endregion
	}
	////////////////////////////////////////////////////////////////////////////

	#endregion

	#region //// Writer ////////////

	////////////////////////////////////////////////////////////////////////////
	[ContentTypeWriter]
	class CursorWriter : ContentTypeWriter<CursorFile>
	{

		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////
		protected override void Write(ContentWriter output, CursorFile value)
		{
			output.Write(value.Data.Length);
			output.Write(value.Data);
		}
		///////////////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////////////////
		public override string GetRuntimeType(TargetPlatform targetPlatform)
		{
			return typeof(CursorFile).AssemblyQualifiedName;
		}
		////////////////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////////////////    
		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
            return AssemblyHelper.GetRuntimeReader(typeof(TomShane.Neoforce.Controls.CursorReader),targetPlatform);
        }
		////////////////////////////////////////////////////////////////////////////

		#endregion
	}


	////////////////////////////////////////////////////////////////////////////

	#endregion

}