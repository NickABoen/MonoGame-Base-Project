using System.Collections.Generic;
using Component_Based_Base_Game.Components;

/// <summary>
/// Singleton Class for managing components and their subscribers. Meant to streamline
/// certain processes like entity deletion and globally accessing components.
/// </summary>
public class ComponentManagementSystem
{
    /// <summary>
    /// Singleton Instance for global static access to System methods
    /// </summary>
    private static ComponentManagementSystem _instance;

    public static ComponentManagementSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ComponentManagementSystem();
            }
            return _instance;
        }
    }

    //Running list of Components to be managed
    public RenderComponent RenderComponent;
	
    /// <summary>
    /// Initializes all components, this is also useful for
    /// reseting the game as this is one step in clearing all entities
    /// </summary>
    public ComponentManagementSystem()
    {
        RenderComponent = new RenderComponent();
    }

    /// <summary>
    /// Removes a single entity from the components that it subscribes to
    /// </summary>
    /// <param name="elementID">ID of the entity to remove</param>
	public void Remove(uint elementID)
	{
        //TODO: Remove element from Components that it is subscribed to
        /*
		PositionComponent.Remove(elementID);
        */
	}
}
