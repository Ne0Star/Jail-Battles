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
public struct WeaponPresset
{
    /// <summary>
    /// Ближний бой
    /// </summary>
    [SerializeField] private AIStatPresset mele;
    /// <summary>
    /// Пистолеты
    /// </summary>
    [SerializeField] private AIStatPresset gun;
    /// <summary>
    /// Автоматы
    /// </summary>
    [SerializeField] private AIStatPresset machine;

    private Weapon weapon;


    public void Init(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void Play(Animator animator, AudioSource sources, float speed, Weapon weapon)
    {
        if (weapon == null)
        {
            mele.Play(animator, sources);
        }
        else
        {
            switch (weapon.WeaponType)
            {
                case WeaponType.Одноручное:
                    gun.Play(animator, sources, speed);
                    break;
                case WeaponType.Двуручное:
                    machine.Play(animator, sources, speed);
                    break;
            }
        }
    }

    public void Play(Animator animator, AudioSource sources, Weapon weapon)
    {
        if (weapon == null)
        {
            mele.Play(animator, sources);
        }
        else
        {
            switch (weapon.WeaponType)
            {
                case WeaponType.Одноручное:
                    gun.Play(animator, sources);
                    break;
                case WeaponType.Двуручное:
                    machine.Play(animator, sources);
                    break;
            }
        }
    }
}

[System.Serializable]
public struct FightPresset
{
    [SerializeField] private List<AIStatPresset> standartStrikes;
    [SerializeField] private List<AIStatPresset> specialStrikes;
    [SerializeField] private List<AIStatPresset> bossStrikes;
    public List<AIStatPresset> StandartStrikes { get => standartStrikes; }
    public List<AIStatPresset> SpecialStrikes { get => specialStrikes; }
    public List<AIStatPresset> BossStrikes { get => bossStrikes; }
}


[System.Serializable]
public struct AIStatsPresset
{
    [SerializeField] private WeaponPresset walk;
    [SerializeField] private WeaponPresset idle;
    [SerializeField] private WeaponPresset figtStance;

    private Weapon weapon;

    [SerializeField] private List<AIStatPresset> handsAttacks;
    [SerializeField] private List<AIStatPresset> meleAttacks;
    [SerializeField] private List<AIStatPresset> rangeAttacks;

    public void Init(Weapon weapon)
    {
        walk.Init(weapon);
        idle.Init(weapon);
        figtStance.Init(weapon);
        this.weapon = weapon;
    }

    public void Attack(Animator animator, AudioSource source, float speed)
    {
        if (weapon == null)
        {
            handsAttacks[Random.Range(0, handsAttacks.Count - 1)].Play(animator, source, speed);
        }
        else
        {
            switch (weapon.WeaponType)
            {
                case WeaponType.Одноручное:
                    meleAttacks[Random.Range(0, meleAttacks.Count - 1)].Play(animator, source, speed);
                    break;
                case WeaponType.Двуручное:
                    rangeAttacks[Random.Range(0, rangeAttacks.Count - 1)].Play(animator, source, speed);
                    break;
            }
        }
    }

    /// <summary>
    /// Ходьба
    /// </summary>
    public WeaponPresset Walk { get => walk; }
    /// <summary>
    /// Покой
    /// </summary>
    public WeaponPresset Idle { get => idle; }
    /// <summary>
    /// Боевая стойка
    /// </summary>
    public WeaponPresset FigtStance { get => figtStance; }
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
public abstract class AI : MonoBehaviour
{
    [SerializeField] protected Weapon weapon;
    [SerializeField] protected SpriteRenderer rangeSprite;
    [SerializeField] protected bool isAttack = false;
    [SerializeField] private Transform rotateParent;
    [SerializeField] private float rotateOffset;
    [SerializeField] private AIType type;
    [SerializeField] protected Entity entity;
    [SerializeField] private AITypes targetTypes;
    [SerializeField] protected List<AreaType> areaTypes;
    public Transform RotateParent { get => rotateParent; }
    public float RotateOffset { get => rotateOffset; }
    public AIType Type { get => type; }
    public Entity Entity { get => entity; }
    public AITypes TargetTypes { get => targetTypes; }
    public Animator Animator { get => entity.Animator; }
    public AudioSource Source { get => entity.Source; }
    public NavMeshAgent Agent => entity.Agent;
    public HitBar Hit => entity.HitBar;
    public AIStatsPresset Stats { get => entity.Stats; }
    public Weapon Weapon { get => weapon; }
    protected virtual void Disable()
    {

    }
    protected virtual void Enable()
    {

    }
    protected virtual void Create()
    {

    }
    public virtual void MarkTarget(Entity entity)
    {

    }

    [SerializeField] protected UpdateData attackSpeed;
    [SerializeField] protected UpdateData attackDamage;
    [SerializeField] protected UpdateData moveSpeed;
    [SerializeField] protected UpdateData health;
    [SerializeField] protected UpdateData healthBonus;


