using B._Fun___Practical._1._Homing_Missile.HomingMissile.Data;
using Unity.Entities;
using UnityEngine;

namespace B._Fun___Practical._1._Homing_Missile.HomingMissile.Authoring
{
    public class TargetAuthoring : MonoBehaviour
    {
        public GameObject targetGameObject; 
        class Baker : Baker<TargetAuthoring>
        {
            public override void Bake(TargetAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new TargetEntity()
                {
                    Value = GetEntity(authoring.targetGameObject, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}