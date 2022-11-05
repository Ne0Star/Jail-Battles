using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;


public class Enemu : Entity, ICustomListItem
{

    [SerializeField] protected Entity target;

    [SerializeField] private Stat moveSpeed;

    [SerializeField] private EntityTrigger[] triggers;
    [SerializeField] private float currentTime;
    [SerializeField] private int updateCount;
    [SerializeField] private List<EntityType> targetType;
    [SerializeField] private float rotateOffset;
    [SerializeField] private float rotateSpeed;
    [SerializeField] protected Transform gunParent, machineParent, meleParent, rotateParent;


    [SerializeField] protected Weapon weapon;
    [SerializeField] protected Mele mele;
    [SerializeField] protected Gun gun;
    public virtual void Attack()
    {

    }

    public void Left()
    {
        if (gun)
            gun.Left();
    }
    public void Top()
    {
        if (gun)
            gun.Top();
    }

    private void SetAllFalse()
    {
        if (weapon)
        {
            weapon.gameObject.SetActive(false);
        }
        if (mele)
        {
            mele.gameObject.SetActive(false);
        }
        if (gun)
        {
            gun.gameObject.SetActive(false);
        }
        animator.Animator.SetBool("none", false);
        animator.Animator.SetBool("gun", false);
        animator.Animator.SetBool("machine", false);
        animator.Animator.SetBool("mele", false);
    }


    public void SetMele(Mele mele)
    {
        this.mele = mele;
        SetWeapon(mele);
    }

    public void SetGun(Gun gun)
    {
        this.gun = gun;
        SetWeapon(gun);
    }

    public void SetWeapon(Weapon gun)
    {
        if (!gun) return;
        SetAllFalse();

        if (gun as Mele)
        {
            animator.Animator.SetBool("mele", true);
            gun.transform.SetParent(meleParent);
            this.mele = (Mele)gun;
        }
        else if (gun as Gun)
        {
            animator.Animator.SetBool("gun", true);
            gun.transform.SetParent(gunParent);
            this.gun = (Gun)gun;
        }
        else if (gun as Machine)
        {
            animator.Animator.SetBool("machine", true);
            gun.transform.SetParent(machineParent);
        }
        else if (gun as None)
        {
            animator.Animator.SetBool("none", true);
            gun.transform.SetParent(transform);
        }

        gun.transform.position = gun.transform.parent.position;
        gun.transform.localRotation = Quaternion.Euler(Vector3.zero);
        gun.transform.localScale = Vector3.one;
        gun.Free = false;
        gun.gameObject.SetActive(true);

        OnDied.AddListener((e) =>
        {
            gun.gameObject.SetActive(false);
            gun.Free = true;
        });
        this.weapon = gun;
    }

    [SerializeField] protected List<AIAction> stackActions = new List<AIAction>();
    [SerializeField] protected List<AIAction> lifeActions = new List<AIAction>();
    [SerializeField] protected AIAction currentAction;
    [SerializeField] protected AIAction lastAction;
    [SerializeField] private int index = 0;
    public float RotateOffset { get => rotateOffset; }
    public Transform RotateParent { get => rotateParent; }
    public Gun WeaponGun { get => gun; }
    public Mele WeaponMele { get => mele; }
    public Weapon Weapon { get => weapon; }
    public float RotateSpeed { get => rotateSpeed; }
    public Stat MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Entity Target { get => target; }

    protected override void Create()
    {
        triggers = GetComponentsInChildren<EntityTrigger>(true);
        foreach (EntityTrigger trigger in triggers)
        {
            trigger.targetType = targetType;
            trigger.OnStay?.AddListener((e) => OnCustomTriggerStay(e));
        }
        SpriteLibrary library = GetComponentInChildren<SpriteLibrary>(true);
        SpriteResolver[] resolvers = GetComponentsInChildren<SpriteResolver>(true);
        foreach (SpriteResolver resolver in resolvers)
        {
            List<string> labels = new List<string>();
            foreach (string s in library.spriteLibraryAsset.GetCategoryLabelNames(resolver.GetCategory()))
            {
                labels.Add(s);
            }
            resolver.SetCategoryAndLabel(resolver.GetCategory(), labels[Random.Range(0, labels.Count - 1)]);
        }
    }

    sealed protected override void Enable()
    {

        currentAction = null;
        lifeActions = new List<AIAction>();
        Enabled();
    }
    protected virtual void OnCustomTriggerStay(Entity e)
    {

    }

    protected virtual void Enabled()
    {
        //SetWeapon(null);
        //animator.Play("Покой");
    }
    /// <summary>
    /// Устанавливает новое действие, если использовать стек то текущее выполняемое добавится в стек
    /// </summary>
    /// <param name="action"></param>
    /// <param name="useStack"></param>
    public void SetAction(AIAction action)
    {
        action.OnComplete?.AddListener((a) =>
        {
            if (currentAction == a)
            {
                currentAction = null;
            }
            stackActions.Remove(a);
        });

        action.id = -1;
        currentAction = action;
        bool addStack = true;

        foreach (AIAction act in stackActions)
        {
            if (act.id == currentAction.id)
            {
                addStack = false;
                break;
            }
        }
        if (addStack && !action.BlockStack)
            stackActions.Add(currentAction);


    }
    protected void AddAction(AIAction action)
    {
        if (action == null) return;
        action.id = lifeActions.Count + 1;
        lifeActions.Add(action);
        //if (currentAction == null)
        //{
        currentAction = action;
        action.OnComplete.AddListener((a) =>
        {
            if (currentAction == a)
            {
                currentAction = null;
            }
        });

        action.Initial();
        //}
    }

    protected virtual void OnUpdate()
    {

    }
    public int diedCount = 0;
    public void CustomUpdate()
    {
        if (!gameObject.activeSelf)
        {
            if (currentTime >= LevelManager.Instance.GetColorByRange(updateCount).respawnTime)
            {
                gameObject.SetActive(true);
                diedCount++;
                currentTime = 0f;
            }
            currentTime += 0.02f;
            return;
        }


        moveSpeed.CurrentValue = agent.speed;
        moveSpeed.Normalize();
        agent.speed = moveSpeed.CurrentValue;

        if (currentAction != null && currentAction != lastAction)
        {
            currentAction.Initial();
            lastAction = currentAction;
        }

        if (currentAction != null)
        {
            currentAction.CustomUpdate();
            if (stackActions.Contains(currentAction))
            {
                stackActions.Remove(currentAction);
            }
        }
        else if (stackActions != null && stackActions.Count > 0)
        {
            currentAction = stackActions[stackActions.Count - 1];
        }
        else if (lifeActions != null && lifeActions.Count > 0)
        {
            currentAction = lifeActions[index];
            index = index + 1 > lifeActions.Count - 1 ? 0 : index + 1;
        }



        OnUpdate();
    }
}
