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
#if REACH 
         cmanagerInternal = new ResourceContentManager(game.Services, PloobsEngineReach.Resource2.ResourceManager);
#elif WINDOWS_PHONE
         cmanagerInternal = new ResourceContentManager(game.Services, Resource3.ResourceManager);
#else
         cmanagerInternal = new ResourceContentManager(game.Services, Resources.ResourceManager);
#endif            
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
