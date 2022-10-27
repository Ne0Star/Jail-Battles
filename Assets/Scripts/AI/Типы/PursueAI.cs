using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Преследователь, или же просто зек
/// Бродит по тюрьме, имеет шанс напасть на другого зека или на игрока, время от времени отходит в туалет
/// </summary>
public class PursueAI : AI
{
    [SerializeField] private UpdateData pursueChance;
    [SerializeField] private bool isPursue = false;
    [SerializeField] private bool isWandering = false;

    [SerializeField] private bool useChance = false;

    public override void MarkTarget(Entity entity)
    {
        useChance = false;
        OnCustomTriggerStay(entity);
    }

    protected override void Enable()
    {
        pursueChance.Update();
    }

    /// <summary>
    /// Текущее действие перенести в стек и начать выполнение нового действия
    /// Когда новое действиие закончиться вернуться к последнему действию из стека
    /// </summary>
    /// <param name="entity"></param>
    protected override void OnCustomTriggerStay(Entity entity)
    {
        if (isPursue || isAttack || entity == this.entity || CurrentAction == null) return;

        Debug.Log("Заметил  " + entity);
        return;
        if (useChance)
        {
            bool attack = Random.Range(0, 100) >= pursueChance.CurrentValue;
            if (!attack) return;
        }
        else
        {
            useChance = true;
        }


        isPursue = true;
        SetAction(new ActionMoveToTarget(this, entity, 4f + ((Enemu)Entity).RespawnCount, 1.2f), (v) =>
        {
            isPursue = false;

            entity.TakeDamage(Entity, 0);
            isAttack = true;
            SetAction(new ActionAttack(this, entity, attackSpeed.CurrentValue, attackDamage.CurrentValue), (v) =>
            {
                isAttack = false;
                isPursue = false;
            }, (v) =>
            {
                isAttack = false;
                isPursue = false;
            });
        }, (v) =>
        {
            isPursue = false;
        });
    }

    protected override void OnDamaged(Entity entity, float value)
    {
        useChance = false;
        OnCustomTriggerStay(entity);
    }

    protected override void Create()
    {
        isWandering = true;
        AddAction(new ActionMoveByArea(this, LevelManager.Instance.AiManager.Areas, AreaType.Столовая), (v) =>
        {
            isWandering = false;
        });

    }
}
