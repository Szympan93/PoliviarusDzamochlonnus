using System.Collections;
using Music.Settings;
using UnityEngine;

namespace Music.Layer
{
    public class StartGame : LayerSettings
    {
        #region Fields
        [SerializeField] private int _stingerAmountTact = 1;
        private bool IsStinger { get; set; }
        protected override bool CanStart
        {
            get => _musicManager.LayerManager.CanEndMenuLoop;
        }
        #endregion

        #region Non-Unity Methods
        private IEnumerator StingerOrganizer()
        {
            IsStinger = true;
            yield return new WaitForSeconds(_stingerAmountTact * Tact);
            IsStinger = false;
        }

        protected override IEnumerator LayerOrganizer(MusicState state)
        {
            _musicManager.LayerManager.CanStartLayers = false;
            SetupAmountTact(state);
            SetupClips(state);
            var clip = ChooseRandomClip(Clips);

            yield return new WaitUntil(() => CanStart);

            if (state != MusicState.I1)
                StartCoroutine(StingerOrganizer());

            yield return new WaitUntil(() => !IsStinger);

            for (int i = 0; i < AmountAudioSources; i++)
            {
                StartCoroutine(AudioSourceOrganizer(i, clip));
                yield return new WaitForSeconds(Timer);
            }

            yield return new WaitWhile(() => IfChoosenAudioSourceIsPlaying());
            _musicManager.LayerManager.CanStartLayers = true;
        }
        #endregion
    }
}