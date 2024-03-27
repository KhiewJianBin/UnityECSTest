using Unity.Entities;

namespace CustomWorld
{
    /// <summary>
    /// A new Custom SystemGroup
    /// </summary>
    //Set Parent Group
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class CustomSystemGroup : ComponentSystemGroup
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}