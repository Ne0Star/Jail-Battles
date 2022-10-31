using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TrashType
{
    Blood
}

public class Trash : MonoBehaviour
{
    [SerializeField] public bool block = false;
    [SerializeField] private Animator animator;
    [SerializeField] private bool cleaning = false;
    [SerializeField] private TrashType trashType;
    public UnityEvent<Trash> onComplete;
    [SerializeField] private SpriteRenderer render;

    private void Start()
    {
        TrashData data = LevelManager.Instance.CleanersManager.GetTrashData(trashType);
        render.sprite = data.sprites[Random.Range(0, data.sprites.Count)];
    }

    private void OnDisable()
    {
        block = false;
        cleaning = false;
        onComplete?.Invoke(this);
    }

    private void OnEnable()
    {
        animator.Play("Enable");
    }

    [SerializeField] private float currentTime;
    [SerializeField] private float cleaningTime;

    public void Cleaning(float cleaningTime)
    {
        if (cleaning) return;
        this.cleaningTime = cleaningTime;
        block = true;
        cleaning = true;
        animator.Play("Clean");
    }

    private void Update()
    {
        if (cleaning)
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
