using System;
using PloobsEngine.Cameras;

namespace PloobsEngine.Physics
{
    /// <summary>
    /// Specification of Physic Debug Draw
    /// </summary>
    public interface IDebugDrawn
    {
        void Draw(Microsoft.Xna.Framework.GameTime gameTime, ICamera camera, IPhysicWorld world);
    }
}
