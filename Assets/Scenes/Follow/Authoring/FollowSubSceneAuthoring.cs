using UnityEngine;
using Unity.Entities;

namespace Follow
{
    public class FollowSubSceneAuthoring : MonoBehaviour
    {
        class Baker : Baker<FollowSubSceneAuthoring>
        {
            public override void Bake(FollowSubSceneAuthoring authoring)
            {
                // 1. Setup The Game's Initial Systems Here
                var followCubeSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<FollowCubeSystem>();

                // 2. Find Existing SystemGroup to add
                var SimSG = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SimulationSystemGroup>();

                // 3. Add System to Appropriate Group
                if (SimSG != null)
                {
                    SimSG.AddSystemToUpdateList(followCubeSystemHandle);
                }
            }
        }
    }
}