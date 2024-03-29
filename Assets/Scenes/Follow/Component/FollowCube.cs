using Unity.Entities;

public struct FollowCube : IComponentData, IEnableableComponent
{
    public Entity Target;
}