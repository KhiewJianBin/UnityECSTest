using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// Spawner Authoring - Contains the information needed to convert GameObject To Entity
/// </summary>
class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate;

    /// <summary>
    /// Spawner Baker - Using information from authoring, Creates Entities and pass on the required variables to Entities
    /// </summary>
    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            // 1. Setup The Game's Initial Systems Here
            var spawnerSystemHandle = World.DefaultGameObjectInjectionWorld.CreateSystem<SpawnerSystem>();

            // 2. Find Existing SystemGroup to add
            var rootSys2 = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SimulationSystemGroup>();

            // 3. Add System to Appropriate Group
            if (rootSys2 != null)
            {
                rootSys2.AddSystemToUpdateList(spawnerSystemHandle);
            }





            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Spawner
            {
                // By default, each authoring GameObject turns into an Entity.
                // Given a GameObject (or authoring component), GetEntity looks up the resulting Entity.
                EntityPrefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = authoring.transform.position,
                NextSpawnTime = 0.0f,
                SpawnRate = authoring.SpawnRate
            });
        }
    }
}

public struct Spawner : IComponentData
{
    public Entity EntityPrefab;
    public float3 SpawnPosition;
    public float NextSpawnTime;
    public float SpawnRate;
}