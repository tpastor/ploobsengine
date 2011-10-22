using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace PloobsEngine.Utils.RemoteLogger
{
    /// <summary>
    /// Remote Looger, writes messages to MY personal Log Google app server
    /// Users should cleat they own Logger    
    /// </summary>
    public class RemoteLogger : ILogger
    {
        #region ILogger Members

        /// <summary>
        /// Log the message.
        /// </summary>
        /// <param name="Message">The message.</param>
        public void LogMessage(string Message)
        {
            WebClient wc = new WebClient();
            String where = String.Format("UserName: {0} - OSVersion {1} - ProcessorCount {2} - CurrentDirectory {3} - MachineName {4}", Environment.UserName, Environment.OSVersion, Environment.ProcessorCount, Environment.CurrentDirectory, Environment.MachineName);                       
            //String where = String.Format("UserName:{0} OSVersion {1} MachineName {2}", Environment.UserName, Environment.OSVersion, Environment.MachineName);                       
            //My Personal Google App log server
            wc.OpenReadAsync(new Uri("http://app-pastor.appspot.com/plserver?action=Log&where=" +where + "&desc=" + Message ));
            //wc.OpenRead(new Uri("http://app-pastor.appspot.com/plserver?action=Log&where=123&desc=" + Message));
        }

        #endregion
    }
}
