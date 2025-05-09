using A._Core_Vector_Concepts._2._Vector_Subtraction.Vector_Subtraction.Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace A._Core_Vector_Concepts._2._Vector_Subtraction.Vector_Subtraction
{
    [WorldSystemFilter(WorldSystemFilterFlags.Default | WorldSystemFilterFlags.Editor)]
    public partial struct DirectionBetweenEntitiesSystem : ISystem
    {
        private EntityQuery _targetQuery;
        private EntityQuery _observerQuery;

        public void OnCreate(ref SystemState state)
        {
            _targetQuery = state.GetEntityQuery(ComponentType.ReadOnly<LocalTransform>(),
                ComponentType.ReadOnly<TargetEntityTag>());
            _observerQuery = state.GetEntityQuery(ComponentType.ReadOnly<LocalTransform>(),
                ComponentType.ReadOnly<ObserverEntityTag>(), ComponentType.ReadWrite<DirectionToTargetResult>());
            state.RequireForUpdate(_targetQuery); // Only run if at least one target exists
            state.RequireForUpdate(_observerQuery); // And at least one observer
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            if (_targetQuery.IsEmpty || _observerQuery.IsEmpty)
                return;

            LocalTransform targetTransform = _targetQuery.GetSingleton<LocalTransform>();
            float3 targetPosition = targetTransform.Position;


            foreach (var (observerTransform, directionResult) in
                     SystemAPI.Query<RefRO<LocalTransform>, RefRW<DirectionToTargetResult>>()
                         .WithAll<ObserverEntityTag>())
            {
                float3 observerPosition = observerTransform.ValueRO.Position;
                float3 vectorToTarget = targetPosition - observerPosition;

                directionResult.ValueRW.Value = math.normalizesafe(vectorToTarget);
                directionResult.ValueRW.Distance = math.length(vectorToTarget);

                var direction = directionResult.ValueRO.Value;
                Debug.DrawRay(observerPosition, direction * directionResult.ValueRO.Distance, Color.yellow);

                Debug.Log(
                    $"Observer at {observerPosition} -> Target at {targetPosition}. Direction: {direction}, Distance: {directionResult.ValueRO.Distance}");
            }
        }
    }
}