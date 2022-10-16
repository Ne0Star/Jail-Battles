using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;




/// <summary>
/// Набор действий AI
/// </summary>
public class AIConsistency : AIEvents
{

#if UNITY_EDITOR
    private void DrawArrow(Vector2 startPos, Vector2 endPos)
    {

        Vector2 arrowPos;
        Vector2 arrowDirection;
        Vector3 angleVectorUp = new Vector3(0f, 0.40f, -1f) /*length*/;
        Vector3 angleVectorDown = new Vector3(0f, -0.40f, -1f) /*length*/;
        Vector2 upTmp;
        Vector2 downTmp;

        arrowDirection = endPos - startPos;
        arrowPos = startPos + (arrowDirection * 1f/*position along line*/);

        upTmp = Quaternion.LookRotation(arrowDirection) * (angleVectorUp * 0.5f);
        downTmp = Quaternion.LookRotation(arrowDirection) * (angleVectorDown * 0.5f);

        Gizmos.DrawLine(startPos, endPos);
        Gizmos.DrawRay(arrowPos, upTmp);
        Gizmos.DrawRay(arrowPos, downTmp);
    }


    private void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
    }

    private void Draw()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            if (t)
            {

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(t.position, 1f);
                if (i + 1 < transform.childCount)
                {
                    Gizmos.color = Color.blue;
                    float distance = Vector2.Distance(transform.GetChild(i + 1).position, transform.GetChild(i).position);
                    Vector3 dir = transform.GetChild(i + 1).position - transform.GetChild(i).position;
                    DrawArrow(transform.GetChild(i).position, transform.GetChild(i + 1).position);
                    //ForGizmo(transform.GetChild(i).position, dir);
                    //Debug.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
                }
            }
        }
    }


    [SerializeField] private bool drawGizmos = false;
    private void OnDrawGizmos()
    {
        if(drawGizmos)
        {
            Draw();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Draw();
    }
#endif

    [SerializeField] private bool findChild = false;
    [SerializeField] private AITypes applyTypes;
    /// <summary>
    /// Типы AI которые могут принять данный сценарий
    /// </summary>
    public AITypes ApplyTypes { get => applyTypes; }


    [SerializeField] private bool free;
    [SerializeField] private List<AIAction> actions;
    [SerializeField] private System.Action onStart;


    /// <summary>
    /// Свободен ли данный сценарий для выполнения
    /// </summary>
    public bool Free { get => CheckFree(); set => free = value; }


    private void Awake()
    {
        if (findChild)
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
    /// Запускает сценарий для данного AI
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
        Debug.Log("Сценарий завершён");
        SetFree(true);
        onComplete?.Invoke();
        yield break;
    }

}
