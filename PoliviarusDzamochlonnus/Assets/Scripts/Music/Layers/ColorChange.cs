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
            if (state == MusicState.Menu || state == MusicState.Win) yield break;

            SetupAmountTact(state);
            SetupClips(state);
            var clip = ChooseRandomClip(Clips);

            while (!GameManager.Instance.IsGameEnd)
            {
                yield return new WaitUntil(() => IsLeaderChange);

                for (int i = 0; i < AmountAudioSources; i++)
                {
                    if (GameManager.Instance.IsGameEnd) break;
                    StartCoroutine(AudioSourceOrganizer(i, clip));
                    yield return new WaitForSeconds(Timer);
                }
                IsLeaderChange = false;
            }
        }
        #endregion
    }
}