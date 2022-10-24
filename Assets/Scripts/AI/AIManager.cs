using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Управляем всеми AI, имеет вспомогательные методы
/// </summary>
public class AIManager : MonoBehaviour
{
    [SerializeField] private List<AI> allAi;
    [SerializeField] private List<AIArea> areas;

    private void Awake()
    {
        //allAction.AddRange(FindObjectsOfType<AIAction>());
        areas.AddRange(FindObjectsOfType<AIArea>(true));
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
    public Entity[] GetAllEntityByAI(AIType type)
    {
        List<Entity> result = new List<Entity>();
        switch (type)
        {
            case AIType.Преследователь:
                foreach (AI ai in puesues)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.Псих:
                foreach (AI ai in crazies)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.Бычара:
                foreach (AI ai in bycharaz)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.Трус:
                foreach (AI ai in cowards)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.Маньяк:
                foreach (AI ai in maniacs)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.Уборщица:
                foreach (AI ai in cleanings)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.Повариха:
                foreach (AI ai in cooks)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.ГлавнаяПовариха:
                foreach (AI ai in headCooks)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.ГлавнаяУборщица:
                foreach (AI ai in headCleanings)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.Охранник:
                foreach (AI ai in guardians)
                {
                    result.Add(ai.Entity);
                }
                break;
            case AIType.Крыса:
                return null;
            case AIType.Игрок:
                result.Add(LevelManager.Instance.Player);
                break;
        }
        return result.ToArray();
    }
    public AI[] GetAllAi(AIType type)
    {
        List<Entity> result = new List<Entity>();
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
            case AIType.Игрок:

                break;
        }
        return null;
    }
    public void ChangeAI(Entity sources, ref AI last, AIUniversalData presset, AIType type, AITypes targetTypes)
    {
        if (last != null)
        {
            Destroy(last.gameObject);
            allAi.Remove(last);
            RemoveAi(last);
            last = AddAI(sources, presset, type, targetTypes);
        }
        else
        {
            last = AddAI(sources, presset, type, targetTypes);
        }

        //return result;
    }
    private AI AddAI<T>(ref Entity sources, AIUniversalData presset, AIType type, AITypes targetTypes) where T : Component
    {
        GameObject obj = new GameObject("Ai");
        obj.transform.parent = sources.transform;
        AI result = obj.gameObject.AddComponent<T>() as AI;
        result.SetPresset(presset, sources, type, targetTypes);
        return result;
    }
    private AI AddAI<T>(ref Entity sources, AIUniversalData presset, AIType type, string name, AITypes targetTypes) where T : Component
    {
        GameObject obj = new GameObject("AI: " + name + " " + allAi.Count);
        obj.transform.parent = sources.transform;
        AI result = obj.gameObject.AddComponent<T>() as AI;
        result.SetPresset(presset, sources, type, targetTypes);
        return result;
    }
    public AI AddAI(Entity sources, AIUniversalData presset, AIType type, AITypes targetTypes)
    {
        AI result = null;
        int index = 0;
        switch (type)
        {
            case AIType.Преследователь:
                result = AddAI<PursueAI>(ref sources, presset, type, "Преследователь", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                puesues.Add((PursueAI)allAi[index]);
                return result;
            case AIType.Псих:
                result = AddAI<CrazyAI>(ref sources, presset, type, "Псих", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                crazies.Add((CrazyAI)allAi[index]);
                return result;
            case AIType.Бычара:
                result = AddAI<BycharaAI>(ref sources, presset, type, "Бычара", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                bycharaz.Add((BycharaAI)allAi[index]);
                return result;
            case AIType.Трус:
                result = AddAI<CowardAI>(ref sources, presset, type, "Трус", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                cowards.Add((CowardAI)allAi[index]);
                return result;
            case AIType.Маньяк:
                result = AddAI<ManiacAI>(ref sources, presset, type, "Маньяк", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                maniacs.Add((ManiacAI)allAi[index]);
                return result;
            case AIType.Уборщица:
                result = AddAI<CleaningAI>(ref sources, presset, type, "Уборщица", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                cleanings.Add((CleaningAI)allAi[index]);
                return result;
            case AIType.Повариха:
                result = AddAI<CookAI>(ref sources, presset, type, "Повариха", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                cooks.Add((CookAI)allAi[index]);
                return result;
            case AIType.ГлавнаяПовариха:
                result = AddAI<HeadCookAI>(ref sources, presset, type, "Главная повариха", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                headCooks.Add((HeadCookAI)allAi[index]);
                return result;
            case AIType.ГлавнаяУборщица:
                result = AddAI<MainCleningAI>(ref sources, presset, type, "Главная уборщица", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                headCleanings.Add((MainCleningAI)allAi[index]);
                return result;
            case AIType.Охранник:
                result = AddAI<GuardAI>(ref sources, presset, type, "Охранник", targetTypes);
                allAi.Add(result);
                index = allAi.IndexOf(result);
                cowards.Add((CowardAI)allAi[index]);
                return result;
            case AIType.Крыса:
                result = AddAI<RatAI>(ref sources, presset, type, "Крыса", targetTypes);
                return result;
        }
        return null;
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
    public List<AIArea> Areas { get => areas; }

}