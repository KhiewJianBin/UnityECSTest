using UnityEngine;
using Unity.Entities;

namespace Tag
{
    /// <summary>
    /// Setup World
    /// </summary>
    public class TagSubsceneAuthoring : MonoBehaviour
    {
        class Baker : Baker<TagSubsceneAuthoring>
        {
            public override void Bake(TagSubsceneAuthoring authoring)
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