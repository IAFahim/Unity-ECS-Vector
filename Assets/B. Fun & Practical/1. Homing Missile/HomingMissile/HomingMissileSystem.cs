using B._Fun___Practical._1._Homing_Missile.HomingMissile.Data;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace B._Fun___Practical._1._Homing_Missile.HomingMissile
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))] // Ensure target's transform is up-to-date
    public partial struct HomingMissileSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TargetEntity>();
            state.RequireForUpdate<MissileTag>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            // Using ComponentLookup for safe access to target's LocalTransform
            // Mark as ReadOnly because we only read the target's position.
            ComponentLookup<LocalTransform> targetTransformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true);

            foreach (var (missileTransform, missileVelocity, missileSpeed, targetRef)
                     in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Velocity>, RefRO<Speed>, RefRO<TargetEntity>>()
                         .WithAll<MissileTag>())
            {

                LocalTransform targetTransform = targetTransformLookup[targetRef.ValueRO.Value];
                float3 targetPosition = targetTransform.Position;
                
                // Subtraction: Calculate the vector from missile to target
                float3 directionToTarget = targetPosition - missileTransform.ValueRO.Position;

                // Normalization: Get a pure direction
                // Use normalizesafe in case missile reaches the target (distance is zero)
                float3 desiredDirection = math.normalizesafe(directionToTarget);

                // Scalar Multiplication: Calculate the desired velocity
                float3 desiredVelocity = desiredDirection * missileSpeed.ValueRO.Value;

                // Simple steering: Instantaneously set velocity.
                // For smoother turning, you'd typically lerp or apply a turning force.
                // e.g., missileVelocity.ValueRW.Value = math.lerp(missileVelocity.ValueRO.Value, desiredVelocity, turnRate * deltaTime);
                missileVelocity.ValueRW.Value = desiredVelocity;

                // Addition: Update missile's position
                missileTransform.ValueRW.Position += missileVelocity.ValueRO.Value * deltaTime;

                // (Optional) Rotation: Make the missile visually face its desiredDirection
                if (math.lengthsq(desiredDirection) > 0.0001f) // Avoid LookRotation with zero direction
                {
                    missileTransform.ValueRW.Rotation = quaternion.LookRotationSafe(desiredDirection, math.up());
                }
            }
        }
    }
}