using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private float updateTime = 0.1f;
    [SerializeField] private List<Trigger> triggers = new List<Trigger>();

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



    private void TriggersLife(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            if (i <= triggers.Count - 1 && i >= 0)
            {
                Trigger trigger = triggers[i];
                trigger.CustomUpdate();
            }
            else return;
        }
    }

    private IEnumerator Life()
    {
        int index = 5;
        int count = 0;
        for (int i = 0; i < triggers.Count; i += 5)
        {
            TriggersLife(i, index);
            index = i + 5;
            count++;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(updateTime * count);
        StartCoroutine(Life());
        yield break;
    }

}
