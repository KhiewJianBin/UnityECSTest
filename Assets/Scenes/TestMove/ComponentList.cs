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

