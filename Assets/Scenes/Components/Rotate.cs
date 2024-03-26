using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Rotate : IComponentData, IEnableableComponent
{
    [Tooltip("Rotate Speed in Deg X,Y,Z")]
    public float3 Speed;
}