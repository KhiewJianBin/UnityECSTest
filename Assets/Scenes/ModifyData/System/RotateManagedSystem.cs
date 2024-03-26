using System.ComponentModel;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ModifyData
{
    /// <summary>
    /// Unmanaged System
    /// </summary>
    [BurstCompile]
    [DisableAutoCreation] // Unity will not this System Automatically
    [RequireMatchingQueriesForUpdate] // Only Run OnUpdate when any Query code used in the struct is valid
    public partial class RotateManagedSystem : SystemBase
    {
        protected override void OnCreate() { }

        /// <summary>
        /// Called when System is Destoryed
        /// </summary>
        protected override void OnDestroy() { }

        /// <summary>
        /// Called Just before the first OnUpdate() - pre-update
        /// </summary>
        protected override void OnStartRunning()
        {
            base.OnStartRunning();
        }

        /// <summary>
        /// Called when system stop updating and before OnDestroy
        /// </summary>
        protected override void OnStopRunning()
        {
            base.OnStartRunning();
        }

        /// <summary>
        /// Run Every Frame
        /// Schedule Jobs Here
        /// </summary>
        protected override void OnUpdate()
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

            // Method 3. Using Entities.ForEach with Required Components defined Explictly
            //----------------------------------------------------------------------
            // WithAll<LocalTransform>  - required all components + whatever is inside foreach lambda
            // WithAny                  - required at least one component + whatever is inside foreach lambda
            // WithNone                 - required to not have any of the components
            // WithChangeFilter()       - returns only entities that might? have changed
            // WithSharedComponentFilter(ISharedComponentData) - returns chunks that have specified value for shared component
            // Note: use "ref" for ReadWrite, "in" Read
            // Note: Foreach can add upto 8 parameters
            // Note: To have more parameters you must use custom delegate foreach overload and delare entity, entityInQueryIndex, and nativeThreadIndex
            // Note: Can insert your own varaibles for job to process and output, but should bet native containers and blittable types 
            // Optionals : Entity e
            //             int entityInQueryIndex
            //             int nativeThreadIndex //Id of thread running the lambda
            //Entities.WithName("NameFor_ProfillerChecking").ForEach((Entity e, ref LocalTransform localtransform, in Rotate rotate) =>
            //{
            //    localtransform = localtransform.Rotate(quaternion.Euler(rotate.Speed * deltaTime));
            //})
            //.ScheduleParallel();

            // Method 3b. Using Entities.ForEach with Arbitrary data lookup
            //----------------------------------------------------------------------
            ComponentLookup<LocalTransform> localtransform_LU = GetComponentLookup<LocalTransform>();
            ComponentLookup<Rotate> rotate_LU = GetComponentLookup<Rotate>();
            Entities.WithName("Rotate_EntitesForeach").ForEach((Entity e) =>
            {
                if (!localtransform_LU.HasComponent(e) || !rotate_LU.HasComponent(e)) return;

                var localtransform = localtransform_LU.GetRefRW(e);
                var rotate = rotate_LU.GetRefRO(e);

                localtransform.ValueRW = localtransform.ValueRW.Rotate(quaternion.Euler(rotate.ValueRO.Speed * deltaTime));
            })
            .Schedule();
        }
    }
}