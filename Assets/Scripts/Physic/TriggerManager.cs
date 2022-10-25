using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public interface ICustomListItem
{
    void CustomUpdate();

}
[System.Serializable]
public class CustomList<T> where T : ICustomListItem
{
   [SerializeField] private List<T> values;
    private float updateTime = 0.1f;
    private int stepCount = 30;
    [SerializeField]
    private bool stop = true;

    public List<T> Values { get => values; }

    public CustomList(float updateTime, int stepCount)
    {
        this.updateTime = updateTime;
        this.stepCount = stepCount;
        values = new List<T>();
        stop = true;
    }
    public CustomList(float updateTime)
    {
        this.updateTime = updateTime;
        values = new List<T>();
        stop = true;
    }

    public void RegisterRange(T[] values)
    {
        this.values.AddRange(values);
    }

    public void StopLife()
    {
        stop = true;
    }
    public void StartLife(MonoBehaviour context)
    {
        context.StartCoroutine(Life());
    }

    public void Register(T trigger)
    {
        values.Add(trigger);
    }
    public void UnRegister(T trigger)
    {
        values.Remove(trigger);
        values.Distinct();
    }


    private void TriggersLife(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            if (i <= values.Count - 1 && i >= 0)
            {
                T trigger = values[i];
                trigger.CustomUpdate();
            }
            else return;
        }
    }

    private IEnumerator Life()
    {
        stop = false;
        while (!stop)
        {
            for (int i = 0; i < values.Count; i += stepCount)
            {
                TriggersLife(i, i + stepCount);
                yield return new WaitForSeconds(updateTime);
            }
            yield return new WaitForFixedUpdate();
        }
    }

}


public class TriggerManager : MonoBehaviour
{
    [SerializeField] private float updateTime = 0.1f;
    [SerializeField] private int stepCount = 30;
    [SerializeField] private CustomList<Trigger> triggers;

    private void Awake()
    {
        triggers = new CustomList<Trigger>(updateTime, stepCount);
        triggers.RegisterRange(FindObjectsOfType<Trigger>());
        triggers.StartLife(this);
    }

    public void Register(Trigger trigger)
    {
        if(triggers != null)
        triggers.Register(trigger);
    }
    public void UnRegister(Trigger trigger)
    {
        if (triggers != null)
            triggers.UnRegister(trigger);
    }
}
