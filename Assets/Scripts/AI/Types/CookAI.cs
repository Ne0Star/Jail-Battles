using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Поведение поваров
/// </summary>
public class CookAI : AI, IAI
{

    public void DebugCheck()
    {
#if UNITY_EDITOR
        Debug.Log("DebugCheck(): " + gameObject.name);
#endif
    }


    //private void StartConsistency(AIConsistency consistency)
    //{
    //    free = false;
    //    UnityEngine.Events.UnityEvent onComplete = new UnityEngine.Events.UnityEvent();

    //    onComplete.AddListener(() =>
    //    {
    //        free = true;
    //    });

    //    consistency.StartConsisstency(this, onComplete);
    //}

    //private void StartAction(AIAction action)
    //{
    //    free = false;
    //    UnityEngine.Events.UnityEvent onComplete = new UnityEngine.Events.UnityEvent();

    //    onComplete.AddListener(() =>
    //    {
    //        free = true;
    //    });

    //    action.StartAction(this, onComplete);
    //}
    public int asd = 0;
    public override void CustomUpdate()
    {

        if (!free)
        {
            return;
        }

        List<AIConsistency> consistency = LevelManager.Instance.AiManager.GetConsistences(this);
        asd = consistency.Count;
        AIConsistency aIConsistency = consistency[Random.Range(0, consistency.Count)];

        free = false;
        aIConsistency.StartConsisstency(this, () => { free = true; });
    }
    public void Initial()
    {
        Debug.Log("initial");
    }
}