    private void OnDisable()
    {
        transform.localPosition = Vector2.zero;
        isAttack = false;
    }

    [SerializeField] private int updateCount = 0;

    public void UpdateCurrentAI()
    {
        updateCount++;
        healthBonus.Update();
        health.Update();
        attackDamage.Update();
        attackDamage.Update();
        moveSpeed.Update();

        Hit.UpdateHealth(health.CurrentValue);
        Hit.AddHealth(healthBonus.CurrentValue);
        Agent.speed = moveSpeed.CurrentValue;

        StartCoroutine(WaitComplete());
        UpdateCirrentAI_();
    }

    protected virtual void UpdateCirrentAI_()
    {

    }

    private void OnEnable()
    {
        Stats.Init(weapon);
        List<AIArea> aIAreas = new List<AIArea>();
        foreach (AreaType areaType in areaTypes)
        {
            foreach (AIArea area in LevelManager.Instance.AiManager.Areas)
            {
                if (area.AreaType == areaType)
                {
                    aIAreas.Add(area);
                }
            }
        }
        //aIAreas[Random.Range(0, aIAreas.Count - 1)].GetVector(Agent, (result) =>
        //{
        //    Agent.Warp(result);
        //});
        StartCoroutine(WaitComplete());
        entity.transform.position = LevelManager.Instance.AiManager.GetSpawnPoint();
        Enable();
    }

    private IEnumerator WaitComplete()
    {
        yield return new WaitForSeconds(0.1f);
        rangeSprite.color = LevelManager.Instance.GetColorByRange(updateCount).color;
        weapon = LevelManager.Instance.WeaponManager.GetFreeWeapon(weaponType, false);
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localScale = new Vector3(1,1,1);
    }


    private int index;
    [SerializeField] private List<AIAction> lifeActions = new List<AIAction>();
    [SerializeField] private List<AIAction> stackActions = new List<AIAction>();
    [SerializeField] private AIAction currentAction;
    [SerializeField] private AIAction lastAction = null;
    public AIAction CurrentAction { get => currentAction; }
    public bool IsAttack { get => isAttack; }
    public int UpdateCount { get => updateCount; }


    /// <summary>
    /// Добавляет новое действие для AI
    /// </summary>
    /// <param name="action"></param>
    /// <param name="breakMode"></param>
    protected void AddAction(AIAction action, System.Action<AIAction> onComplete)
    {
        if (action == null) return;
        lifeActions.Add(action);
        action.OnComplete.AddListener((v) => onComplete(v));
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
    protected void SetAction(AIAction action, System.Action<AIAction> onComplete)
    {
        #region текущее в стек
        lifeActions[index].Break();
        stackActions.Add(lifeActions[index]);
        #endregion

        currentAction = action;
        currentAction.OnComplete?.AddListener((v) =>
        {
            onComplete(v);
        });
    }
    protected void SetAction(AIAction action, System.Action<AIAction> onComplete, System.Action<AIAction> onBreak)
    {
        #region текущее в стек
        lifeActions[index].Break();
        if (!stackActions.Contains(lifeActions[index]))
            stackActions.Add(lifeActions[index]);
        #endregion
        currentAction = action;
        currentAction.OnComplete?.AddListener((v) =>
        {
            onComplete(v);
        });
        currentAction.OnBreak?.AddListener((v) =>
        {
            onBreak(v);
        });
    }


    public void CustomUpdate()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }


        if (currentAction != lastAction && currentAction != null)
        {
            currentAction.Initial();

            currentAction.OnComplete?.AddListener((v) =>
            {
                if (stackActions.Contains(v))
                {
                    stackActions.Remove(v);
                }
                if (currentAction == v)
                {
                    currentAction = null;
                }
                v = null;
            });

            currentAction.OnBreak?.AddListener((v) =>
            {
                if (stackActions.Contains(v))
                {
                    stackActions.Remove(v);
                }
                if (currentAction == v)
                {
                    currentAction = null;
                }
                v = null;
            });

            lastAction = currentAction;
        }


        if (currentAction != null)
        {
            currentAction.CustomUpdate();
        }
        else
        {
            if (stackActions != null && stackActions.Count > 0)
            {
                currentAction = stackActions[stackActions.Count - 1];
                stackActions.Remove(currentAction);
            }
            else
            {
                index = index + 1 > lifeActions.Count - 1 ? 0 : index + 1;
                currentAction = lifeActions[index];
            }

        }


        UpdateAI();
    }
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private Transform weaponParent;
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
    protected virtual void OnCustomTriggerStay(Entity ai)
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
    /// <summary>
    /// Устанавливает найстроки AI
    /// </summary>
    /// <param name="entity"></param>
    public void SetPresset(Entity entity)
    {
        this.entity = entity;
        currentAction = null;
        stackActions = new List<AIAction>();
        lifeActions = new List<AIAction>();
        index = 0;
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
