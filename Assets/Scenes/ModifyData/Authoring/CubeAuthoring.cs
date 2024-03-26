using Unity.Entities;
using UnityEngine;

namespace ModifyData
{
    public class CubeAuthoring : MonoBehaviour
    {
        class Baker : Baker<CubeAuthoring>
        {
            public override void Bake(CubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic); // Indicate to allow transform changes in runtime

                // Create & Add Rotate Component
                AddComponent(entity, new Rotate());
            }
        }
    }
}