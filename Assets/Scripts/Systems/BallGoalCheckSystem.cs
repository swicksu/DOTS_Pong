using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

[AlwaysSynchronizeSystem]
public class BallGoalCheckSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.WithAll<BallTag>()
            .WithStructuralChanges()
            .WithoutBurst()
            .ForEach((Entity entity, in Translation trans) =>
        {
            float3 pos = trans.Value;
            float bound = GameManager.main.xBound;

            if (pos.x >= bound)
            {
                GameManager.main.PlayerScored(0);
                EntityManager.DestroyEntity(entity);
            }
            else if (pos.x <= -bound)
            {
                GameManager.main.PlayerScored(1);
                EntityManager.DestroyEntity(entity);
            }
        }).Run();
        return default;
    }
}
