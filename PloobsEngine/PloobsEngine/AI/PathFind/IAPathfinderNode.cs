using System;
using System.Collections.Generic;
using System.Text;

namespace PloobsEngine.IA
{
    #if !WINDOWS_PHONE
    [Serializable]
#endif
    public class IAPathfinderNode
    {
        public float F;
        public float G;
        public float H;  // f = gone + heuristic
        public float X;
        public float Y;
        public float Z;
        public float PX; // Parent
        public float PY;
        public float PZ;
        public int parentId;
    }
}
