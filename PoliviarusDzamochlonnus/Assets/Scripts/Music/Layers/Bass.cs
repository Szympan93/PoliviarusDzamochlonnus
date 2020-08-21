using System.Collections;
using UnityEngine;
using Music.Settings;

namespace Music.Layer
{
    public class Bass : LayerSettings
    {
        #region Non-Unity Methods
        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            yield return new WaitUntil(() => CanStart);

            SetupAmountTact();
            SetupClips();

            while (!_musicManager.IsGameEnd)
            {
                for (int i = 0; i < AmountAudioSources; i++)
                {
                    if (_musicManager.IsGameEnd) break;
                    var clip = ChooseRandomClip(Clips);
                    StartCoroutine(AudioSourceOrganizer(i, clip));
                    yield return new WaitForSeconds(Timer);
                }
            }
        }
        #endregion
    }
}