using UnityEngine;
using UnityEngine.SceneManagement;

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
    Menu,
    Win,
    None
}

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool _isGameEnd = false;
    [SerializeField] private MusicState _currentMusicState = MusicState.None;
    [SerializeField] private ActualLeader _currentActualLeader = ActualLeader.None;
    [SerializeField] private int[] _musicStateDuration = new int[3] { 90, 90, 120 };
    #endregion

    #region Fields
    public static GameManager Instance { get; private set; }
    public MusicManager MusicManager { get; private set; }

    public int[] MusicStateDuration
    {
        get => _musicStateDuration;
    }
    public MusicState CurrentMusicState
    {
        get => _currentMusicState;
        set
        {
            if (_currentMusicState == value) return;
            _currentMusicState = value;

            MusicManager.OnMusicStateChange.AddListener(MusicManager.LayerManager.MuteLayers);
            MusicManager.OnMusicStateChange.Invoke(_currentMusicState);
        }
    }
    public ActualLeader CurrentActualLeader
    {
        get => _currentActualLeader;
        set
        {
            if (_currentActualLeader == value) return;
            _currentActualLeader = value;
            MusicManager.LayerManager.IsCurrentLeaderChange = true;
        }
    }

    public bool IsGameEnd
    {
        get => _isGameEnd;
        set
        {
            _isGameEnd = value;
            if (_isGameEnd)
            {
                Debug.Log($"Winner: {System.Enum.GetName(typeof(ActualLeader), CurrentActualLeader)}");
                MusicManager.CityLoop.Stop();
                SceneManager.LoadScene("Winner");
            }
        }
    }
    #endregion

    #region Unity methods
    private void Awake()
    {
        if(Instance is null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        // for testing change current leader Red <-> Blue
        if (Input.GetKeyDown(KeyCode.Space))
            CurrentActualLeader = CurrentActualLeader == ActualLeader.Red ? ActualLeader.Blue : ActualLeader.Red;
    }
    #endregion

    #region Non-Unity methods
    public void MenuSetup()
    {   
        MusicManager = GetComponentInChildren<MusicManager>();
        if(MusicManager is object)
        {
            MusicManager.CityLoop.Play();
            MusicManager.SetupMusicManager(_currentMusicState);
        }

        CurrentMusicState = MusicState.Menu;
    }

    public void StartGame()
    {
        IsGameEnd = false;
        MusicManager.LayerManager.StartGameplay = true;
        CurrentMusicState = MusicState.I1;
    }
    #endregion
}
