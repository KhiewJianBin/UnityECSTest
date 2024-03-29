using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Input
{
    [BurstCompile]
    [DisableAutoCreation]
    public partial struct InputUpdateSystem : ISystem, ISystemStartStop
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) 
        {
            state.RequireForUpdate<InputState>();
        }
        public void OnDestroy(ref SystemState state) { }
        public void OnStartRunning(ref SystemState state) { }
        public void OnStopRunning(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var toggleManager = GameObject.FindFirstObjectByType<InputManager>();

            if (toggleManager == null) return;

            SystemAPI.SetSingleton(toggleManager.inputstate);
        }
    }
}