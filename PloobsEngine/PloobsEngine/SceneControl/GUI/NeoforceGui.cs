using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomShane.Neoforce.Controls;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.SceneControl.GUI
{
    public class NeoforceGui : IGui
    {
        public NeoforceGui(String skin = null)
        {
            this.skin = skin;
        }

        String skin;
        Manager manager;

        public Manager Manager
        {
            get { return manager; }            
        }

        protected override void Dispose()
        {
            manager.Dispose();
        }

        protected override void Initialize(Engine.EngineStuff engine, Engine.GraphicFactory factory, Engine.GraphicInfo ginfo)
        {
            if(skin != null)
                manager = new Manager(engine, skin);     
            else
                manager = new Manager(engine);     
            if(skin!= null)
                Manager.SkinDirectory = @"Content\";       
            else
                Manager.SkinDirectory = @"Content\Skins";

            manager.Initialize();
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            manager.Update(gt);
        }

        protected override void EndDraw(Microsoft.Xna.Framework.GameTime gt, RenderHelper render, Engine.GraphicInfo ginfo)
        {
            manager.EndDraw();
            render.ResyncStates();
        }

        protected override void BeginDraw(Microsoft.Xna.Framework.GameTime gt, RenderHelper render, Engine.GraphicInfo ginfo)
        {
            manager.BeginDraw(gt);
            render.ResyncStates();
        }

    }
}
