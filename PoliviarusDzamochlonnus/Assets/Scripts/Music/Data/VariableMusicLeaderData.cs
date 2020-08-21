using Music.Dict;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VariableLeaderMusicData", menuName = "MusicData/VariableDependsOnLeaderMusic", order = 1)]
public class VariableMusicLeaderData : IMusicData
{
    #region Variables
    private MyDictionary<AudioClip> LeadDictionary { get; set; }

    [Header("I1")]
    [SerializeField] private List<AudioClip> _i1Neutral;
    [SerializeField] private List<AudioClip> _i1Red;
    [SerializeField] private List<AudioClip> _i1Blue;

    [Space]
    [Header("I2")]
    [SerializeField] private List<AudioClip> _i2Neutral;
    [SerializeField] private List<AudioClip> _i2Red;
    [SerializeField] private List<AudioClip> _i2Blue;

    [Space]
    [Header("I3")]
    [SerializeField] private List<AudioClip> _i3Neutral;
    [SerializeField] private List<AudioClip> _i3Red;
    [SerializeField] private List<AudioClip> _i3Blue;

    [Space]
    [Header("Tacts")]
    [SerializeField] private List<int> _stateTacts;
    //Queue -> Neutraul, Red, Blue
    [SerializeField] private List<int> _i3StateTacts;
    #endregion

    public override void SetupAllDict()
    {
        LeadDictionary = new MyDictionary<AudioClip>();
        LeadDictionary.I1Dict = LeadDictionary.AddClipsListToDict(_i1Neutral, _i1Red, _i1Blue);
        LeadDictionary.I2Dict = LeadDictionary.AddClipsListToDict(_i2Neutral, _i2Red, _i2Blue);
        LeadDictionary.I3Dict = LeadDictionary.AddClipsListToDict(_i3Neutral, _i3Red, _i3Blue);

        LeadDictionary.I3StateTactsForI3ClipDict = LeadDictionary.AddI3TactsListToDict(_i3StateTacts, _i3Neutral, _i3Red, _i3Blue);
    }

    public override List<AudioClip> GetAudioClips(MusicState state, ActualLeader leader)
    {
        List<AudioClip> clipList;
        switch (state)
        {
            case MusicState.I1:
                clipList = LeadDictionary.GetValueAudioClipList(LeadDictionary.I1Dict, leader);
                break;
            case MusicState.I2:
                clipList = LeadDictionary.GetValueAudioClipList(LeadDictionary.I2Dict, leader);
                break;
            case MusicState.I3:
                clipList = LeadDictionary.GetValueAudioClipList(LeadDictionary.I3Dict, leader);
                break;
            default:
                string error = string.Format($"Error from {0}: The MusicState doesn't exist. The MusicState don't have to be {1}.", this.name, nameof(state).ToString());
                throw new System.IndexOutOfRangeException(error);
        }
        return clipList;
    }

    public override int GetTact(MusicState state, AudioClip clip)
    {
        switch (state)
        {
            case MusicState.I1:
                return _stateTacts[0];
            case MusicState.I2:
                return _stateTacts[1];
            case MusicState.I3:
                return LeadDictionary.GetValueTactsList(clip);
            default:
                string error = string.Format($"Error from {0}: The StateTact doesn't exist. The MusicState don't have to be {1}.", this.name, nameof(state).ToString());
                throw new System.IndexOutOfRangeException(error);
        }
    }
}
