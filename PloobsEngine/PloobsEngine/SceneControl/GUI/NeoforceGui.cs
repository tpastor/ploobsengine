#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
#if !WINDOWS_PHONE && !REACH 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomShane.Neoforce.Controls;
using Microsoft.Xna.Framework.Graphics;
#if !XBOX360
using System.Windows.Forms;
#endif

namespace PloobsEngine.SceneControl.GUI
{
    /// <summary>
    /// Neoforce tomshade Gui implementation
    /// </summary>
    public class NeoforceGui : IGui
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NeoforceGui"/> class.
        /// </summary>
        /// <param name="skin">The skin. Null for default</param>
        public NeoforceGui(String skin = null)
        {
            this.skin = skin;
        }

        String skin;
        Manager manager;

        /// <summary>
        /// Gets the Neoforce manager.
        /// </summary>
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

#if !XBOX
            if(skin != null)
                manager = new Manager(engine, skin,ginfo.Window );     
            else
                manager = new Manager(engine, ginfo.Window);     
#else
            if(skin != null)
                manager = new Manager(engine, skin);     
            else
                manager = new Manager(engine);     
#endif

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

            manager.Draw(gt);
            render.ResyncStates();
        }

    }
}
#endif