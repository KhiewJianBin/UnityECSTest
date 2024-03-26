using System;
using Unity.Entities;

/// <summary>
/// Example of Creating a Custom World
/// Note: Can only have One Class that uses ICustomBootstrap, Remeber Check Packages for conflicts (Netcode for entities)
/// Note: Unity looks for this by Reflection
/// #UNITY_DISABLE_AUTOMATIC_SYSTEM_BOOTSTRAP
/// #UNITY_DISABLE_AUTOMATIC_SYSTEM_BOOTSTRAP_EDITOR_WORLD  
/// #UNITY_DISABLE_AUTOMATIC_SYSTEM_BOOTSTRAP_RUNTIME_WORLD 
/// </summary>

public class CustomWorld : ICustomBootstrap
{
    public bool Initialize(string defaultWorldName)
    {
        // 1. Create World
        World customWorld = new World("Custom World");

        // 2. Add Systems
        Type[] systems = new Type[] 
        { 
            typeof(RotateSystem) 
        };
        DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(customWorld, systems);

        // 3. Add world to PlayerLoop to Update the world
        ScriptBehaviourUpdateOrder.AppendWorldToCurrentPlayerLoop(customWorld);

        // 4. Set the Default World
        World.DefaultGameObjectInjectionWorld = customWorld;

        // 5a. return true if we had setup a default world
        // 5b. return false if we want unity to setup default world.
        return false;
    }
}