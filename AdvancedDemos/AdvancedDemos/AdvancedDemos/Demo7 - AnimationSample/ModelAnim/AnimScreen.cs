using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Physics.Bepu;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Features;
using PloobsEngine.Commands;
using PloobsEngine.Loader;
using PloobsEngine.Modelo.Animation;
using PloobsEngine.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Animation Screen
    /// </summary>
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class AnimScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-9.8f,true,1), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);
        }

        // Shortcut references to the bones that we are going to animate.
        // We could just look these up inside the Draw method, but it is more
        // efficient to do the lookups while loading and cache the results.
        ModelBone leftBackWheelBone;
        ModelBone rightBackWheelBone;
        ModelBone leftFrontWheelBone;
        ModelBone rightFrontWheelBone;
        ModelBone leftSteerBone;
        ModelBone rightSteerBone;
        ModelBone turretBone;
        ModelBone cannonBone;
        ModelBone hatchBone;


        // Store the original transform matrix for each animating bone.
        Matrix leftBackWheelTransform;
        Matrix rightBackWheelTransform;
        Matrix leftFrontWheelTransform;
        Matrix rightFrontWheelTransform;
        Matrix leftSteerTransform;
        Matrix rightSteerTransform;
        Matrix turretTransform;
        Matrix cannonTransform;
        Matrix hatchTransform;


        // Array holding all the bone transform matrices for the entire model.
        // We could just allocate this locally inside the Draw method, but it
        // is more efficient to reuse a single array, as this avoids creating
        // unnecessary garbage.
        Matrix[] boneTransforms;


        // Current animation positions.
        float wheelRotationValue;
        float steerRotationValue;
        float turretRotationValue;
        float cannonRotationValue;
        float hatchRotationValue;

        FullAnimatedModel simpleModel;
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
           {
                simpleModel = new FullAnimatedModel(factory, "Model//tank");
                GhostObject tmesh = new GhostObject();
                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);

                Model tankModel = simpleModel.model;

                // Look up shortcut references to the bones we are going to animate.
                leftBackWheelBone = tankModel.Bones["l_back_wheel_geo"];
                rightBackWheelBone = tankModel.Bones["r_back_wheel_geo"];
                leftFrontWheelBone = tankModel.Bones["l_front_wheel_geo"];
                rightFrontWheelBone = tankModel.Bones["r_front_wheel_geo"];
                leftSteerBone = tankModel.Bones["l_steer_geo"];
                rightSteerBone = tankModel.Bones["r_steer_geo"];
                turretBone = tankModel.Bones["turret_geo"];
                cannonBone = tankModel.Bones["canon_geo"];
                hatchBone = tankModel.Bones["hatch_geo"];

                // Store the original transform matrix for each animating bone.
                leftBackWheelTransform = leftBackWheelBone.Transform;
                rightBackWheelTransform = rightBackWheelBone.Transform;
                leftFrontWheelTransform = leftFrontWheelBone.Transform;
                rightFrontWheelTransform = rightFrontWheelBone.Transform;
                leftSteerTransform = leftSteerBone.Transform;
                rightSteerTransform = rightSteerBone.Transform;
                turretTransform = turretBone.Transform;
                cannonTransform = cannonBone.Transform;
                hatchTransform = hatchBone.Transform;

                // Allocate the transform matrix array.
                boneTransforms = new Matrix[tankModel.Bones.Count];
            }

            #region NormalLight
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.5f;
            ld1.LightIntensity = li;
            ld2.LightIntensity = li;
            ld3.LightIntensity = li;
            ld4.LightIntensity = li;
            ld5.LightIntensity = li;
            this.World.AddLight(ld1);
            this.World.AddLight(ld2);
            this.World.AddLight(ld3);
            this.World.AddLight(ld4);
            this.World.AddLight(ld5);
            #endregion
            
            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(40), MathHelper.ToRadians(-15), new Vector3(500, 500, 500), GraphicInfo);
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grasscube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }

        protected override void Update(GameTime gameTime)
        {

            float time = (float)gameTime.TotalGameTime.TotalSeconds;

            // Update the animation properties on the tank object. In a real game
            // you would probably take this data from user inputs or the physics
            // system, rather than just making everything rotate like this!

            wheelRotationValue = time * 5;
            steerRotationValue = (float)Math.Sin(time * 0.75f) * 0.5f;
            turretRotationValue = (float)Math.Sin(time * 0.333f) * 1.25f;
            cannonRotationValue = (float)Math.Sin(time * 0.25f) * 0.333f - 0.333f;
            hatchRotationValue = MathHelper.Clamp((float)Math.Sin(time * 2) * 2, -1, 0);
            
            // Calculate matrices based on the current animation position.
            Matrix wheelRotation = Matrix.CreateRotationX(wheelRotationValue);
            Matrix steerRotation = Matrix.CreateRotationY(steerRotationValue);
            Matrix turretRotation = Matrix.CreateRotationY(turretRotationValue);
            Matrix cannonRotation = Matrix.CreateRotationX(cannonRotationValue);
            Matrix hatchRotation = Matrix.CreateRotationX(hatchRotationValue);

            // Apply matrices to the relevant bones.
            leftBackWheelBone.Transform = wheelRotation * leftBackWheelTransform;
            rightBackWheelBone.Transform = wheelRotation * rightBackWheelTransform;
            leftFrontWheelBone.Transform = wheelRotation * leftFrontWheelTransform;
            rightFrontWheelBone.Transform = wheelRotation * rightFrontWheelTransform;
            leftSteerBone.Transform = steerRotation * leftSteerTransform;
            rightSteerBone.Transform = steerRotation * rightSteerTransform;
            turretBone.Transform = turretRotation * turretTransform;
            cannonBone.Transform = cannonRotation * cannonTransform;
            hatchBone.Transform = hatchRotation * hatchTransform;

            ///MUST BE CALLED, to make current changes avaliable to the model
            simpleModel.UpdateTransformations();

            base.Update(gameTime);
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            render.RenderTextComplete("Animation Sample - Non Skinned Meshes Animation", new Vector2(10, 15), Color.White, Matrix.Identity);
            
        }

    }
}
