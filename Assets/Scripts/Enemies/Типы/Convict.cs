using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convict : Enemu
{
    [SerializeField] private float exitDistance;
    [SerializeField] private Entity target;
    [SerializeField] private WeaponType initialWeapon;
    protected override void Enabled()
    {
        Weapon weapon = LevelManager.Instance.WeaponManager.GetRandomWeaponByType(initialWeapon, false);
        //if (weapon != null)
        //{
        //    SetWeaponParent(weapon);
        //    weapon.transform.localScale = Vector3.one;
        //    weapon.transform.localPosition = Vector3.zero;
        //    weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //}

        SetWeapon(weapon);
        animator.Play("Покой");
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(agent.transform.position, exitDistance);
        if (weapon)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(weapon.transform.position, weapon.AttackDistance);
        }
    }

    protected override void OnCustomTriggerStay(Entity e)
    {
        if (e == this || target == e || target) return;
        this.target = e;


        AttackTarget attackAction = new AttackTarget(this, target, weapon, exitDistance, false);
        attackAction.OnComplete?.AddListener((a) =>
        {
            target = null;
        });
        attackAction.OnBreak?.AddListener((a) =>
        {
            target = null;
        });
        SetAction(attackAction);
    }
}
