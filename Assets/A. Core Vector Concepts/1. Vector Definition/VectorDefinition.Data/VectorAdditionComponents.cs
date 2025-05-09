using Unity.Entities;
using Unity.Mathematics;

namespace A._Core_Vector_Concepts._1._Vector_Definition.VectorDefinition.Data
{
    public struct VectorInputA : IComponentData { public float3 Value; }
    public struct VectorInputB : IComponentData { public float3 Value; }
    public struct VectorSumResult : IComponentData { public float3 Value; }
}
