using UnityEngine;
using Unity.Entities;

namespace ChangeFilter
{
    /// <summary>
    /// Setup World
    /// </summary>
    public class ChangeFilterSubSceneAuthoring : MonoBehaviour
    {
        class Baker : Baker<ChangeFilterSubSceneAuthoring>
        {
            public override void Bake(ChangeFilterSubSceneAuthoring authoring)
            {
                // 1. Setup The Game's Initial Systems Here
                var rotateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<RotateSystem>();

                // 2. Find Existing SystemGroup to add
                var InitSG = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<InitializationSystemGroup>();
                var SimSG = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SimulationSystemGroup>();
                var PresentationSG = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PresentationSystemGroup>();

                // 3. Add System to Appropriate Group
                if (SimSG != null)
                {
                    SimSG.AddSystemToUpdateList(rotateSystemHandle);
                }
            }
        }
    }
}