using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Commands;


namespace PloobsEngine.Input
{
    public class ClearInputsCommand : ICommand
    {       

        public ClearInputsCommand()
        {            
        }

        #region ICommand Members

        private InputAdvanced ia;

        protected override void execute()
        {
            ia.Clear();
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
