using System.Diagnostics;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ModifyData
{
    /// <summary>
    /// Unmanaged System
    /// </summary>
    [BurstCompile]
    [DisableAutoCreation] // Unity will not this System Automatically
    [RequireMatchingQueriesForUpdate] // Only Run OnUpdate when any All Query code used in this struct/class is not empty - Not including Update Itself
    public partial struct RotateSystem : ISystem, ISystemStartStop
    {
        EntityQuery query;

        public ComponentTypeHandle<LocalTransform> localTransformHandle;
        [ReadOnly] public ComponentTypeHandle<Rotate> rotateHandle;

        [BurstCompile]
        public void OnCreate(ref SystemState state) 
        {
            query = state.GetEntityQuery(ComponentType.ReadWrite<LocalTransform>(), ComponentType.ReadOnly<Rotate>());
            state.RequireForUpdate(query);

            localTransformHandle = state.GetComponentTypeHandle<LocalTransform>(false);
            rotateHandle = state.GetComponentTypeHandle<Rotate>(true);
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

            // Get Set Data using 
            //----------------------------------------------------------------------

            // Method 1. System API Query Foreach
            //----------------------------------------------------------------------
            // Supports IAspect, IComponentData, ISharedComponentData, DynamicBuffer<T>, RefRO<T>, RefRW<T>, EnabledRefRO<T>, EnabledRefRW<T>
            // Note: This foreach code is replaced with a cache version after compile
            // Note: Source Generators Injected code to Completes all read - readwrite dependicies before foreach
            // Note: Use RefRO , RefRW When possible
            // Note: Add .WithEntityAccess() to get access to entity at the end of tuple
            //foreach (var (transform, rotate, entity) in SystemAPI.Query<RefRW<LocalTransform>, Rotate>().WithEntityAccess())
            //{
            //    transform.ValueRW = transform.ValueRO.Rotate(quaternion.Euler(rotate.Speed * deltaTime));
            //}

            // Method 2. Using Entity Job with Required Components defined Explictly
            //----------------------------------------------------------------------
            // Note: Use Schedule or Schedule in Parallel depending of entity count
            //var rotateJob = new Rotate_EntityJob
            //{
            //    deltaTime = SystemAPI.Time.DeltaTime
            //};
            //rotateJob.ScheduleParallel();

            // Method 2b. Using Entity Job with Arbitrary data lookup
            //----------------------------------------------------------------------
            //var rotateJob2 = new Rotate_EntityJob
            //{
            //    deltaTime = SystemAPI.Time.DeltaTime
            //};
            //rotateJob2.ScheduleParallel();

            // Method 3. Using Chunk Job with Query
            //----------------------------------------------------------------------
            localTransformHandle.Update(ref state);
            rotateHandle.Update(ref state);
            var rotateJob3 = new Rotate_ChunkJob
            {
                LocalTransformHandle = this.localTransformHandle,
                RotateHandle = this.rotateHandle,
                deltaTime = SystemAPI.Time.DeltaTime
            };
            state.Dependency = rotateJob3.ScheduleParallel(query, state.Dependency);
        }
    }


    [BurstCompile]
    public partial struct Rotate_EntityJob : IJobEntity
    {
        public float deltaTime;

        void Execute(Entity e, ref LocalTransform localtransform, in Rotate rotate,
                    [EntityIndexInChunk] int EntityIndexInChunk,
                    [ChunkIndexInQuery] int ChunkIndexInQuery,
                    [EntityIndexInQuery] int EntityIndexInQuery)
        {
            localtransform = localtransform.Rotate(quaternion.Euler(rotate.Speed * deltaTime));
        }
    }

    [BurstCompile]
    public partial struct Rotate2_EntityJob : IJobEntity
    {
        [ReadOnly] public ComponentLookup<LocalTransform> localTransform_LU;
        [ReadOnly] public ComponentLookup<Rotate> rotate_LU;

        //BufferLookup<BufferData> buffersOfAllEntities = this.GetBufferLookup<BufferData>(true);

        public float deltaTime;

        void Execute(Entity e,
                    [EntityIndexInChunk] int EntityIndexInChunk,
                    [ChunkIndexInQuery] int ChunkIndexInQuery,
                    [EntityIndexInQuery] int EntityIndexInQuery)
        {
            if (!localTransform_LU.HasComponent(e) || !rotate_LU.HasComponent(e)) return;

            var localTransform =  localTransform_LU.GetRefRW(e);
            var rotate = rotate_LU.GetRefRO(e);

            localTransform.ValueRW = localTransform.ValueRW.Rotate(quaternion.Euler(rotate.ValueRO.Speed * deltaTime));
        }
    }

    [BurstCompile]
    public partial struct Rotate_ChunkJob : IJobChunk
    {
        public ComponentTypeHandle<LocalTransform> LocalTransformHandle;
        [ReadOnly] public ComponentTypeHandle<Rotate> RotateHandle;

        public float deltaTime;

        public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
        {
            //if (chunk.Has<OptionalComp>(OptionalCompType))

            var chunkLocalTransform = chunk.GetNativeArray(ref LocalTransformHandle);
            var chunkRotate = chunk.GetNativeArray(ref RotateHandle);

            for (var i = 0; i < chunk.Count; i++)
            {
                var localTransform = chunkLocalTransform[i];
                var rotate = chunkRotate[i];

                chunkLocalTransform[i] = localTransform.Rotate(quaternion.Euler(rotate.Speed * deltaTime));
            }
        }
    }
}