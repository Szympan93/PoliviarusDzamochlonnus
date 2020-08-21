using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContinuousMusicData", menuName = "MusicData/ContinuousMusic", order = 1)]
public class ContinuousMusicData : IMusicData
{
    #region Fields
    [SerializeField] private List<AudioClip> _i;
    [SerializeField] private int _stateTact;
    #endregion

    public override List<AudioClip> GetAudioClips() => _i;
    public override int GetTact() => _stateTact;
}
