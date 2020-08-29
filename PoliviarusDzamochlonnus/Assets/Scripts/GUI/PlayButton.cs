﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(1));
        GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.StartGame());
    }
}
