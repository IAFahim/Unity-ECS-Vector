using Unity.Entities;
using Unity.Mathematics;

namespace A._Core_Vector_Concepts._1._Vector_Definition.VectorDefinition.Data
{
    public struct ScalarInput : IComponentData { public float Value; }
    public struct VectorScaledResult : IComponentData { public float3 Value; }
}