using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Enemu : Entity, ICustomListItem
{
    [SerializeField] private EntityTrigger[] triggers;
    [SerializeField] private float currentTime;
    [SerializeField] private int updateCount;

    [SerializeField] private float rotateOffset;

    [SerializeField] protected Transform gunParent, machineParent, meleParent, rotateParent;
     protected Weapon weapon;

    protected void SetWeaponParent(Weapon weapon)
    {
        switch (weapon.WeaponType)
        {
            case WeaponType.None:
                break;
            case WeaponType.Gun:
                weapon.transform.parent = gunParent;
                
                break;
            case WeaponType.Machine:
                weapon.transform.parent = machineParent;
                break;
            case WeaponType.Mele:
                weapon.transform.parent = meleParent;
                break;
        }
    }

    private void SetAllFalse()
    {
        animator.Animator.SetBool("none", false);
        animator.Animator.SetBool("gun", false);
        animator.Animator.SetBool("machine", false);
        animator.Animator.SetBool("mele", false);
    }

    public void SetWeapon(Weapon weapon)
    {
        if (weapon == null || (weapon != null && weapon.WeaponType == WeaponType.None))
        {
            this.weapon = null;
            SetAllFalse();
            animator.Animator.SetBool("none", true);
            return;
        };
        SetAllFalse();
        switch (weapon.WeaponType)
        {
            case WeaponType.None:
                animator.Animator.SetBool("none", true);
                this.weapon = null;
                return;
            case WeaponType.Gun:
                animator.Animator.SetBool("gun", true);
                weapon.transform.parent = gunParent;
                break;
            case WeaponType.Machine:
                animator.Animator.SetBool("machine", true);
                weapon.transform.parent = machineParent;
                break;
            case WeaponType.Mele:
                animator.Animator.SetBool("mele", true);
                weapon.transform.parent = meleParent;
                break;
        }
        this.weapon = weapon;
    }

    [SerializeField] protected List<AIAction> stackActions = new List<AIAction>();
    [SerializeField] protected List<AIAction> lifeActions = new List<AIAction>();
    [SerializeField] protected AIAction currentAction;
    [SerializeField] protected AIAction lastAction;


    [SerializeField] private int lifeActionIndex = 0;

    public float RotateOffset { get => rotateOffset; }
    public Transform RotateParent { get => rotateParent; }


    sealed protected override void Enable()
    {
        triggers = GetComponentsInChildren<EntityTrigger>(true);
        foreach (EntityTrigger trigger in triggers)
        {
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
        Enabled();
    }
    protected virtual void OnCustomTriggerStay(Entity e)
    {

    }

    protected virtual void Enabled()
    {
        SetWeapon(null);
        animator.Play("Покой");
    }
    /// <summary>
    /// Устанавливает новое действие
    /// </summary>
    /// <param name="action"></param>
    protected void SetAction(AIAction action)
    {
        // Текущее действие в стек
        if (currentAction != null)
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
    public void CustomUpdate()
    {
        if (!gameObject.activeSelf)
        {
            if (currentTime >= LevelManager.Instance.GetColorByRange(updateCount).respawnTime)
            {
                gameObject.SetActive(true);
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
            else
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

    }
}
