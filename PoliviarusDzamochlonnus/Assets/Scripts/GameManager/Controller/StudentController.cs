using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AIStudent))]
public class StudentController : IController
{
    #region Variables
    [SerializeField] [Range(0.01f, 1f)] private float precision = 0.1f; 
    [SerializeField] private List<Transform> pathPosList;
    [SerializeField] private float[] minMaxWaittingTime = new float[2];

    private Vector3 _startingPos;
    private Vector3 _currentPos;
    private Vector3 _nextPos;
    private AIStudent _student;
    private readonly List<Vector3> _pathVectorList = new List<Vector3>();
    #endregion

    #region Fields
    private float RandomTime { get => Random.Range(minMaxWaittingTime[0], minMaxWaittingTime[1]); }
    private bool CanChooseNextPos { get => _nextPos == Vector3.zero || _student.ReachDestination(_currentPos, _nextPos, precision); }
    #endregion

    #region Unity methods
    protected override void Awake()
    {
        base.Awake();
        _student = GetComponent<AIStudent>();
    }
    private void Start() => StartSetupStudent();
    #endregion

    #region Non-Unity methods
    protected override void Move()
    {
        _currentPos = transform.position;

        if (_student.CanStartMove)
        {
            _nextPos = _student.ChooseRandomDestinationPos(_pathVectorList, _currentPos);
            _student.MoveTo(_nextPos);
        }
        else
        {
            if (CanChooseNextPos)
                _student.TimeToWait(RandomTime);
        }
    }

    private void StartSetupStudent()
    {
        _startingPos = transform.position;
        _currentPos = _startingPos;

        foreach (var point in pathPosList)
        {
            _pathVectorList.Add(point.position);
        }

        _student.SpeedSetup(speedMovement, speedRotate);
    }
    #endregion
}
