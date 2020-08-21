using System.Collections;
using Music.Settings;
using UnityEngine;

namespace Music.Layer
{
    public class I3Timer : LayerSettings
    {
        protected override bool CanStart
        {
            get => _musicManager.CanTikTok;
        }

        #region Non-Unity methods
        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            if(state == MusicState.I3)
            {
                SetupAmountTact();
                SetupClips();
                var clip = ChooseRandomClip(Clips);

                while(!_musicManager.IsGameEnd)
                {
                    yield return new WaitUntil(() => CanStart);

                    for (int i = 0; i < AmountAudioSources; i++)
                    {
                        StartCoroutine(AudioSourceOrganizer(i, clip));
                        yield return new WaitForSeconds(Timer);
                    }
                }
            }
        }
        #endregion
    }
}