#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Helper functions to serialize and deserialize XML
    /// </summary>
    public static class SerializerHelper
    {
        private static System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.InstalledUICulture;
        private static System.Globalization.NumberFormatInfo ni;

        /// <summary>
        /// Sets the number format.
        /// MUST BE CALLED ONCE IF YOUR SYSTEM USES "," for decimal separator
        /// </summary>
        public static void ChangeDecimalSymbolToPoint()
        {
            ni = (System.Globalization.NumberFormatInfo)ci.NumberFormat.Clone();
            ni.NumberDecimalSeparator = ".";
        }

        /// <summary>
        /// Changes the decimal symbol to system default.
        /// </summary>
        public static void ChangeDecimalSymbolToSystemDefault()
        {
            ni = (System.Globalization.NumberFormatInfo)ci.NumberFormat.Clone();
        }

        /// <summary>
        /// Serializa a vector
        /// </summary>
        /// <param name="vector">vetor</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="textWriter">textwriter</param>
        public static void SerializeVector3(Vector3 vector, String fieldName, System.Xml.XmlTextWriter textWriter)
        {
            textWriter.WriteStartElement(fieldName, null);
            textWriter.WriteElementString("x", Convert.ToString(vector.X, ni));
            textWriter.WriteElementString("y", Convert.ToString(vector.Y, ni));
            textWriter.WriteElementString("z", Convert.ToString(vector.Z, ni));
            textWriter.WriteEndElement();
        }

        /// <summary>
        /// Serializes the vector3.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="textWriter">The text writer.</param>
        public static void SerializeVector3(Vector3? vector, String fieldName, System.Xml.XmlTextWriter textWriter)
        {
            if (vector.HasValue)
            {
                SerializeVector3((Vector3)vector, fieldName, textWriter);
                return;
            }
            else
            {
                textWriter.WriteStartElement(fieldName, null);
                textWriter.WriteElementString("x", null);
                textWriter.WriteElementString("y", null);
                textWriter.WriteElementString("z", null);
                textWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Serializes the vector2.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="textWriter">The text writer.</param>
        public static void SerializeVector2(Vector2 vector, String fieldName, System.Xml.XmlTextWriter textWriter)
        {
            textWriter.WriteStartElement(fieldName, null);
            textWriter.WriteElementString("x", Convert.ToString(vector.X, ni));
            textWriter.WriteElementString("y", Convert.ToString(vector.Y, ni));
            textWriter.WriteEndElement();
        }
        /// <summary>
        /// Serializes the new element.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="textWriter">The text writer.</param>
        public static void SerializeNewElement(String fieldName, System.Xml.XmlTextWriter textWriter)
        {
            textWriter.WriteStartElement(fieldName);
        }
        /// <summary>
        /// Serializes the end element.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        public static void SerializeEndElement(System.Xml.XmlTextWriter textWriter)
        {
            textWriter.WriteEndElement();
        }

        /// <summary>
        /// Serializes the type of the base.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param">The param.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="textWriter">The text writer.</param>
        public static void SerializeBaseType<T>(T param, String fieldName, System.Xml.XmlTextWriter textWriter)
        {
            textWriter.WriteElementString(fieldName, Convert.ToString(param, ni));
        }

        /// <summary>
        /// Serializes the type of the attribute base.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param">The param.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="textWriter">The text writer.</param>
        public static void SerializeAttributeBaseType<T>(T param, String fieldName, System.Xml.XmlTextWriter textWriter)
        {
            textWriter.WriteAttributeString(fieldName, Convert.ToString(param, ni));
        }

        /// <summary>
        /// Serializes the type of the listbase.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="textWriter">The text writer.</param>
        public static void SerializeListbaseType<T>(IList<T> list, String fieldName, System.Xml.XmlTextWriter textWriter)
        {
            textWriter.WriteStartElement(fieldName, null);
            for (int i = 0; i < list.Count; i++)
            {
                textWriter.WriteElementString("Item_" + i, Convert.ToString(list[i], ni));
            }
            textWriter.WriteEndElement();
        }

        /// <summary>
        /// Serializes the dictionary.
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="dic">The dic.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="textWriter">The text writer.</param>
        public static void SerializeDictionary<X, Y>(IDictionary<X, Y> dic, String fieldName, System.Xml.XmlTextWriter textWriter)
        {
            textWriter.WriteStartElement(fieldName, null);
            SerializeListbaseType<X>(dic.Keys.ToList<X>(), "Keys", textWriter);
            SerializeListbaseType<Y>(dic.Values.ToList<Y>(), "Values", textWriter);
            textWriter.WriteEndElement();

        }
        /// <summary>
        /// Des the serialize dictionary.
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static IDictionary<X, Y> DeSerializeDictionary<X, Y>(String fieldName, System.Xml.XmlNode node)
        {
            IDictionary<X, Y> dic = new Dictionary<X, Y>();
            IList<X> keys = DeserializeSerializeListbaseType<X>("Keys", node[fieldName]);
            IList<Y> values = DeserializeSerializeListbaseType<Y>("Values", node[fieldName]);
            for (int i = 0; i < keys.Count; i++)
            {
                dic.Add(keys[i], values[i]);
            }
            return dic;
        }

        /// <summary>
        /// Deserializes the type of the serialize listbase.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static IList<T> DeserializeSerializeListbaseType<T>(String fieldName, System.Xml.XmlNode node)
        {
            IList<T> list = new List<T>();
            int num = node[fieldName].ChildNodes.Count;
            for (int i = 0; i < num; i++)
            {
                list.Add(SerializerHelper.DeserializeBaseType<T>("Item_" + i, node[fieldName]));

            }
            return list;
        }


        /// <summary>
        /// Serializes the matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="textWriter">The text writer.</param>
        public static void SerializeMatrix(Matrix matrix, String fieldName, System.Xml.XmlTextWriter textWriter)
        {
            textWriter.WriteStartElement(fieldName, null);
            textWriter.WriteElementString("M11", Convert.ToString(matrix.M11, ni));
            textWriter.WriteElementString("M12", Convert.ToString(matrix.M12, ni));
            textWriter.WriteElementString("M13", Convert.ToString(matrix.M13, ni));
            textWriter.WriteElementString("M14", Convert.ToString(matrix.M14, ni));
            textWriter.WriteElementString("M21", Convert.ToString(matrix.M21, ni));
            textWriter.WriteElementString("M22", Convert.ToString(matrix.M22, ni));
            textWriter.WriteElementString("M23", Convert.ToString(matrix.M23, ni));
            textWriter.WriteElementString("M24", Convert.ToString(matrix.M24, ni));
            textWriter.WriteElementString("M31", Convert.ToString(matrix.M31, ni));
            textWriter.WriteElementString("M32", Convert.ToString(matrix.M32, ni));
            textWriter.WriteElementString("M33", Convert.ToString(matrix.M33, ni));
            textWriter.WriteElementString("M34", Convert.ToString(matrix.M34, ni));
            textWriter.WriteElementString("M41", Convert.ToString(matrix.M41, ni));
            textWriter.WriteElementString("M42", Convert.ToString(matrix.M42, ni));
            textWriter.WriteElementString("M43", Convert.ToString(matrix.M43, ni));
            textWriter.WriteElementString("M44", Convert.ToString(matrix.M44, ni));
            textWriter.WriteEndElement();
        }
        /// <summary>
        /// Deserializes the vector3 nullable.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static Vector3? DeserializeVector3Nullable(String fieldName, System.Xml.XmlNode node)
        {
            if (node[fieldName] != null)
                return DeserializeVector3(fieldName, node);

            return null;
        }


        /// <summary>
        /// Deserializes the vector3.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static Vector3 DeserializeVector3(String fieldName, System.Xml.XmlNode node)
        {
            Vector3 vec = new Vector3();
            vec.X = float.Parse(node[fieldName]["x"].InnerText, ni);
            vec.Y = float.Parse(node[fieldName]["y"].InnerText, ni);
            vec.Z = float.Parse(node[fieldName]["z"].InnerText, ni);
            return vec;
        }
        /// <summary>
        /// Deserializes the vector2.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static Vector2 DeserializeVector2(String fieldName, System.Xml.XmlNode node)
        {
            Vector2 vec = new Vector2();
            vec.X = float.Parse(node[fieldName]["x"].InnerText, ni);
            vec.Y = float.Parse(node[fieldName]["y"].InnerText, ni);

            return vec;
        }


        /// <summary>
        /// Parses a basic Type
        /// Very Deep Dark Magic
        /// </summary>
        /// <typeparam name="T">basic type</typeparam>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static T DeserializeBaseType<T>(String fieldName, System.Xml.XmlNode node)
        {
            return (T)Convert.ChangeType(node[fieldName].InnerText, typeof(T), ni);
        }

        /// <summary>
        /// Deserializes attributes .
        /// </summary>
        /// <typeparam name="T">base type</typeparam>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static T DeserializeAttributeBaseType<T>(String fieldName, System.Xml.XmlNode node)
        {
            return (T)Convert.ChangeType(node.Attributes[fieldName].InnerText, typeof(T), ni);
        }


        /// <summary>
        /// Desserialize matrix.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static Matrix DeSerializeMatrix(String fieldName, System.Xml.XmlNode node)
        {

            float m11 = Convert.ToSingle(node[fieldName]["M11"].InnerText, ni);
            float m12 = Convert.ToSingle(node[fieldName]["M12"].InnerText, ni);
            float m13 = Convert.ToSingle(node[fieldName]["M13"].InnerText, ni);
            float m14 = Convert.ToSingle(node[fieldName]["M14"].InnerText, ni);

            float m21 = Convert.ToSingle(node[fieldName]["M21"].InnerText, ni);
            float m22 = Convert.ToSingle(node[fieldName]["M22"].InnerText, ni);
            float m23 = Convert.ToSingle(node[fieldName]["M23"].InnerText, ni);
            float m24 = Convert.ToSingle(node[fieldName]["M24"].InnerText, ni);

            float m31 = Convert.ToSingle(node[fieldName]["M31"].InnerText, ni);
            float m32 = Convert.ToSingle(node[fieldName]["M32"].InnerText, ni);
            float m33 = Convert.ToSingle(node[fieldName]["M33"].InnerText, ni);
            float m34 = Convert.ToSingle(node[fieldName]["M34"].InnerText, ni);

            float m41 = Convert.ToSingle(node[fieldName]["M41"].InnerText, ni);
            float m42 = Convert.ToSingle(node[fieldName]["M42"].InnerText, ni);
            float m43 = Convert.ToSingle(node[fieldName]["M43"].InnerText, ni);
            float m44 = Convert.ToSingle(node[fieldName]["M44"].InnerText, ni);

            return new Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

        }

    }
}
#endif