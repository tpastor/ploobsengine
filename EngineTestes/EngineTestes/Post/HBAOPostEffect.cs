using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine;
using PloobsEngine.Engine.Logger;
namespace EngineTestes.Post
{
        public class HorizonBasedAmbientOcclusionShader : IPostEffect
        {
            public HorizonBasedAmbientOcclusionShader()
                : base(PostEffectType.Deferred)
            {
            }

            
            // Number of Steps.
            // The far samplers have a lower effect in the result. This controls how faster their weight decay.
            private int numberSteps = 6;

            // Number of Directions.
            // The far samplers have a lower effect in the result. This controls how faster their weight decay.
            private int numberDirections = 10;

            // Contrast.
            private float contrast = 0.8f;

            // Line Attenuation.
            // The far samplers have a lower effect in the result. This controls how faster their weight decay.
            private float lineAttenuation = 1.2f;

            // Radius.
            // Bigger the radius more cache misses will occur. Be careful!!
            private float radius = 0.015f;

            // Angle Bias (grades).
            private float angleBias = 30f * 3.1416f / 180f;

            #region Properties
                        
            /// <summary>
            /// Number of Steps
            /// The far samplers have a lower effect in the result. This controls how faster their weight decay.
            /// </summary>
            public int NumberSteps
            {
                get { return numberSteps; }
                set
                {
                    numberSteps = value;
                    if (numberSteps <= 0)
                        numberSteps = 0;
                }
            } // NumberSteps

            /// <summary>
            /// Number of Directions
            /// The far samplers have a lower effect in the result. This controls how faster their weight decay.
            /// </summary>
            public int NumberDirections
            {
                get { return numberDirections; }
                set
                {
                    numberDirections = value;
                    if (numberDirections <= 0)
                        numberDirections = 0;
                }
            } // NumberDirections

            /// <summary>
            /// Contrast.
            /// </summary>
            public float Contrast
            {
                get { return contrast; }
                set
                {
                    contrast = value;
                    if (contrast <= 0)
                        contrast = 0;
                }
            } // Contrast

            /// <summary>
            /// Line Attenuation.
            /// </summary>
            public float LineAttenuation
            {
                get { return lineAttenuation; }
                set
                {
                    lineAttenuation = value;
                    if (lineAttenuation <= 0)
                        lineAttenuation = 0;
                }
            } // Line Attenuation

            /// <summary>
            /// Radius.
            /// Bigger the radius more cache misses will occur. Be careful!!
            /// </summary>
            public float Radius
            {
                get { return radius; }
                set
                {
                    radius = value;
                    if (radius <= 0)
                        radius = 0;
                    if (radius > 1)
                        radius = 1;
                }
            } // Radius

            /// <summary>
            /// Angle Bias (grades)
            /// </summary>
            public float AngleBias
            {
                get { return angleBias; }
                set
                {
                    angleBias = value * 3.1416f / 180f;
                    if (angleBias <= 0)
                        angleBias = 0;
                    if (angleBias > 1)
                        angleBias = 1;
                }
            } // AngleBias

            #endregion


            #region Shader Parameters

            #region Handles

            /// <summary>
            /// Effect handles
            /// </summary>
            private static EffectParameter  // Textures
                                            epNormalTexture,
                                            epDepthTexture,
                // Parameters
                                            epResolution,
                                            epInverseResolution,
                                            epNumberSteps,
                                            epNumberDirections,
                                            epContrast,
                                            epLineAttenuation,
                                            epAngleBias,
                                            epRadius,
                                            ivp,
                // Others
                                            epFocalLength,
                                            epInvFocalLength,
                                            epHalfPixel,
                                            epSqrRadius,
                                            epInvRadius,
                                            epTanAngleBias;

            #endregion

            #region Resolution

            private static Vector2? lastUsedResolution;
            private static void SetResolution(Vector2 _resolution)
            {
                if (lastUsedResolution != _resolution)
                {
                    lastUsedResolution = _resolution;
                    epResolution.SetValue(_resolution);
                }
            } // SetResolution

            #endregion

            #region Inverse Resolution

            private static Vector2? lastUsedInverseResolution;
            private static void SetInverseResolution(Vector2 _inverseResolution)
            {
                if (lastUsedInverseResolution != _inverseResolution)
                {
                    lastUsedInverseResolution = _inverseResolution;
                    epInverseResolution.SetValue(_inverseResolution);
                }
            } // SetInverseResolution

            #endregion

            #region Number Steps

