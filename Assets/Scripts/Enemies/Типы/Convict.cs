using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convict : Enemu
{
    [SerializeField] private float exitDistance = 8f;
    [SerializeField] private Entity target;


    [SerializeField] private bool isBegin = false;
    [SerializeField] private bool isIdle = false;
    [SerializeField] private bool isAttack = false;

    protected override void Attacked(Entity attacker)
    {
        StartAttack(attacker);
    }

    protected override void OnCustomTriggerStay(Entity e)
    {

        StartAttack(e);
    }


    //[SerializeField] private float gunChance = 10f, machineChance = 5f, meleChance = 20f, noneChance = 50f;

    //[SerializeField] private float exitDistance;
    //[SerializeField] private Entity target;

    protected override void Disable()
    {
        currentAction = null;
        stackActions = null;
        lifeActions = null;
        target = null;
        isBegin = false;
        isIdle = false;
        isAttack = false;
    }

    private void SetLife()
    {
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
    }

    public override void Attack()
    {
        if (target && weapon)
        {
            target.TakeDamage(this, weapon.AttackDamage, () =>
            {

            });
            weapon.AnimateAttack();
        }
    }

    //protected override void Disable()
    //{
    //    stackActions.Clear();
    //    currentAction = null;
    //    lifeActions.Clear();
    //    target = null;
    //}

    protected override void Enabled()
    {
        stackActions = new List<AIAction>();
        lifeActions = new List<AIAction>();
        Gun r = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Gun>();
        SetGun(r);
        Mele re = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Mele>();
        SetMele(re);

        SetLife();
    }

    //public Entity Target { get => target; }

    //[SerializeField] private bool agroBlock = false;
    private void CheckAttackResult()
    {
        if (HitBar.Health <= HitBar.GetMaxHealth() / 2 && !isBegin)
        {
            isBegin = true;

            GetInLineAction inline = new GetInLineAction(this, InlineType.ВМедпункт);
            inline.OnComplete?.AddListener((a) =>
            {
                isBegin = false;
            });
            return;
        }
    }

    private void StartAttack(Entity e)
    {
        if (isBegin || isIdle || this.target != null || this == e) return;
        this.target = e;
        isAttack = true;
        AttackTarget attackAction = new AttackTarget(this, ref target, exitDistance, true);
        attackAction.OnComplete?.AddListener((a) =>
        {
            target = null;
            isAttack = false;
            CheckAttackResult();
        });
        SetAction(attackAction);
    }

    //protected override void OnCustomTriggerStay(Entity e)
    //{
    //    if (agroBlock) return;

    //    if (e && e as Convict)
    //    {
    //        Convict c = (Convict)e;
    //        Если у него есть цель
    //        if (c.Target && c.Target.gameObject.activeSelf)
    //        {
    //            return;
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }
    //    e.TakeDamage(this, 0f, () => { });
    //    StartAttack(e);
    //}
}
