////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: KeyboardLayout.cs                            //
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
using Microsoft.Xna.Framework.Input;
using System.Globalization;
using System.Collections.Generic;
using System;
using System.Text;
//////////////////////////////////////////////////////////////////////////////

#endregion


namespace TomShane.Neoforce.Controls
{

	public class KeyboardLayout
	{

		#region //// Fields ////////////

		//////////////////////////////////////////////////////////////////////////            
		private string name = "English";
		public List<int> LayoutList = new List<int>();
		//////////////////////////////////////////////////////////////////////////    

		#endregion

		#region //// Properties ////////

		//////////////////////////////////////////////////////////////////////////	  
		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}
		//////////////////////////////////////////////////////////////////////////

		#endregion

		#region //// Constructors //////

		//////////////////////////////////////////////////////////////////////////
		public KeyboardLayout()
		{
			LayoutList.Add(1033);
		}
		//////////////////////////////////////////////////////////////////////////

		#endregion

		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////    
		public virtual string GetKey(KeyEventArgs args)
		{
			string ret = "";

			if (args.Caps && !args.Shift) ret = KeyToString(args).ToUpper();
			else if (!args.Caps && args.Shift) ret = KeyToString(args).ToUpper();
			else if (args.Caps && args.Shift) ret = KeyToString(args).ToLower();
			else if (!args.Caps && !args.Shift) ret = KeyToString(args).ToLower();


			return ret;
		}
		////////////////////////////////////////////////////////////////////////////    

		////////////////////////////////////////////////////////////////////////////    
		protected virtual string KeyToString(KeyEventArgs args)
		{
			switch (args.Key)
			{
				case Keys.A:
					return "a";
				case Keys.B:
					return "b";
				case Keys.C:
					return "c";
				case Keys.D:
					return "d";
				case Keys.E:
					return "e";
				case Keys.F:
					return "f";
				case Keys.G:
					return "g";
				case Keys.H:
					return "h";
				case Keys.I:
					return "i";
				case Keys.J:
					return "j";
				case Keys.K:
					return "k";
				case Keys.L:
					return "l";
				case Keys.M:
					return "m";
				case Keys.N:
					return "n";
				case Keys.O:
					return "o";
				case Keys.P:
					return "p";
				case Keys.Q:
					return "q";
				case Keys.R:
					return "r";
				case Keys.S:
					return "s";
				case Keys.T:
					return "t";
				case Keys.U:
					return "u";
				case Keys.V:
					return "v";
				case Keys.W:
					return "w";
				case Keys.X:
					return "x";
				case Keys.Y:
					return "y";
				case Keys.Z:
					return "z";

				case Keys.D0:
					return (args.Shift) ? ")" : "0";
				case Keys.D1:
					return (args.Shift) ? "!" : "1";
				case Keys.D2:
					return (args.Shift) ? "@" : "2";
				case Keys.D3:
					return (args.Shift) ? "#" : "3";
				case Keys.D4:
					return (args.Shift) ? "$" : "4";
				case Keys.D5:
					return (args.Shift) ? "%" : "5";
				case Keys.D6:
					return (args.Shift) ? "^" : "6";
				case Keys.D7:
					return (args.Shift) ? "&" : "7";
				case Keys.D8:
					return (args.Shift) ? "*" : "8";
				case Keys.D9:
					return (args.Shift) ? "(" : "9";

				case Keys.OemPlus:
					return (args.Shift) ? "+" : "=";
				case Keys.OemMinus:
					return (args.Shift) ? "_" : "-";
				case Keys.OemOpenBrackets:
					return (args.Shift) ? "{" : "[";
				case Keys.OemCloseBrackets:
					return (args.Shift) ? "}" : "]";
				case Keys.OemQuestion:
					return (args.Shift) ? "?" : "/";
				case Keys.OemPeriod:
					return (args.Shift) ? ">" : ".";
				case Keys.OemComma:
					return (args.Shift) ? "<" : ",";
				case Keys.OemPipe:
					return (args.Shift) ? "|" : "\\";
				case Keys.Space:
					return " ";
				case Keys.OemSemicolon:
					return (args.Shift) ? ":" : ";";
				case Keys.OemQuotes:
					return (args.Shift) ? "\"" : "'";
				case Keys.OemTilde:
					return (args.Shift) ? "~" : "`";

				case Keys.NumPad0:
					return (args.Shift) ? "" : "0";
				case Keys.NumPad1:
					return (args.Shift) ? "" : "1";
				case Keys.NumPad2:
					return (args.Shift) ? "" : "2";
				case Keys.NumPad3:
					return (args.Shift) ? "" : "3";
				case Keys.NumPad4:
					return (args.Shift) ? "" : "4";
				case Keys.NumPad5:
					return (args.Shift) ? "" : "5";
				case Keys.NumPad6:
					return (args.Shift) ? "" : "6";
				case Keys.NumPad7:
					return (args.Shift) ? "" : "7";
				case Keys.NumPad8:
					return (args.Shift) ? "" : "8";
				case Keys.NumPad9:
					return (args.Shift) ? "" : "9";
				case Keys.Decimal:
					return (args.Shift) ? "" : ".";

				case Keys.Divide:
					return (args.Shift) ? "/" : "/";
				case Keys.Multiply:
					return (args.Shift) ? "*" : "*";
				case Keys.Subtract:
					return (args.Shift) ? "-" : "-";
				case Keys.Add:
					return (args.Shift) ? "+" : "+";

				default:
					return "";
			}
		}
		////////////////////////////////////////////////////////////////////////////

