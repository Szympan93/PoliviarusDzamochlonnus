using System.Collections;
using UnityEngine;
using Music.Settings;

namespace Music.Layer
{
    public class Drum : LayerSettings
    {
        #region Non-Unity Methods  
        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            yield return new WaitUntil(() => CanStart);

            SetupAmountTact(state);
            SetupClips(state);
            var clip = ChooseRandomClip(Clips);

            while (!_musicManager.IsGameEnd)
            {
                for (int i = 0; i < AmountAudioSources; i++)
                {
                    if (_musicManager.IsGameEnd) break;
                    StartCoroutine(AudioSourceOrganizer(i, clip));
                    yield return new WaitForSeconds(Timer);
                }
            }
        }
        #endregion
    }
}