using UnityEngine;
using System.Collections;
using Music.Settings;

namespace Music.Layer
{
    public class Winner : LayerSettings
    {
        public ActualLeader CurrentActualLeader
        {
            get => _musicManager.CurrentActualLeader;
        }
        protected override bool CanStart
        {
            get => _musicManager.IsGameEnd;
        }

        #region Non-Unity Methods
        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            yield return new WaitUntil(() => CanStart);

            SetupAmountTact(CurrentActualLeader);
            SetupClips(CurrentActualLeader);
            var clip = ChooseRandomClip(Clips);

            for (int i = 0; i < AmountAudioSources; i++)
            {
                StartCoroutine(AudioSourceOrganizer(i, clip));
                yield return new WaitForSeconds(Timer);
            }
            _musicManager.LayerManager.CanEndGameStart = true;
        }
        #endregion
    }
}