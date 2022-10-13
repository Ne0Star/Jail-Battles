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

    [SerializeField] private AIConsistency consistency;
    public bool mark;
    public override void CustomUpdate()
    {
        if (!free)
        {
            return;
        }
        mark = !mark;
        consistency = LevelManager.Instance.AiManager.GetConsistency(this);
        if (consistency != null && consistency.Free)
        {
            //consistency.Free = false;
            free = false;
            consistency.StartConsisstency(this, () => { free = true; });
        } else
        {
            consistency = null;
        }
    }
    public void Initial()
    {
        Debug.Log("initial");
    }
}
