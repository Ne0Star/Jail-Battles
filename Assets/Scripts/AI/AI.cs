﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIType
{
    /// <summary>
    /// Появляется в закрытой зоне
    /// Весь цикл жизни прследует игрока
    /// </summary>
    Преследователь,

    /// <summary>
    /// Двигается по карте и никого не трогает, но в случае нападения на него -
    /// начинает цикл жестоких убийств после окончания цикла снова становится спокойным
    /// </summary>
    Псих,

    /// <summary>
    /// С момента появления сразу находит себе цель, и дерётся до самой смерти
    /// </summary>
    Бычара,

    /// <summary>
    /// Избегает драк, в случае опасности убегает, в состоянии покоя ищет свободный угол что бы там спрятаться
    /// </summary>
    Трус,

    /// <summary>
    /// Появляется только в скромных местах карты, не уходоит далеко от точки спавна
    /// Когда кто либо проходит рядом он вырубает его и уносит к точке появления, там издевается над жертвой до её смерти
    /// </summary>
    Маньяк,

    /// <summary>
    /// Статичный AI, ходит по карте, оставляет лужи с водой
    /// </summary>
    Уборщица,

    /// <summary>
    /// Готовит еду, никого не трогает, не отходит далеко от главной поварихи
    /// </summary>
    Повариха,

    /// <summary>
    /// Ходит по кухне берёт вилки ложки ножи, подходит к окну выдачи и кидается в тех кто дерётся перед столовой
    /// </summary>
    ГлавнаяПовариха,

    /// <summary>
    /// Ходит по карте и никого не трогает в случае обнаружения драк вступает в бой и вызывает подкрепление ближайших уборщиц
    /// </summary>
    ГлавнаяУборщица,

    /// <summary>
    /// Не отходит далеко от спавна, убивает всех кто близко подходит
    /// </summary>
    Охранник,

    /// <summary>
    /// Бегают по карте группой, если голодны сьедают зека
    /// </summary>
    Крыса,


    Игрок

}


//[Flags]
//public enum AnimationTypes
//{
//    Idle,
//    Move,
//    Run,
//    Attack,
//    Special
//}

//[Flags]
//public enum AITypes
//{
//    Преследователь,
//    Псих,
//    Бычара,
//    Трус,
//    Маньяк,
//    Уборщица,
//    Повариха,
//    ГлавнаяПовариха,
//    ГлавнаяУборщица,
//    Охранник,
//    Крыса

//}

[System.Serializable]
public struct AITB
{
    //[SerializeField] private bool mark;
    [SerializeField] private AIType type;

    //public bool Mark { get => mark; }
    public AIType Type { get => type; }
}
[System.Serializable]
public struct AITypes
{
    [SerializeField]
    private List<AITB> aiTypes;

    public List<AITB> AiTypes { get => aiTypes; }

    //public List<AITB> AiTypes { get => aiTypes; }

    /// <summary>
    /// true если содержит переданный тип
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool Contains(AIType type)
    {
        bool result = false;
        if (aiTypes != null)
            for (int i = 0; i < aiTypes.Count; i++)
            {
                if (type == aiTypes[i].Type)
                {
                    result = true;
                    break;
                }
            }
        return result;
    }

}


[System.Serializable]
public struct AIStatPresset
{
    [SerializeField] private bool isRun;
    [SerializeField] private AnimationPresset animation;
    [SerializeField] private SoundPresset sound;

    public AnimationPresset Animation { get => animation; }
    public SoundPresset Sound { get => sound; }
}

[System.Serializable]
public struct AIStatsPresset
{
    [SerializeField] private AIStatPresset walk;
    [SerializeField] private AIStatPresset idle;

    [SerializeField] private List<AIStatPresset> standartStrikes;
    [SerializeField] private List<AIStatPresset> specialStrikes;
    [SerializeField] private List<AIStatPresset> bossStrikes;

