using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Commands;
using PloobsEngine.Cameras;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Features
{
    public class SkyBoxSetTextureCube : ICommand
    {   
        private String texName = null;
        private SkyBox sb;
        bool enable;
        
        
        public SkyBoxSetTextureCube(String textureCubeName,bool enable = true)
        {
            if (String.IsNullOrEmpty(textureCubeName))
            {
                ActiveLogger.LogMessage("You must provide the a name for the TextureCube of the Skybox", LogLevel.FatalError);
                throw new Exception("You must provide the a name for the TextureCube of the Skybox");
            }
            this.texName = textureCubeName;
            this.enable = enable;
        }

        #region ICommand Members        

        protected override void execute()
        {            
             sb.setParameters(texName);
             sb.setParameters(enable);
        }

        protected override void setTarget(object obj)
        {
            this.sb = obj as SkyBox;            
        }

        #endregion

        #region ICommand Members


        public override string TargetName
        {
            get { return SkyBox.MyName; }
        }

        #endregion
    }
    
}
