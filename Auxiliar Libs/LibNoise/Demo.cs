using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using LibNoise.Xna;
using LibNoise.Xna.Generator;
using LibNoise.Xna.Operator;

namespace LibNoise.Xna.Demo
{
	public class LibNoiseXnaDemo : Microsoft.Xna.Framework.Game
	{
		#region Fields

		private GraphicsDeviceManager m_graphics = null;
		private SpriteBatch m_spriteBatch = null;
		private Noise2D m_noiseMap = null;
		private Texture2D[] m_textures = new Texture2D[3];

		#endregion

		#region Constructors

		public LibNoiseXnaDemo()
		{
			this.m_graphics = new GraphicsDeviceManager(this);
			this.Content.RootDirectory = "Content";
		}

		#endregion

		#region Game Members

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			this.m_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Create the module network
			Perlin perlin = new Perlin();
			RiggedMultifractal rigged = new RiggedMultifractal();
			Add add = new Add(perlin, rigged);

			// Initialize the noise map
			this.m_noiseMap = new Noise2D(64, 64, add);
			this.m_noiseMap.GeneratePlanar(-1, 1, -1, 1);

			// Generate the textures
			this.m_textures[0] = this.m_noiseMap.GetTexture(this.m_graphics.GraphicsDevice, Gradient.Grayscale);
			this.m_textures[1] = this.m_noiseMap.GetTexture(this.m_graphics.GraphicsDevice, Gradient.Terrain);
			this.m_textures[2] = this.m_noiseMap.GetNormalMap(this.m_graphics.GraphicsDevice, 3.0f);

			// Zoom in or out do something like this.
			float zoom = 0.5f;
			this.m_noiseMap.GeneratePlanar(-1 * zoom, 1 * zoom, -1 * zoom, 1 * zoom);
		}

		protected override void UnloadContent()
		{
			this.m_noiseMap.Dispose();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) { this.Exit(); }
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			int w = this.m_graphics.GraphicsDevice.Viewport.Width / 3;
			GraphicsDevice.Clear(Color.Black);
			this.m_spriteBatch.Begin();
			this.m_spriteBatch.Draw(this.m_textures[0], new Rectangle(0, 0, w, w), Color.White);
			this.m_spriteBatch.Draw(this.m_textures[1], new Rectangle(w, 0, w, w), Color.White);
			this.m_spriteBatch.Draw(this.m_textures[2], new Rectangle(w * 2, 0, w, w), Color.White);
			this.m_spriteBatch.End();
			base.Draw(gameTime);
		}

		#endregion
	}

	static class Program
	{
		#region Methods

		static void Main(string[] args)
		{
			using (LibNoiseXnaDemo game = new LibNoiseXnaDemo()) { game.Run(); }
		}

		#endregion
	}
}