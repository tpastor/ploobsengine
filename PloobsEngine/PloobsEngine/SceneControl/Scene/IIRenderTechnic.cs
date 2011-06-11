using System;
namespace PloobsEngine.SceneControl.Scene
{
    public interface IIRenderTechnic
    {
#if !WINDOWS_PHONE
        void AddPostEffect(global::PloobsEngine.SceneControl.IPostEffect postEffect);
        bool ContainsPostEffect(global::PloobsEngine.SceneControl.IPostEffect postEffect);
        void RemovePostEffect(global::PloobsEngine.SceneControl.IPostEffect postEffect);
#endif
    }
}