    /// <summary>
    /// Ходьба
    /// </summary>
    public AIStatPresset Walk { get => walk; }
    /// <summary>
    /// Покой
    /// </summary>
    public AIStatPresset Idle { get => idle; }


    /// <summary>
    /// Стандартные удары, руками, ногами, в среднем 8-14 ударов для убийства
    /// </summary>
    public List<AIStatPresset> StandartStrikes { get => standartStrikes; }
    /// <summary>
    /// Специальные удары AI, например уборщица может ударить шваброй, в среднем 3-6 ударов до убийства
    /// </summary>
    public List<AIStatPresset> SpecialStrikes { get => specialStrikes; }
    /// <summary>
    /// Особые сложные удары, для особо опасных AI, которые могут убить с 1-3 ударов
    /// </summary>
    public List<AIStatPresset> BossStrikes { get => bossStrikes; }
}




[System.Serializable]
public struct AIPressset
{
    [SerializeField] private Transform rotateParent;
    [SerializeField] private float rotateOffset;
    [SerializeField] private AITypes targetTypes;
    [SerializeField] private AITypes ignoreTypes;
    [SerializeField] private List<Collider2D> colliders;

    [SerializeField] private AIStatsPresset states;


    public AITypes Targets { get => targetTypes; }
    public AITypes Ignores { get => ignoreTypes; }
    public Transform RotateParent { get => rotateParent; }
    public float RotateOffset { get => rotateOffset; }
    public AIStatsPresset States { get => states; }
    public List<Collider2D> Colliders { get => colliders; }
}
public abstract class AI : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private Animator animator;
    [SerializeField] private AIType type;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected AIPressset presset;
    [SerializeField] private bool blocker = false;
    [SerializeField] protected Entity entity;
    [SerializeField] protected bool free = true;
    /// <summary>
    /// Свободен ли данный AI в текущий момент
    /// </summary>
    public bool Free { get => free; }
    public NavMeshAgent Agent { get => agent; }
    public AIPressset Presset { get => presset; }
    public AIType Type { get => type; }
    public Animator Animator { get => animator; }
    public AudioSource Source { get => source; }
    public Entity Entity { get => entity; }

    private void Awake()
    {
        blocker = true;
        transform.localPosition = Vector3.zero;
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        blocker = false;
    }


    [SerializeField] protected AIConsistency consistency;
    public bool mark;

    public void CustomUpdate()
    {
        if (!free || !gameObject.activeInHierarchy)
        {
            return;
        }
        mark = !mark;
        consistency = LevelManager.Instance.AiManager.GetConsistency(this, true);
        if (consistency != null && consistency.Free)
        {
            //consistency.Free = false;
            free = false;
            consistency.StartConsisstency(this, () => { free = true; });
        }
        else
        {
            consistency = null;
        }
        if (blocker) return;
        UpdateAI();
    }

    /// <summary>
    /// Оптимизированное время обновления для AI
    /// </summary>
    protected abstract void UpdateAI();

    /// <summary>
    /// В один из триггеров AI зашла цель
    /// </summary>
    /// <param name="entity"></param>
    protected virtual void OnCustomTriggerStay(Entity entity)
    {

    }

    /// <summary>
    /// Сущность управляемая AI получила урон
    /// </summary>
    /// <param name="sources"></param>
    /// <param name="value"></param>
    protected virtual void OnDamaged(Entity sources, float value)
    {

    }

    public void SetPresset(AIPressset presset, NavMeshAgent agent, Entity entity, Animator animator, AudioSource source, AIType type)
    {
        this.presset = presset;
        this.agent = agent;
        this.type = type;
        this.animator = animator;
        this.source = source;
        this.entity = entity;

        if (entity && entity.HitBar)
            entity.HitBar.OnDamaged?.AddListener(OnDamaged);

        AITrigger[] triggers = entity.gameObject.GetComponentsInChildren<AITrigger>(true);
        foreach (AITrigger t in triggers)
        {
            t.SetAi(this);
            t.OnStay.AddListener((e) => OnCustomTriggerStay(e));
        }
    }
}
