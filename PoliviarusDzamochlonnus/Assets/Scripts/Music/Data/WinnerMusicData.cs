using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinnerMusicData", menuName = "MusicData/WinnerMusic", order = 1)]
public class WinnerMusicData : IMusicData
{
    #region Fields
    [SerializeField] private List<AudioClip> _redWinnerList;
    [SerializeField] private List<AudioClip> _blueWinnerList;

    [SerializeField] private int _redTact;
    [SerializeField] private int _blueTact;
    #endregion

    public override int GetTact(ActualLeader leader)
    {
        switch (leader)
        {
            case ActualLeader.Red:
                return _redTact;
            case ActualLeader.Blue:
                return _blueTact;
            default:
                string error = string.Format($"Error from {0}: The Winner doesn't exist. The ActualLeader don't have to be {1}.", this.name, nameof(leader));
                throw new System.IndexOutOfRangeException(error);
        }
    }

    public override List<AudioClip> GetAudioClips(ActualLeader leader)
    {
        switch (leader)
        {
            case ActualLeader.Red:
                return _redWinnerList;
            case ActualLeader.Blue:
                return _blueWinnerList;
            default:
                string error = string.Format($"Error from {0}: The Winner doesn't exist.", this.name);
                throw new System.IndexOutOfRangeException(error);
        }
    }
}
