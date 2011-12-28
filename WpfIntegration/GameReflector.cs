using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngineIntegration
{
    internal static class GameReflector
    {
        internal static void CreateGame(Game game, Visual visual)
        {
            var deviceManager = GetGraphicsDeviceManager(game);

            deviceManager.PreparingDeviceSettings += (sender, e) =>
            {
#if RESIZABLE
                //If using a RenderTarget2D for drawing, the GraphicsDevice buffer can be whatever size it needs to be
                e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = 4096;
                e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = 4096;
#else
                //If using the GraphicsDevice for drawing, create with the standard XNA back buffer dimensions
                e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = 800;
                e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = 600;
#endif
                e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.DiscardContents;
                e.GraphicsDeviceInformation.PresentationParameters.DepthStencilFormat = DepthFormat.Depth24Stencil8;
                e.GraphicsDeviceInformation.PresentationParameters.BackBufferFormat = SurfaceFormat.Color;
                e.GraphicsDeviceInformation.PresentationParameters.IsFullScreen = false;
                e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = (PresentationSource.FromVisual(visual) as HwndSource).Handle;
            };

            //A non-public method which creates the GraphicsDevice and performs other initializations
            var changeDevice = deviceManager.GetType().GetMethod("ChangeDevice", BindingFlags.NonPublic | BindingFlags.Instance);
            changeDevice.Invoke(deviceManager, new object[] { true });

            var initialize = game.GetType().GetMethod("Initialize", BindingFlags.NonPublic | BindingFlags.Instance);
            initialize.Invoke(game, new object[] { });
        }

        private static GraphicsDeviceManager GetGraphicsDeviceManager(Game game)
        {
            foreach (var field in game.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType == typeof(GraphicsDeviceManager))
                {
                    return (GraphicsDeviceManager)field.GetValue(game);
                }
            }

            throw new InvalidOperationException("Game contains no GraphicsDeviceManager");
        }

        internal static IntPtr GetRenderTargetSurface(RenderTarget2D renderTarget)
        {
            IntPtr surfacePointer;
            var texture = GetIUnknownObject<IDirect3DTexture9>(renderTarget);            
            Marshal.ThrowExceptionForHR(texture.GetSurfaceLevel(0, out surfacePointer));
            Marshal.ReleaseComObject(texture);
            return surfacePointer;
        }

        internal static IntPtr GetGraphicsDeviceSurface(GraphicsDevice graphicsDevice)
        {
            IntPtr surfacePointer;
            var device = GetIUnknownObject<IDirect3DDevice9>(graphicsDevice);
            Marshal.ThrowExceptionForHR(device.GetBackBuffer(0, 0, 0, out surfacePointer));
            Marshal.ReleaseComObject(device);
            return surfacePointer;
        }

        internal static T GetIUnknownObject<T>(object container)
        {
            unsafe
            {
                //Get the COM object pointer from the D3D object and marshal it as one of the interfaces defined below
                var deviceField = container.GetType().GetField("pComPtr", BindingFlags.NonPublic | BindingFlags.Instance);
                var devicePointer = new IntPtr(Pointer.Unbox(deviceField.GetValue(container)));
                return (T)Marshal.GetObjectForIUnknown(devicePointer);
            }
        }

        [ComImport, Guid("85C31227-3DE5-4f00-9B3A-F11AC38C18B5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IDirect3DTexture9
        {
            void GetDevice();
            void SetPrivateData();
            void GetPrivateData();
            void FreePrivateData();
            void SetPriority();
            void GetPriority();
            void PreLoad();
            void GetType();
            void SetLOD();
            void GetLOD();
            void GetLevelCount();
            void SetAutoGenFilterType();
            void GetAutoGenFilterType();
            void GenerateMipSubLevels();
            void GetLevelDesc();
            int GetSurfaceLevel(uint level, out IntPtr surfacePointer);
        }

        [ComImport, Guid("D0223B96-BF7A-43fd-92BD-A43B0D82B9EB"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDirect3DDevice9
        {
            void TestCooperativeLevel();
            void GetAvailableTextureMem();
            void EvictManagedResources();
            void GetDirect3D();
            void GetDeviceCaps();
            void GetDisplayMode();
            void GetCreationParameters();
            void SetCursorProperties();
            void SetCursorPosition();
            void ShowCursor();
            void CreateAdditionalSwapChain();
            void GetSwapChain();
            void GetNumberOfSwapChains();
            void Reset();
            void Present();
            int GetBackBuffer(uint swapChain, uint backBuffer, int type, out IntPtr backBufferPointer);
        }
    }
}
