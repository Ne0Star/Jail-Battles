using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Мед сестра
/// 
/// Сидит в кабинете и ест, иногда выходит в туалет
/// Если встречает зека, атакует его, и излечивает тем самым
/// 
/// </summary>
public class Nurse : Enemu
{
    [SerializeField] private float time = 0f;
    [SerializeField] private float actionTime = 40f;
    [SerializeField] private bool isFree;

    [SerializeField] private UnityEvent targetComplete;

    public bool IsFree { get => isFree; }

    public void Health()
    {
        //if (target) target.HitBar.AddHealth(LevelManager.Instance.LevelData.HealValue);
        targetComplete?.Invoke();
    }
    public void GetHealth(Entity sources, System.Action onComplete)
    {
        if (!isFree)
        {
            onComplete?.Invoke();
            return;
        }
        //this.target = sources;
        isFree = false;
        TakeHealth take = new TakeHealth(this, sources, targetComplete);
        take.OnComplete?.AddListener((a) =>
        {
            isFree = true;
            onComplete?.Invoke();
        });
        SetAction(take);
    }

    protected override void OnUpdate()
    {
        if (time >= actionTime)
        {



            time = 0f;
        }
        time += Time.unscaledDeltaTime;
    }

    protected override void Enabled()
    {
        isFree = true;
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.КабинетМедсестры)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.КабинетМедсестры)));
    }
    /// <summary>
    /// Метод должен вызываться из оружия/анимации сущности
    /// </summary>
    public override void Attack()
    {

    }

    protected override void Attacked(Entity attacker)
    {
        if (!isFree)
            return;
    }

    protected override void OnCustomTriggerStay(Entity e)
    {
        if (e == this) return;

    }
}
