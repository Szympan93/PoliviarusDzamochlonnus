using System.Collections;
using UnityEngine;
using Music.Settings;

namespace Music.Layer
{
    public class EndGameLoop : LayerSettings
    {
        protected override bool CanStart
        {
            get => _musicManager.LayerManager.CanEndGameLoop;
        }

        #region Non-Unity Methods
        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            yield return new WaitUntil(() => CanStart);

            SetupAmountTact();
            SetupClips();
            var clip = ChooseRandomClip(Clips);

            while (true)
            {
                for (int i = 0; i < AmountAudioSources; i++)
                {
                    StartCoroutine(AudioSourceOrganizer(i, clip));
                    yield return new WaitForSeconds(Timer);
                }
            }
        }
        #endregion
    }
}