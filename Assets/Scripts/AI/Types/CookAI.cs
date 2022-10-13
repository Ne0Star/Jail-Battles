using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Поведение поваров
/// </summary>
public class CookAI : AI, IAI
{
    private bool isIdle = true;
    private bool isMove = false;

    public void DebugCheck()
    {
#if UNITY_EDITOR
        Debug.Log("DebugCheck(): " + gameObject.name);
#endif
    }


    public override void CustomUpdate()
    {

        if (!free)
        {
            return;
        }

        List<AIConsistency> consistency = LevelManager.Instance.AiManager.GetConsistences(this);
        AIConsistency aIConsistency = consistency[Random.Range(0, consistency.Count)];

        free = false;
        aIConsistency.StartConsisstency(this, () => { free = true; });
    }
    public void Initial()
    {
        Debug.Log("initial");
    }
}
