using B._Fun___Practical._1._Homing_Missile.HomingMissile.Data;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace B._Fun___Practical._1._Homing_Missile.HomingMissile.Authoring
{
    [RequireComponent(typeof(TargetAuthoring))]
    public class HomingMissileAuthoring : MonoBehaviour
    {
        public float speed = 10f;

        class Baker : Baker<HomingMissileAuthoring>
        {
            public override void Bake(HomingMissileAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<MissileTag>(entity);
                AddComponent(entity, new Velocity { Value = float3.zero });
                AddComponent(entity, new Speed { Value = authoring.speed });
            }
        }
    }

}