            private static float? lastUsedNumberSteps;
            private static void SetNumberSteps(float _numberSteps)
            {
                if (lastUsedNumberSteps != _numberSteps)
                {
                    lastUsedNumberSteps = _numberSteps;
                    epNumberSteps.SetValue(_numberSteps);
                }
            } // SetNumberSteps

            #endregion

            #region Number Directions

            private static float? lastUsedNumberDirections;
            private static void SetNumberDirections(float _numberDirections)
            {
                if (lastUsedNumberDirections != _numberDirections)
                {
                    lastUsedNumberDirections = _numberDirections;
                    epNumberDirections.SetValue(_numberDirections);
                } // if        
            } // SetNumberDirections

            #endregion

            #region Contrast

            private static float? lastUsedContrast;
            private static void SetContrast(float _contrast)
            {
                if (lastUsedContrast != _contrast)
                {
                    lastUsedContrast = _contrast;
                    epContrast.SetValue(_contrast);
                }
            } // SetContrast

            #endregion

            #region Line Attenuation

            private static float? lastUsedLineAttenuation;
            private static void SetLineAttenuation(float _lineAttenuation)
            {
                if (lastUsedLineAttenuation != _lineAttenuation)
                {
                    lastUsedLineAttenuation = _lineAttenuation;
                    epLineAttenuation.SetValue(_lineAttenuation);
                } // if
            } // SetLineAttenuation

            #endregion

            #region Radius

            private static float? lastUsedRadius;
            private static void SetRadius(float _radius)
            {
                if (lastUsedRadius != _radius)
                {
                    lastUsedRadius = _radius;
                    epRadius.SetValue(_radius);
                }
            } // SetRadius

            #endregion

            #region AngleBias

            private static float? lastUsedAngleBias;
            private static void SetAngleBias(float _angleBias)
            {
                if (lastUsedAngleBias != _angleBias)
                {
                    lastUsedAngleBias = _angleBias;
                    epAngleBias.SetValue(_angleBias);
                } //
            } // SetAngleBias

            #endregion

            #region Half Pixel

            private static Vector2? lastUsedHalfPixel;
            private static void SetHalfPixel(Vector2 _halfPixel)
            {
                if (lastUsedHalfPixel != _halfPixel)
                {
                    lastUsedHalfPixel = _halfPixel;
                    epHalfPixel.SetValue(_halfPixel);
                }
            } // SetHalfPixel

            #endregion

            #region Focal Length

            private static Vector2? lastUsedFocalLength;
            private static void SetFocalLength(Vector2 focalLength)
            {
                if (lastUsedFocalLength != focalLength)
                {
                    lastUsedFocalLength = focalLength;
                    epFocalLength.SetValue(focalLength);
                }
            } // SetFocalLength

            #endregion

            #region Inverse Focal Length

            private static Vector2? lastUsedInverseFocalLength;
            private static void SetInverseFocalLength(Vector2 inverseFocalLength)
            {
                if (lastUsedInverseFocalLength != inverseFocalLength)
                {
                    lastUsedInverseFocalLength = inverseFocalLength;
                    epInvFocalLength.SetValue(inverseFocalLength);
                }
            } // SetInverseFocalLength

            #endregion

            #region Square Radius

            private static float? lastUsedSquareRadius;
            private static void SetSquareRadius(float squareRadius)
            {
                if (lastUsedSquareRadius != squareRadius)
                {
                    lastUsedSquareRadius = squareRadius;
                    epSqrRadius.SetValue(squareRadius);
                } // if
            } // SetSquareRadius

            #endregion

            #region Inverse Radius

            private static float? lastUsedInverseRadius;
            private static void SetInverseRadius(float inverseRadius)
            {
                if (lastUsedInverseRadius != inverseRadius)
                {
                    lastUsedInverseRadius = inverseRadius;
                    epInvRadius.SetValue(inverseRadius);
                }
            } // SetInverseRadius

            #endregion

            #region Tangent Angle Bias

            private static float? lastUsedTanAngleBias;
            private static void SetTanAngleBias(float tanAngleBias)
            {
                if (lastUsedTanAngleBias != tanAngleBias)
                {
                    lastUsedTanAngleBias = tanAngleBias;
                    epTanAngleBias.SetValue(tanAngleBias);
                }
            } // SetTanAngleBias

            #endregion


            #endregion


