using Unity.Entities;
using UnityEngine;

namespace SharedFilter
{
    public class CubeAuthoring : MonoBehaviour
    {
        [SerializeField] int StartState = 0;

        class Baker : Baker<CubeAuthoring>
        {
            public override void Bake(CubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic); // Indicate to allow transform changes in runtime

                // Create & Add Rotate Component
                Rotate rotate = new Rotate()
                {
                    Speed = 1
                };
                AddComponent(entity, rotate);

                // Create & Add Shared Component
                var state = new CubeStateComponent();
                state.State = authoring.StartState;
                AddSharedComponent(entity, state);
            }
        }
    }
}