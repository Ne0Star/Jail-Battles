using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] private List<CustomCollider> colliders;

    private void Awake()
    {
        StartCoroutine(Life());
    }

    private void Start()
    {

    }

    public void Register(CustomCollider collider)
    {
        colliders.Add(collider);
    }

    private IEnumerator Life()
    {
        foreach (CustomCollider collider in colliders)
        {
            collider.CustomUpdate();
        }

        yield return new WaitForSeconds(0.34f);
        StartCoroutine(Life());
        yield break;
    }

}
