using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private float updateTime = 0.1f;
    [SerializeField] private List<Trigger> triggers;

    private void Awake()
    {
        StartCoroutine(Life());
    }

    private void Start()
    {

    }

    public void Register(Trigger trigger)
    {
        triggers.Add(trigger);
    }
    public void UnRegister(Trigger trigger)
    {
        triggers.Remove(trigger);
        triggers.Distinct();
    }

    private IEnumerator Life()
    {
        foreach (Trigger trigger in triggers)
        {
            trigger.CustomUpdate();
        }

        yield return new WaitForSeconds(updateTime);
        StartCoroutine(Life());
        yield break;
    }

}
