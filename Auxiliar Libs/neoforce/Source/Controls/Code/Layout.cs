////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Layout.cs                                    //
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

////////////////////////////////////////////////////////////////////////////
using System;
using Microsoft.Xna.Framework;
using System.Xml;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Controls
{


	public static class Layout
	{

		#region //// Fields ////////////

		////////////////////////////////////////////////////////////////////////////                     
		////////////////////////////////////////////////////////////////////////////

		#endregion

		#region //// Properties ////////

		////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////

		#endregion

		#region //// Construstors //////

		////////////////////////////////////////////////////////////////////////////       
		////////////////////////////////////////////////////////////////////////////

		#endregion

		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////    
		public static Container Load(Manager manager, string asset)
		{
			Container win = null;
			XDocument doc = new XDocument();
			ContentManager content = new ContentManager(manager.Game.Services);

			try
			{
				content.RootDirectory = manager.LayoutDirectory;

#if (!XBOX && !XBOX_FAKE)

				string file = content.RootDirectory + asset;

				if (File.Exists(file))
				{
					doc = XDocument.Load(file) as XDocument;
				}
				else

#endif
				{
					doc = content.Load<XDocument>(asset);
				}


				if (doc != null && doc.Element("Layout").Element("Controls") != null && doc.Element("Layout").Element("Controls").HasElements)
				{
					var enumem = doc.Element("Layout").Element("Controls").Elements("Control").GetEnumerator();
                    if (enumem.MoveNext() == false)
                        throw new Exception("Must be at least one control");
                    var node = enumem.Current;

					string cls = node.Attribute("Class").Value;
					Type type = Type.GetType(cls);

					if (type == null)
					{
						cls = "TomShane.Neoforce.Controls." + cls;
						type = Type.GetType(cls);
					}

					win = (Container)LoadControl(manager, node, type, null);
				}

			}
			finally
			{
				content.Dispose();
			}

			return win;
		}
		////////////////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////////////////
		private static Control LoadControl(Manager manager, XNode node, Type type, Control parent)
		{
			Control c = null;

			Object[] args = new Object[] { manager };

			c = (Control)type.InvokeMember(null, BindingFlags.CreateInstance, null, null, args);
			if (parent != null) c.Parent = parent;
			if (node.NodeType == XmlNodeType.Element)
			{
				XElement element = node as XElement;
				c.Name = element.Attribute("Name").Value;

				if (node != null && element.Element("Properties") != null && element.Element("Properties").HasElements)
				{
					LoadProperties(element.Element("Properties").Elements("Property"), c);
				}

				if (node != null && element.Element("Controls") != null && element.Element("Controls").HasElements)
				{
					foreach (var e in element.Element("Controls").Elements("Control"))
					{
						string cls = e.Attribute("Class").Value;
						Type t = Type.GetType(cls);

						if (t == null)
						{
							cls = "TomShane.Neoforce.Controls." + cls;
							t = Type.GetType(cls);
						}
						LoadControl(manager, e, t, c);
					}
				}
			}
			return c;
		}
		////////////////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////////////////
		private static void LoadProperties(IEnumerable<XElement> node, Control c)
		{
			foreach (var e in node)
			{
				string name = e.Attribute("Name").Value;
				string val = e.Attribute("Value").Value;

				PropertyInfo i = c.GetType().GetProperty(name);

				if (i != null)
				{
					{
						try
						{
							i.SetValue(c, Convert.ChangeType(val, i.PropertyType, null), null);
						}
						catch
						{
						}
					}
				}
			}
		}
		////////////////////////////////////////////////////////////////////////////

		#endregion

	}

}
