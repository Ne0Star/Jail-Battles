using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool allowSpawn = true;

    public bool AllowSpawn { get => allowSpawn; }


    private void OnBecameInvisible()
    {
        allowSpawn = true;
    }
    private void OnBecameVisible()
    {
         allowSpawn = false;
    }
}
