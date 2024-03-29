using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Input
{
    [BurstCompile]
    [DisableAutoCreation]
    public partial struct RotateSystem : ISystem, ISystemStartStop
    {
        public void OnCreate(ref SystemState state) { }
        public void OnDestroy(ref SystemState state) { }
        public void OnStartRunning(ref SystemState state) { }
        public void OnStopRunning(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var inputs = SystemAPI.GetSingleton<InputState>();

            float deltaTime = SystemAPI.Time.DeltaTime;

            //if (!UnityEngine.Input.GetKey(KeyCode.R)) return;

            if (inputs.R_Key_State != KeyState.KeyPress) return;

            foreach (var (transform, rotate) in SystemAPI.Query<RefRW<LocalTransform>, Rotate>())
            {
                transform.ValueRW = transform.ValueRO.Rotate(quaternion.Euler(rotate.Speed * deltaTime));
            }
        }
    }
}