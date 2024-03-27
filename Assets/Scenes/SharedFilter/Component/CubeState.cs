using System;
using Unity.Entities;

/// <summary>
/// Using Shared Component as State
/// </summary>
public struct CubeStateComponent : ISharedComponentData
{
    public int State;
}

/// <summary>
/// Using Shared Component as State
/// </summary>
public struct CubeManagedStateComponent : ISharedComponentData, IEquatable<CubeManagedStateComponent>
{
    public int State;

    public bool Equals(CubeManagedStateComponent other)
    {
        return State.Equals(other.State);
    }

    public override int GetHashCode()
    {
        return State.GetHashCode();
    }
}