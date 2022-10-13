using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Soundpresset", order = 1)]
public class SoundPresset : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool loop;
    [SerializeField] private float volume;
    [SerializeField] private float pitch;

    public AudioClip Clip { get => clip; }
    public bool Loop { get => loop; }
    public float Volume { get => volume; }
    public float Pitch { get => pitch; }
}
