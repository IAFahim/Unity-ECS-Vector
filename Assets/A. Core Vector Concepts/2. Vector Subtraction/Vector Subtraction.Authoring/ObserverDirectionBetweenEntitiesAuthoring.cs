using A._Core_Vector_Concepts._2._Vector_Subtraction.Vector_Subtraction.Data;
using Unity.Entities;
using UnityEngine;

namespace A._Core_Vector_Concepts._2._Vector_Subtraction.Vector_Subtraction.Authoring
{
    public class ObserverDirectionBetweenEntitiesAuthoring : MonoBehaviour
    {
        private class ObserverDirectionBetweenEntitiesAuthoringBaker : Baker<ObserverDirectionBetweenEntitiesAuthoring>
        {
            public override void Bake(ObserverDirectionBetweenEntitiesAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<ObserverEntityTag>(entity);
                AddComponent<DirectionToTargetResult>(entity);
            }
        }
    }
}