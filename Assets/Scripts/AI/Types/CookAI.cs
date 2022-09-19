using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookAI : AI, IAI
{
    public void DebugCheck()
    {
#if UNITY_EDITOR
        Debug.Log("DebugCheck(): " + gameObject.name);
#endif
    }

    public void Initial()
    {

    }
}
