
using System;
namespace EngineTestes
{
    interface ILightProbeReader
    {
        Microsoft.Xna.Framework.Color LightAccess(Microsoft.Xna.Framework.Vector3 direction);
    }
}
