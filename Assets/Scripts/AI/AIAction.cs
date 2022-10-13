using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct ActionState
{
    public string animationName;
    public SoundPresset presset;
}
[System.Serializable]
public struct ActionStates
{
    public ActionState onStart;
    public ActionState onComplete;
}

/// <summary>
/// Действие AI
/// </summary>
public abstract class AIAction : AIEvents
{
    [SerializeField] private List<System.Action<AI>> onStart;
    [SerializeField] private List<System.Action> onEnd;

    [SerializeField] protected ActionStates states;


    [SerializeField] protected float duration;
    [SerializeField] private bool free = true;

    /// <summary>
    /// Свободно ли данное действие
    /// </summary>
    public bool Free { get => free; }
    /// <summary>
    /// Задержка действия, в зависимости от действия используется по разному
    /// </summary>
    public float Duration { get => duration; }
    public ActionStates States { get => states; }

    private void End()
    {
        free = true;
        if (onEnd != null)
            foreach (System.Action e in onEnd)
            {

                e?.Invoke();
            }
    }

    /// <summary>
    /// Запускает действие для данного AI
    /// </summary>
    /// <param name="executor">Исполнитель</param>
    /// <param name="onComplete">Сработает когда действие будет завершено</param>
    public void StartAction(AI executor, System.Action onComplete)
    {
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
