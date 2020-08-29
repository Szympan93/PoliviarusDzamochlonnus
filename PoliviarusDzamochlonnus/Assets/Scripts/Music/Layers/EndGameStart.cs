using System.Collections;
using UnityEngine;
using Music.Settings;

namespace Music.Layer
{
    public class EndGameStart : LayerSettings
    {
        protected override bool CanStart
        {
            get => _musicManager.LayerManager.CanEndGameStart;
        }

        #region Non-Unity Methods
        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            if (state == MusicState.Win)
            {
                yield return new WaitUntil(() => CanStart);

                SetupAmountTact();
                SetupClips();
                var clip = ChooseRandomClip(Clips);

                for (int i = 0; i < AmountAudioSources; i++)
                {
                    StartCoroutine(AudioSourceOrganizer(i, clip));
                    yield return new WaitForSeconds(Timer);
                }
                _musicManager.LayerManager.CanEndGameLoop = true;
            }
        }
        #endregion
    }
}