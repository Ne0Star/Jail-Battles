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

    public Vector3 worldPos => cleaningCenter.position;
    [SerializeField] private Transform cleaningCenter;
    [SerializeField] private TrashType trashType;
    [SerializeField] private bool autoDestroy;
    [SerializeField] private Animator animator;
    [SerializeField] private bool cleaning = false;

    [SerializeField] private SpriteRenderer render;


    public void Initial(TrashData data)
    {
        this.trashType = data.TrashType;
        SpriteColor sc = data.SpriteVariants[Random.Range(0, data.SpriteVariants.Count - 1)];
        render.sprite = sc.Sprite;
        render.color = sc.Color;
        this.autoDestroy = data.AutoDestroy;
    }

    private void OnEnable()
    {
        animator.Play("Enable");
    }

    [SerializeField] private float currentTime;
    [SerializeField] private float cleaningTime;

    public TrashType TrashType { get => trashType; }
    public bool Cleaning { get => cleaning; }

    public void StartCleaning(float cleaningTime)
    {
        if (cleaning) return;
        cleaning = true;
        isFree = false;
        this.cleaningTime = cleaningTime;

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
        isFree = true;
        cleaning = false;
        onComplete?.Invoke(this);
    }

}
