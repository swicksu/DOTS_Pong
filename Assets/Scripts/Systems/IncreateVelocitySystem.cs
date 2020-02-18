using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Mathematics;

[AlwaysSynchronizeSystem]
public class IncreateVelocitySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref PhysicsVelocity velocity, in SpeedIncreateOvertime data) =>
        {
            float2 modifier = new float2(data.increatePerSecond * deltaTime);
            float2 newVel = velocity.Linear.xy;          
            newVel += math.lerp(-modifier, modifier, math.sin(newVel));
            velocity.Linear.xy = newVel;
        }).Run();
        return default;
    }
}
