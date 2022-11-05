using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIArea : MonoBehaviour
{
    [SerializeField] private NavMeshSurface2d surface;
    [SerializeField] private AreaType areaType;
    [SerializeField] private Vector3 size;

    private void Awake()
    {

    }

    public AreaType AreaType { get => areaType; }

    private IEnumerator GetVector_(NavMeshAgent agent, System.Action<Vector3> onComplete)
    {
        bool complete = false;
        Vector3 result = new Vector3(Random.Range(transform.position.x - size.x / 2, transform.position.x + size.x / 2), Random.Range(transform.position.y - size.y / 2, transform.position.y + size.y / 2), 0);
        while (!complete)
        {
            result = new Vector3(Random.Range(transform.position.x - size.x / 2, transform.position.x + size.x / 2), Random.Range(transform.position.y - size.y / 2, transform.position.y + size.y / 2), 0);
            NavMeshPath path = new NavMeshPath();
            if (!agent.enabled || !agent.gameObject.activeInHierarchy || !agent) { yield return null; break; };
            agent.CalculatePath(result, path);
            complete = path.status == NavMeshPathStatus.PathComplete;
            yield return new WaitForFixedUpdate();
        }
        onComplete(result);
    }

    public void GetVector(NavMeshAgent agent, System.Action<Vector3> complete)
    {
        if (!gameObject || !gameObject.activeInHierarchy || !transform) return;
        StartCoroutine(GetVector_(agent, complete));
        // return new Vector3(Random.Range(transform.position.x - size.x / 2, transform.position.x + size.x / 2), Random.Range(transform.position.y - size.y / 2, transform.position.y + size.y / 2), 0);
    }

    private void OnDrawGizmos()
    {

        //GetVector((r) =>
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(r, 0.5f);

        //});
        switch (areaType)
        {
            case AreaType.Кухня:
                Gizmos.color = Color.yellow;
                break;
            case AreaType.Столовая:
                Gizmos.color = Color.red;
                break;
            case AreaType.КабинетМедсестры:
                Gizmos.color = Color.blue;
                break;
        }

        //Vector3 size = new Vector3(surface.navMeshData.sourceBounds.size.z / 2, surface.navMeshData.sourceBounds.size.x / 2, 0);
        Gizmos.DrawWireCube(transform.position, size);

    }
}
