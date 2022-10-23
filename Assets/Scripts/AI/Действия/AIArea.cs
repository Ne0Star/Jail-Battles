using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaType
{
    Кухня,
    Столовая
}

public class AIArea : MonoBehaviour
{
    [SerializeField] private AreaType areaType;
    [SerializeField] private Vector3 size;

    public AreaType AreaType { get => areaType; }

    public Vector3 GetVector()
    {
        return new Vector3(Random.Range(transform.position.x - size.x / 2, transform.position.x + size.x / 2), Random.Range(transform.position.y - size.y / 2, transform.position.y + size.y / 2), 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetVector(), 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
