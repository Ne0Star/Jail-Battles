using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentCollider : MonoBehaviour
{
    public BoxCollider2D Collider;
    public int Position;


    [SerializeField] private List<BoxVirtualCollider> colliders;
    [SerializeField] private List<Vector3> points;

    [SerializeField] private bool proportionally;
    [SerializeField] private bool genearate;


    /// <summary>
    /// Разбить пространство 
    /// </summary>
    public void Split()
    {

    }

    public void Generate()
    {
        colliders.Clear();

        Vector3 lostSize = Collider.size;

        int count = Random.Range(2, 10);
        for (int i =count; i > 0; i-- )
        {
            Vector3 size = Vector3.zero;
            if (proportionally)
            {
                float s = Random.Range(lostSize.x / i, lostSize.x);
                size = new Vector3(s, s, 0);
            }
            {
                size = new Vector3(Random.Range(lostSize.x / i, lostSize.x), Random.Range(lostSize.y / i, lostSize.y), 0);
            }
            lostSize -= size;

            colliders.Add(new BoxVirtualCollider(transform.position, size));
        }

        for(int i =0; i < colliders.Count; i++)
        {

        }

    }




    private void OnDrawGizmos()
    {
        if (genearate)
        {
            Generate();
            genearate = false;
        }
        Gizmos.DrawWireCube(transform.position, Collider.size);

        if (colliders != null)
        {
            Gizmos.color = Color.red;
            foreach (BoxVirtualCollider colider in colliders)
            {
                Gizmos.DrawWireCube(colider.Position, colider.Size);
            }
        }

    }

}
