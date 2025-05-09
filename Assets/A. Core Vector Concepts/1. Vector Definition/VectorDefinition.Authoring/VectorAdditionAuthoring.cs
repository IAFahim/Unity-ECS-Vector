using A._Core_Vector_Concepts._1._Vector_Definition.VectorDefinition.Data;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace A._Core_Vector_Concepts._1._Vector_Definition.VectorDefinition.Authoring
{
    public class VectorAdditionAuthoring : MonoBehaviour
    {
        public float3 vectorA = new float3(1, 0, 0);
        public float3 vectorB = new float3(0, 1, 0);

        class Baker : Baker<VectorAdditionAuthoring>
        {
            public override void Bake(VectorAdditionAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new VectorInputA { Value = authoring.vectorA });
                AddComponent(entity, new VectorInputB { Value = authoring.vectorB });
                AddComponent(entity, new VectorSumResult { Value = float3.zero });
            }
        }
    }
}