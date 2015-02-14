using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Component_Based_Base_Game.Interfaces
{
    /// <summary>
    /// Defines a general outline for a system and what it should have
    /// </summary>
    public interface ISystem
    {
        /// <summary>
        /// Called as many times as possible between frames to update the game state
        /// </summary>
        /// <param name="gameTime">Time elapsed since last update call</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Called once every frame to draw the current game state to the screen
        /// </summary>
        /// <param name="gameTime">Time elapsed since last draw call</param>
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
