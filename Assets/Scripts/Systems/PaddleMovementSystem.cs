using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
public class PaddleMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float yBound = GameManager.main.yBound;

        Entities.ForEach((ref Translation trans, in PaddleMovementData move) =>
        {
            trans.Value.y = math.clamp(trans.Value.y + (move.speed * move.direction * deltaTime), -yBound, yBound);
        }).Run();

        return default;
    }
}
