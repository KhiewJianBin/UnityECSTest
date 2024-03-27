using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/// <summary>
/// Managed System
/// </summary>
[RequireMatchingQueriesForUpdate] // Only Run OnUpdate when there's valid Queries
[DisableAutoCreation]
[UpdateBefore(typeof(Rotate))] // Set Child Order
public partial class TranslateSystem : SystemBase
{
    /// <summary>
    /// Called when System is created
    /// </summary>
    protected override void OnCreate()
    {
        // 1a. Declare the Component is required to run OnUpdate
        // 1b1. Build a Query and Declare that the Query must satisfy to run OnUpdate 
        // 1b2. Build a Query using Query Description and Declare that the Query must satisfy to run OnUpdate
        // Note: Doing this will override any cache queries that uses attribute [RequireMatchingQueriesForUpdate] to do the same thing, meaning that the query in this takes precedence

        //1a
        //RequireForUpdate<Translate>();

        //1b1
        EntityQuery query = GetEntityQuery(ComponentType.ReadOnly<Translate>());

        //1b2
        //EntityQueryDesc qdescription = new EntityQueryDesc
        //{
        //    All = new ComponentType[] // All of the inside must be in the archetype & enabled
        //    {
        //    },
        //    Disabled = new ComponentType[] // All of the inside must be in the archetype & disabled
        //    {
        //    },
        //    None = new ComponentType[] // None of the inside must be in the archetype ignore disabled
        //    {
        //    },
        //    Absent = new ComponentType[] // None of the inside must be in the archetype including disabled
        //    {
        //    },
        //};
        //EntityQuery query = state.GetEntityQuery(qdescription);

        RequireForUpdate(query);

        //EntityManager.CreateSingleton()

    }

    /// <summary>
    /// Called when System is Destoryed
    /// </summary>
    protected override void OnDestroy()
    {

    }

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
        var time = (float)SystemAPI.Time.ElapsedTime;
        var deltaTime = SystemAPI.Time.DeltaTime;

        // 1. System API Query Foreach - Loop though entities with matching component and returns the component
        // Supports IAspect,IComponentData,ISharedComponentData,DynamicBuffer<T>,RefRO<T>,RefRW<T>,EnabledRefRO<T>,EnabledRefRW<T>
        // Note: this foreach code is replaced with a cache version after compile
        // Note: Source Generators Injected code to Completes all read - readwrite dependicies before foreach
        // Note: Use RefRO , RefRW 
        // Note: Add .WithEntityAccess() to get access to entity at tuple end
        // withSharedComponentFilter
        foreach (var (transform, translate) in SystemAPI.Query<RefRW<LocalTransform>, Translate>())
        {
            var t = transform.ValueRO;
            t.Position = new float3(math.sin(translate.Speed.x * time), t.Position.y, t.Position.z);
            transform.ValueRW = t;
        }

        // Schedule Job in System
        // 1a. Automatically create a query from the job's Execute function
        // new AddSpeedJob().ScheduleParallel();
        // 2b. Create Job using EntityQuery
        EntityQuery query_ForJob = GetEntityQuery(ComponentType.ReadWrite<Translate>());

        // 2. Entities.ForEach - Loop though entities
        // WithAll<LocalTransform>  - required all components + whatever is inside foreach lambda
        // WithAny                  - required at least one component + whatever is inside foreach lambda
        // WithNone                 - required to not have any of the components
        // WithChangeFilter()       - returns only entities that might? have changed
        // WithSharedComponentFilter(ISharedComponentData) - returns chunks that have specified value for shared component
        // Note: use "ref" for ReadWrite, "in" Read
        // Note: Foreach can add upto 8 parameters
        // Optionals : Entity e
        //             int entityInQueryIndex
        //             int nativeThreadIndex //Id of thread running the lambda

        //Able to capture only native container and blitable types local varaible used in foreach
        //Entities.WithName("Update_Displacement").ForEach((Entity e,ref LocalTransform transform, in Translate translate) =>
        //{

        //})
        //.ScheduleParallel(); // Schedule as a parallel job

        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        EntityCommandBuffer.ParallelWriter parallelEcb = ecb.AsParallelWriter();

        Entities.ForEach((Entity e, in Translate translate) =>
        {
            //Do things with component
            //translate
        }).Schedule();

        Entities.ForEach((Entity e, in Translate translate) =>
        {
            //Do things with component
            //translate
        }).ScheduleParallel();

        //Dependency.Complete();

        //Note that 
        //ecb.Playback(EntityManager);
        ecb.Dispose();


        //var singleton = SystemAPI.GetSingleton<FooECBSystem.Singleton>();
        //EntityCommandBuffer ecb = singleton.CreateCommandBuffer(state.WorldUnmanaged);

        //state.GetEntityQuery()
        //var handle = new JobHandle().Schedule(state.Dependency);
        //var otherHandle = new JobHandle().Schedule(state.Dependency);
        //state.Dependency = JobHandle.CombineDependencies(Handle, otherHandle);
    }

    //private void ReinterpretEntitysChunk(Entity e)
    //{
    //    DynamicBuffer<MyElement> myBuff = EntityManager.GetBuffer<MyElement>(e);

    //    // Valid as long as each MyElement struct is four bytes. 
    //    DynamicBuffer<int> intBuffer = myBuff.Reinterpret<int>();

    //    intBuffer[2] = 6;  // same effect as: myBuff[2] = new MyElement { Value = 6 };

    //    // The MyElement value has the same four bytes as int value 6. 
    //    MyElement myElement = myBuff[2];
    //    Debug.Log(myElement.Value);    // 6
    //}
}


//Create Custom IJobEntity - Struct
[BurstCompile]
[WithNone(typeof(Rotate))]
public partial struct AddSpeedJob : IJobEntity
{
    // Adds one to every SampleComponent value
    void Execute(Entity e, ref Translate translate,
        [EntityIndexInChunk]int EntityIndexInChunk,
        [ChunkIndexInQuery] int ChunkIndexInQuery,
        [EntityIndexInQuery] int EntityIndexInQuery)
    {
        translate.Speed += 1f;
    }
}
