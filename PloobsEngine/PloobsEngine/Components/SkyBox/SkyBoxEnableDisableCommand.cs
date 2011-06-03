#if !WINDOWS_PHONE
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
    public class SkyBoxEnableDisableCommand : ICommand
    {
        bool enable;
        private SkyBox sb;


        public SkyBoxEnableDisableCommand(bool enable)
        {
            this.enable = enable;
        }

        #region ICommand Members        

        protected override void execute()
        {
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
#endif