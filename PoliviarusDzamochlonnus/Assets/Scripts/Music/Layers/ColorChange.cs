using UnityEngine;
using System.Collections;
using Music.Settings;

namespace Music.Layer
{
    public class ColorChange : LayerSettings
    {
        private bool IsLeaderChange
        {
            get => _musicManager.LayerManager.IsCurrentLeaderChange;
            set => _musicManager.LayerManager.IsCurrentLeaderChange = value;
        }

        #region Non-Unity Methods
        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            yield return new WaitUntil(() => CanStart);

            SetupAmountTact(state);
            SetupClips(state);
            var clip = ChooseRandomClip(Clips);

            while (!_musicManager.IsGameEnd)
            {
                yield return new WaitUntil(() => IsLeaderChange);

                for (int i = 0; i < AmountAudioSources; i++)
                {
                    if (_musicManager.IsGameEnd) break;
                    StartCoroutine(AudioSourceOrganizer(i, clip));
                    yield return new WaitForSeconds(Timer);
                }
                IsLeaderChange = false;
            }
        }
        #endregion
    }
}