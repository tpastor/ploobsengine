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

namespace IntroductionDemo4._0
{
    [PloobsSerializeAll]
    public class DynamicObjectSaver 
    {
        public DynamicObjectSaver(String modelName, Vector3 position, Matrix orientation, Vector3 scale, Vector3 LinearVelocity, Vector3 AngularVelocity)
        {
            this.modelName = modelName;
            this.position = position;
            this.scale = scale;
            this.orientation = orientation;
            this.LinearVelocity = LinearVelocity;
            this.AngularVelocity = AngularVelocity;
        }

        public String modelName;
        public Vector3 position;
        public Vector3 scale;
        public Matrix orientation;
        public Vector3 LinearVelocity;
        public Vector3 AngularVelocity;


    }
}

