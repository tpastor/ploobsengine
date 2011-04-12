using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Content Manager, used to load stuffs like Model, Textures
    /// Cache the results
    /// </summary>
    public class EngineContentManager : IContentManager
    {
        ContentManager cmanagerInternal;

        public EngineContentManager(Game game)
        {
            cmanagerInternal = new ResourceContentManager(game.Services, Resources.ResourceManager);
            externalContentManager = game.Content;
        }

        private static IDictionary<String, object> _modelDicInt = new Dictionary<String, object>();
        private static IDictionary<String, object> _modelDicExt = new Dictionary<String, object>();         

        #region IContentManager Members

        private T LoadAsset<T>(String path, bool isInternal = false)
        {
            if (isInternal)
            {
                T t = cmanagerInternal.Load<T>(path);
                _modelDicInt.Add(path, t);
                return t;
            }
            else
            {
                T t = externalContentManager.Load<T>(path);
                _modelDicExt.Add(path, t);
                return t;
            }
        }

        #endregion

        #region IContentManager Members

        public override T GetAsset<T>(string fileName, bool isInternal = false)
        {
            if (isInternal)
            {
                if (_modelDicInt.ContainsKey(fileName))
                {
                    return (T)_modelDicInt[fileName];
                }
                else
                {
                    return LoadAsset<T>(fileName, isInternal);
                }
            }
            else
            {
                if (_modelDicExt.ContainsKey(fileName))
                {
                    return (T)_modelDicExt[fileName];
                }
                else
                {
                    return LoadAsset<T>(fileName, isInternal);
                }
            }

        }

        public ModelMesh GetModelParts(string fileName, int indexInModel)
        {
            if (_modelDicExt.ContainsKey(fileName))
            {
                Model m = (Model)_modelDicExt[fileName];
                return m.Meshes[indexInModel];
            }
            else
            {
                return LoadAsset<Model>(fileName).Meshes[indexInModel];
            }            
        }
      
        #endregion

        #region IContentManager Members

        public Matrix[] GetAnimatedTransformationMatrix(string fileName)
        {
            //   if (_modelDic.ContainsKey(fileName))
            //{
            //    SkinnedModel m = (SkinnedModel)_modelDic[fileName];
            //    Matrix[] m2 = new Matrix[m.Model.Bones.Count];
            //    m.Model.CopyBoneTransformsTo(m2);
            //    return m2;                
            //}
            //else
            //{
            //    SkinnedModel m = LoadAsset<SkinnedModel>(fileName);
            //    Matrix[] m2 = new Matrix[m.Model.Bones.Count];
            //    m.Model.CopyBoneTransformsTo(m2);
            //    return m2;                
            //}            
            return null;
        }

        public Matrix[] GetTransformationMatrix(string fileName)
        {
            if (_modelDicExt.ContainsKey(fileName))
            {
                Model m = (Model)_modelDicExt[fileName];
                Matrix[] m2 = new Matrix[m.Bones.Count];
                m.CopyAbsoluteBoneTransformsTo(m2);
                return m2;                
            }
            else
            {
                Model m = LoadAsset<Model>(fileName);
                Matrix[] m2 = new Matrix[m.Bones.Count];
                m.CopyAbsoluteBoneTransformsTo(m2);
                return m2;                
            }            
        }

        #endregion

        
    }
}
