using System.Collections;
using UnityEngine;

/// <summary>
/// ��������� � ����
/// </summary>

[System.Serializable]
public class ActionMoveToTarget : AIAction
{


    [SerializeField] private AI executor;
    [SerializeField] private Entity target;
    public ActionMoveToTarget(AI executor, Entity target)
    {
        this.executor = executor;
        this.target = target;
    }

    public void SetTarget(Entity target)
    {
        this.target = target;
    }

    [SerializeField] private float breakDistance;
    [SerializeField] private bool breakToFar;
    /// <summary>
    /// ������������ ���� �� ��� ��� ���� ��� �� �������� ��� �� ����� ����������
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="target"></param>
    /// <param name="breakDistance"></param>
    public ActionMoveToTarget(AI executor, Entity target, float breakDistance)
    {
        this.executor = executor;
        this.target = target;
        this.breakDistance = breakDistance;
        this.breakToFar = true;
    }

    [SerializeField] bool andAttack;
    [SerializeField] private float damageMultipler;
    /// <summary>
    /// ������������ ���� �� ��� ��� ���� ��� �� ��������, ��� �� ����� ����������,
    /// �� ������ ������ ��������� � � ���������� ������������� ���� ��� ����������
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="target"></param>
    /// <param name="breakDistance"></param>
    /// <param name="damage"></param>
    public ActionMoveToTarget(AI executor, Entity target, float breakDistance, float damageMultipler)
    {
        this.executor = executor;
        this.target = target;
        this.breakDistance = breakDistance;
        this.damageMultipler = damageMultipler;
        breakToFar = true;
        andAttack = true;
    }

    private IEnumerator Attack()
    {
        bool complete = false;
        while (!complete)
        {
            if (!target.gameObject.activeSelf)
            {
        executor.Agent.isStopped = false;
        onComplete?.Invoke();
                yield break;
            }
            if (breakToFar && distance >= breakDistance)
            {
                executor.Agent.isStopped = false;
                onBreak?.Invoke();
                yield break;
            }
            yield return new WaitForSeconds(LevelManager.Instance.CustomTime);
            yield return new WaitUntil(() => reached); // ����� ������
            AIFightStat fightStat = executor.Stats.GetStandartStrike(); // �������� ����
            fightStat.presset.Play(executor.Animator, executor.Source); // ��������� ����
            yield return new WaitForSeconds(fightStat.presset.Animation.duration); // ��������� ����� ���� �����������
            target.TakeDamage(executor.Entity, damageMultipler * fightStat.damage); // ������� ����
            executor.Stats.FigtStance.Play(executor.Animator, executor.Source); // ��������� ������ ������
            yield return new WaitForSeconds(fightStat.attackSpeed); // ��������� ����� ����������� �����
        }
    }
    [SerializeField] private float distance;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private bool reached = false;
    public override void CustomUpdate()
    {
        if (reached)
        {
            GameUtils.LookAt2D(executor.Data.RotateParent, target.transform.position, executor.Data.RotateOffset);
        }
        else
        {
            GameUtils.LookAt2D(executor.Data.RotateParent, executor.Agent.transform.position + executor.Agent.velocity, executor.Data.RotateOffset);
        }
        executor.Agent.isStopped = reached;
        distance = Vector2.Distance(executor.Agent.transform.position, target.transform.position);
        if (distance <= (executor.Agent.radius + target.Agent.radius) + 0.1f)
        {
            reached = true;
            if (andAttack)
            {
                if (!isAttack)
                {
                    isAttack = true;
                    executor.StartCoroutine(Attack());
                }
            }
            else
            {
                onComplete?.Invoke();
            }
        }
        else
        {
            if (reached != false)
            {
                executor.Stats.Walk.Play(executor.Animator, executor.Source, executor.Agent.speed);
            }
            executor.Agent.SetDestination(target.transform.position);
            reached = false;
        }
        if ((breakToFar && distance > breakDistance))
        {
            onBreak?.Invoke();
        }
    }

    public override void Initial()
    {

    }

    public override void Break()
    {

    }
}
