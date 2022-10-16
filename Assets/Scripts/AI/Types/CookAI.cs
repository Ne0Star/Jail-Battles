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

    protected override void UpdateAI()
    {
    }
    public void Initial()
    {
        Debug.Log("initial");
    }
}
