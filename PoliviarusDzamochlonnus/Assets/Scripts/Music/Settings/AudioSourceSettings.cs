using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Music.Settings
{
    public class AudioSourceSettings : MonoBehaviour
    {
        #region Fields
        private List<AudioSource> _audioSources;
        private readonly float Reduction = 0.1f;
        public int AmountAudioSources
        {
            get => _audioSources.Count;
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _audioSources = gameObject.GetComponents<AudioSource>().ToList();
            if (_audioSources is null)
                Debug.LogError(string.Format($"{0}: The AudioSources didn't set well.", gameObject.name));
        }
        #endregion

        #region Non-Unity Methods
        public AudioSource ChosenAudioSource(int index = 0) => _audioSources[index];
        public float VolumeChosenAudioSource(int index = 0) => _audioSources[index].volume;
        public bool IfChosenAudioSourceIsPlaying(int index = 0) => ChosenAudioSource(index).isPlaying;

        public void SetOnCurrentAudioSource(AudioClip clip, int index = 0)
        {
            _audioSources[index].enabled = true;
            _audioSources[index].volume = 1f;
            _audioSources[index].clip = clip;
            _audioSources[index].Play();
        }

        public void SetOffCurrentAudioSource(int index = 0)
        {
            _audioSources[index].Stop();
            _audioSources[index].clip = null;
            _audioSources[index].enabled = false;
        }

        public void TurnDownCurrentAudioSource(float reduction, int index = 0)
        {
            float currentVolume = _audioSources[index].volume - reduction;
            _audioSources[index].volume = Mathf.Max(currentVolume, 0f);
        }

        public IEnumerator TurnDownAudioSource(int numAudioSource)
        {
            while (VolumeChosenAudioSource(numAudioSource) != 0f)
            {
                TurnDownCurrentAudioSource(Reduction, numAudioSource);
                yield return new WaitForSeconds(Reduction * LayerSettings.Tact);
            }
        }
        #endregion
    }
}