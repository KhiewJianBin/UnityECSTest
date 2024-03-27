using Unity.Entities;
using UnityEngine;

namespace CustomWorld
{
    public class CustomWorldSubSceneAuthoring : MonoBehaviour
    {
        class Baker : Baker<CustomWorldSubSceneAuthoring>
        {
            public override void Bake(CustomWorldSubSceneAuthoring authoring)
            {
                // 1. Setup The Game's Initial Systems Here
                var translateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<TranslateSystem>();
                var rotateSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<RotateSystem>();
                var scaleSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<ScaleSystem>();

                // 2. Find Existing SystemGroup to add
                var transformChangeSystemGroup = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<CustomSystemGroup>();

                // 3. Add System to Appropriate Group
                if (transformChangeSystemGroup != null)
                {
                    transformChangeSystemGroup.AddSystemToUpdateList(translateSystemHandle);
                    transformChangeSystemGroup.AddSystemToUpdateList(rotateSystemHandle);
                    transformChangeSystemGroup.AddSystemToUpdateList(scaleSystemHandle);
                }
            }
        }
    }
}
