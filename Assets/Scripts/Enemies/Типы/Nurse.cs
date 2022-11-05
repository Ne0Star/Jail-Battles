using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ������
/// 
/// ����� � �������� � ���, ������ ������� � ������
/// ���� ��������� ����, ������� ���, � ���������� ��� �����
/// 
/// </summary>
public class Nurse : Enemu
{
    [SerializeField] private float time = 0f;
    [SerializeField] private float actionTime = 40f;

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
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.����������������)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.����������������)));
    }
    /// <summary>
    /// ����� ������ ���������� �� ������/�������� ��������
    /// </summary>
    public override void Attack()
    {

    }

    protected override void Attacked(Entity attacker)
    {

    }

    protected override void OnCustomTriggerStay(Entity e)
    {
        if (e == this) return;
        e.HitBar.AddHealth(100);

    }
}
