using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct AICondition
{
    [SerializeField] private UnityEvent onSet;
    [SerializeField] private bool value;
    [SerializeField] private string name;
    public AICondition(ref bool value) : this()
    {
        this.value = value;
    }

    public UnityEvent OnSet { get => onSet; }
    public string Name { get => name;}
}

public class Enemu : Entity, ICustomListItem
{
    [SerializeField] private float currentTime;

    [SerializeField] private int updateCount;

    [SerializeField] private AICondition idle;
    [SerializeField] private AICondition move;


    protected override void Enable()
    {


        if (agent)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    public void CustomUpdate()
    {
        if (!gameObject.activeSelf)
        {
            if (currentTime >= LevelManager.Instance.GetColorByRange(updateCount).respawnTime)
            {
                gameObject.SetActive(true);
                currentTime = 0f;
            }
            currentTime += 0.02f;
            return;
        }



    }
}
