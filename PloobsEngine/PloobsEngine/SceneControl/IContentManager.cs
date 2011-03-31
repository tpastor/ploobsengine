using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public interface IContentManager
    {
        T GetAsset<T>(String fileName);
        ModelMesh GetModelParts(String fileName, int indexInModel);
        Matrix[] GetTransformationMatrix(String fileName);
        Matrix[] GetAnimatedTransformationMatrix(string fileName);        
    }
    
}
