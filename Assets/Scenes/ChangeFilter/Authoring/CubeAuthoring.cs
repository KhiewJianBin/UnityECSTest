using Unity.Entities;
using UnityEngine;

namespace ChangeFilter
{
    public class CubeAuthoring : MonoBehaviour
    {
        [SerializeField] bool isPlayer = false;

        class Baker : Baker<CubeAuthoring>
        {
            public override void Bake(CubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic); // Indicate to allow transform changes in runtime

                // Create & Add Rotate Component
                AddComponent(entity, new Rotate());

                // Create & Add Player Tag Component
                if (authoring.isPlayer)
                {
                    AddComponent(entity, new PlayerTag());
                }
            }
        }
    }
}