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


    public void Play(Animator animator, AudioSource sources)
    {
        if (animation && animator.gameObject.activeSelf)
        {
            animator.speed = animation.speed;

            animator.Play(animation.animationName);
        }
        if (sound && sources.gameObject.activeSelf)
        {
            sources.loop = sound.Loop;
            sources.clip = sound.Clip;
            sources.pitch = sound.Pitch;
            sources.volume = sound.Volume;
            sources.Play();
        }
        else
        {
            sources.clip = null;
        }
    }
    public void Play(Animator animator, AudioSource sources, float animationSpeed)
    {
        if (animation && animator.gameObject.activeSelf)
        {
            animator.speed = animation.speed * animationSpeed;

            animator.Play(animation.animationName);
        }
        if (sound && sources.gameObject.activeSelf)
        {
            sources.loop = sound.Loop;
            sources.clip = sound.Clip;
            sources.pitch = sound.Pitch * animationSpeed;
            sources.volume = sound.Volume;
            sources.Play();
        }
    }
}
[System.Serializable]
public class AIFightStat
{
    public float damage;
    public float attackSpeed;
    public AIStatPresset presset;
}
[System.Serializable]
public struct AIStatsPresset
{
    [SerializeField] private AIStatPresset walk;
    [SerializeField] private AIStatPresset idle;
    [SerializeField] private AIStatPresset figtStance;

    [SerializeField] private List<AIFightStat> standartStrikes;
    [SerializeField] private List<AIFightStat> specialStrikes;
    [SerializeField] private List<AIFightStat> bossStrikes;

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

    public AIFightStat GetStandartStrike()
    {
        return standartStrikes[Random.Range(0, standartStrikes.Count)];
    }
    public AIFightStat GetSpecialStrike()
    {
        return specialStrikes[Random.Range(0, specialStrikes.Count)];
    }
    public AIFightStat GetBossStrike()
    {
        return bossStrikes[Random.Range(0, bossStrikes.Count)];
    }

    /// <summary>
    /// Стандартные удары, руками, ногами, в среднем 8-14 ударов для убийства
    /// </summary>
    public List<AIFightStat> StandartStrikes { get => standartStrikes; }
    /// <summary>
    /// Специальные удары AI, например уборщица может ударить шваброй, в среднем 3-6 ударов до убийства
    /// </summary>
    public List<AIFightStat> SpecialStrikes { get => specialStrikes; }
    /// <summary>
    /// Особые сложные удары, для особо опасных AI, которые могут убить с 1-3 ударов
    /// </summary>
    public List<AIFightStat> BossStrikes { get => bossStrikes; }

}

[System.Serializable]
public struct AIUniversalData
{
    [SerializeField] private List<AIAction> life;

    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private float rotateOffset;
    [SerializeField] private Transform rotateParent;
    public float Speed { get => speed; }
    public float RotateSpeed { get => rotateSpeed; }
    public float RotateOffset { get => rotateOffset; }
    public Transform RotateParent { get => rotateParent; }
    public List<AIAction> Life { get => life; }
}

public abstract class AI : MonoBehaviour
{
    [SerializeField] private AIType type;
    [SerializeField] protected AIUniversalData data;
    [SerializeField] protected Entity entity;
    [SerializeField] private AITypes targetTypes;

    public AIType Type { get => type; }
    public Entity Entity { get => entity; }
    public AITypes TargetTypes { get => targetTypes; }
    public AIUniversalData Data { get => data; }


    public Animator Animator { get => entity.Animator; }
    public AudioSource Source { get => entity.Source; }
    public NavMeshAgent Agent => entity.Agent;
    public HitBar Hit => entity.HitBar;
    public AIStatsPresset Stats { get => entity.Stats; }


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



    [SerializeField] private int index;
    [SerializeField] private List<AIAction> lifeActions = new List<AIAction>();
    [SerializeField] private List<AIAction> stackActions = new List<AIAction>();
    [SerializeField] private AIAction currentAction;
    [SerializeField] private AIAction lastAction = null;
    public AIAction CurrentAction { get => currentAction; }


    /// <summary>
    /// Добавляет новое действие для AI
    /// </summary>
    /// <param name="action"></param>
    /// <param name="breakMode"></param>
    protected void AddAction(AIAction action, System.Action onComplete)
    {
        if (action == null ) return;
        lifeActions.Add(action);
        action.OnComplete.AddListener(() => onComplete());
    }
    /// <summary>
    /// Добавляет новое действие для AI
    /// </summary>
    /// <param name="action"></param>
    /// <param name="breakMode"></param>
    protected void AddAction(AIAction action)
    {
        if (action == null) return;
        lifeActions.Add(action);
    }
    /// <summary>
    /// Принудительно начинает выполнение указанного действия, после возвращается к предыдущему
    /// </summary>
    /// <param name="action"></param>
    /// <param name="onComplete">Выполнится когда указанное действие закончит работу</param>
    protected void SetAction(AIAction action, System.Action onComplete)
    {
        #region текущее в стек
        lifeActions[index].Break();
        stackActions.Add(lifeActions[index]);
        #endregion

        currentAction = action;
        currentAction.OnComplete?.AddListener(() =>
        {
            currentAction = stackActions[stackActions.Count - 1];
            stackActions.RemoveAt(stackActions.Count - 1);
            onComplete();
        });
    }
    protected void SetAction(AIAction action, System.Action onComplete, System.Action onBreak)
    {
        #region текущее в стек
        lifeActions[index].Break();
        stackActions.Add(lifeActions[index]);
        #endregion
        currentAction = action;
        currentAction.OnComplete?.AddListener(() =>
        {
            if (stackActions != null)
            {
                currentAction = stackActions[stackActions.Count - 1];
                stackActions.RemoveAt(stackActions.Count - 1);
            }
            onComplete();
        });
        currentAction.OnBreak?.AddListener(() =>
        {
            if (stackActions != null)
            {
                currentAction = stackActions[stackActions.Count - 1];
                stackActions.RemoveAt(stackActions.Count - 1);
            }
            onBreak();
        });
    }


    public void CustomUpdate()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }


        if (currentAction != null)
        {
            currentAction.CustomUpdate();
        }
        else
        {
            index = index + 1 > lifeActions.Count - 1 ? 0 : index + 1;
            currentAction = lifeActions[index];
        }
        if (currentAction != lastAction && currentAction != null)
        {
            currentAction.Initial();

            currentAction.OnComplete?.AddListener(() =>
            {
                currentAction = null;
            });

            currentAction.OnBreak?.AddListener(() =>
            {
                currentAction = null;
            });

            lastAction = currentAction;
        }

        UpdateAI();
    }

    /// <summary>
    /// Оптимизированное время обновления для AI
    /// </summary>
    protected virtual void UpdateAI()
    {

    }
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
    public void SetPresset(AIUniversalData data, Entity entity, AIType type, AITypes targetTypes)
    {
        this.targetTypes = targetTypes;
        this.data = data;
        this.type = type;
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
