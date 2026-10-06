using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace RockPaperScissors
{
	class RPSSprite
	{
		Texture2D _rockTxr, _paperTxr, _scissorsTxr;
		float _spriteSpeed = 2.0f;
		Rectangle _boundary, _drawRectangle;
		Vector2 _position, _velocity;
		SpriteType _currentType;
		bool dead = false;

		// Constructor
		// Needs to store all three RPS textures, as well as the RNG and window size
		//(Texture2D fireTxr, Texture2D lightningTxr, Texture2D waterTxr, Random rng, Point windowSize) // TODO - add the the right keywords to declare the constructor for RPSSprite ************************************************************
		{
			_rockTxr = fireTxr;
			_paperTxr = lightningTxr;
			_scissorsTxr = waterTxr;
			_currentType = (SpriteType)rng.Next(3);



			// Set the size of the boundary, from the top-left to the bottom-right, accounting for the size of the sprites
			_boundary = new Rectangle(0, 0, windowSize.X - CurrentTxr().Width, windowSize.Y - CurrentTxr().Height);

			// Set a random starting position for the sprite, within the boundary
			_position = new Vector2(rng.Next(_boundary.X, _boundary.X + _boundary.Width), rng.Next(_boundary.Y, _boundary.Y + _boundary.Height));

			// Set a random velocity for the sprite. Use a normalised vector, so all sprites move at the same speed.
			_velocity = _spriteSpeed * Vector2.Normalize(new Vector2((float)rng.NextDouble() - 0.5f, (float)rng.NextDouble() - 0.5f));
		}


		public void Update(GameTime gameTime, List<RPSSprite> spriteList, Random rng) // TODO - add the right keywords at the start of the line to declare the Update method ************************************************************
		{
			// Do not update if the sprite is dead
			if (!dead)
			{
				// Use the sprite list to check collision against all other sprites
				//foreach () // TODO - make a foreach loop that checks every sprite in the sprite list, using the temporary name "otherSprite" ************************************************************
				{
					// If we find another sprite that is not dead...
					if (!otherSprite.dead && otherSprite != this)
					{
						// ...check to see if we are overlapping with it...
						if (_drawRectangle.Intersects(otherSprite._drawRectangle))
							// ... and if we are, check to see if it beats us...
							if (_currentType == SpriteType.Rock && otherSprite._currentType == SpriteType.Paper
								|| _currentType == SpriteType.Paper && otherSprite._currentType == SpriteType.Scissors
								|| _currentType == SpriteType.Scissors && otherSprite._currentType == SpriteType.Rock
								)
								// ... and if we've found another sprite that is not dead,
								// is overlapping and is a type that beats us,
								// then this sprite is dead
								dead = true;

						// If two sprites of different types are almost touching, nudge their velocity towards each other.
						// This means that near misses are more likely to become hits.
						if (_currentType != otherSprite._currentType && (_position - otherSprite._position).Length() < CurrentTxr().Width * 1.5f)
							_velocity = _spriteSpeed * Vector2.Normalize(_velocity + (Vector2.Normalize(otherSprite._position - _position)));
					}
				}

				// Update the position using the velocity
				// _position // TODO - add code to update _position ************************************************************

				// Use the current position to update the draw rectangle, used in both Draw in the collision detection.
				_drawRectangle = new Rectangle((int)_position.X, (int)_position.Y, CurrentTxr().Width, CurrentTxr().Height);

				// If we've hit the boundary, bounce us off it
				if (_position.X <= _boundary.X || _position.X >= _boundary.X + _boundary.Width) _velocity.X *= -1;
				if (_position.Y <= _boundary.Y || _position.Y >= _boundary.Y + _boundary.Height) _velocity.Y *= -1;
			}
		}


		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			// If the sprite is dead, draw it as a transparent black ghost
			if (dead) spriteBatch.Draw(CurrentTxr(), _drawRectangle, new Color(Color.White, 255)); // TODO - Make the ghost sprites black and transparent ************************************************************
																								   // otherwise draw it normally.
			else spriteBatch.Draw(CurrentTxr(), _drawRectangle, Color.White);
		}

		// A function to select the correct texture for this sprite, based on it's type
		Texture2D CurrentTxr()
		{
			switch (_currentType)
			{
				case SpriteType.Rock:
					return _rockTxr;

				case SpriteType.Paper:
					return _paperTxr;

				default:
					return _scissorsTxr;
			}
		}
	}
}
