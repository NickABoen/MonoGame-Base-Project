using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Component_Based_Base_Game
{
    /// <summary>
    /// Allows for meta management of components. See ComponentManagementSystem.cs
    /// </summary>
    public interface IGameComponent
    {
        void Remove(uint elementID);

        bool Contains(uint elementID);

        int Count();
    }
}
