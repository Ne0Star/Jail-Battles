using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTranslator : MonoBehaviour
{
    [SerializeField] Transform target;
    private void FixedUpdate()
    {
        transform.rotation = target.rotation;
    }
}
