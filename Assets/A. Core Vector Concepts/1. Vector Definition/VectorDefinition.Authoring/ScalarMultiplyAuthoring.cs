using A._Core_Vector_Concepts._1._Vector_Definition.VectorDefinition.Data;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ScalarMultiplyAuthoring : MonoBehaviour
{
    public float3 directionVector = new float3(0.707f, 0.707f, 0); // Should be normalized for "direction"
    public float scalarMultiplier = 5.0f;

    class Baker : Baker<ScalarMultiplyAuthoring>
    {
        public override void Bake(ScalarMultiplyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            // Ensure directionVector is normalized if it's meant to be a pure direction
            AddComponent(entity, new VectorInputA { Value = math.normalizesafe(authoring.directionVector) });
            AddComponent(entity, new ScalarInput { Value = authoring.scalarMultiplier });
            AddComponent(entity, new VectorScaledResult { Value = float3.zero });
        }
    }
}