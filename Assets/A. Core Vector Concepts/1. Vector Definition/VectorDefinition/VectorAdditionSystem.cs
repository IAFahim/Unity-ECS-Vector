using A._Core_Vector_Concepts._1._Vector_Definition.VectorDefinition.Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace A._Core_Vector_Concepts._1._Vector_Definition.VectorDefinition
{
    [WorldSystemFilter(WorldSystemFilterFlags.Editor| WorldSystemFilterFlags.Default)]
    public partial struct VectorAdditionSystem : ISystem
    {

        public void OnUpdate(ref SystemState state)
        {
            float3 drawOrigin = float3.zero;
            

            // For direct iteration with Debug.DrawRay (often clearer for demos):
            foreach (var (inputA, inputB, sumResult, entity) in
                     SystemAPI.Query<RefRO<VectorInputA>, RefRO<VectorInputB>, RefRW<VectorSumResult>>()
                         .WithEntityAccess())
            {
                sumResult.ValueRW.Value = inputA.ValueRO.Value + inputB.ValueRO.Value;

                // Visualization
                if (SystemAPI.HasComponent<LocalTransform>(entity))
                {
                    drawOrigin = SystemAPI.GetComponent<LocalTransform>(entity).Position;
                }
                else
                {
                    drawOrigin = float3.zero; // Default to world origin
                }

                Debug.DrawRay(drawOrigin, inputA.ValueRO.Value, Color.red);        // Vector A
                Debug.DrawRay(drawOrigin + inputA.ValueRO.Value, inputB.ValueRO.Value, Color.green); // Vector B (from A's tip)
                Debug.DrawRay(drawOrigin, sumResult.ValueRO.Value, Color.blue);   // Sum Result
            }
        }
    }
}