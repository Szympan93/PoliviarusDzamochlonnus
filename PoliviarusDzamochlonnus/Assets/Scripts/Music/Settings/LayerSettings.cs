using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Music.Layer;

namespace Music.Settings
{
    [RequireComponent(typeof(AudioSourceSettings))]
    [RequireComponent(typeof(MusicSettings))]
    public abstract class LayerSettings : MonoBehaviour, ILayer
    {
        #region Variables
        public static readonly float Tact = 12f / 7f;
        public int AmountTact { get; protected set; }
        public float Timer { get => AmountTact * Tact; }
        public List<AudioClip> Clips { get; protected set; }
        public int AmountAudioSources { get => _audioSourceSettings.AmountAudioSources; }
        public bool IfChoosenAudioSourceIsPlaying(int numAudioSource = 0) => _audioSourceSettings.IfChosenAudioSourceIsPlaying(numAudioSource);

        protected virtual bool CanStart
        {
            get => _musicManager.LayerManager.CanStartLayers;
            set => _musicManager.LayerManager.CanStartLayers = value;
        }

        protected AudioSourceSettings _audioSourceSettings;
        protected MusicSettings _musicSettings;
        protected MusicManager _musicManager;
        #endregion

        #region Unity Method
        protected void Awake()
        {
            _musicSettings = GetComponent<MusicSettings>();
            if (_musicSettings is null)
                Debug.LogError(string.Format($"{0}: The MusicSettings didn't set well.", gameObject.name));

            _audioSourceSettings = GetComponent<AudioSourceSettings>();
            if (_audioSourceSettings is null)
                Debug.LogError(string.Format($"{0}: The AudioSourceSettings didn't set well.", gameObject.name));

            _musicManager = FindObjectOfType<MusicManager>();
            if (_musicManager is null)
                Debug.LogError(string.Format($"{0}: The MusicManager didn't find.", gameObject.name));
        }
        #endregion

        #region Non-Unity Methods
        public void StartPlayingMusic(MusicState state)
        {
            StopAllCoroutines();
            StartCoroutine(LayerOrganizer(state));
        }

        public void StopPlayingMusic()
        {
            StopAllCoroutines();
            for (int i = 0; i < AmountAudioSources; i++)
            {
                _audioSourceSettings.SetOffCurrentAudioSource(i);
            }
        }

        public void MutePlayingMusic()
        {
            for (int i = 0; i < AmountAudioSources; i++)
            {
                StartCoroutine(_audioSourceSettings.TurnDownAudioSource(i));
            }
        }

        protected void SetupAmountTact(MusicState state, AudioClip clip = null)
        {
            AmountTact = _musicSettings.ChooseTact(state, clip);
        }

        protected void SetupAmountTact(ActualLeader leader)
        {
            AmountTact = _musicSettings.ChooseTact(leader);
        }
        protected void SetupAmountTact()
        {
            AmountTact = _musicSettings.ChooseTact();
        }

        protected virtual void SetupClips()
        {
            Clips = _musicSettings.ChooseAudioClip();
        }
        protected virtual void SetupClips(MusicState state)
        {
            Clips = _musicSettings.ChooseAudioClip(state);
        }
        protected virtual void SetupClips(ActualLeader leader)
        {
            Clips = _musicSettings.ChooseAudioClip(leader);
        }

        protected AudioClip ChooseRandomClip(List<AudioClip> clips)
        {
            return clips.Count == 1
                ? clips.FirstOrDefault()
                : clips[Random.Range(0, clips.Count)];
        }

        protected IEnumerator AudioSourceOrganizer(int numAudioSource, AudioClip clip)
        {
            _audioSourceSettings.SetOnCurrentAudioSource(clip, numAudioSource);
            yield return new WaitWhile(() => IfChoosenAudioSourceIsPlaying(numAudioSource));
            _audioSourceSettings.SetOffCurrentAudioSource(numAudioSource);
        }

        protected abstract IEnumerator LayerOrganizer(MusicState state);
        #endregion
    }
}