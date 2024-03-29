using UnityEngine;
using Unity.Entities;

namespace Toggle
{
    /// <summary>
    /// Setup World
    /// </summary>
    public class ToggleSubSceneAuthoring : MonoBehaviour
    {
        class Baker : Baker<ToggleSubSceneAuthoring>
        {
            public override void Bake(ToggleSubSceneAuthoring authoring)
            {
                // 1. Setup The Game's Initial Systems Here
                var initToggleSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<InitToggleSceneSystem>();
                var rotateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<RotateSystem>();

                // 2. Find Existing SystemGroup to add
                var SimSG = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SimulationSystemGroup>();

                // 3. Add System to Appropriate Group
                if (SimSG != null)
                {
                    SimSG.AddSystemToUpdateList(initToggleSystemHandle);
                    SimSG.AddSystemToUpdateList(rotateSystemHandle);
                }

                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<ToggleSubSceneLoaded>(entity);
            }
        }
    }
    public struct ToggleSubSceneLoaded : IComponentData { }
}