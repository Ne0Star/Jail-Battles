using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public event System.Action<Trash> onComplete;
    [SerializeField]private SpriteRenderer render;

    private void Start()
    {
        TrashData data = LevelManager.Instance.CleanersManager.GetTrashData(trashType);
        render.sprite = data.sprites[Random.Range(0, data.sprites.Count)];
    }

    private void OnEnable()
    {

        animator.Play("Enable");
    }

    private void OnDisable()
    {
        animator.Play("Disable");
    }

    public void Cleaning()
    {
        if (cleaning) return;
        block = true;
        cleaning = true;
        animator.Play("Clean");
    }

    public void Complete()
    {
        onComplete(this);
        gameObject.SetActive(false);
        block = false;
        cleaning = false;
    }

}
