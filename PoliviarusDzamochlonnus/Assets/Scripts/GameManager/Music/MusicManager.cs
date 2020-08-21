using System;
using UnityEngine;
using UnityEngine.Events;
using Music;

public enum ActualLeader
{
    None = 0,
    Red,
    Blue
}

public enum MusicState
{
    I1 = 0,
    I2,
    I3,
    None
}

[System.Serializable]
internal class MyStateEvent : UnityEvent<MusicState> { }

[RequireComponent(typeof(LayerManager))]
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    #region Fields
    public LayerManager LayerManager { get; private set; }
    private AudioSource _cityLoop;
    private Timer _timer;
    [SerializeField] private bool _isGameEnd = false;
    [SerializeField] private MusicState _currentMusicState = MusicState.None;
    [SerializeField] private ActualLeader _currentActualLeader = ActualLeader.Red;
    public MusicState CurrentMusicState
    {
        get => _currentMusicState;
        set
        {
            if (_currentMusicState == value) return;
            _currentMusicState = value;
            OnMusicStateChange.AddListener(LayerManager.MuteLayers);
            OnMusicStateChange.Invoke(_currentMusicState);
        }
    }
    public ActualLeader CurrentActualLeader
    {
        get => _currentActualLeader;
        set
        {
            if (_currentActualLeader == value) return;
            _currentActualLeader = value;
            LayerManager.IsCurrentLeaderChange = true;
        }
    }

    public bool IsGameEnd
    {
        get => _isGameEnd;
        set
        {
            _isGameEnd = value;
            if(_isGameEnd)
            {
                Debug.Log($"Winner: {Enum.GetName(typeof(ActualLeader), CurrentActualLeader)}");
                _cityLoop.Stop();
                LayerManager.MuteLayers();
            }
        }
    }

    public bool CanTikTok { get => _timer.TotalTime == 30f || _timer.TotalTime == 10f; }

    public UnityEvent<MusicState> OnMusicStateChange = new MyStateEvent();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        LayerManager = GetComponent<LayerManager>();
        _cityLoop = GetComponent<AudioSource>();

        _timer = FindObjectOfType<Timer>();
        if (_timer is null)
            Debug.LogError(string.Format($"{0}: The Timer didn't set well.", gameObject.name));
    }
    private void Start()
    {
        IsGameEnd = false;
        _cityLoop.Play();
        LayerManager.StartMenuLoop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            LayerManager.CanEndMenuLoop = true;
            SetupMusicManager(CurrentMusicState);
            CurrentMusicState = MusicState.I1;
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            CurrentMusicState = MusicState.I1;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            CurrentMusicState = MusicState.I2;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            CurrentMusicState = MusicState.I3;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentActualLeader = (CurrentActualLeader == ActualLeader.Red)
                ? ActualLeader.Blue
                : ActualLeader.Red;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IsGameEnd = true;
        }
    }
    #endregion

    #region Non-Unity Methods
    private void SetupMusicManager(MusicState state)
    {
        LayerManager.MuteLayers();
        OnMusicStateChange.AddListener(StartMusicOrganizer);
        OnMusicStateChange.AddListener(LayerManager.StartLayers);
        OnMusicStateChange.Invoke(state);
    }

    // for testing
    private void StartMusicOrganizer(MusicState state)
    {
        Debug.Log("Current state: " + state);
    }
    #endregion
}
