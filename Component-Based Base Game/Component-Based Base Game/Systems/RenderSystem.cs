using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Component_Based_Base_Game.Interfaces;
using Component_Based_Base_Game.Components;

namespace Component_Based_Base_Game.Systems
{
    /// <summary>
    /// Singleton System for rendering visual components to the screen.
    /// </summary>
    public class RenderSystem : ISystem
    {
        /// <summary>
        /// Stored instance of the RenderSystem
        /// </summary>
        private static RenderSystem _instance;
        
        /// <summary>
        /// Property for global read access to the instance of the RenderSystem
        /// </summary>
        public static RenderSystem Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RenderSystem();

                return _instance;
            }
        }

        /// <summary>
        /// Constructor for the RenderSystem
        /// </summary>
        public RenderSystem()
        {

        }

        /// <summary>
        /// An update that occurs between frames. May be called more than once
        /// before anything is drawn
        /// </summary>
        /// <param name="gameTime">The time elapsed since the last update call</param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// The draw phase of the game loop. This is called exactly once per
        /// frame.
        /// </summary>
        /// <param name="gameTime">The time elapsed since the last draw call</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            RenderComponent RenderComponent = ComponentManagementSystem.Instance.RenderComponent;
            RenderObject renderObject;

            spriteBatch.Begin();
            foreach(uint id in RenderComponent.Keys)
            {
                renderObject = RenderComponent[id];

                if(renderObject.isVisible)
                {
                    spriteBatch.Draw(renderObject.Image, Vector2.Zero, Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}
