using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI timerTMPro;
    [SerializeField] private readonly int gameTime = 1 * 60;     //czas w sekundach
    #endregion

    #region Fields
    private int TotalTime { get; set; }
    private bool TikTokSound { get => TotalTime == 30 || TotalTime == 10; }
    #endregion

    #region Unity methods
    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        GameStatusDependendOnTime();
        GameManager.Instance.MusicManager.CanTikTok = TikTokSound;
    }
    #endregion

    #region Non-Unity methods
    private void GameStatusDependendOnTime()
    {
        if (TotalTime == gameTime - GameManager.Instance.MusicStateDuration[(int)MusicState.I1])
            GameManager.Instance.CurrentMusicState = MusicState.I2;
        else if (TotalTime == gameTime - (GameManager.Instance.MusicStateDuration[(int)MusicState.I1] + GameManager.Instance.MusicStateDuration[(int)MusicState.I2]))
            GameManager.Instance.CurrentMusicState = MusicState.I3;
        else if (TotalTime == 0)
            GameManager.Instance.IsGameEnd = true;
    }

    private void SetTimerText()
    {
        timerTMPro.text = $"{TotalTime / 60:00}:{TotalTime % 60:00}";
    }

    private IEnumerator StartCountdown()
    {
        while(TotalTime >= 0)
        {
            SetTimerText();
            yield return new WaitForSeconds(1.0f);
            TotalTime--;
        }
    }

    public void StartTimer()
    {
        TotalTime = gameTime;
        StartCoroutine(StartCountdown());
    }
    #endregion
}
