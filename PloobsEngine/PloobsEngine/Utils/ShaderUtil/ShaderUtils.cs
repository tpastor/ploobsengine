using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Shader ecoding BitField Helper
    /// Used to create the field ShaderID
    /// </summary>
    public class ShaderUtils
    {
        public static int CreateSpecificBitField(bool DoNotIlluminate = false, bool isBackGround = false,bool NotEffectedByMotionBlur = false )
        {
            return CreateBitField(DoNotIlluminate, isBackGround, NotEffectedByMotionBlur);
        }

        public static int CreateBitField(bool op1 = false, bool op2 = false, bool op3 = false, bool op4 = false, bool op5 = false,bool op6 = false,bool op7 = false,bool op8 = false,bool op9 = false,bool op10 = false,bool op11 = false)
        {
            int flags = 
            (op1 ? 1 : 0) |
            (op2 ? 2 : 0) |
            (op3 ? 4 : 0) |
            (op4 ? 8 : 0) |
            (op5 ? 16 : 0) |
            (op6 ? 32 : 0) |
            (op7 ? 64 : 0) |
            (op8 ? 128 : 0) |
            (op9 ? 256 : 0) |
            (op10 ? 512 : 0) |
            (op11 ? 1024 : 0) 
            ;
            return flags;
        }
    }
}
    /// Example
    /// bool fullbright = fmod(flags, 2) == 1;  
    /// bool clampTexture = fmod(flags, 4) >= 2;  
    /// bool xTextureRepeat = fmod(flags, 8) >= 4;  
    /// bool yTextureRepeat = fmod(flags, 16) >= 8; 