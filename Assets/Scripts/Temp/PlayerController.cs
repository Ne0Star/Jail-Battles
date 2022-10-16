using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            agent.SetDestination(transform.position + new Vector3(0, speed));
        }
        if (Input.GetKey(KeyCode.A))
        {
            agent.SetDestination(transform.position + new Vector3(-speed, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            agent.SetDestination(transform.position + new Vector3(0, -speed));
        }
        if (Input.GetKey(KeyCode.D))
        {
            agent.SetDestination(transform.position + new Vector3(speed, 0));
        }
    }
}