		#endregion

	}

	public class CzechKeyboardLayout : KeyboardLayout
	{

		#region //// Constructors //////

		//////////////////////////////////////////////////////////////////////////
		public CzechKeyboardLayout()
		{
			Name = "Czech";
			LayoutList.Clear();
			LayoutList.Add(1029);
		}
		//////////////////////////////////////////////////////////////////////////

		#endregion

		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////    
		protected override string KeyToString(KeyEventArgs args)
		{
			switch (args.Key)
			{
				case Keys.D0:
					return (args.Shift) ? "0" : ")";
				case Keys.D1:
					return (args.Shift) ? "1" : "!";
				case Keys.D2:
					return (args.Shift) ? "2" : "@";
				case Keys.D3:
					return (args.Shift) ? "3" : "#";
				case Keys.D4:
					return (args.Shift) ? "4" : "$";
				case Keys.D5:
					return (args.Shift) ? "5" : "%";
				case Keys.D6:
					return (args.Shift) ? "6" : "^";
				case Keys.D7:
					return (args.Shift) ? "7" : "&";
				case Keys.D8:
					return (args.Shift) ? "8" : "*";
				case Keys.D9:
					return (args.Shift) ? "9" : "(";

				case Keys.Decimal:
					return (args.Shift) ? "" : ",";

				default:
					return base.KeyToString(args);
			}
		}
		////////////////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////////////////    
		////////////////////////////////////////////////////////////////////////////

		#endregion

	}

	public class GermanKeyboardLayout : KeyboardLayout
	{

		#region //// Constructors //////

		//////////////////////////////////////////////////////////////////////////
		public GermanKeyboardLayout()
		{
			Name = "German";
			LayoutList.Clear();
			LayoutList.Add(1031);
		}
		//////////////////////////////////////////////////////////////////////////

		#endregion

		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////    
		protected override string KeyToString(KeyEventArgs args)
		{
			switch (args.Key)
			{
				case Keys.D0:
					return (args.Shift) ? ")" : "0";
				case Keys.D1:
					return (args.Shift) ? "!" : "1";
				case Keys.D2:
					return (args.Shift) ? "@" : "2";
				case Keys.D3:
					return (args.Shift) ? "#" : "3";
				case Keys.D4:
					return (args.Shift) ? "$" : "4";
				case Keys.D5:
					return (args.Shift) ? "%" : "5";
				case Keys.D6:
					return (args.Shift) ? "^" : "6";
				case Keys.D7:
					return (args.Shift) ? "&" : "7";
				case Keys.D8:
					return (args.Shift) ? "*" : "8";
				case Keys.D9:
					return (args.Shift) ? "(" : "9";

				case Keys.Decimal:
					return (args.Shift) ? "" : ".";

				default:
					return base.KeyToString(args);
			}
		}
		////////////////////////////////////////////////////////////////////////////  


		#endregion

	}

	public class PolishKeyboardLayout : KeyboardLayout
	{

		#region //// Constructors //////
		//////////////////////////////////////////////////////////////////////////
		public PolishKeyboardLayout()
		{
			Name = "Polish";
			LayoutList.Clear();
			LayoutList.Add(1045);
		}
		//////////////////////////////////////////////////////////////////////////

		#endregion

		#region //// Methods ///////////

		////////////////////////////////////////////////////////////////////////////
		protected override string KeyToString(KeyEventArgs args)
		{
			return base.KeyToString(args);
		}
		////////////////////////////////////////////////////////////////////////////

		#endregion

	}

}

