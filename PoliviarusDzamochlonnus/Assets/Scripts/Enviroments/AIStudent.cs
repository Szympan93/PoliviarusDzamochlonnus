using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NavmeshMovement))]
public class AIStudent : MonoBehaviour
{
    #region Variables
    private NavmeshMovement _movement;
    #endregion

    #region Fields
    public bool Waitting { get; private set; }
    public bool CanStartMove { get; private set; }
    #endregion

    #region Unity methods
    void Awake()
    {
        _movement = GetComponent<NavmeshMovement>();
    }

    private void Start()
    {
        CanStartMove = true;
        Waitting = false;
    }
    #endregion

    #region Non-Unity methods
    public void SpeedSetup(float maxMoveSpeed, float maxRotateSpeed)
    {
        _movement.Agent.speed = maxMoveSpeed;
        _movement.Agent.angularSpeed = maxRotateSpeed;
    }
    public bool ReachDestination(Vector3 a, Vector3 b, float precision = 0.01f) => Vector3.SqrMagnitude(a - b) < precision;
    public void MoveTo(Vector3 targetPos)
    {
        _movement.Agent.SetDestination(targetPos);
        CanStartMove = false;
    }
    public Vector3 ChooseRandomDestinationPos(List<Vector3> posList, Vector3 currentPos)
    {
        var r = new System.Random();
        return posList.Where(pos => pos != currentPos).ElementAt(r.Next(0, posList.Count));
    }
    public void TimeToWait(float time)
    {
        if (!Waitting)
            StartCoroutine(WaitAndChat(time));
    }

    private IEnumerator WaitAndChat(float time)
    {
        Debug.Log("Start waitting...");
        Waitting = true;
        yield return new WaitForSeconds(time);
        Waitting = false;
        CanStartMove = true;
        Debug.Log("End waitting...");
    }

    #endregion
}
