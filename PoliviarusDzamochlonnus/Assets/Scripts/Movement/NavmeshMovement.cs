using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavmeshMovement : MonoBehaviour, IMovement
{
    private NavMeshAgent _agent;
    public NavMeshAgent Agent { get => _agent; }

    protected void Awake()
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
