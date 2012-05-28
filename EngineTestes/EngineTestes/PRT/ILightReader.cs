
using System;
namespace EngineTestes
{
    public interface ILightProbeReader
    {
        Microsoft.Xna.Framework.Color LightAccess(Microsoft.Xna.Framework.Vector3 direction);
    }
}
