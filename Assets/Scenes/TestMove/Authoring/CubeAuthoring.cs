using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CubeAuthoring : MonoBehaviour
{
    [SerializeField] float3 Rotate_Speed;
    [SerializeField] float3 Translate_Speed;

    class TestBaker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring authoring)
        {
            
            //DependsOn(authoring);



            var entity = GetEntity(TransformUsageFlags.Dynamic); // Indicate to allow transform changes in runtime
            //var entity = GetEntity(TransformUsageFlags.ManualOverride); // Indicate that we will perform the transform conversion ourselves
            //var entity = GetEntity(TransformUsageFlags.NonUniformScale); // transform has non uniform scale?
            //var entity = GetEntity(TransformUsageFlags.None);
            //var entity = GetEntity(TransformUsageFlags.Renderable); // transform used for rendering only and not for moving
            //var entity = GetEntity(TransformUsageFlags.WorldSpace); // entity transform to be put in world space - not sure whats this use

            // Create & Add Rotate Component
            Rotate rotate = new Rotate
            {
                Speed = authoring.Rotate_Speed
            };
            AddComponent(entity, rotate);

            // Create & Add Translate Component
            Translate translate = new Translate
            {
                Speed = authoring.Translate_Speed
            };
            AddComponent(entity, translate);

            

           

            //var go = GameObject.Instantiate(authoring.gameObject);
            //var go2 = GameObject.Instantiate(authoring.gameObject);
            //go.name = "Child Cube A";
            //go2.name = "Child Cube B";
            //var additionalA = CreateAdditionalEntity(TransformUsageFlags.Dynamic, entityName: "Cube A");
            //var additionalB = CreateAdditionalEntity(TransformUsageFlags.Dynamic, entityName: "Cube B");
            //World.DefaultGameObjectInjectionWorld.CreateSystem(typeof(TranslateSystem));????
        }
    }
}
