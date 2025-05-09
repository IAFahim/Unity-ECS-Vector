using A._Core_Vector_Concepts._1._Vector_Definition.VectorDefinition.Data;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace A._Core_Vector_Concepts._1._Vector_Definition.VectorDefinition
{
    [WorldSystemFilter(WorldSystemFilterFlags.Editor| WorldSystemFilterFlags.Default)]
    public partial struct ScalarMultiplySystem : ISystem
    {
        public void OnCreate(ref SystemState state) { }
        public void OnDestroy(ref SystemState state) { }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (vectorInput, scalar, scaledResult) in SystemAPI.Query<RefRO<VectorInputA>, RefRO<ScalarInput>, RefRW<VectorScaledResult>>())
            {
                scaledResult.ValueRW.Value = vectorInput.ValueRO.Value * scalar.ValueRO.Value;
                Debug.DrawRay(float3.zero, vectorInput.ValueRO.Value, Color.gray);
                Debug.DrawRay(float3.zero, scaledResult.ValueRO.Value, Color.white);
            }
        }
    }
}