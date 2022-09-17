using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour
{
    [SerializeField] private EntitySkin skin;
    [SerializeField] private Animator animator;
    public EntitySkin Skin { get => skin; }
    public Animator Animator { get => animator; }
}
