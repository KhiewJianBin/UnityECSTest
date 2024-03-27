using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SharedFilter
{
    /// <summary>
    /// Unmanaged System
    /// </summary>
    [BurstCompile]
    [DisableAutoCreation] // Unity will not this System Automatically
    public partial struct RotateSystem : ISystem, ISystemStartStop
    {
        public void OnCreate(ref SystemState state) { }

        /// <summary>
        /// Called when System is Destoryed
        /// </summary>
        public void OnDestroy(ref SystemState state) { }

        /// <summary>
        /// Called before the first OnUpdate, and resume after stopped or disabled
        /// </summary>
        public void OnStartRunning(ref SystemState state) { }

        /// <summary>
        /// Called when disabled or when OnUpdate is stopped called because of non matching queries
        /// </summary>
        public void OnStopRunning(ref SystemState state) { }

        /// <summary>
        /// Run Every Frame
        /// Schedule Jobs Here
        /// </summary>
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //Only Player Tag will Change Rotate
            foreach (var rotate in SystemAPI.Query<RefRW<Rotate>>().WithAll<PlayerTag>())
            {
                rotate.ValueRW.Speed.x += SystemAPI.Time.DeltaTime * 3;
            }

            //Only Rotated Change is included
            foreach (var (transform, rotate, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Rotate>>().WithChangeFilter<Rotate>().WithEntityAccess())
            {
                MonoBehaviour.print("Rotate Changed for entity index "+ entity.Index);
                transform.ValueRW.Rotation = quaternion.Euler(rotate.ValueRO.Speed);
            }
        }
    }
}