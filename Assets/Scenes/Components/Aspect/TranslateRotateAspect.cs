using Unity.Entities;
using Unity.Transforms;

public readonly partial struct TranslateRotateAspect : IAspect
{
    public readonly RefRO<LocalTransform> localTransform;
    public readonly RefRO<Translate> translate;
    public readonly RefRO<Rotate> rotate;
}
