using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class GrapplingHookReachedTargetSystem : GameReactiveSystem
{
    public GrapplingHookReachedTargetSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.GrapplingHookLine,
            GameMatcher.GrapplingHookCurrentPoint, GameMatcher.GrapplingHookEndPoint, GameMatcher.GrapplingHookUserId));
    }

    protected override bool Filter(GameEntity entity)
    {
        if (entity != null && entity.hasGrapplingHookCurrentPoint && entity.hasGrapplingHookEndPoint && entity.hasGrapplingHookUserId && entity.isGrapplingHookHitTarget == false)
        {
            GameEntity userEntity = _context.GetEntityWithId(entity.grapplingHookUserId.Value);

            Vector2 originDirection = ((Vector3)entity.grapplingHookCurrentPoint.Value - userEntity.position.position).normalized;
            Vector2 currentDirection = (entity.grapplingHookEndPoint.Value - entity.grapplingHookCurrentPoint.Value).normalized;

            float angle = Vector2.Angle(originDirection, currentDirection);
            float distance = Vector3.Distance(entity.grapplingHookCurrentPoint.Value, entity.grapplingHookEndPoint.Value);

            return distance <= 0.01f || angle != 0f;
        }

        return false;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            GameEntity userEntity = _context.GetEntityWithId(entity.grapplingHookUserId.Value);
            userEntity.RemoveUsedGrapplingHookId();
            userEntity.isUseGrapplingHook = false;
            
            entity.DestroyEntity();
        }
    }
}