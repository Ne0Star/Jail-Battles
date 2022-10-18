using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Действие AI
/// </summary>
public abstract class AIAction : AIEvents
{
    [SerializeField] private List<System.Action<AI>> onStart;
    [SerializeField] private List<System.Action> onEnd;

    [SerializeField] protected AnimationStatesPresset states;

    [SerializeField] protected float duration;
    [SerializeField] private bool free = true;

    /// <summary>
    /// Свободно ли данное действие
    /// </summary>
    public bool Free { get => free; }

    [SerializeField] protected bool isStop = true;
    /// <summary>
    /// Задержка действия, в зависимости от действия используется по разному
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
    /// Запускает действие для данного AI
    /// </summary>
    /// <param name="executor">Исполнитель</param>
    /// <param name="onComplete">Сработает когда действие будет завершено</param>
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
    /// Корутина для выполнения действий с задержкой
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    protected abstract IEnumerator Action(AI executor, System.Action onComplete);

}
