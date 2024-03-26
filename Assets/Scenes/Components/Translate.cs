using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Translate : IComponentData , IEnableableComponent
{
    [Tooltip("Translate Speed in X,Y,Z")]
    public float3 Speed;
}