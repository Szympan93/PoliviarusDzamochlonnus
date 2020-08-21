using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTMPro;
    //czas w sekundach
    [SerializeField] private int gameTime;
    private int _totalTime;

    public int TotalTime { get => _totalTime; }

    void SetTimer() 
    {
        timerTMPro.text = $"{_totalTime / 60:00}:{_totalTime % 60:00}";
    }

    void Start()
    {
        _totalTime = gameTime;
        StartCoroutine(StartCountdown());
    }

     public IEnumerator StartCountdown() 
    {
        while(_totalTime >= 0) 
        {
            SetTimer();
            yield return new WaitForSeconds(1.0f);
            _totalTime--;
        }
    }

}
