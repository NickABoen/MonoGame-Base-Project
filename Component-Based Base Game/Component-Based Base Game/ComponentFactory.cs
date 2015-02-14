using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Component_Based_Base_Game.Components;
using Component_Based_Base_Game.Structure;

namespace Component_Based_Base_Game
{
    /// <summary>
    /// Global factory for creating all components in the game
    /// </summary>
    public static class ComponentFactory
    {
        public static uint CreatePlayer()
        {
            uint eid = IDManager.GetNewID();
            RenderObject renderObject = new RenderObject
            {
                EntityID = eid,
                isVisible = true,
                Image = AssetManager.LoadTexture(AssetNames.Player)
            };

            ComponentManagementSystem.Instance.RenderComponent.Add(eid, renderObject);

            return eid;
        }
    }
}
