using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Преследователь, или же просто зек
/// Бродит по тюрьме, имеет шанс напасть на другого зека или на игрока, время от времени отходит в туалет
/// </summary>
public class PursueAI : AI
{
    [SerializeField] private float pursueChance = 0.1f;
    [SerializeField] private float damageMultipler = 1f;
    [SerializeField] private bool pursue = false;

    private ActionMoveToTarget actionMoveToTarget;

    /// <summary>
    /// Текущее действие перенести в стек и начать выполнение нового действия
    /// Когда новое действиие закончиться вернуться к последнему действию из стека
    /// </summary>
    /// <param name="entity"></param>
    protected override void OnCustomTriggerStay(Entity entity)
    {
        if (pursue || entity == this.entity || CurrentAction == null || !entity.AllowAttack) return;
        bool attack = Random.Range(0, 100) >= pursueChance;
        if (!attack) return;
        pursue = true;

        actionMoveToTarget = new ActionMoveToTarget(this, entity, 10, damageMultipler);
        SetAction(actionMoveToTarget, () =>
        {
            pursue = false;
            this.Entity.AllowAttack = true;
        }, () =>
        {
            pursue = false;
            this.Entity.AllowAttack = true;
        });
    }

    protected override void OnDamaged(Entity entity, float value)
    {
        if (actionMoveToTarget != null)
        {
            if (!entity.AllowAttack) return;
            actionMoveToTarget.SetTarget(entity);
        }
    }

    protected override void Create()
    {
        AddAction(new ActionMoveByArea(this, LevelManager.Instance.AiManager.Areas, AreaType.Столовая));
    }
}
