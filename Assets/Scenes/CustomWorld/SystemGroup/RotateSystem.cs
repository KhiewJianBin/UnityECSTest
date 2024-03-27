using Unity.Burst;
using Unity.Entities;

namespace CustomWorld
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(CustomSystemGroup))] // Set Parent System
    public partial struct RotateSystem : ISystem, ISystemStartStop
    {
        public void OnCreate(ref SystemState state) { }
        public void OnDestroy(ref SystemState state) { }
        public void OnStartRunning(ref SystemState state) { }
        public void OnStopRunning(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

        }
    }
}