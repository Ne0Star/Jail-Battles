using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен для динамической смены AI во время игры
/// </summary>
public class AIManager : MonoBehaviour
{

    [SerializeField] private List<AI> allAi;


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
            last = AddAI( sources, presset, type);
        }

        //return result;
    }

    private AI AddAI<T>(ref Entity sources, AIPressset presset) where T : Component
    {
        GameObject obj = new GameObject("Ai");
        obj.transform.parent = sources.transform;
        AI result = obj.gameObject.AddComponent<T>() as AI;
        allAi.Add(result);
        result.SetPresset(presset, sources.Agent);
        return result;
    }
    private AI AddAI<T>(ref Entity sources, AIPressset presset, string name) where T : Component
    {
        GameObject obj = new GameObject("AI: " + name);
        obj.transform.parent = sources.transform;
        AI result = obj.gameObject.AddComponent<T>() as AI;
        allAi.Add(result);
        result.SetPresset(presset, sources.Agent);
        return result;
    }
    public AI AddAI(Entity sources, AIPressset presset, AIType type)
    {
        AI result = null;
        switch (type)
        {
            case AIType.Преследователь:
                result = AddAI<PursueAI>(ref sources, presset, "Преследователь");
                return result;
            case AIType.Псих:
                result = AddAI<CrazyAI>(ref sources, presset, "Псих");
                return result;
            case AIType.Бычара:
                result = AddAI<BycharaAI>(ref sources, presset, "Бычара");
                return result;
            case AIType.Трус:
                result = AddAI<CowardAI>(ref sources, presset, "Трус");
                return result;
            case AIType.Маньяк:
                result = AddAI<ManiacAI>(ref sources, presset, "Маньяк");
                return result;
            case AIType.Уборщица:
                result = AddAI<CleaningAI>(ref sources, presset, "Уборщица");
                return result;
            case AIType.Повариха:
                result = AddAI<CookAI>(ref sources, presset, "Повариха");
                return result;
            case AIType.ГлавнаяПовариха:
                result = AddAI<HeadCookAI>(ref sources, presset, "Главная повариха");
                return result;
            case AIType.ГлавнаяУборщица:
                result = AddAI<MainCleningAI>(ref sources, presset, "Главная уборщица");
                return result;
            case AIType.Охранник:
                result = AddAI<GuardAI>(ref sources, presset, "Охранник");
                return result;
            case AIType.Крыса:
                result = AddAI<RatAI>(ref sources, presset, "Крыса");
                return result;
        }
        return null;
    }
}