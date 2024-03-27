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
                Rotate rotate = new Rotate
                {
                    Speed = new Vector3(1, 0, 0)
                };
                AddComponent(entity, rotate);
            }
        }
    }
}