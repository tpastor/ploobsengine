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
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Content Manager, used to load stuffs like Model, Textures
    /// Cache the results
    /// </summary>
    public class IContentManager 
    {
        ContentManager cmanagerInternal;

#if !WINRT
        protected ContentTracker externalContentManager;

        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentTracker ContentManager
        {
            get { return externalContentManager; }
        }
#else
        protected ContentManager externalContentManager;

        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentManager ContentManager
        {
            get { return externalContentManager; }
        }
#endif


#if SILVER 
        public IContentManager(ContentManager ContentManager)
#else
        public IContentManager(Game game)
#endif
        {
#if XBOX360
            cmanagerInternal = new ResourceContentManager(game.Services, ResourcesXbox.ResourceManager);
#elif WINRT
            cmanagerInternal = new ContentManager(game.Content.ServiceProvider);
#elif MONODX
            cmanagerInternal = new ResourceContentManager(game.Services, PloobsEngine.MonoResources.ResourceManager);
#elif MONO 
            cmanagerInternal = new ResourceContentManager(game.Services, PloobsEngine.ResourcesPC.ResourceManager);
#elif REACH 
            cmanagerInternal = new ResourceContentManager(game.Services, PloobsEngineReach.Resource2.ResourceManager);
#elif WINDOWS_PHONE && !SILVER
            cmanagerInternal = new ResourceContentManager(game.Services, Resource3.ResourceManager);
#elif SILVER 
            cmanagerInternal = new ResourceContentManager(ContentManager.ServiceProvider, Resource3.ResourceManager);
#else
            cmanagerInternal = new ResourceContentManager(game.Services, ResourcesPC.ResourceManager);
     
#endif

#if SILVER 
            ContentTracker ContentTracker = new ContentTracker(ContentManager.ServiceProvider,"Content");
            System.Diagnostics.Debug.Assert(ContentTracker != null);
            this.externalContentManager = ContentTracker;
#elif WINRT
            this.externalContentManager = game.Content;
#else
            ContentTracker ContentTracker = game.Content as ContentTracker;
            System.Diagnostics.Debug.Assert(ContentTracker != null);
            this.externalContentManager = ContentTracker;
#endif


        }

        private static IDictionary<String, object> _modelDicInt = new Dictionary<String, object>();        

        #region EngineContentManager Members

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
                T t = ContentManager.Load<T>(path);                
                return t;
            }
        }

        #endregion

        #region EngineContentManager Members

        public T GetAsset<T>(string fileName, bool isInternal = false, bool fromDisk = false)
        {
            System.Diagnostics.Debug.Assert(fileName != null);
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
                if (fromDisk)
                {
#if !WINRT
                    return this.externalContentManager.LoadFromDisk<T>(fileName);
#else
                    return this.externalContentManager.Load<T>(fileName);
#endif
                }
                else
                {
                        return LoadAsset<T>(fileName, isInternal);
                }
            }

        }

        public ModelMesh GetModelParts(string fileName, int indexInModel)
        {
                System.Diagnostics.Debug.Assert(fileName != null);
                System.Diagnostics.Debug.Assert(indexInModel > 0);
                return LoadAsset<Model>(fileName).Meshes[indexInModel];
        }
      
        #endregion

        #region EngineContentManager Members


        /// <summary>
        /// Gets the transformation matrix.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public Matrix[] GetTransformationMatrix(string fileName)
        {
                System.Diagnostics.Debug.Assert(fileName != null);
                Model m = LoadAsset<Model>(fileName);
                Matrix[] m2 = new Matrix[m.Bones.Count];
                m.CopyAbsoluteBoneTransformsTo(m2);
                return m2;                
        }

        #endregion

#if !WINRT
        /// <summary>
        /// Gets the asset assync.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="AssetLoaded">The onassetloaded method. -- cant be null</param>
        /// <returns></returns>
        public AssetTracker GetAssetAssync<T>(String fileName, PloobsEngine.Engine.ContentTracker.AssetLoaded AssetLoaded)
        {
            System.Diagnostics.Debug.Assert(fileName != null);
            System.Diagnostics.Debug.Assert(AssetLoaded != null);
            return externalContentManager.LoadAsync<T>(fileName, AssetLoaded);
        }
#endif

        /// <summary>
        /// Unload an asset
        /// </summary>
        /// <param name="assetName">Name of the asset.</param>
        /// <param name="releaseChildren">if set to <c>true</c> [release children].</param>
        public void UnloadAsset(string assetName, bool releaseChildren = true)
        {
            System.Diagnostics.Debug.Assert(assetName != null);
#if !WINRT
            externalContentManager.Unload(assetName, releaseChildren);
#endif
        }

        /// <summary>
        /// Unload all assets
        /// </summary>
        public void Unload()
        {
            externalContentManager.Unload();
        }

#if !WINRT
        public Dictionary<String, int> DumpAssetReferenceCount()
        {
            Dictionary<String, int> refs = new Dictionary<string, int>();
            foreach (var item in externalContentManager.GetLoadedAssetNames())
            {
                refs.Add(item, externalContentManager.GetReferenceCount(item));
            }
            return refs;
        }
#endif

        /// <summary>
        /// Releases the asset.
        /// </summary>
        /// <param name="assetName">Name of the asset.</param>
        public void ReleaseAsset(String assetName)
        {
            if (String.IsNullOrEmpty(assetName))
                return;
#if !WINRT
            externalContentManager.Release(assetName);
#endif
        }
    }
}
