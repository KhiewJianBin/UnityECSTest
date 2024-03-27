using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

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
            float deltaTime = SystemAPI.Time.DeltaTime;

            //CubeStateComponent cubeState0 = new CubeStateComponent()
            //{
            //    State = 0
            //};
            //CubeStateComponent cubeState1 = new CubeStateComponent()
            //{
            //    State = 1
            //};
            //CubeStateComponent cubeState2 = new CubeStateComponent()
            //{
            //    State = 2
            //};

            //foreach (var (transform, rotate, entity) in SystemAPI.Query<RefRW<LocalTransform>, Rotate>().WithSharedComponentFilter<CubeStateComponent>(cubeState0).WithEntityAccess())
            //{
            //    transform.ValueRW = transform.ValueRO.RotateX(rotate.Speed.x * deltaTime);
            //}

            //foreach (var (transform, rotate, entity) in SystemAPI.Query<RefRW<LocalTransform>, Rotate>().WithSharedComponentFilter<CubeStateComponent>(cubeState1).WithEntityAccess())
            //{
            //    transform.ValueRW = transform.ValueRO.RotateY(rotate.Speed.x * deltaTime);
            //}

            //foreach (var (transform, rotate, entity) in SystemAPI.Query<RefRW<LocalTransform>, Rotate>().WithSharedComponentFilter<CubeStateComponent>(cubeState2).WithEntityAccess())
            //{
            //    transform.ValueRW = transform.ValueRO.RotateZ(rotate.Speed.x * deltaTime);
            //}

            //Get all cube states
            state.EntityManager.GetAllUniqueSharedComponents<CubeStateComponent>(out var cubeStates, Allocator.Temp);

            for (int i = 0; i < cubeStates.Length; i++)
            {
                var cubeState = cubeStates[i];

                foreach (var (transform, rotate, entity) in SystemAPI.Query<RefRW<LocalTransform>, Rotate>().WithSharedComponentFilter<CubeStateComponent>(cubeState).WithEntityAccess())
                {
                    float3 r = new float3();
                    if (cubeState.State == 0) r.x = rotate.Speed.x;
                    else if (cubeState.State == 1) r.y = rotate.Speed.x;
                    else if (cubeState.State == 2) r.z = rotate.Speed.x;

                    transform.ValueRW = transform.ValueRO.Rotate(quaternion.Euler(r * deltaTime));
                }
            }
        }
    }
}