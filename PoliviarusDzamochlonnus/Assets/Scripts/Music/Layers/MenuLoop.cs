using System.Collections;
using UnityEngine;
using Music.Settings;

namespace Music.Layer
{
    public class MenuLoop : LayerSettings
    {
        protected override bool CanStart
        {
            get => _musicManager.LayerManager.StartGameplay;
        }

        #region Non-Unity Methods
        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            if(state == MusicState.Menu)
            {
                SetupAmountTact();
                SetupClips();
                var clip = ChooseRandomClip(Clips);

                while (!CanStart)
                {
                    for (int i = 0; i < AmountAudioSources; i++)
                    {
                        if (CanStart) break;
                        StartCoroutine(AudioSourceOrganizer(i, clip));
                        float elapsedTime = 0f;
                        while (elapsedTime < Timer)
                        {
                            elapsedTime += Time.deltaTime;
                            if (CanStart) break;
                            yield return null;
                        }
                    }
                }
            }
        }
        #endregion
    }
}