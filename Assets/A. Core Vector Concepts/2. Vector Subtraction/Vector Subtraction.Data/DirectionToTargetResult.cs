using Unity.Entities;

namespace A._Core_Vector_Concepts._2._Vector_Subtraction.Vector_Subtraction.Data
{
    public struct DirectionToTargetResult : IComponentData
    {
        public Unity.Mathematics.float3 Value;
        public float Distance;
    }
}