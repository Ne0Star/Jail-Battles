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
    private void Update()
    {
                if(Input.GetMouseButtonDown(0))
        agent.SetDestination((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    public override void CustomUpdate()
    {

    }
    public void Initial()
    {
        Debug.Log("initial");
    }
}
