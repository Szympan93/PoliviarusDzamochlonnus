using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VariableMusicData", menuName = "MusicData/VariableMusic", order = 1)]
public class VariableMusicData : IMusicData
{
    #region Variables
    [SerializeField] private List<AudioClip> _i1;
    [SerializeField] private List<AudioClip> _i2;
    [SerializeField] private List<AudioClip> _i3;

    [SerializeField] private List<int> _stateTacts;
    #endregion

    public override List<AudioClip> GetAudioClips(MusicState state, ActualLeader leader = ActualLeader.None)
    {
        switch (state)
        {
            case MusicState.I1:
                return _i1;
            case MusicState.I2:
                return _i2;
            case MusicState.I3:
                return _i3;
            default:
                string error = string.Format($"Error from {0}: the List of AudioClip doesn't exist. The MusicState don't have to be {1}", this.name, nameof(state).ToString());
                throw new System.IndexOutOfRangeException(error);
        }
    }

    public override int GetTact(MusicState state, AudioClip clip = null)
    {
        switch (state)
        {
            case MusicState.I1:
                return _stateTacts[0];
            case MusicState.I2:
                return _stateTacts[1];
            case MusicState.I3:
                return _stateTacts[2];
            default:
                string error = string.Format($"Error from {0}: The StateTact doesn't exist. The MusicState don't have to be {1}.", this.name, nameof(state).ToString());
                throw new System.IndexOutOfRangeException(error);
        }
    }
}
