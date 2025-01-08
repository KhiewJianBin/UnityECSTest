using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Toggle
{
    [BurstCompile]
    [DisableAutoCreation] // Unity will not this System Automatically
    public partial struct InitToggleSceneSystem : ISystem, ISystemStartStop
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) { }
        public void OnDestroy(ref SystemState state) { }
        public void OnStartRunning(ref SystemState state) { }
        public void OnStopRunning(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //Active for one time only
            state.Enabled = false;

            var toggleManager = GameObject.FindFirstObjectByType<ToggleManager>();

            var toggleManagerManaged = new ToggleManagerManaged();
            toggleManagerManaged.cube = toggleManager.cube;
            toggleManagerManaged.text = toggleManager.text;
            toggleManagerManaged.toggle = toggleManager.toggle;

            var rotate = new Rotate
            {
                Speed = new float3(200, 0, 0)
            };

            var entity = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity, toggleManagerManaged);
            state.EntityManager.AddComponentData(entity, rotate);
        }
    }
}