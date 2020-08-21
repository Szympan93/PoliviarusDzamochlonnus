using System.Collections.Generic;
using UnityEngine;

namespace Music.Settings
{
    public class MusicSettings : MonoBehaviour
    {
        #region Variables
        [SerializeField] private IMusicData _data;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            if (_data is VariableMusicLeaderData)
                _data.SetupAllDict();
        }
        #endregion

        #region Non-Unity Methods
        public int ChooseTact() => _data.GetTact();
        public int ChooseTact(MusicState state = MusicState.None, AudioClip clip = null) => _data.GetTact(state, clip);
        public int ChooseTact(ActualLeader leader) => _data.GetTact(leader);

        public List<AudioClip> ChooseAudioClip() => _data.GetAudioClips();
        public List<AudioClip> ChooseAudioClip(MusicState state = MusicState.None, ActualLeader leader = ActualLeader.None) => _data.GetAudioClips(state, leader);
        public List<AudioClip> ChooseAudioClip(ActualLeader leader) => _data.GetAudioClips(leader);
        #endregion
    }
}