            Effect shader;
            RenderTarget2D RenderTarget2D;
            RenderTarget2D RenderTarget2D2;
            public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
            {
                shader = factory.GetEffect("HBAO\\teste");
                Texture randomNormalTexture = factory.GetTexture2D("HBAO\\RandomNormal");
                shader.Parameters["randomTexture"].SetValue(randomNormalTexture);

                ivp = shader.Parameters["InvertViewProjection"];
                epResolution = shader.Parameters["resolution"];
                epInverseResolution = shader.Parameters["invResolution"];
                epNumberSteps = shader.Parameters["numberSteps"];
                epNumberDirections = shader.Parameters["numberDirections"];
                epContrast = shader.Parameters["contrast"];
                epLineAttenuation = shader.Parameters["attenuation"];
                epRadius = shader.Parameters["radius"];
                epAngleBias = shader.Parameters["angleBias"];
                // Others
                epFocalLength = shader.Parameters["focalLength"];
                epInvFocalLength = shader.Parameters["invFocalLength"];
                epHalfPixel = shader.Parameters["halfPixel"];
                epSqrRadius = shader.Parameters["sqrRadius"];
                epInvRadius = shader.Parameters["invRadius"];
                epTanAngleBias = shader.Parameters["tanAngleBias"];

                RenderTarget2D = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
                RenderTarget2D2 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);

                g = new GaussianBlurPostEffect();
                g.Init(ginfo, factory);

                ssaofinal = factory.GetEffect("ssaofinal", false, true);
            }

            Effect ssaofinal;
            GaussianBlurPostEffect g;
            #region Render

            public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
            {

                // Set shader atributes
                //SetNormalTexture(rHelper[PrincipalConstants.normalRt]);
                shader.Parameters["depthMap"].SetValue(rHelper[PrincipalConstants.DephRT]);
                shader.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(world.CameraManager.ActiveCamera.Projection));

                // It works a the depth texture resolution. Use a downsampled version of the G-Buffer.
                SetResolution(new Vector2(GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight));
                SetInverseResolution(new Vector2(1 / (float)GraphicInfo.BackBufferWidth, 1 / (float)GraphicInfo.BackBufferHeight));
                SetNumberSteps(NumberSteps );
                SetNumberDirections(NumberDirections );
                SetContrast(Contrast  / (1.0f - (float)Math.Sin(AngleBias)));
                SetLineAttenuation(LineAttenuation);
                SetRadius(Radius  );
                SetAngleBias(AngleBias);
                SetHalfPixel(new Vector2(-1f / GraphicInfo.BackBufferWidth, 1f / GraphicInfo.BackBufferHeight));
                float fieldOfView = world.CameraManager.ActiveCamera.FieldOfView;
                Vector2 focalLen = new Vector2
                {
                    X = 1.0f / (float)Math.Tan(fieldOfView * (3.1416f / 180) * 0.5f) * (float)GraphicInfo.BackBufferHeight / (float)GraphicInfo.BackBufferWidth,
                    Y = 1.0f / (float)Math.Tan(fieldOfView * (3.1416f / 180) * 0.5f)
                };
                SetFocalLength(focalLen);
                SetInverseFocalLength(new Vector2(1 / focalLen.X, 1 / focalLen.Y));
                SetSquareRadius(AngleBias * AngleBias);
                SetInverseRadius(1 / AngleBias);
                SetTanAngleBias((float)Math.Tan(AngleBias));                
                
                rHelper.PushRenderTarget(RenderTarget2D);
                rHelper.Clear(Color.White);
                rHelper.RenderFullScreenQuadVertexPixel(shader,SamplerState.PointClamp);
                Texture2D t = rHelper.PopRenderTargetAsSingleRenderTarget2D();

                rHelper.PushRenderTarget(RenderTarget2D2);
                rHelper.Clear(Color.Black);
                g.Draw(t, rHelper, gt, GraphicInfo, world, useFloatBuffer);
                t = rHelper.PopRenderTargetAsSingleRenderTarget2D();
                
                ssaofinal.Parameters["SSAOTex"].SetValue(t);
                ssaofinal.Parameters["SceneTexture"].SetValue(ImageToProcess);
                ssaofinal.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
                ssaofinal.Parameters["weight"].SetValue(1);
                if (useFloatBuffer)
                    rHelper.RenderFullScreenQuadVertexPixel(ssaofinal, SamplerState.PointClamp);
                else
                    rHelper.RenderFullScreenQuadVertexPixel(ssaofinal, GraphicInfo.SamplerState);              

            }

            #endregion

        } // HorizonBasedAmbientOcclusionShader
    } // XNAFinalEngine.Graphics

