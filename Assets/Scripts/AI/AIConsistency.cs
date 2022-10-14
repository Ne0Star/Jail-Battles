using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ����� �������� AI
/// </summary>
public class AIConsistency : AIEvents
{
    [SerializeField] private bool findChild = false;
    [SerializeField] private AITypes applyTypes;
    /// <summary>
    /// ���� AI ������� ����� ������� ������ ��������
    /// </summary>
    public AITypes ApplyTypes { get => applyTypes; }


    [SerializeField] private bool free;
    [SerializeField] private List<AIAction> actions;
    [SerializeField] private System.Action onStart;


    /// <summary>
    /// �������� �� ������ �������� ��� ����������
    /// </summary>
    public bool Free { get => CheckFree(); set => free = value; }


    private void Awake()
    {
        if(findChild)
        {
            actions.Clear();
            actions.AddRange(transform.GetComponentsInChildren<AIAction>());
        }
    }


    private bool CheckFree()
    {
        bool tempFree = true;
        foreach (AIAction action in actions)
        {
            if (!action.Free)
            {
                tempFree = false;
                break;
            }
        }
        return tempFree;
    }


    private void SetFree(bool val)
    {
        free = val;
    }

    /// <summary>
    /// ��������� �������� ��� ������� AI
    /// </summary>
    /// <param name="onComplete"></param>
    public void StartConsisstency(AI ai, System.Action onComplete)
    {
        if (!free) return;
        SetFree(false);
        StartCoroutine(StartActions(ai, onComplete));
    }
    [SerializeField] int index = 0;
    private IEnumerator StartActions(AI ai, System.Action onComplete)
    {
        index = 0;
        foreach (AIAction action in actions)
        {
            bool next = false;
            action.StartAction(ai, () => next = true);
            yield return new WaitUntil(() => next);
            index++;
        }
        Debug.Log("�������� ��������");
        SetFree(true);
        onComplete?.Invoke();
        yield break;
    }

}
