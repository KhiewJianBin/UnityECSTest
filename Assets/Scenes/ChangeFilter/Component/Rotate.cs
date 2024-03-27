using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ChangeFilter
{
    public struct Rotate : IComponentData, IEnableableComponent
    {
        [Tooltip("Rotate Speed in Deg X,Y,Z")]
        public float3 Deg;
    }
}