using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Toggle
{
    [BurstCompile]
    [DisableAutoCreation]
    public partial struct RotateSystem : ISystem, ISystemStartStop
    {
        public void OnCreate(ref SystemState state) 
        {
            state.RequireForUpdate<ToggleManagerManaged>();
        }
        public void OnDestroy(ref SystemState state) { }
        public void OnStartRunning(ref SystemState state) { }
        public void OnStopRunning(ref SystemState state) { }

        /// <summary>
        /// Run Every Frame
        /// Schedule Jobs Here
        /// </summary>
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var toggle = SystemAPI.ManagedAPI.GetSingleton<ToggleManagerManaged>();

            if (!toggle.toggle.isOn) return;

            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var rotate in SystemAPI.Query<RefRO<Rotate>>())
            {
                toggle.text.text = rotate.ValueRO.Speed.ToString();
                toggle.cube.transform.rotation *= Quaternion.Euler(rotate.ValueRO.Speed * deltaTime);
            }
        }
    }
}