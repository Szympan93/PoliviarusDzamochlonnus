using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerMenu : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sprite _blueWinnerSprite;
    [SerializeField] private Sprite _redWinnerSprite;

    [SerializeField] private Image _winnerImage;
    #endregion

    #region Unity methods
    private void Start()
    {
        GameManager.Instance.CurrentMusicState = MusicState.Win;

        if (_winnerImage is object)
            _winnerImage.sprite = GameManager.Instance.CurrentActualLeader == ActualLeader.Red
                ? _redWinnerSprite
                : _blueWinnerSprite;
    }
    #endregion
}
