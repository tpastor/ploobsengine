using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Material
{
    public class BasicMaterialDecorator : IMaterial
    {
        public BasicMaterialDecorator(IMaterial material, RasterizerState RasterizerState = null, BlendState BlenderState = null, SamplerState[] SamplerState = null, int[] samplePositions = null)
        {
            System.Diagnostics.Debug.Assert(SamplerState.Count() == samplePositions.Count());
            this.material = material;
            this.RasterizerState = RasterizerState;
            this.SamplerState = SamplerState;
            this.SamplePositions = samplePositions;
            this.BlendState = BlendState;
        }

        public BlendState BlendState
        {
            get;
            set;
        }

        public SamplerState[] SamplerState
        {
            set;
            get;
        }
        public int[] SamplePositions
        {
            get;
            set;
        }

        public RasterizerState RasterizerState
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
        Dictionary<int, SamplerState> samplers = new Dictionary<int, SamplerState>();
        public void Drawn(Microsoft.Xna.Framework.GameTime gt, SceneControl.IObject obj, Cameras.ICamera cam, IList<Light.ILight> lights, SceneControl.RenderHelper render)
        {
            if (SamplePositions != null)
            {
                for (int i = 0; i < SamplePositions.Count(); i++)
                {
                    render.SetSamplerState(SamplerState[i], SamplePositions[i]);
                    samplers[SamplePositions[i]] =  SamplerState[i];
                }
            }

            if(BlendState != null)
                render.PushBlendState(BlendState);
            if(RasterizerState!= null)
                render.PushRasterizerState(RasterizerState);
            material.Drawn(gt, obj, cam, lights, render);

            if(RasterizerState!= null)
                render.PopRasterizerState();
            if(BlendState!=null)
                render.PopBlendState();

            if (SamplePositions != null)
            {
                for (int i = 0; i < SamplePositions.Count(); i++)
                {
                    render.SetSamplerState(samplers[SamplePositions[i]], SamplePositions[i]);
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
#if (WINDOWS || XBOX) && !REACH
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
#endif

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

        #region IMaterial Members


        public void AfterAdded(SceneControl.IObject obj)
        {
        }

        #endregion
    }
}
