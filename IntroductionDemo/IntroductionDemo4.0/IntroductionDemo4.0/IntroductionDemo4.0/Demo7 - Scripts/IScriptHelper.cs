using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Engine;
using PloobsEngine.Modelo;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Material;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using PloobsEngine.AssetLoader;
using System.IO;
using PloobsEngine.Utils;
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Cameras;
using PloobsEngine.Features;
using PloobsEngine.Commands;

namespace IntroductionDemo4._0
{
    public class IScriptHelper
    {
        public IWorld world;
        public GraphicInfo graphicInfo;
        public GraphicFactory graphicFactory;
        public IRenderTechnic renderTechnic;

        ContentBuilder ContentBuilder = new ContentBuilder();
        Dictionary<String, String> modelLoaded = new Dictionary<string, string>();
        Dictionary<String, String> textureLoaded = new Dictionary<string, string>();

        private void CreateInternalTexture(String fileName)
        {
            if (!textureLoaded.ContainsKey(fileName))
            {
                ContentBuilder.Clear();
                String iname = "Texture" + StaticRandom.Random();
                ContentBuilder.Add(fileName, iname, null, "TextureProcessor");
                String buildError = ContentBuilder.Build();
                if (string.IsNullOrEmpty(buildError))
                {
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Content/Loaded"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Content/Loaded");
                    }
                    MakeCopy(ContentBuilder.OutputDirectory, Directory.GetCurrentDirectory() + "/Content/Loaded/");
                    textureLoaded.Add(fileName, iname);
                }
                else
                {
                    throw new Exception(buildError);
                }
            }
        }

        private void CreateInternalModel(String fileName)
        {
            if (!modelLoaded.ContainsKey(fileName))
            {
                ContentBuilder.Clear();
                String iname = "Model" + StaticRandom.Random();
                ContentBuilder.Add(fileName, iname, null, "ModelProcessor");
                String buildError = ContentBuilder.Build();
                if (string.IsNullOrEmpty(buildError))
                {
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Content/Loaded"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Content/Loaded");
                    }
                    MakeCopy(ContentBuilder.OutputDirectory, Directory.GetCurrentDirectory() + "/Content/Loaded/");
                    modelLoaded.Add(fileName, iname);
                }
                else
                {
                    throw new Exception(buildError);
                }
            }
        }        

        private void MakeCopy(String source, String destiny)
        {
            foreach (String item in Directory.EnumerateFiles(source))
            {
                int i = item.LastIndexOf("\\");
                i = i == 0 ? 0 : i + 1;
                String name = item.Substring(i);
                File.Copy(item, destiny + name, true);
            }

            foreach (String item in Directory.EnumerateDirectories(source))
            {
                int i = item.LastIndexOf("\\");
                i = i == 0 ? 0 : i + 1;
                String name = item.Substring(i);
                Directory.CreateDirectory(destiny + name);
                MakeCopy(item, destiny + name + "\\");
            }
        }

        protected void AddToWorld(IObject obj)
        {
            world.AddObject(obj);
        }

        protected void AddToWorld(ILight obj)
        {
            world.AddLight(obj);
        }

        protected void AddToWorld(ICamera obj)
        {
            world.CameraManager.AddCamera(obj);
        }

        protected ICamera CreateDefaultCamera()
        {
            return new CameraFirstPerson(graphicInfo);
        }
        
        protected DirectionalLightPE CreateDirectionalLight(Vector3 direction, Color color, float intensity)
        {
            DirectionalLightPE DirectionalLightPE = new DirectionalLightPE(direction, color);
            DirectionalLightPE.LightIntensity = intensity;            
            return DirectionalLightPE;
        }

        protected PointLightPE CreatePointLight(Vector3 position,float radius, Color color,float intensity)
        {
            return new PointLightPE( position, color,radius,intensity);
        }

        protected SpotLightPE CreateSpotLight(Vector3 position, Vector3 direction, float coneDecay,float lightRadius ,Color color,float angleInDegrees,float intensity)
        {            
            return new SpotLightPE(position, direction, coneDecay, lightRadius, color, (float)Math.Cos(MathHelper.ToRadians(angleInDegrees)), intensity);
        }

        protected void ChangeModelTexture(IObject obj,String textureName)
        {    
            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                foreach (var item in obj.Modelo.GetTextureInformation(i))
                {
                    item.SetTexture(textureName, TextureType.DIFFUSE);
                }
            }
        }

        protected void ChangeModelTexture(IObject obj, Color color)
        {
            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                foreach (var item in obj.Modelo.GetTextureInformation(i))
                {
                    item.SetTexture(graphicFactory.CreateTexture2DColor(1, 1, color), TextureType.DIFFUSE);
                }
            }
        }

        protected void AddPostEffect(IPostEffect posteffect)
        {
            renderTechnic.AddPostEffect(posteffect);
        }
        
        protected void AddSkyBox(String skyboxtextureName)
        {
            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube(skyboxtextureName);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }

        protected IObject CreateBoxMeshModel(String fileName, Vector3 position, Matrix orientation, float xlengh, float ylengh,float zlengh, Vector3 scale , float mass = 10)
        {
            SimpleModel simpleModel = new SimpleModel(graphicFactory, fileName);
            ///Physic info (position, rotation and scale are set here)
            BoxObject tmesh = new BoxObject(position, xlengh,ylengh,zlengh, mass, scale,orientation, MaterialDescription.DefaultBepuMaterial());
            ///Shader info (must be a deferred type)
            DeferredNormalShader shader = new DeferredNormalShader();
            ///Material info (must be a deferred type also)
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            ///The object itself
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected IObject CreateSphereMeshModel(String fileName, Vector3 position, Matrix orientation, float raio, float scale = 1, float mass = 10)
        {
            SimpleModel simpleModel = new SimpleModel(graphicFactory, fileName);
            ///Physic info (position, rotation and scale are set here)
            SphereObject tmesh = new SphereObject(position,raio,mass,scale, MaterialDescription.DefaultBepuMaterial());
            ///Shader info (must be a deferred type)
            DeferredNormalShader shader = new DeferredNormalShader();
            ///Material info (must be a deferred type also)
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            ///The object itself
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected IObject CreateTriangleMeshModel(String fileName, Vector3 position, Matrix orientation, Vector3 scale)
        {
            SimpleModel simpleModel = new SimpleModel(graphicFactory, fileName);
            ///Physic info (position, rotation and scale are set here)
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, position, orientation, scale, MaterialDescription.DefaultBepuMaterial());
            ///Shader info (must be a deferred type)
            DeferredNormalShader shader = new DeferredNormalShader();
            ///Material info (must be a deferred type also)
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            ///The object itself
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected IObject CreateSphereMeshModelU(String fileName, Vector3 position, Matrix orientation, float raio, float scale = 1, float mass = 10)
        {
            CreateInternalModel(fileName);
            SimpleModel simpleModel = new SimpleModel(graphicFactory, "Loaded/" + modelLoaded[fileName]);

            ///Physic info (position, rotation and scale are set here)
            SphereObject tmesh = new SphereObject(position, raio, mass, scale, MaterialDescription.DefaultBepuMaterial());
            ///Shader info (must be a deferred type)
            DeferredNormalShader shader = new DeferredNormalShader();
            ///Material info (must be a deferred type also)
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            ///The object itself
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected IObject CreateBoxMeshModelU(String fileName, Vector3 position, Matrix orientation, float xlengh, float ylengh, float zlengh, Vector3 scale, float mass = 10)
        {
            SimpleModel simpleModel = new SimpleModel(graphicFactory, fileName);
            ///Physic info (position, rotation and scale are set here)
            BoxObject tmesh = new BoxObject(position, xlengh, ylengh, zlengh, mass, scale, orientation, MaterialDescription.DefaultBepuMaterial());
            ///Shader info (must be a deferred type)
            DeferredNormalShader shader = new DeferredNormalShader();
            ///Material info (must be a deferred type also)
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            ///The object itself
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected IObject CreateTriangleMeshModelU(String fileName, Vector3 position, Matrix orientation, Vector3 scale)
        {
            CreateInternalModel(fileName);
            SimpleModel simpleModel = new SimpleModel(graphicFactory, "Loaded/" + modelLoaded[fileName]);

            ///Physic info (position, rotation and scale are set here)
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, position, orientation, scale, MaterialDescription.DefaultBepuMaterial());
            ///Shader info (must be a deferred type)
            DeferredNormalShader shader = new DeferredNormalShader();
            ///Material info (must be a deferred type also)
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            ///The object itself
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected void AddSkyBoxU(String skyboxtextureName)
        {
            CreateInternalTexture(skyboxtextureName);
            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube(skyboxtextureName);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }

        protected void ChangeModelTextureU(IObject obj, String textureName)
        {
            CreateInternalTexture(textureName);
            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                foreach (var item in obj.Modelo.GetTextureInformation(i))
                {
                    item.SetTexture(textureLoaded[textureName], TextureType.DIFFUSE);
                }
            }
        }

    }
}
