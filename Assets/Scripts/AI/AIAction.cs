using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �������� AI
/// </summary>
public abstract class AIAction : AIEvents
{
    [SerializeField] private List<System.Action<AI>> onStart;
    [SerializeField] private List<System.Action> onEnd;

    [SerializeField] protected AnimationStatesPresset states;

    [SerializeField] protected float duration;
    [SerializeField] private bool free = true;

    /// <summary>
    /// �������� �� ������ ��������
    /// </summary>
    public bool Free { get => free; }

    [SerializeField] protected bool isStop = true;
    /// <summary>
    /// �������� ��������, � ����������� �� �������� ������������ �� �������
    /// </summary>
    public float Duration { get => duration; }

    private void End()
    {
        free = true;
        if (onEnd != null)
            foreach (System.Action e in onEnd)
            {

                e?.Invoke();
            }
    }


    public void StopAction()
    {
        isStop = true;
        free = true;
    }

    /// <summary>
    /// ��������� �������� ��� ������� AI
    /// </summary>
    /// <param name="executor">�����������</param>
    /// <param name="onComplete">��������� ����� �������� ����� ���������</param>
    public void StartAction(AI executor, System.Action onComplete)
    {
        isStop = false;
        free = false;
        if (onStart != null)
            foreach (System.Action<AI> e in onStart)
            {
                e?.Invoke(executor);
            }
        onComplete += End;
        StartCoroutine(Action(executor, onComplete));
    }
    /// <summary>
    /// �������� ��� ���������� �������� � ���������
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    protected abstract IEnumerator Action(AI executor, System.Action onComplete);

}
