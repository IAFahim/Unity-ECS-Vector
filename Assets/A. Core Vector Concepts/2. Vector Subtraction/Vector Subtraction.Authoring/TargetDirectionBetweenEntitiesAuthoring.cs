using A._Core_Vector_Concepts._2._Vector_Subtraction.Vector_Subtraction.Data;
using Unity.Entities;
using UnityEngine;

namespace A._Core_Vector_Concepts._2._Vector_Subtraction.Vector_Subtraction.Authoring
{
    public class TargetDirectionBetweenEntitiesAuthoring : MonoBehaviour
    {
        class Baker : Baker<TargetDirectionBetweenEntitiesAuthoring>
        {
            public override void Bake(TargetDirectionBetweenEntitiesAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<TargetEntityTag>(entity);
            }
        }
    }
}