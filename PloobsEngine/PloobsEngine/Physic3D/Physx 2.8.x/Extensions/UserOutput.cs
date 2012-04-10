#if WINDOWS
using System;
using System.Collections.Generic;
using StillDesign.PhysX;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics
{
	public class UserOutput : UserOutputStream
	{
		public override void Print(string message)
		{
			ActiveLogger.LogMessage("PhysX: " + message,LogLevel.Info);
		}
		public override AssertResponse ReportAssertionViolation(string message, string file, int lineNumber)
		{
            ActiveLogger.LogMessage("PhysX: " + message, LogLevel.RecoverableError);

			return AssertResponse.Continue;
		}
		public override void ReportError(ErrorCode errorCode, string message, string file, int lineNumber)
		{
            if(errorCode == ErrorCode.Assertion || errorCode == ErrorCode.InternalError || errorCode == ErrorCode.OutOfMemory )
                ActiveLogger.LogMessage("PhysX: " + message, LogLevel.FatalError);
            else
                ActiveLogger.LogMessage("PhysX: " + message, LogLevel.RecoverableError);
		}

        
	}
}
#endif