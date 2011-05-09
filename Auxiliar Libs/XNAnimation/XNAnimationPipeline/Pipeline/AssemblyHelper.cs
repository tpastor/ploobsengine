/*
 * AssemblyHelper.cs
 * Author: Bruno Evangelista
 * Copyright (c) 2008 Bruno Evangelista. All rights reserved.
 *
 * THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace XNAnimationPipeline.Pipeline
{
    internal static class AssemblyHelper
    {
        private static readonly string windowsPublicKeyTokens = "0c21691816f8c6d0";
        //private static readonly string windowsPublicKeyTokens = "7ebcd2ab87b5cecb";
        //private static readonly string xboxPublicKeyTokens = "121fbd51268a0405";

        private static readonly string[] assemblySplitter = {", "};

        internal static string GetRuntimeReader(Type type, TargetPlatform targetPlatform)
        {
            // Type full name
            string typeFullName = type.FullName;

            // Assembly name tokenized
            string fullAssemblyName = type.Assembly.FullName;
            string[] assemblyTokens = fullAssemblyName.Split(assemblySplitter, StringSplitOptions.None);

            return
                //typeFullName + ", " + assemblyTokens[0] + ", " + assemblyTokens[1] + ", " +
                //    assemblyTokens[2] + ", " + GetAssemblyPublicKey(targetPlatform);
                typeFullName + ", " + "PloobsEngineDebug" + ", " + assemblyTokens[1] + ", " +
                    assemblyTokens[2] + ", " + GetAssemblyPublicKey(targetPlatform);
        }

        internal static string GetAssemblyPublicKey(TargetPlatform targetPlatform)
        {
            string publicKey = "PublicKeyToken=";

            switch (targetPlatform)
            {
                case TargetPlatform.Windows:
                    publicKey += windowsPublicKeyTokens;
                    break;

                case TargetPlatform.Xbox360:
                    //publicKey += xboxPublicKeyTokens;
                    throw new Exception("XBOX NOT SUPPORTED");
                    break;

                default:
                    throw new ArgumentException("targetPlatform");
            }

            return publicKey;
        }
    }
}