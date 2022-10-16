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
        int index = 0;
        foreach (AIConsistency consistency in allConsistencies)
        {
            if (consistency.ApplyTypes.Contains(sourcess.Type))
            {
                //Debug.Log("Содержит");
                if (consistency.Free)
                {
                    if (!random)
                        return consistency;
                    index++;
                    result.Add(consistency);
                    //    Debug.Log("Свободен");
                }
            }
        }
        if (result == null || result.Count == 0) return null;
        return result[Random.Range(0, Mathf.Clamp(result.Count, 0, index))];
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

    private void RemoveAi(AI ai)
    {
        AIType type = ai.Type;
        switch (type)
        {
            case AIType.Преследователь:
                puesues.Remove((PursueAI)ai);
                break;
            case AIType.Псих:
                crazies.Remove((CrazyAI)ai);
                break;
            case AIType.Бычара:
                bycharaz.Remove((BycharaAI)ai);
                break;
            case AIType.Трус:
                cowards.Remove((CowardAI)ai);
                break;
            case AIType.Маньяк:
                maniacs.Remove((ManiacAI)ai);
                break;
            case AIType.Уборщица:
                cleanings.Remove((CleaningAI)ai);
                break;
            case AIType.Повариха:
                cooks.Remove((CookAI)ai);
                break;
            case AIType.ГлавнаяПовариха:
                headCooks.Remove((HeadCookAI)ai);
                break;
            case AIType.ГлавнаяУборщица:
                headCleanings.Remove((MainCleningAI)ai);
                break;
            case AIType.Охранник:
                guardians.Remove((GuardAI)ai);
                break;
            case AIType.Крыса:
                break;
        }
    }


    public AI[] GetAllAi(AIType type)
    {
        switch (type)
        {
            case AIType.Преследователь:
                return puesues.ToArray();
            case AIType.Псих:
                return crazies.ToArray();
            case AIType.Бычара:
                return bycharaz.ToArray();
            case AIType.Трус:
                return cowards.ToArray();
            case AIType.Маньяк:
                return maniacs.ToArray();
            case AIType.Уборщица:
                return cleanings.ToArray();
            case AIType.Повариха:
                return cooks.ToArray();
            case AIType.ГлавнаяПовариха:
                return headCooks.ToArray();
            case AIType.ГлавнаяУборщица:
                return headCleanings.ToArray();
            case AIType.Охранник:
                return guardians.ToArray();
            case AIType.Крыса:
                break;
        }
        return null;
    }

    public void ChangeAI(Entity sources, ref AI last, AIPressset presset, AIType type)
    {
        if (last != null)
        {
            Destroy(last.gameObject);
            allAi.Remove(last);
            RemoveAi(last);
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
        result.SetPresset(presset, sources.Agent, sources.Animator, sources.Source, type);
        return result;
    }
    private AI AddAI<T>(ref Entity sources, AIPressset presset, AIType type, string name) where T : Component
    {
        GameObject obj = new GameObject("AI: " + name);
        obj.transform.parent = sources.transform;
        AI result = obj.gameObject.AddComponent<T>() as AI;
        result.SetPresset(presset, sources.Agent, sources.Animator, sources.Source, type);
        return result;
    }


    [SerializeField] private List<PursueAI> puesues;
    [SerializeField] private List<CrazyAI> crazies;
    [SerializeField] private List<BycharaAI> bycharaz;
    [SerializeField] private List<CowardAI> cowards;
    [SerializeField] private List<ManiacAI> maniacs;
    [SerializeField] private List<GuardAI> guardians;

    [SerializeField] private List<CleaningAI> cleanings;
    [SerializeField] private List<MainCleningAI> headCleanings;

    [SerializeField] private List<CookAI> cooks;
    [SerializeField] private List<HeadCookAI> headCooks;

    public List<PursueAI> Puesues { get => puesues; }
    public List<CrazyAI> Crazies { get => crazies; }
    public List<BycharaAI> Bycharaz { get => bycharaz; }
    public List<CowardAI> Cowards { get => cowards; }
    public List<ManiacAI> Maniacs { get => maniacs; }
    public List<GuardAI> Guardians { get => guardians; }
    public List<CleaningAI> Cleanings { get => cleanings; }
    public List<MainCleningAI> HeadCleanings { get => headCleanings; }
    public List<CookAI> Cooks { get => cooks; }
    public List<HeadCookAI> HeadCooks { get => headCooks; }

    public AI AddAI(Entity sources, AIPressset presset, AIType type)
    {
        AI result = null;
        int index = 0;
        switch (type)
        {
            case AIType.Преследователь:
                result = AddAI<PursueAI>(ref sources, presset, type, "Преследователь");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                puesues.Add((PursueAI)allAi[index]);
                return result;
            case AIType.Псих:
                result = AddAI<CrazyAI>(ref sources, presset, type, "Псих");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                crazies.Add((CrazyAI)allAi[index]);
                return result;
            case AIType.Бычара:
                result = AddAI<BycharaAI>(ref sources, presset, type, "Бычара");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                bycharaz.Add((BycharaAI)allAi[index]);
                return result;
            case AIType.Трус:
                result = AddAI<CowardAI>(ref sources, presset, type, "Трус");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                cowards.Add((CowardAI)allAi[index]);
                return result;
            case AIType.Маньяк:
                result = AddAI<ManiacAI>(ref sources, presset, type, "Маньяк");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                maniacs.Add((ManiacAI)allAi[index]);
                return result;
            case AIType.Уборщица:
                result = AddAI<CleaningAI>(ref sources, presset, type, "Уборщица");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                cleanings.Add((CleaningAI)allAi[index]);
                return result;
            case AIType.Повариха:
                result = AddAI<CookAI>(ref sources, presset, type, "Повариха");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                cooks.Add((CookAI)allAi[index]);
                return result;
            case AIType.ГлавнаяПовариха:
                result = AddAI<HeadCookAI>(ref sources, presset, type, "Главная повариха");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                headCooks.Add((HeadCookAI)allAi[index]);
                return result;
            case AIType.ГлавнаяУборщица:
                result = AddAI<MainCleningAI>(ref sources, presset, type, "Главная уборщица");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                headCleanings.Add((MainCleningAI)allAi[index]);
                return result;
            case AIType.Охранник:
                result = AddAI<GuardAI>(ref sources, presset, type, "Охранник");
                allAi.Add(result);
                index = allAi.IndexOf(result);
                cowards.Add((CowardAI)allAi[index]);
                return result;
            case AIType.Крыса:
                result = AddAI<RatAI>(ref sources, presset, type, "Крыса");
                return result;
        }
        return null;
    }
}