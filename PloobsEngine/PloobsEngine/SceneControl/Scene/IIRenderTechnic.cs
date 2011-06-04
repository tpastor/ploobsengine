using System;
namespace PloobsEngine.SceneControl.Scene
{
    interface IIRenderTechnic
    {
        void AddPostEffect(global::PloobsEngine.SceneControl.IPostEffect postEffect);
        bool ContainsPostEffect(global::PloobsEngine.SceneControl.IPostEffect postEffect);
        void RemovePostEffect(global::PloobsEngine.SceneControl.IPostEffect postEffect);
    }
}
