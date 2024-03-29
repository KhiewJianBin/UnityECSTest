using Unity.Entities;
using UnityEngine;

/// <summary>
/// Setup World
/// </summary>
public class GameAuthoring : MonoBehaviour
{
    class GameAuthoringBaker : Baker<GameAuthoring>
    {
        public override void Bake(GameAuthoring authoring)
        {
            // 1. Setup The Game's Initial Systems Here
            //var rotateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<RotateSystem>();
            var translateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<TranslateSystem>();
            
            // 2. Find Existing SystemGroup to add
            var rootSys1 = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<InitializationSystemGroup>();
            var rootSys2 = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SimulationSystemGroup>();
            var rootSys3 = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PresentationSystemGroup>();

            // 3. Add System to Appropriate Group
            if (rootSys2 != null)
            {
                //rootSys2.AddSystemToUpdateList(rotateSystemHandle);
                rootSys2.AddSystemToUpdateList(translateSystemHandle);
            }
        }
    }
}
