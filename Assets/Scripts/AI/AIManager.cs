using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управляем всеми AI, имеет вспомогательные методы
/// </summary>
public class AIManager : MonoBehaviour
{

    [SerializeField] private List<AI> allAi;

    [SerializeField] private List<AIAction> allAction;
    [SerializeField] private List<AIConsistency> allConsistencies;

    private void Awake()
    {
        allAction.AddRange(FindObjectsOfType<AIAction>());
        allConsistencies.AddRange(FindObjectsOfType<AIConsistency>());
    }

    private void SortAI()
    {

    }


    /// <summary>
    /// Возвращает все сценарии для переданного типа AI
    /// </summary>
    /// <returns></returns>
    public List<AIConsistency> GetConsistences(AI sourcess)
    {
        List<AIConsistency> result = new List<AIConsistency>();
        foreach (AIConsistency consistency in allConsistencies)
        {
            if (consistency.ApplyTypes.Contains(sourcess.Type))
            {
                result.Add(consistency);
            }
        }
        return result;
    }
    /// <summary>
    /// Возвращает свободный сценарий для переданного типа AI
    /// </summary>
    /// <returns></returns>
    public AIConsistency GetConsistency(AI sourcess, bool random)
    {
        List<AIConsistency> result = null;
        if (random) result = new List<AIConsistency>();
        foreach (AIConsistency consistency in allConsistencies)
        {
            if (consistency.ApplyTypes.Contains(sourcess.Type))
            {
                //Debug.Log("Содержит");
                if (consistency.Free)
                {
                    if (!random)
                        return consistency;
                    result.Add(consistency);
                    //    Debug.Log("Свободен");
                }
            }
        }
        return result[Random.Range(0, Mathf.Clamp(result.Count - 1, 0, 100))];
    }
    /// <summary>
    /// Возвращает свободный сценарий для переданного типа AI
    /// </summary>
    /// <returns></returns>
    public AIConsistency GetConsistency(AI sourcess)
    {
        foreach (AIConsistency consistency in allConsistencies)
        {
            if (consistency.ApplyTypes.Contains(sourcess.Type))
            {
                //Debug.Log("Содержит");
                if (consistency.Free)
                {
                    return consistency;
                    //    Debug.Log("Свободен");
                }
            }
        }
        return null;
    }



    /// <summary>
    /// Возвращает все действия для указанного типа Ai
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<AIAction> GetAllAction<T>()
    {
        List<AIAction> result = new List<AIAction>();
        foreach (AIAction action in allAction)
        {

        }
        return result;
    }


    public void ChangeAI(Entity sources, ref AI last, AIPressset presset, AIType type)
    {
        if (last != null)
        {
            Destroy(last.gameObject);
            allAi.Remove(last);
            last = AddAI(sources, presset, type);
        }
        else
        {
            last = AddAI(sources, presset, type);
        }

        //return result;
    }

    private AI AddAI<T>(ref Entity sources, AIPressset presset, AIType type) where T : Component
    {
        GameObject obj = new GameObject("Ai");
        obj.transform.parent = sources.transform;
        AI result = obj.gameObject.AddComponent<T>() as AI;
        allAi.Add(result);
        result.SetPresset(presset, sources.Agent, sources.Animator, sources.Source, type);
        return result;
    }
    private AI AddAI<T>(ref Entity sources, AIPressset presset, AIType type, string name) where T : Component
    {
        GameObject obj = new GameObject("AI: " + name);
        obj.transform.parent = sources.transform;
        AI result = obj.gameObject.AddComponent<T>() as AI;
        allAi.Add(result);
        result.SetPresset(presset, sources.Agent, sources.Animator, sources.Source, type);
        return result;
    }
    public AI AddAI(Entity sources, AIPressset presset, AIType type)
    {
        AI result = null;
        switch (type)
        {
            case AIType.Преследователь:
                result = AddAI<PursueAI>(ref sources, presset, type, "Преследователь");
                return result;
            case AIType.Псих:
                result = AddAI<CrazyAI>(ref sources, presset, type, "Псих");
                return result;
            case AIType.Бычара:
                result = AddAI<BycharaAI>(ref sources, presset, type, "Бычара");
                return result;
            case AIType.Трус:
                result = AddAI<CowardAI>(ref sources, presset, type, "Трус");
                return result;
            case AIType.Маньяк:
                result = AddAI<ManiacAI>(ref sources, presset, type, "Маньяк");
                return result;
            case AIType.Уборщица:
                result = AddAI<CleaningAI>(ref sources, presset, type, "Уборщица");
                return result;
            case AIType.Повариха:
                result = AddAI<CookAI>(ref sources, presset, type, "Повариха");
                return result;
            case AIType.ГлавнаяПовариха:
                result = AddAI<HeadCookAI>(ref sources, presset, type, "Главная повариха");
                return result;
            case AIType.ГлавнаяУборщица:
                result = AddAI<MainCleningAI>(ref sources, presset, type, "Главная уборщица");
                return result;
            case AIType.Охранник:
                result = AddAI<GuardAI>(ref sources, presset, type, "Охранник");
                return result;
            case AIType.Крыса:
                result = AddAI<RatAI>(ref sources, presset, type, "Крыса");
                return result;
        }
        return null;
    }
}