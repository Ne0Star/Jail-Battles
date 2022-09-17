using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class InvisibleCleaner : MonoBehaviour
{
    [SerializeField] private bool findChildComponents;
    [SerializeField] private MonoBehaviour[] components;
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Transform[] transforms;

    private void OnEnable()
    {

    }

    private void OnBecameInvisible()
    {
        foreach (Transform t in transforms)
        {
            if (t && t != transform)
            {
                t.gameObject.SetActive(false);
            }
        }
        foreach(MonoBehaviour component in components)
        {
            if(component)
            {
                component.enabled = false;
            }
        }
    }
    private void OnBecameVisible()
    {
        foreach (Transform t in transforms)
        {
            if (t)
            {
                t.gameObject.SetActive(true);
            }
        }
        foreach (MonoBehaviour component in components)
        {
            if (component)
            {
                component.enabled = true;
            }
        }
    }
}
