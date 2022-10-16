using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomCircle2DCollider : CustomCollider
{

    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private float radius;
    /// <summary>
    /// �� ���� ����� �������
    /// </summary>
    [SerializeField] private List<Entity> visitors;
    /// <summary>
    /// �� ���� ������� ����� �����
    /// </summary>
    [SerializeField] private AITypes searchTypes;

    private void Awake()
    {
        LevelManager.Instance.ColliderManager.Register(this);
        onEnter?.AddListener(() =>
        {
            Debug.Log("��� �� �����");
        });
    }
    [SerializeField] private List<AI> targets;
    public override void CustomUpdate()
    {
        List<AI> res = new List<AI>();
        for (int i = 0; i < searchTypes.AiTypes.Count; i++)
        {
            res.AddRange(LevelManager.Instance.AiManager.GetAllAi(searchTypes.AiTypes[i].Type));
        }
        targets = res;

        foreach(AI target in targets)
        {
            if(Vector2.Distance(target.transform.position, transform.position) <= radius)
            {
                Debug.Log("� ��� ������� ����: " + target.name);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
