using Unity.Entities;

/// <summary>
/// Declaring this as a new Custom SystemGroup
/// </summary>

//Set Parent Group
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial class TransformChange : ComponentSystemGroup
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
    }
}