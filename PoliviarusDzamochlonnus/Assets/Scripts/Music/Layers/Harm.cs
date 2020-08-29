using System.Collections;
using UnityEngine;
using Music.Settings;

namespace Music.Layer
{
    public class Harm : LayerSettings
    {
        //Normal 0.5f
        //For testing 0.0f
        [SerializeField] [Range(0, .5f)] float _chanceToNotPlaying = 0.5f;

        #region Non-Unity Methods
        private bool CanStartPlaying() => Random.value > _chanceToNotPlaying;

        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            yield return new WaitUntil(() => CanStart);

            while (!GameManager.Instance.IsGameEnd)
            {
                SetupAmountTact(state);
                SetupClips(state);

                if (!CanStartPlaying())
                    yield return new WaitForSeconds(Timer);
                else
                {
                    for (int i = 0; i < AmountAudioSources; i++)
                    {
                        if (GameManager.Instance.IsGameEnd) break;
                        var clip = ChooseRandomClip(Clips);
                        StartCoroutine(AudioSourceOrganizer(i, clip));
                        yield return new WaitForSeconds(Timer);
                    }
                }
            }
        }
        #endregion
    }
}