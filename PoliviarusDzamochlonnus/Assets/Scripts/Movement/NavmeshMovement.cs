using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavmeshMovement : MonoBehaviour, IPlayerMovement
{
    private NavMeshAgent _agent;

    protected void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Move(float distance)
    {
        _agent.Move(transform.rotation * Vector3.forward * distance);
    }

    public void Rotate(float angle)
    {
        transform.localEulerAngles += Vector3.up * angle;
    }
}
