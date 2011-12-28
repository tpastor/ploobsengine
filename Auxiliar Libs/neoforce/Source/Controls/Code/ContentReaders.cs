////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: ContentReaders.cs                            //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 11/09/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Copyright (c) by Tom Shane                                //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

//////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Content;

#if (!XBOX && !XBOX_FAKE)
using System.Windows.Forms;
#endif
//////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Controls
{
	public class SkinReader : ContentTypeReader<XDocument>
	{

		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////
		protected override XDocument Read(ContentReader input, XDocument existingInstance)
		{
			if (existingInstance == null)
			{
				return XDocument.Parse(input.ReadString());
			}
			else
			{
				existingInstance = XDocument.Parse(input.ReadString());
			}

			return existingInstance;
		}
		////////////////////////////////////////////////////////////////////////////

		#endregion

	}

	public class LayoutReader : ContentTypeReader<XDocument>
	{

		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////
		protected override XDocument Read(ContentReader input, XDocument existingInstance)
		{
			if (existingInstance == null)
			{
				return XDocument.Parse(input.ReadString());
			}
			else
			{
				existingInstance = XDocument.Parse(input.ReadString());
			}

			return existingInstance;
		}
		////////////////////////////////////////////////////////////////////////////

		#endregion

	}

#if (!XBOX && !XBOX_FAKE)

	public class CursorReader : ContentTypeReader<Cursor>
	{

		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////
		protected override Cursor Read(ContentReader input, Cursor existingInstance)
		{
			if (existingInstance == null)
			{
				int count = input.ReadInt32();
				byte[] data = input.ReadBytes(count);

				string path = Path.GetTempFileName();
				File.WriteAllBytes(path, data);

				IntPtr handle = NativeMethods.LoadCursor(path);
				Cursor cur = new Cursor(handle);
				File.Delete(path);

				return cur;
			}
			else
			{
			}

			return existingInstance;
		}
		////////////////////////////////////////////////////////////////////////////

		#endregion

	}

#endif

}

