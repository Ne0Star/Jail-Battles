using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
public class AIStatPresset
{
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
    [SerializeField] private AIStatPresset figtStance;

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
    /// Боевая стойка
    /// </summary>
    public AIStatPresset FigtStance { get => figtStance; }




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
public struct AIUniversalData
{
    [SerializeField] private List<AIAction> life;

    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private float rotateOffset;
    [SerializeField] private Transform rotateParent;

    public List<AIAction> Life { get => life; }
    public float Speed { get => speed; }
    public float RotateSpeed { get => rotateSpeed; }
    public float RotateOffset { get => rotateOffset; }
    public Transform RotateParent { get => rotateParent; }
}

public abstract class AI : MonoBehaviour
{
    [SerializeField] protected bool visible = false;
    [SerializeField] private AudioSource source;
    [SerializeField] private Animator animator;
    [SerializeField] private AIType type;
    [SerializeField] protected AIUniversalData data;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] private bool blocker = false;
    [SerializeField] protected Entity entity;
    [SerializeField] protected bool free = true;
    [SerializeField] private AITypes targetTypes;

    /// <summary>
    /// Свободен ли данный AI в текущий момент
    /// </summary>
    public bool Free { get => free; }
    public NavMeshAgent Agent { get => agent; }
    public AIType Type { get => type; }
    public Animator Animator { get => animator; }
    public AudioSource Source { get => source; }
    public Entity Entity { get => entity; }
    public AITypes TargetTypes { get => targetTypes; }
    public AIUniversalData Data { get => data; }


    protected virtual void Disable()
    {

    }
    protected virtual void Enable()
    {

    }
    protected virtual void Create()
    {

    }
    private void OnDisable()
    {
        Disable();
    }
    private void OnEnable()
    {
        transform.localPosition = Vector2.zero;
        Enable();
    }

    public void SetVisible(bool visible)
    {
        this.visible = visible;
    }

    public void CustomUpdate()
    {
        if (!free || !gameObject.activeInHierarchy)
        {
            return;
        }
        if (blocker) return;

        //if (!visible)
        //{
        //    agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        //    entity.Animator.enabled = false;
        //    entity.Source.enabled = false;
        //    entity.HitBar.HideVisual();
        //}
        //else
        //{
        //    agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        //    entity.Animator.enabled = true;
        //    entity.Source.enabled = true;
        //    entity.HitBar.ShowVisual();
        //}

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
    public void SetPresset(AIUniversalData data, NavMeshAgent agent, Entity entity, Animator animator, AudioSource source, AIType type, AITypes targetTypes)
    {
        this.targetTypes = targetTypes;
        this.data = data;
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
        Create();
    }
}
