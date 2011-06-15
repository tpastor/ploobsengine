#if !WINDOWS_PHONE
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
        public XmlModelMeshInfo(string modeName, string collisionType, float mass, float dinamicfriction, float staticfriction, float ellasticity, string difuseName, string bumpName, string specularName, string glowName)
        {
            this.modelName = modeName;
            this.collisionType = collisionType;
            this.difuseName = difuseName;
            this.bumpName = bumpName;
            this.specularName = specularName;
            this.glowName = glowName;

            this.mass = mass;
            this.dinamicfriction = dinamicfriction;
            this.staticfriction = staticfriction;
            this.ellasticity = ellasticity;




        }

        public string collisionType;
        public string modelName;
        public string difuseName;
        public string bumpName;
        public string specularName;
        public string glowName;



        public float mass;
        public float dinamicfriction;
        public float staticfriction;
        public float ellasticity;

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
        public Vector3 pos;
        public String bodyA;
        public String bodyB;
        public bool breakable;
    }


    public class ExtractXmlModelLoader : IModelLoader
    {
        string path = "Content\\ModelInfos\\";
        string modelPath = "..\\Content\\Model\\";
        string texturePath = "..\\Content\\Textures\\";

        /// <summary>
        /// Combine the xmlBasePath + Name + .xml
        /// when using load, just pass the Name
        /// </summary>
        /// <param name="xmlBasePath">The XML base path.</param>
        /// <param name="modelPath">The model path.</param>
        /// <param name="texturePath">The texture path.</param>
        public ExtractXmlModelLoader(string xmlBasePath, string modelPath , string texturePath)
        {
            this.path = xmlBasePath;
            this.modelPath = modelPath;
            this.texturePath = texturePath;
        }

        /// <summary>
        /// Use the default Path for everything
        //string xmlpath = "Content\\ModelInfos\\";
        //string modelPath = "..\\Content\\Model\\";
        //string texturePath = "..\\Content\\Textures\\";
        /// </summary>
        public ExtractXmlModelLoader()
        {            
        }

        #region IModelLoader Members        

        public ModelLoaderData Load(GraphicFactory factory,GraphicInfo ginfo,String Name)
        {
            ModelLoaderData elements = new ModelLoaderData();
            Dictionary<String, XmlModelMeshInfo> infos = new Dictionary<string, XmlModelMeshInfo>();
            Dictionary<String, targetInfo> targets = new Dictionary<string, targetInfo>();            
            Dictionary<String, SpotLightInformation> spotLights = new Dictionary<string, SpotLightInformation>();
            //Dictionary<String, ConstraintInformation> constraints = new Dictionary<string, ConstraintInformation>();
            Dictionary<String, CameraInfo> cameras = new Dictionary<string, CameraInfo>();

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
                    }
                    XmlElement material = node["material"];
                    if (material == null)
                    {
                        info.difuseName = "white";
                    }
                    else
                    {
                        XmlElement difuse = material["diffuse"];
                        if (difuse != null)
                        {
                            info.difuseName = removeExtension(SerializerHelper.DeserializeAttributeBaseType<String>("name", difuse));
                        }
                        XmlElement bump = material["bump"];
                        if (bump != null)
                        {
                            info.bumpName = removeExtension(SerializerHelper.DeserializeAttributeBaseType<String>("name", bump));
                        }
                        XmlElement specular = material["specular"];
                        if (specular != null)
                        {
                            info.specularName = removeExtension(SerializerHelper.DeserializeAttributeBaseType<String>("name", specular));
                        }
                        XmlElement glow = material["glow"];
                        if (glow != null)
                        {
                            info.glowName = removeExtension(SerializerHelper.DeserializeAttributeBaseType<String>("name", glow));
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
                    cameras.Add(co.Name,co);
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
            foreach (var item in spotLights)
            {
                SpotLightInformation si = item.Value;
                targetInfo ti =  targets[item.Key + ".Target"];
                SpotLightPE sl = new SpotLightPE(si.pos, Vector3.Normalize(ti.targetPos - si.pos), si.decay, (ti.targetPos - si.pos).Length() * 10f, si.color, (float)Math.Cos(si.angle / 2), si.multiplier);
                sl.CastShadown = si.castShadow;
                sl.Name = si.name;                
                elements.LightsInfo.Add(sl);

            }

            ///////PROCCESS CAMERAS/////////////////////
            foreach (var item in cameras)
            {
                CameraInfo ci = item.Value;
                targetInfo ti = targets[item.Key + ".Target"];
                ci.Target = ti.targetPos;
                elements.CameraInfo.Add(ci);
            }            


            Model model = factory.GetModel(modelPath + Name);
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
                        mi.textureInformation.LoadTexture();
                                                
                        if (inf.difuseName != null)
                            mi.textureInformation.SetTexture(factory.GetTexture2D(texturePath + inf.difuseName),TextureType.DIFFUSE);

                        if (inf.glowName != null)
                            mi.textureInformation.SetTexture(factory.GetTexture2D(texturePath + inf.glowName),TextureType.GLOW);

                        if (inf.specularName != null)
                            mi.textureInformation.SetTexture(factory.GetTexture2D(texturePath + inf.specularName),TextureType.SPECULAR);

                        if (inf.bumpName != null)
                            mi.textureInformation.SetTexture(factory.GetTexture2D(texturePath + inf.bumpName),TextureType.BUMP);

                        elements.ModelMeshesInfo.Add(mi);
                    }
                }
            }

            

            SerializerHelper.ChangeDecimalSymbolToSystemDefault();
            ///Clear Stuffs
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
    }

        #endregion    
}
#endif