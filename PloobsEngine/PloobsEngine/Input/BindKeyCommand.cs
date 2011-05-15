using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Commands;

namespace PloobsEngine.Input
{
    public class BindKeyCommand : ICommand
    {
        private InputPlayableKeyBoard ip;
        private BindAction ba;

        public BindAction BindAction
        {
            get { return ba; }
            set { ba = value; }
        }
        public BindKeyCommand(InputPlayableKeyBoard ip, BindAction ba)
        {
            this.ip = ip;
            this.ba = ba;
        }
        
        #region ICommand Members
                
        private InputAdvanced ia;

        protected override void execute()
        {
            ia.BindKey(ip,ba);
        }

        protected override void setTarget(object obj)
        {
            ia = (InputAdvanced)obj;
        }

        #endregion

        #region ICommand Members


        public override string TargetName
        {
            get { return InputAdvanced.MyName; }
        }

        #endregion
    }
}
