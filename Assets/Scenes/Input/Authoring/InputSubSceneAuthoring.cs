using UnityEngine;
using Unity.Entities;

namespace Input
{
    /// <summary>
    /// Setup World
    /// </summary>
    public class InputSubSceneAuthoring : MonoBehaviour
    {
        class Baker : Baker<InputSubSceneAuthoring>
        {
            public override void Bake(InputSubSceneAuthoring authoring)
            {
                // 1. Setup The Game's Initial Systems Here
                var rotateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<RotateSystem>();
                var inputUpdateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<InputUpdateSystem>();

                // 2. Find Existing SystemGroup to add
                var SimSG = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SimulationSystemGroup>();

                // 3. Add System to Appropriate Group
                if (SimSG != null)
                {
                    SimSG.AddSystemToUpdateList(rotateSystemHandle);
                    SimSG.AddSystemToUpdateList(inputUpdateSystemHandle);
                }

                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<InputState>(entity);
            }
        }
    }
}