using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsSerializator;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;

namespace EngineTestes.Serialization
{
    public class ObjectMock : IObject, PloobsSerializator.ICustomSerializable
    {
        public ObjectMock(GraphicFactory GraphicFactory, String modelName, Vector3 position, Matrix orientation, Vector3 scale)
        {
            this.modelName = modelName;
            this.position = position;
            this.scale = scale;
            this.orientation = orientation;
            Modelo = new SimpleModel(GraphicFactory, modelName);
            PhysicObject = new TriangleMeshObject(Modelo, position, orientation, scale, MaterialDescription.DefaultBepuMaterial());
            ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
            Material = new ForwardMaterial(shader);
            IObjectAttachment = new List<IObjectAttachment>();
        }


        public String modelName;
        public Vector3 position;
        public Vector3 scale;
        public Matrix orientation;

        #region ICustomSerializable Members

        public object Deserialize(DeSerializerProxy DeSerializerProxy)
        {            
            GraphicFactory GraphicFactory = (GraphicFactory)DeSerializerProxy.Context;
            ObjectMock ObjectMock = new ObjectMock(GraphicFactory,DeSerializerProxy.DeSerialize<String>("modelname") ,DeSerializerProxy.DeSerialize<Vector3>("position"),
            DeSerializerProxy.DeSerialize<Matrix>("ori"),DeSerializerProxy.DeSerialize<Vector3>("scale"));
            return ObjectMock;
        }

        public void Serialize(SerializerProxy SerializerProxy)
        {
            SerializerProxy.Serialize("modelname", modelName);
            SerializerProxy.Serialize("position", position);
            SerializerProxy.Serialize("scale", scale);
            SerializerProxy.Serialize("ori", orientation);
        }

        #endregion
    }
}
