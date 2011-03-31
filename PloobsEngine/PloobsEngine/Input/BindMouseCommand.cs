using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Commands;

namespace PloobsEngine.Input
{
    public class BindMouseCommand : ICommand
    {
        private InputPlaybleMouseBottom ip;
        private InputPlaybleMousePosition ipx;
        private BindAction ba;        
        int type;
        private readonly static int POSITION = 1;
        private readonly static int BOTTOM = 2;

        public BindMouseCommand(InputPlaybleMouseBottom ip, BindAction ba)
        {
            this.ip = ip;
            this.ba = ba;
            type = BOTTOM;
        }
        public BindMouseCommand(InputPlaybleMousePosition ipx, BindAction ba)
        {
            this.ipx = ipx;
            this.ba = ba;
            type = POSITION;
        }
        
        #region ICommand Members
                
        private InputAdvanced ia;

        public void execute()
        {
            if (type == POSITION)
            {
                ia.BindMousePosition(ipx, ba);
            }
            else if (type == BOTTOM)
            {
                ia.BindMouseBottom(ip, ba);
            }
        }

        public void setTarget(object obj)
        {
            ia = (InputAdvanced)obj;
        }
        #endregion

        #region ICommand Members


        public string TargetName
        {
            get { return InputAdvanced.MyName; }
        }

        #endregion

        public BindAction BindAction
        {
            get { return ba; }
            set { ba = value; }
        }
    }
}
