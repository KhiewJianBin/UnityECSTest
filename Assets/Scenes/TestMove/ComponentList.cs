using System;
using Unity.Entities;
using UnityEngine;

// Managed Component - Classs
public class ManagedComponentWithExternalResource : IComponentData,
    ICloneable, // Declare that duplicating this component also duplicate the managed data
    IDisposable // To use as a Destructor
{
    public ParticleSystem ParticleSystem;

    public object Clone()
    {
        return new ManagedComponentWithExternalResource { ParticleSystem = UnityEngine.Object.Instantiate(ParticleSystem) };
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(ParticleSystem);
    }
}

//Unmanaged Component - Struct
public struct ExampleUnmanagedComponent : IComponentData
{
    public int Value;
}

//Unmanaged Shared Component - Struct

public struct ExampleUnmanagedSharedComponent : ISharedComponentData
{
    public int Value;
}

// Managed Shared Component - Class
public struct ExampleManagedSharedComponent : ISharedComponentData, IEquatable<ExampleManagedSharedComponent>
{
    public string Value;

    public bool Equals(ExampleManagedSharedComponent other)
    {
        return Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

// Cleanup Component - Struct - To mark as a entity for *cleanup*
public struct ExampleCleanupComponent : ICleanupComponentData
{
    //// Creates an entity that contains a cleanup component.
    //Entity e = EntityManager.CreateEntity(typeof(Translation), typeof(Rotation), typeof(ExampleCleanup));

    //// Attempts to destroy the entity but, because the entity has a cleanup component, Unity doesn't actually destroy the entity. Instead, Unity just removes the Translation and Rotation components. 
    //EntityManager.DestroyEntity(e);
}

// Cleanup shared Component - Struct - To mark as a entity for *cleanup*
public struct ExampleSharedCleanupComponent : ICleanupSharedComponentData
{
    public int Value;
}

// Tag Component - Struct - Just for identifying

public struct ExampleTagComponent : IComponentData
{

}

// Dynamic buffer - h
// Set Capacity ti change buffer size
// Set InternalBufferCapacity to 0 if the dynamic buffer changes
//
[InternalBufferCapacity(16)]
public struct ExampleBufferComponent : IBufferElementData
{
    public int Value;

    //public void GetDynamicBufferComponentExample(Entity e)
    //{
    //    DynamicBuffer<ExampleBufferComponent> myDynamicBuffer = EntityManager.GetBuffer<ExampleBufferComponent>(e);
    //}
}

// Unmanaged Component to add as a chunk component
public struct ExampleChunkComponent : IComponentData
{
    // Use EntityManager.AddChunkComponentData<YourChunkComponent>(Entity) to add as chunk
    public int Value;
}

