using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/// <summary>
/// Unmanaged System
/// </summary>
[BurstCompile]
[RequireMatchingQueriesForUpdate] // Only Run OnUpdate when there's valid Queries
[DisableAutoCreation]
[UpdateAfter(typeof(Translate))] // Set Child Order
public partial struct RotateSystem : ISystem , ISystemStartStop
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // 1a. Declare the Component is required to run OnUpdate
        // 1b1. Build a Query and Declare that the Query must satisfy to run OnUpdate 
        // 1b2. Build a Query using Query Description and Declare that the Query must satisfy to run OnUpdate
        // Note: Doing this will override any cache queries that uses attribute [RequireMatchingQueriesForUpdate] to do the same thing, meaning that the query in this takes precedence

        //1a
        //state.RequireForUpdate<Rotate>();

        //1b1
        EntityQuery query = state.GetEntityQuery(ComponentType.ReadOnly<Rotate>());

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

        state.RequireForUpdate(query);

        //state.EntityManager.CreateSingleton()
    }

    /// <summary>
    /// Called when System is Destoryed
    /// </summary>
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }

    /// <summary>
    /// Called before the first OnUpdate, and resume after stopped or disabled
    /// </summary>
    [BurstCompile]
    public void OnStartRunning(ref SystemState state)
    {

    }

    /// <summary>
    /// Called when disabled or when OnUpdate is stopped called because of non matching queries
    /// </summary>
    [BurstCompile]
    public void OnStopRunning(ref SystemState state)
    {

    }

    /// <summary>
    /// Run Every Frame
    /// Schedule Jobs Here
    /// </summary>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        // 1. System API Query Foreach - Loop though entities with matching component and returns the component
        // Supports IAspect,IComponentData,ISharedComponentData,DynamicBuffer<T>,RefRO<T>,RefRW<T>,EnabledRefRO<T>,EnabledRefRW<T>
        // Note: this foreach code is replaced with a cache version after compile
        // Note: Source Generators Injected code to Completes all read - readwrite dependicies before foreach
        // Note: Use RefRO , RefRW 
        // Note: Add .WithEntityAccess() to get access to entity at tuple end
        foreach (var (transform, rotate, entity) in SystemAPI.Query<RefRW<LocalTransform>, Rotate>().WithEntityAccess())
        {
            // Update Transform Value - as taking the transform and rotating it
            transform.ValueRW = transform.ValueRO.Rotate(quaternion.Euler(rotate.Speed * deltaTime));
        }
    }
}