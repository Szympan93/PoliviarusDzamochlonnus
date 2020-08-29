using UnityEngine;
using UnityEngine.Events;
using Music;

[System.Serializable]
internal class MyStateEvent : UnityEvent<MusicState> { }

[RequireComponent(typeof(LayerManager))]
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    #region Variables
    public UnityEvent<MusicState> OnMusicStateChange = new MyStateEvent();
    #endregion

    #region Fields
    public LayerManager LayerManager { get; private set; }
    public AudioSource CityLoop { get; private set; }
    public bool CanTikTok { get; set; }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        LayerManager = GetComponent<LayerManager>();
        CityLoop = GetComponent<AudioSource>();
    }

    private void Start()
    {
        LayerManager.SetupLayerState();
    }
    #endregion

    #region Non-Unity Methods

    public void SetupMusicManager(MusicState state)
    {
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
