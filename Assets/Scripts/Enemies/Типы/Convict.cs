using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convict : Enemu
{
    [SerializeField] private Entity target;
    [SerializeField] private WeaponType initialWeapon;
    protected override void Enabled()
    {

        Weapon weapon = LevelManager.Instance.WeaponManager.GetRandomWeaponByType(initialWeapon, false);
        if (weapon != null)
        {
            SetWeaponParent(weapon);
            weapon.transform.localScale = Vector3.one;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        SetWeapon(weapon);
        animator.Play("Покой");
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
    }
    protected override void OnCustomTriggerStay(Entity e)
    {
        if (e == this || target == e) return;
        this.target = e;


            AttackTarget attackAction = new AttackTarget(this, target, weapon, weapon.AttackDistance);
            attackAction.OnComplete?.AddListener((a) =>
            {
                target = null;
            });
            attackAction.OnBreak?.AddListener((a) =>
            {
                target = null;
            });
            SetAction(attackAction);


        //MoveToTarget moveToTarget = new MoveToTarget(this, e, 10f);
        //moveToTarget.OnComplete?.AddListener((a) =>
        //{

        //});
        //SetAction(moveToTarget);
    }
}
