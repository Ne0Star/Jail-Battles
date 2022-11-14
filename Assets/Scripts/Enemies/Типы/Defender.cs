using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Enemu
{

    protected override void Enabled()
    {

        List<AIArea> areas = LevelManager.Instance.GetAreas(AreaType.������������);

        AddAction(new MoveFromArea(this, areas));
        AddAction(new MoveFromArea(this, areas));

        Gun r = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Gun>();
        SetGun(r);
        Mele re = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Mele>();
        SetMele(re);

    }
    public override void Attack()
    {
        if (target && weapon)
        {

            YG.WeaponData weaponData = new YG.WeaponData();
            foreach (YG.WeaponData m in LevelManager.Instance.EnemuManager.WeaponDatas)
            {
                if (m.weaponType == weapon.WeaponType)
                {
                    weaponData = m;
                    break;
                }
            }

            target.TakeDamage(this, weaponData.attackDamage.Value, () =>
            {

            });
            weapon.AnimateAttack();
        }
    }
    protected override void Attacked(Entity attacker)
    {
        if (isAttack || !attacker) return;
        this.target = attacker;
        SetAction(new AttackTarget(this, ref attacker, true));
    }
    [SerializeField] private bool isAttack = false;
    protected override void OnCustomTriggerStay(Entity e)
    {
        if (!e || isAttack) return;
        this.target = e;
        isAttack = true;
        AttackTarget at = new AttackTarget(this, ref e, true);
        at.OnComplete?.AddListener((a) =>
        {
            isAttack = false;
        });
        SetAction(at);

    }

}
