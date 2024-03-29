using UnityEngine;
using Unity.Entities;

namespace Follow
{
    public class FollowCubeAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject targetGO;

        class Baker : Baker<FollowCubeAuthoring>
        {
            public override void Bake(FollowCubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                var entitytarget = GetEntity(authoring.targetGO, TransformUsageFlags.Dynamic);

                FollowCube fc = new FollowCube
                {
                    Target = entitytarget
                };
                AddComponent(entity, fc);
            }
        }
    }
}