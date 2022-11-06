using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelData
{

    [Header("Шанс что любой бот пойдет кушать")]
    [SerializeField] private float feedChance;
    [Header("Шанс любой бот пойдет в туалет")]
    [SerializeField] private float toiletChance;

    [Space(1)]

    [Header("Шанс что зек вступит в бой")]
    [SerializeField] private float pursurehance;
    [Header("Шанс что зек убежит из боя при низком уровне ХП")]
    [SerializeField] private float beginChance;

    [Space(1)]

    [Header("Шанс что зек пойдет лечится")]
    [SerializeField] private float healChance;
    [Header("Количесво хп для исцеления")]
    [SerializeField] private float healValue;

    public LevelData(float pursurehance, float feedChance, float toiletChance, float beginChance, float healChance, float healValue)
    {
        this.pursurehance = pursurehance;
        this.feedChance = feedChance;
        this.toiletChance = toiletChance;
        this.beginChance = beginChance;
        this.healChance = healChance;
        this.healValue = healValue;
    }
    public float Pursurehance { get => pursurehance; }
    public float FeedChance { get => feedChance; }
    public float ToiletChance { get => toiletChance; }
    public float BeginChance { get => beginChance; }
    public float HealChance { get => healChance; }
    public float HealValue { get => healValue; }
}

[System.Serializable]
public struct RangeRange
{
    public string name;
    public Color color;
    public int min, max;
    public float respawnTime;
}

[System.Serializable]
public struct UpdateData
{
    [SerializeField] private bool addtive;
    [SerializeField] float currentValue;
    [SerializeField] float min, max;

    public float CurrentValue { get => currentValue; }

    public void Update()
    {
        if (addtive)
        {
            currentValue += Random.Range(min, max);
        }
        else
        {
            currentValue -= Random.Range(min, max);
        }

    }

}

[System.Serializable]
public struct Stat
{
    [SerializeField] private bool addtive;
    [SerializeField] private float defaultValue;
    [SerializeField] private float maxValue;
    [SerializeField] private float currentValue;
    [SerializeField] private float regeneratePercent;

    public float CurrentValue { get => currentValue; set => currentValue = value; }
    public float DefaultValue { get => defaultValue; }
    public float MaxValue { get => maxValue; }

    public void UpdateDefaultValue(float value)
    {
        this.defaultValue = value;
    }
    public void Normalize()
    {
        float regenValue = (1f * regeneratePercent / 100);

        currentValue = Mathf.Lerp(currentValue, defaultValue, regenValue);

        //currentValue = Mathf.Clamp(addtive ? currentValue + regenValue : currentValue - regenValue, defaultValue, 9999);
    }

}

public enum AreaType
{
    Кухня,
    Столовая,
    КабинетМедсестры
}

[System.Serializable]
public struct AIAreas
{
    [SerializeField] private AreaType areaType;
    [SerializeField] private List<AIArea> areas;

    public AreaType AreaType { get => areaType; }
    public List<AIArea> Areas { get => areas; }
}

public class LevelManager : OneSingleton<LevelManager>
{
    [SerializeField] private List<AIAreas> areas;
    [SerializeField] private List<RangeRange> ranges;
    [SerializeField] private float customTime;
    [SerializeField] private LevelData levelData;
    [SerializeField] private Player player;
    [SerializeField] private LevelPresset levelPresset;

    public Player Player { get => player; }
    public LevelPresset LevelPresset { get => levelPresset; }
    public LevelData LevelData { get => levelData; }
    public float CustomTime { get => customTime; }


    #region Managers
    [SerializeField] private EnemuManager enemuManager;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private TriggerManager triggerManager;
    [SerializeField] private CleanerManager cleanerManager;
    [SerializeField] private TrashManager trashManager;



    public EnemuManager EnemuManager { get => enemuManager; }
    public WeaponManager WeaponManager { get => weaponManager; }
    public TriggerManager TriggerManager { get => triggerManager; }
    public CleanerManager CleanerManager { get => cleanerManager; }
    public TrashManager TrashManager { get => trashManager; }

    #endregion




    public List<AIArea> GetAreas(AreaType type)
    {
        foreach (AIAreas area in areas)
        {
            if (area.AreaType == type)
            {
                return area.Areas;
            }
        }
        return null;
    }

    public RangeRange GetColorByRange(int range)
    {
        RangeRange result = new RangeRange();
        foreach (RangeRange r in ranges)
        {
            if (range <= r.max && range >= r.min)
            {
                result = r;
                return result;
            }
        }
        return result;
    }


    public Entity[] GetAllEntites()
    {
        List<Entity> result = new List<Entity>();
        result.AddRange(enemuManager.AllEnemies.Values);
        result.Add(Player);
        return result.ToArray();
    }
    [SerializeField] private List<AudioClip> findClips = new List<AudioClip>();
    [SerializeField] private Dictionary<string, AudioClip> allClips = new Dictionary<string, AudioClip>();


    public AudioClip GetClip(string name)
    {
        AudioClip result = null;
        allClips.TryGetValue(name, out result);
        return result;
    }

    private void Awake()
    {
        LevelManager.Instance = this;

        foreach (AudioClip clip in findClips)
        {
            if (clip != null)
            {
                allClips.Add(clip.name, clip);
            }
        }
        findClips = null;
        if (!enemuManager) enemuManager = FindObjectOfType<EnemuManager>(true);
        if (!triggerManager) triggerManager = FindObjectOfType<TriggerManager>(true);
        if (!weaponManager) weaponManager = FindObjectOfType<WeaponManager>(true);
        if (!cleanerManager) cleanerManager = FindObjectOfType<CleanerManager>(true);
        if (!trashManager) trashManager = FindObjectOfType<TrashManager>(true);
        StartCoroutine(Wait());
    }

    private IEnumerator Start()
    {
        triggerManager.gameObject.SetActive(true);

        weaponManager.gameObject.SetActive(true);

        cleanerManager.gameObject.SetActive(true);
        enemuManager.gameObject.SetActive(true);

        trashManager.gameObject.SetActive(true);

        yield return null;
    }

    private IEnumerator Wait()
    {
        for (int i = 0; i < 180; i++)
        {
            //Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("ПРОШЛО 3 МИНУТЫ");
    }
}
