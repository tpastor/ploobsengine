using System;
using Microsoft.Xna.Framework;
namespace PloobsEngine.SceneControl._2DScene
{
    public interface ICamera2D
    {
        Vector2 ConvertScreenToWorld(Vector2 location);
        Vector2 ConvertWorldToScreen(Vector2 location);
        void MoveCamera(Microsoft.Xna.Framework.Vector2 amount);
        Microsoft.Xna.Framework.Vector2 Position { get; set; }
        void RotateCamera(float amount);
        float Rotation { get; set; }
        Microsoft.Xna.Framework.Matrix SimProjection { get; }
        Microsoft.Xna.Framework.Matrix SimView { get; }
        void Update(Microsoft.Xna.Framework.GameTime gameTime);
        Microsoft.Xna.Framework.Matrix View { get; }
        float Zoom { get; set; }
    }
}
