using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Music.Settings;

namespace Music.Layer
{
    public class Lead : LayerSettings
    {
        //Normal { 0.5f, 0.3f, 0.5f}
        //For testing { 0f, 0f, 0f}
        private readonly List<float> ChanceToNotPlayingActualLeader = new List<float> { 0.5f, 0.3f, 0.5f };
        private bool IfActualLeaderClipsIsSetting { get => Random.value > ChanceToNotPlayingActualLeader[(int)CurrentMusicState]; }

        public ActualLeader CurrentActualLeader
        {
            get => _musicManager.CurrentActualLeader;
        }
        public MusicState CurrentMusicState
        {
            get => _musicManager.CurrentMusicState;
        }

        #region Non-Unity Methods
        protected override void SetupClips(MusicState state)
        {
            if (IfActualLeaderClipsIsSetting)
            {
                Clips = _musicSettings.ChooseAudioClip(state, CurrentActualLeader);
            }
            else
            {
                Clips = _musicSettings.ChooseAudioClip(state, ActualLeader.None);
            }
        }

        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            yield return new WaitUntil(() => CanStart);

            while (!_musicManager.IsGameEnd)
            {
                for (int i = 0; i < AmountAudioSources; i++)
                {
                    if (_musicManager.IsGameEnd) break;
                    SetupClips(state);
                    var clip = ChooseRandomClip(Clips);
                    SetupAmountTact(state, clip);
                    StartCoroutine(AudioSourceOrganizer(i, clip));
                    yield return new WaitForSeconds(Timer);
                }
            }
        }
        #endregion
    }
}