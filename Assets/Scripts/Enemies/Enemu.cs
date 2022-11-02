using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;


public class Enemu : Entity, ICustomListItem
{
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
        gun.Left();
    }
    public void Top()
    {
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
    [SerializeField] private int lifeActionIndex = 0;
    public float RotateOffset { get => rotateOffset; }
    public Transform RotateParent { get => rotateParent; }
    public Gun WeaponGun { get => gun; }
    public Mele WeaponMele { get => mele; }
    public Weapon Weapon { get => weapon; }
    public float RotateSpeed { get => rotateSpeed; }

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
    /// Устанавливает новое действие
    /// </summary>
    /// <param name="action"></param>
    protected void SetAction(AIAction action)
    {
        // Текущее действие в стек
        if (currentAction != null && lifeActions.Contains(currentAction))
            stackActions.Add(currentAction);

        action.OnComplete.AddListener((a) =>
        {
            stackActions.Remove(a);
            if (a == currentAction)
            {
                currentAction = null;
            }
        });
        action.OnBreak.AddListener((a) =>
        {
            stackActions.Remove(a);
            if (a == currentAction)
            {
                currentAction = null;
            }
        });
        action.Initial();
        // Текущим дейсвтием становится это
        currentAction = action;
    }
    protected void AddAction(AIAction action)
    {
        if (action == null) return;
        lifeActions.Add(action);
        if (currentAction == null)
        {
            currentAction = action;
            action.OnComplete.AddListener((a) =>
            {
                if (currentAction == a)
                {
                    currentAction = null;
                }
            });
            action.OnBreak.AddListener((a) =>
            {
                if (currentAction == a)
                {
                    currentAction = null;
                }
            });
            action.Initial();
        }
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

        // Если действие существует
        if (currentAction != null)
        {
            currentAction.CustomUpdate();
        }
        else // Если действия нету
        {
            // Если в стеке есть действия, берем из стека
            if (stackActions != null && stackActions.Count > 0)
            {
                currentAction = stackActions[stackActions.Count - 1];
                currentAction.Initial();
                currentAction.OnComplete.AddListener((a) =>
                {
                    if (currentAction == a)
                    {
                        currentAction = null;
                    }
                });
                currentAction.OnBreak.AddListener((a) =>
                {
                    if (currentAction == a)
                    {
                        currentAction = null;
                    }
                });
                stackActions.Remove(currentAction);
            }
            // Иначе если в стеке нету берём из цикла
            else if (lifeActions != null && lifeActions.Count > 0)
            {
                currentAction = lifeActions[lifeActionIndex];
                currentAction.Initial();
                currentAction.OnComplete.AddListener((a) =>
                {
                    if (currentAction == a)
                    {
                        currentAction = null;
                    }
                });
                currentAction.OnBreak.AddListener((a) =>
                {
                    if (currentAction == a)
                    {
                        currentAction = null;
                    }
                });

                lifeActionIndex = lifeActionIndex + 1 > lifeActions.Count - 1 ? 0 : lifeActionIndex + 1;
            }
        }
        OnUpdate();
    }
}
