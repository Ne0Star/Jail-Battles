using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TrashType
{
    Кровь,
    Запчасти
}

public class Trash : AIActionItem
{
    [SerializeField] private TrashType trashType;
    [SerializeField] private bool autoDestroy;
    [SerializeField] private Animator animator;
    [SerializeField] private bool cleaning = false;

    [SerializeField] private SpriteRenderer render;


    //private void Awake()
    //{
    //    InitialComponent<SpriteRenderer>(ref render);
    //    InitialComponent<Animator>(ref animator);
    //}

    //private void InitialComponent<T>(ref T c) where T : Component
    //{
    //    if(c == null)
    //    {
    //        c = gameObject.GetComponent<T>();
    //        if(c == null)
    //        {
    //            c = gameObject.GetComponentInChildren<T>(true);
    //            if(c == null)
    //            {
    //                throw new System.Exception("Не удалось найти компонент: " + typeof(T));
    //            }
    //        }
    //    }
    //}


    public void Initial(TrashData data)
    {
        this.trashType = data.TrashType;
        SpriteColor sc = data.SpriteVariants[Random.Range(0, data.SpriteVariants.Count - 1)];
        render.sprite = sc.Sprite;
        render.color = sc.Color;
        this.autoDestroy = data.AutoDestroy;
    }


    private void OnDisable()
    {
        isFree = true;
        cleaning = false;
        onComplete?.Invoke(this);
    }

    private void OnEnable()
    {
        isFree = false;
        animator.Play("Enable");
    }

    [SerializeField] private float currentTime;
    [SerializeField] private float cleaningTime;

    public TrashType TrashType { get => trashType; }

    public void StartCleaning(float cleaningTime)
    {
        if (!isFree || cleaning) return;
        isFree = false;
        this.cleaningTime = cleaningTime;
        cleaning = true;
        animator.Play("Clean");
    }

    private void Update()
    {
        if (cleaning || autoDestroy)
        {
            if (currentTime >= cleaningTime)
            {

                Complete();

                currentTime = 0f;
            }
            currentTime += Time.unscaledDeltaTime;
        }
    }

    public void Complete()
    {
        gameObject.SetActive(false);
    }

}
