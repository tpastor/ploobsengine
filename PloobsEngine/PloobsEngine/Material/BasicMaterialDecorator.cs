using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Material
{
    public class BasicMaterialDecorator : IMaterial
    {
        public BasicMaterialDecorator(IMaterial material, RasterizerState RasterizerState, SamplerState[] SamplerState = null, int[] samplePositions = null)
        {
            System.Diagnostics.Debug.Assert(SamplerState.Count() == samplePositions.Count());
            this.material = material;
            this.RasterizerState = RasterizerState;
            this.SamplerState = SamplerState;
            this.SamplePositions = samplePositions;
        }

        SamplerState[] SamplerState
        {
            set;
            get;
        }
        int[] SamplePositions
        {
            get;
            set;
        }

        RasterizerState RasterizerState
        {
            get;
            set;
        }
        IMaterial material;

        #region IMaterial Members

        public void Initialization(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, SceneControl.IObject obj)
        {
            material.Initialization(ginfo, factory, obj);
        }

        public void PreDrawnPhase(Microsoft.Xna.Framework.GameTime gt, SceneControl.IWorld mundo, SceneControl.IObject obj, Cameras.ICamera cam, IList<Light.ILight> lights, SceneControl.RenderHelper render)
        {
            material.PreDrawnPhase(gt, mundo, obj, cam, lights, render);
        }

        public void PosDrawnPhase(Microsoft.Xna.Framework.GameTime gt, SceneControl.IObject obj, Cameras.ICamera cam, IList<Light.ILight> lights, SceneControl.RenderHelper render)
        {
            material.PosDrawnPhase(gt, obj, cam, lights, render);
        }

        public void Drawn(Microsoft.Xna.Framework.GameTime gt, SceneControl.IObject obj, Cameras.ICamera cam, IList<Light.ILight> lights, SceneControl.RenderHelper render)
        {
            if (SamplePositions != null)
            {
                for (int i = 0; i < SamplePositions.Count(); i++)
                {
                    render.SetSamplerState(SamplerState[i], SamplePositions[i]);
                }
            }

            render.PushRasterizerState(RasterizerState);
            material.Drawn(gt, obj, cam, lights, render);
            render.PopRasterizerState();

            if (SamplePositions != null)
            {
                for (int i = 0; i < SamplePositions.Count(); i++)
                {
                    render.RestoreSamplerState(SamplePositions[i]);
                }
            }

        }

        public void Update(Microsoft.Xna.Framework.GameTime gametime, SceneControl.IObject obj, SceneControl.IWorld world)
        {
            material.Update(gametime, obj, world);
        }

        public IShader Shader
        {
            get
            {
                return material.Shader;
            }
            set
            {
                material.Shader = value;
            }
        }

        public MaterialType MaterialType
        {
            get { return material.MaterialType; }
        }

        public bool CanCreateShadow
        {
            get
            {
                return material.CanCreateShadow;
            }
            set
            {
                material.CanCreateShadow = value;
            }
        }

        public bool CanAppearOfReflectionRefraction
        {
            get
            {
                return material.CanAppearOfReflectionRefraction;
            }
            set
            {
                material.CanAppearOfReflectionRefraction = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return material.IsVisible;
            }
            set
            {
                material.IsVisible = value;
            }
        }

        public void CleanUp(Engine.GraphicFactory factory)
        {
            material.CleanUp(factory);
        }

        #endregion

#if WINDOWS
        #region ISerializable Members

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            
        }

        #endregion
#endif
    }
}
