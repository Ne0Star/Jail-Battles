using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIManager : MonoBehaviour
{

    [SerializeField] private List<AI> allAi;


    public void ChangeAI(ref AI last, AIPressset presset, AIType type)
    {
        if (last != null)
        {
            Destroy(last.gameObject);
            allAi.Remove(last);
            last = AddAI(last.transform.parent, presset, type);
        }
        else
        {
            last = AddAI(last.transform.parent, presset, type);
        }

        //return result;
    }
    public AI AddAI(Transform parent, AIPressset presset, AIType type)
    {
        GameObject obj = null;
        AI result = null;
        switch (type)
        {
            case AIType.Преследователь:
                obj = new GameObject("Ai Преследователь");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<PursueAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.Псих:
                obj = new GameObject("Ai Псих");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<CrazyAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.Бычара:
                obj = new GameObject("Ai Бычара");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<BycharaAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.Трус:
                obj = new GameObject("Ai Трус");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<CowardAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.Маньяк:
                obj = new GameObject("Ai Маньяк");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<ManiacAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.Уборщица:
                obj = new GameObject("Ai Уборщица");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<CleaningAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.Повариха:
                obj = new GameObject("Ai Повариха");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<CookAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.ГлавнаяПовариха:
                obj = new GameObject("Ai Главная повариха");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<HeadCookAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.ГлавнаяУборщица:
                obj = new GameObject("Ai Главная уборщица");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<MainCleningAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.Охранник:
                obj = new GameObject("Ai Охранник");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<GuardAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
            case AIType.Крыса:
                obj = new GameObject("Ai Крыса");
                obj.transform.parent = parent;
                result = obj.gameObject.AddComponent<RatAI>(); allAi.Add(result); result.SetPresset(presset);
                return result;
        }
        return null;
    }


    //    [Header("Общее")]
    //    [SerializeField] private int maxPursuersCount = 10;
    //    [SerializeField] private int pursuersCount = 0;
    //    [SerializeField] private int pursuersOrder;

    //    [Header("Техническое ")]
    //    [SerializeField] private float allAiUpdateTime;
    //    [SerializeField] private EnemuManager enemuManager;
    //    [Range(1f, 100f)]
    //    [SerializeField] private float aiToFramePersent;
    //    [SerializeField] private int aiToFrame;
    //    [SerializeField] private int aiLast = 0;
    //    [SerializeField] private int aiCurrent = 0;
    //    [SerializeField] private List<AI> allAi;

    //    public int PursuersCount { get => pursuersCount; }
    //    public int MaxPursuersCount { get => maxPursuersCount; }
    //    public int PursuersOrder { get => pursuersOrder; }

    //    private void Awake()
    //    {
    //        if (!enemuManager) enemuManager = FindObjectOfType<EnemuManager>();

    //        StartCoroutine(AILife());
    //    }

    //    private IEnumerator AILife()
    //    {
    //        yield return new WaitForSeconds(allAiUpdateTime);
    //        if (allAi.Count > 0)
    //        {
    //            aiToFrame = allAi.Count == 0 ? 0 : Mathf.Clamp(Mathf.RoundToInt(allAi.Count / 100f * aiToFramePersent), 1, 100) + 5;
    //            int t = aiLast;
    //            int r = Mathf.Clamp(t + aiToFrame, 0, allAi.Count);
    //            for (int i = t; i < r; i++)
    //            {
    //                if (allAi[i] && allAi[i].gameObject.activeInHierarchy)
    //                {

    //                    allAi[i].BehaviourTick();

    //                }
    //                t++;
    //            }
    //            aiLast = t;
    //            if (aiLast >= allAi.Count)
    //            {
    //                aiLast = 0;
    //            }
    //        }
    //        if (pursuersCount < maxPursuersCount)
    //        {
    //            enemuManager.SpawnPursueEnemu();
    //        }
    //        StartCoroutine(AILife());
    //        yield break;
    //    }

    //    private void AddPursue()
    //    {
    //        pursuersCount++;
    //    }
    //    private void DividePursue()
    //    {
    //        pursuersCount--;
    //    }

    //    public AI CreateAI(GameObject sources, AIType type)
    //    {
    //        AI last = sources.GetComponent<AI>();
    //        AI result = null;
    //        switch (type)
    //        {
    //            case AIType.Динаминчый:
    //                DynamicAI da = sources.AddComponent<DynamicAI>();
    //                da._OnStartPursue?.AddListener(() => AddPursue());
    //                da._OnEndPursue?.AddListener(() => DividePursue());
    //                result = da;
    //                break;
    //            case AIType.Статичный:
    //                StaticAI sa = sources.AddComponent<StaticAI>();
    //                result = sa;
    //                break;
    //            case AIType.Запечённый:
    //                BathAI ba = sources.AddComponent<BathAI>();
    //                result = ba;
    //                break;
    //        }
    //        if (last != null)
    //        {
    //            if (allAi.Contains(last))
    //            {
    //                allAi[allAi.IndexOf(last)] = result;
    //#if UNITY_EDITOR
    //                DestroyImmediate(last);
    //#else
    //                            Destroy(last);
    //#endif
    //            }
    //        }
    //        else
    //        {
    //            allAi.Add(result);
    //        }
    //        return result;
    //    }

}