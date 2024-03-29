using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Follow
{
    /// <summary>
    /// Unmanaged System
    /// </summary>
    [BurstCompile]
    [DisableAutoCreation] // Unity will not this System Automatically
    public partial struct FollowCubeSystem : ISystem, ISystemStartStop
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            EntityQuery query = state.GetEntityQuery(ComponentType.ReadWrite<LocalTransform>(), ComponentType.ReadOnly<FollowCube>());
            state.RequireForUpdate(query);
        }

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
            float deltaTime = SystemAPI.Time.DeltaTime;

            ComponentLookup<LocalTransform> localtransform_LU = state.GetComponentLookup<LocalTransform>();

            foreach (var (transform, follow, entity) in SystemAPI.Query<RefRW<LocalTransform>, FollowCube>().WithEntityAccess())
            {
                if (!localtransform_LU.HasComponent(follow.Target)) return;

                var targettransform = localtransform_LU.GetRefRO(follow.Target);
                var targetPos = targettransform.ValueRO.Position;

                transform.ValueRW.Position = Vector3.Lerp(transform.ValueRO.Position, targetPos, deltaTime);
            }
        }
    }
}