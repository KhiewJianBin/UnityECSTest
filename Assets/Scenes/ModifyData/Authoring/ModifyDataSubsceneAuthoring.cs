using UnityEngine;
using Unity.Entities;

namespace ModifyData
{
    /// <summary>
    /// Setup World
    /// </summary>
    public class ModifyDataSubSceneAuthoring : MonoBehaviour
    {
        class Baker : Baker<ModifyDataSubSceneAuthoring>
        {
            public override void Bake(ModifyDataSubSceneAuthoring authoring)
            {
                // 1. Setup The Game's Initial Systems Here
                var rotateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<RotateSystem>();
                //var rotateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<RotateManagedSystem>();


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