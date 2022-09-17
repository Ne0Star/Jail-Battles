using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCircleCollider : Trigger
{
    public Color GizmozColor = Color.green;
    public Vector3 Offset;

    /// <summary>
    /// ������ true ���� ����� ������ ��������
    /// </summary>
    /// <returns></returns>
    public bool CheckPoint(Vector3 worldPos)
    {
        bool result = false;
        Vector3 center = transform.position + Offset;
        if (Vector2.Distance(center, worldPos) <= triggerRadius)
        {
            result = true;
            return result;
        }
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmozColor;
        Gizmos.DrawWireSphere(transform.position + Offset, triggerRadius);
    }
}