using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsSerializator;
using Microsoft.Xna.Framework;
using System.Xml.Linq;
using System.Xml;
using PloobsEngine.Engine;

namespace EngineTestes.Serialization
{
    public class SerializatorWrapper
    {
        Dictionary<Type, Action<String, object, XmlSerializer>> des = new Dictionary<Type, Action<String, object, XmlSerializer>>();
        Dictionary<Type, Func<XElement, object>> desserializador = new Dictionary<Type, Func<XElement, object>>(); 
        public SerializatorWrapper()
        {
            des.Add(typeof(Matrix),
                (x, a, b) =>
                {
                    Matrix v = (Matrix)a;
                    b.BeginType(x, typeof(Matrix).AssemblyQualifiedName);
                    b.Serialize("m11", v.M11);
                    b.Serialize("m12", v.M12);
                    b.Serialize("m13", v.M13);
                    b.Serialize("m14", v.M14);
                    b.Serialize("m21", v.M21);
                    b.Serialize("m22", v.M22);
                    b.Serialize("m23", v.M23);
                    b.Serialize("m24", v.M24);
                    b.Serialize("m31", v.M31);
                    b.Serialize("m32", v.M32);
                    b.Serialize("m33", v.M33);
                    b.Serialize("m34", v.M34);
                    b.Serialize("m41", v.M41);
                    b.Serialize("m42", v.M42);
                    b.Serialize("m43", v.M43);
                    b.Serialize("m44", v.M44);
                    b.EndType(x);
                }
            );

            des.Add(typeof(Vector3),
                (x, a, b) =>
                {
                    Vector3 v = (Vector3) a;
                    b.BeginType(x, typeof(Vector3).AssemblyQualifiedName);
                    b.Serialize("x", v.X);
                    b.Serialize("y", v.Y);
                    b.Serialize("z", v.Z);
                    b.EndType(x);
                }
            );

            des.Add(typeof(Quaternion),
                (x, a, b) =>
                {
                    Quaternion v = (Quaternion)a;
                    b.BeginType(x, typeof(Quaternion).AssemblyQualifiedName);
                    b.Serialize("x", v.X);
                    b.Serialize("y", v.Y);
                    b.Serialize("z", v.Z);
                    b.Serialize("w", v.W);
                    b.EndType(x);
                }
            );

            des.Add(typeof(Vector4),
          (x, a, b) =>
          {
              Vector4 v = (Vector4)a;
              b.BeginType(x, typeof(Vector4).AssemblyQualifiedName);
              b.Serialize("x", v.X);
              b.Serialize("y", v.Y);
              b.Serialize("z", v.Z);
              b.Serialize("w", v.W);
              b.EndType(x);
          }
      );
            des.Add(typeof(Color),
            (x, a, b) =>
            {
                Color v = (Color)a;
                b.BeginType(x, typeof(Vector2).AssemblyQualifiedName);
                Vector4 c = v.ToVector4();
                b.Serialize("x", c.X);
                b.Serialize("y", c.Y);
                b.Serialize("z", c.Z);
                b.Serialize("w", c.W);
                b.EndType(x);
            }
        );


            des.Add(typeof(Vector2),
            (x, a, b) =>
            {
                Vector2 v = (Vector2)a;
                b.BeginType(x, typeof(Vector2).AssemblyQualifiedName);
                b.Serialize("x", v.X);
                b.Serialize("y", v.Y);                
                b.EndType(x);
            }
        );

            desserializador.Add(typeof(Color),
                (a) =>
                {                    
                    Vector4 v = new Vector4();
                    v.X = Convert.ToSingle(a.Element("x").Value);
                    v.Y = Convert.ToSingle(a.Element("y").Value);
                    v.Z = Convert.ToSingle(a.Element("z").Value);
                    v.W = Convert.ToSingle(a.Element("w").Value);
                    return new Color(v);
                }
            );

            desserializador.Add(typeof(Vector3),
                (a) =>
                {                    
                    Vector3 v = new Vector3();
                    v.X = Convert.ToSingle(a.Element("x").Value);
                    v.Y = Convert.ToSingle(a.Element("y").Value);
                    v.Z = Convert.ToSingle(a.Element("z").Value);
                    return v;                       
                }
            );

            desserializador.Add(typeof(Vector2),
                (a) =>
                {
                    Vector2 v = new Vector2();
                    v.X = Convert.ToSingle(a.Element("x").Value);
                    v.Y = Convert.ToSingle(a.Element("y").Value);
                    return v;
                }
            );

            desserializador.Add(typeof(Vector4),
              (a) =>
              {
                  Vector4 v = new Vector4();
                  v.X = Convert.ToSingle(a.Element("x").Value);
                  v.Y = Convert.ToSingle(a.Element("y").Value);
                  v.Z = Convert.ToSingle(a.Element("z").Value);
                  v.W = Convert.ToSingle(a.Element("w").Value);
                  return v;
              }
          );
            desserializador.Add(typeof(Quaternion),
              (a) =>
              {
                  Quaternion v = new Quaternion();
                  v.X = Convert.ToSingle(a.Element("x").Value);
                  v.Y = Convert.ToSingle(a.Element("y").Value);
                  v.Z = Convert.ToSingle(a.Element("z").Value);
                  v.W = Convert.ToSingle(a.Element("w").Value);
                  return v;
              }
            );

            desserializador.Add(typeof(Matrix),
              (a) =>
              {
                  Matrix v = new Matrix();
                  v.M11 = Convert.ToSingle(a.Element("m11").Value);
                  v.M12 = Convert.ToSingle(a.Element("m12").Value);
                  v.M13 = Convert.ToSingle(a.Element("m13").Value);
                  v.M14 = Convert.ToSingle(a.Element("m14").Value);

                  v.M21 = Convert.ToSingle(a.Element("m21").Value);
                  v.M22 = Convert.ToSingle(a.Element("m22").Value);
                  v.M23 = Convert.ToSingle(a.Element("m23").Value);
                  v.M24 = Convert.ToSingle(a.Element("m24").Value);

                  v.M31 = Convert.ToSingle(a.Element("m31").Value);
                  v.M32 = Convert.ToSingle(a.Element("m32").Value);
                  v.M33 = Convert.ToSingle(a.Element("m33").Value);
                  v.M34 = Convert.ToSingle(a.Element("m34").Value);

                  v.M41 = Convert.ToSingle(a.Element("m41").Value);
                  v.M42 = Convert.ToSingle(a.Element("m42").Value);
                  v.M43 = Convert.ToSingle(a.Element("m43").Value);
                  v.M44 = Convert.ToSingle(a.Element("m44").Value);

                  return v;
              }
            );


        }

        public void Serialize(Object obj, String path)
        {
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            SerializatorMachine.Serialize(obj, path, Encoding.Default, des);
        }

        public T Desserialize<T>(String path, object context = null)
        {
            SerializatorMachine SerializatorMachine = new SerializatorMachine();
            return (T)SerializatorMachine.Desserialize(path, context, desserializador);
        }
    }
}


