using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        if (!isFree)
            return;
    }

    protected override void OnCustomTriggerStay(Entity e)
    {
        if (e == this) return;

    }
}
