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
#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Light;
using PloobsEngine.Utils;
using PloobsEngine.Engine;
using PloobsEngine.Modelo;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Loader
{
    internal struct XmlModelMeshInfo
    {
        public XmlModelMeshInfo(string modeName, string collisionType, float mass, float dinamicfriction, float staticfriction, float ellasticity)
        {
            this.modelName = modeName;
            this.collisionType = collisionType;



            this.mass = mass;
            this.dinamicfriction = dinamicfriction;
            this.staticfriction = staticfriction;
            this.ellasticity = ellasticity;

            this.material = new materialInfo();




        }

        public string collisionType;
        public string modelName;



        public float mass;
        public float dinamicfriction;
        public float staticfriction;
        public float ellasticity;

        public materialInfo material;


    }
    internal struct materialInfo
    {

        public string difuseName;
        public string bumpName;
        public string specularName;
        public string glowName;
        public string reflectionName;

        public Dictionary<string, object> extrainformation;

    }

    internal struct targetInfo
    {
        public Vector3 targetPos;
        public string name;
    }

    internal struct SpotLightInformation
    {
        public Vector3 pos;
        public Color color;
        public float decay;
        public float multiplier;
        public float angle;
        public String name;
        public bool castShadow;
    }

    internal struct ConstraintInformation
    {
        public String name;
        public String type;        
    }


    /// <summary>
    /// Loader for our 3ds max exporter
    /// </summary>
    public class ExtractXmlModelLoader : IModelLoader
    {
        string path = "Content\\ModelInfos\\";
        string modelPath = "..\\Content\\Model\\";
        string texturePath = "..\\Content\\Textures\\";

        List<String> modelNames = new List<string>();
        List<String> texturesNames = new List<string>();

        /// <summary>
        /// Combine the xmlBasePath + Name + .xml
        /// when using load, just pass the Name
        /// </summary>
        /// <param name="xmlBasePath">The XML base path.</param>
        /// <param name="modelPath">The model path.</param>
        /// <param name="texturePath">The texture path.</param>
        public ExtractXmlModelLoader(string xmlBasePath, string modelPath, string texturePath)
        {
            this.path = xmlBasePath;
            this.modelPath = modelPath;
            this.texturePath = texturePath;
        }

        /// <summary>
        /// Use the default Path for everything
        /// string xmlpath = "Content\\ModelInfos\\";
        /// string modelPath = "..\\Content\\Model\\";
        /// string texturePath = "..\\Content\\Textures\\";
        /// </summary>
        public ExtractXmlModelLoader()
        {
        }

#region IModelLoader Members

        public ModelLoaderData Load(GraphicFactory factory, GraphicInfo ginfo, String Name)
        {            
            ModelLoaderData elements = new ModelLoaderData();
            Dictionary<String, XmlModelMeshInfo> infos = new Dictionary<string, XmlModelMeshInfo>();
            Dictionary<String, targetInfo> targets = new Dictionary<string, targetInfo>();
            Dictionary<String, SpotLightInformation> spotLights = new Dictionary<string, SpotLightInformation>();
            //Dictionary<String, ConstraintInformation> constraints = new Dictionary<string, ConstraintInformation>();
            Dictionary<String, CameraInfo> cameras = new Dictionary<string, CameraInfo>();
            Dictionary<String, ParticleInfo> particles = new Dictionary<string, ParticleInfo>();


            SerializerHelper.ChangeDecimalSymbolToPoint();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + Name + ".xml");
            XmlNodeList worldNode = xDoc["SCENE"].ChildNodes;

            foreach (XmlNode node in worldNode)
            {


                if (node.Name == "Constraint")
                {

                    ConstraintInfo cinfo = new ConstraintInfo();


                    cinfo.Name = SerializerHelper.DeserializeAttributeBaseType<String>("name", node);
                    XmlElement type = node["type"];
                    if (type != null)
                    {
                        cinfo.type = SerializerHelper.DeserializeAttributeBaseType<String>("value", type);
                    }

                    XmlElement child = node["child"];
                    if (child != null)
                    {
                        cinfo.bodyA = SerializerHelper.DeserializeAttributeBaseType<String>("name", child);
                    }

                    XmlElement parent = node["parent"];
                    if (parent != null)
                    {
                        cinfo.bodyB = SerializerHelper.DeserializeAttributeBaseType<String>("name", parent);
                    }

                    XmlElement breakable = node["isBreakable"];
                    if (breakable != null)
                    {

                        cinfo.breakable = SerializerHelper.DeserializeAttributeBaseType<bool>("value", breakable);

                    }



                    Vector3 pos = SerializerHelper.DeserializeVector3("position", node);
                    cinfo.Position = new Vector3(pos.X, pos.Y, pos.Z);


                    elements.ConstraintInfo.Add(cinfo);

                }
                if (node.Name == "particle")
                {
                    ParticleInfo pinfo = new ParticleInfo();
                    pinfo.Name = SerializerHelper.DeserializeAttributeBaseType<String>("name", node);
                    pinfo.Position = SerializerHelper.DeserializeVector3("position", node);
                    pinfo.Orientation = SerializerHelper.DeserializeQuaternion("rotation", node);

                    XmlElement mass = node["type"];
                    if (mass != null)
                    {
                        pinfo.Type = SerializerHelper.DeserializeAttributeBaseType<String>("value", mass);
                    }

                    elements.ParticleInfo.Add(pinfo);
                }
                if (node.Name == "body")
                {
                    XmlModelMeshInfo info = new XmlModelMeshInfo();
                    info.modelName = SerializerHelper.DeserializeAttributeBaseType<String>("name", node);



                    XmlElement mass = node["mass"];
                    if (mass != null)
                    {
                        info.mass = SerializerHelper.DeserializeAttributeBaseType<float>("value", mass);
                    }

                    XmlElement dfric = node["dinamicfriction"];
                    if (dfric != null)
                    {
                        info.dinamicfriction = SerializerHelper.DeserializeAttributeBaseType<float>("value", dfric);
                    }

                    XmlElement sfric = node["staticfriction"];
                    if (sfric != null)
                    {
                        info.staticfriction = SerializerHelper.DeserializeAttributeBaseType<float>("value", sfric);
                    }

                    XmlElement ellas = node["ellasticity"];
                    if (ellas != null)
                    {
                        info.ellasticity = SerializerHelper.DeserializeAttributeBaseType<float>("value", ellas);
                    }



                    XmlElement collision = node["collision"];
                    if (collision != null)
                    {
                        info.collisionType = SerializerHelper.DeserializeAttributeBaseType<String>("type", collision);

                        if (info.collisionType.Contains("Water"))
                        {

                            Vector3 pos = SerializerHelper.DeserializeVector3("position", collision);

                            float width = SerializerHelper.DeserializeAttributeBaseType<float>("value", collision["width"]);
                            float length = SerializerHelper.DeserializeAttributeBaseType<float>("value", collision["length"]);

                            info.material.extrainformation = new Dictionary<string, object>();
                            info.material.extrainformation.Add("position", pos);
                            info.material.extrainformation.Add("width", width);
                            info.material.extrainformation.Add("length", length);
                        }
                    }
                    XmlElement material = node["material"];
                    if (material == null)
                    {
                        info.material.difuseName = "white";
                    }
                    else
                    {

                        XmlElement reflect = material["reflection"];
                        if (reflect != null)
                        {
                            info.material.reflectionName = removeExtension(SerializerHelper.DeserializeAttributeBaseType<String>("name", reflect));
                        }
                        else
                        {
                            XmlElement difuse = material["diffuse"];
                            if (difuse != null)
                            {
                                info.material.difuseName = removeExtension(SerializerHelper.DeserializeAttributeBaseType<String>("name", difuse));
                            }
                            XmlElement bump = material["bump"];
                            if (bump != null)
                            {
                                info.material.bumpName = removeExtension(SerializerHelper.DeserializeAttributeBaseType<String>("name", bump));
                            }
                            XmlElement specular = material["specular"];
                            if (specular != null)
                            {
                                info.material.specularName = removeExtension(SerializerHelper.DeserializeAttributeBaseType<String>("name", specular));
                            }
                            XmlElement glow = material["glow"];
                            if (glow != null)
                            {
                                info.material.glowName = removeExtension(SerializerHelper.DeserializeAttributeBaseType<String>("name", glow));
                            }
                        }
                    }
                    infos.Add(info.modelName, info);
                }
                else if (node.Name == "pointlight")
                {
                    String name = SerializerHelper.DeserializeAttributeBaseType<String>("name", node);
                    Vector3 pos = SerializerHelper.DeserializeVector3("position", node);
                    pos = new Vector3(pos.X, -pos.Y, -pos.Z);
                    Vector3 vColor = SerializerHelper.DeserializeVector3("color", node);
                    Color color = new Color(vColor.X / 255, vColor.Y / 255, vColor.Z / 255);
                    float amount = SerializerHelper.DeserializeAttributeBaseType<float>("amount", node["multiplier"]);
                    float decay = SerializerHelper.DeserializeAttributeBaseType<float>("value", node["decay"]);
                    PointLightPE pl = new PointLightPE(pos, color, 200, amount);
                    pl.Name = name;
                    pl.UsePointLightQuadraticAttenuation = true;
                    elements.LightsInfo.Add(pl);
                }
                else if (node.Name == "spotlight")
                {
                    String name = SerializerHelper.DeserializeAttributeBaseType<String>("name", node);
                    Vector3 pos = SerializerHelper.DeserializeVector3("position", node);
                    pos = new Vector3(pos.X, -pos.Y, -pos.Z);
                    Vector3 vColor = SerializerHelper.DeserializeVector3("color", node);
                    float fallof = SerializerHelper.DeserializeBaseType<float>("fallof", node);
                    Color color = new Color(vColor.X / 255, vColor.Y / 255, vColor.Z / 255);
                    float amount = SerializerHelper.DeserializeAttributeBaseType<float>("amount", node["multiplier"]);
                    float decay = SerializerHelper.DeserializeAttributeBaseType<float>("value", node["decay"]);
                    bool castShadow = SerializerHelper.DeserializeBaseType<bool>("castShadows", node);

                    SpotLightInformation spi = new SpotLightInformation();
                    spi.angle = MathHelper.ToRadians(fallof);
                    spi.color = color;
                    spi.decay = decay;
                    spi.multiplier = amount;
                    spi.name = name;
                    spi.pos = pos;
                    spi.castShadow = castShadow;
                    spotLights.Add(spi.name, spi);


                }
                else if (node.Name == "target")
                {
                    String name = SerializerHelper.DeserializeAttributeBaseType<String>("name", node);
                    Vector3 pos = SerializerHelper.DeserializeVector3("position", node);
                    pos = new Vector3(pos.X, -pos.Y, -pos.Z);
                    targetInfo ti = new targetInfo();
                    ti.targetPos = pos;
                    ti.name = name;
                    targets.Add(ti.name, ti);
                }
                else if (node.Name == "camera")
                {
                    String name = SerializerHelper.DeserializeAttributeBaseType<String>("name", node);
                    Vector3 pos = SerializerHelper.DeserializeVector3("position", node);
                    pos = new Vector3(pos.X, -pos.Y, -pos.Z);
                    CameraInfo co = new CameraInfo();
                    co.Name = name;
                    co.Position = pos;
                    cameras.Add(co.Name, co);
                }
                else if (node.Name == "dummy")
                {
                    String name = SerializerHelper.DeserializeAttributeBaseType<String>("name", node);
                    Vector3 pos = SerializerHelper.DeserializeVector3("position", node);
                    pos = new Vector3(pos.X, -pos.Y, -pos.Z);
                    DummyInfo di = new DummyInfo();
                    di.Name = name;
                    di.Position = pos;
                    elements.DummyInfo.Add(di);
                }
            }

            ///////PROCCESS LIGHTS /////////////////////
#if !MONODX
            foreach (var item in spotLights)
            {
                SpotLightInformation si = item.Value;
                targetInfo ti = targets[item.Key + ".Target"];
                SpotLightPE sl = new SpotLightPE(si.pos, Vector3.Normalize(ti.targetPos - si.pos), si.color,si.decay, (ti.targetPos - si.pos).Length() * 10f, (float)Math.Cos(si.angle / 2), si.multiplier);
                sl.CastShadown = si.castShadow;
                sl.Name = si.name;
                elements.LightsInfo.Add(sl);

            }
#endif
            ///////PROCCESS CAMERAS/////////////////////
            foreach (var item in cameras)
            {
                CameraInfo ci = item.Value;
                targetInfo ti = targets[item.Key + ".Target"];
                ci.Target = ti.targetPos;
                elements.CameraInfo.Add(ci);
            }


            Model model = factory.GetModel(modelPath + Name);
            modelNames.Add(modelPath + Name);
            Matrix[] m = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(m);

            ////////////EXTRAINDO MESHES
            for (int i = 0; i < model.Meshes.Count; i++)
            {
                String name = model.Meshes[i].Name.Substring(5);
                if (infos.ContainsKey(name))
                {

                    for (int j = 0; j < model.Meshes[i].MeshParts.Count; j++)
                    {
                        XmlModelMeshInfo inf = infos[name];
                        Matrix tr = m[model.Meshes[i].ParentBone.Index];

                        Vector3 scale;
                        Vector3 pos;
                        Quaternion ori;
                        tr.Decompose(out scale, out ori, out pos);

                        ObjectInformation mi = new ObjectInformation();
                        mi.modelName = inf.modelName;
                        mi.meshPartIndex = j;
                        mi.meshIndex = i;
                        mi.position = pos;
                        mi.scale = scale;
                        mi.rotation = ori;
                        
                        ModelBuilderHelper.Extract(m, model.Meshes[i].MeshParts[j], out mi.batchInformation);
                        mi.ellasticity = inf.ellasticity;
                        mi.dinamicfriction = inf.dinamicfriction;
                        mi.staticfriction = inf.staticfriction;
                        mi.collisionType = inf.collisionType;
                        mi.mass = inf.mass;

                        mi.batchInformation.ModelLocalTransformation = m[model.Meshes[i].ParentBone.Index];

                        mi.textureInformation = new TextureInformation(false, factory);
                        

                        if (inf.material.reflectionName != null)
                        {
                            mi.textureInformation.SetCubeTexture(factory.GetTextureCube(texturePath + inf.material.reflectionName), TextureType.ENVIRONMENT);
                            texturesNames.Add(texturePath + inf.material.reflectionName);
                        }

                        else
                        {
                            if (inf.material.difuseName != null)
                            {
                                mi.textureInformation.SetTexture(factory.GetTexture2D(texturePath + inf.material.difuseName), TextureType.DIFFUSE);
                                texturesNames.Add(texturePath + inf.material.difuseName);
                            }

                            if (inf.material.glowName != null)
                            {
                                mi.textureInformation.SetTexture(factory.GetTexture2D(texturePath + inf.material.glowName), TextureType.GLOW);
                                texturesNames.Add(texturePath + inf.material.glowName);
                            }

                            if (inf.material.specularName != null)
                            {
                                mi.textureInformation.SetTexture(factory.GetTexture2D(texturePath + inf.material.specularName), TextureType.SPECULAR);
                                texturesNames.Add(texturePath + inf.material.specularName);
                            }

                            if (inf.material.bumpName != null)
                            {
                                mi.textureInformation.SetTexture(factory.GetTexture2D(texturePath + inf.material.bumpName), TextureType.BUMP);
                                texturesNames.Add(texturePath + inf.material.bumpName);
                            }

                        }

                        if (inf.collisionType != null)
                        {
                            if (inf.collisionType.Contains("Water"))
                            {
                                mi.extra = inf.material.extrainformation;
                            }
                        }

                        mi.textureInformation.LoadTexture();
                        elements.ModelMeshesInfo.Add(mi);
                    }
                }
            }

            SerializerHelper.ChangeDecimalSymbolToSystemDefault();
            //Clear Stuffs
            infos.Clear();
            targets.Clear();
            spotLights.Clear();
            cameras.Clear();

            return elements;
        }

        private string removeExtension(String str)
        {
            return str.Split('.')[0];
        }

#region IModelLoader Members


        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public void CleanUp(GraphicFactory factory)
        {
            foreach (var item in modelNames)
            {
                factory.ReleaseAsset(item);
            }
            modelNames.Clear();

            foreach (var item in texturesNames)
            {
                factory.ReleaseAsset(item);
            }
            texturesNames.Clear();
        }

#endregion
    }

#endregion
}
#endif