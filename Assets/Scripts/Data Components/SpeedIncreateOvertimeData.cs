using Unity.Entities;

[GenerateAuthoringComponent]
public struct SpeedIncreateOvertime : IComponentData
{
    public float increatePerSecond;
}
