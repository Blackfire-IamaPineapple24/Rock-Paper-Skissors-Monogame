using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace RockPaperScissors
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		Texture2D _rockTxr, _paperTxr, _scissorsTxr, _backgroundTxr;
		List<RPSSprite> _spriteList = new List<RPSSprite>(); // TODO - add code to initialise an empty list of RPSSprite ************************************************************
		Random _rng = new Random();

		// Store the size of the window in a Point
		Point _windowSize = new Point(1000, 1000);

		// Maximum number of sprites
		int _spriteTotal = 80;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// Set the window size.
			// TODO - resize the window using the window size variable ************************************************************

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load all the textures.
			// TODO - load in all the textures and store them as reference variables ************************************************************
		}

		protected override void Update(GameTime gameTime)
		{
			// If Escape is pressed, quit.
			if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

			// Add sprites until there are enough.
			while (false) // TODO - make a condition that adds sprites until the number of sprites matches _spriteTotal ************************************************************
				_spriteList.Add(new RPSSprite(_rockTxr, _paperTxr, _scissorsTxr, _rng, _windowSize));

			// Each sprite should call it's own Update() function.
			// They need the gameTime, and access to the sprite list and RNG.
			foreach (RPSSprite sprite in _spriteList) sprite.Update(gameTime, _spriteList, _rng);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			_spriteBatch.Begin();

			// Draw the packground image first
			_spriteBatch.Draw(_backgroundTxr, new Rectangle(0, 0, 1024, 1024), Color.White);

			// Each sprite should call it's own Draw() method.
			// They need the gameTime access to the sprite batch.
			//foreach (); // TODO - make a foreach loop that calls the Draw method for each sprite ************************************************************

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}

	// An enumerator, creating a list of sprite types
	enum SpriteType
	{
		Rock,
		Paper,
		Scissors
	}
}
