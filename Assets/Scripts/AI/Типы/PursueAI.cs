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
        if (pursue || entity == this.entity || CurrentAction == null) return;
        bool attack = Random.Range(0, 100) >= pursueChance;
        if (!attack) return;
        pursue = true;

        actionMoveToTarget = new ActionMoveToTarget(this, entity, 10, damageMultipler);
        SetAction(actionMoveToTarget, () =>
        {
            pursue = false;

        }, () =>
        {
            pursue = false;
        });
    }

    protected override void OnDamaged(Entity sources, float value)
    {
        if (actionMoveToTarget != null)
        {
            actionMoveToTarget.SetTarget(sources);
        }
    }

    protected override void Create()
    {

        AddAction(new ActionMoveByArea(this, LevelManager.Instance.AiManager.Areas, AreaType.Столовая));
    }
}
