using System.Collections.Generic;
using UnityEngine;

public abstract class IMusicData : ScriptableObject
{
    #region Non-Unity Methods
        public virtual List<AudioClip> GetAudioClips() => null;
        public virtual List<AudioClip> GetAudioClips(ActualLeader leader) => null;
        public virtual List<AudioClip> GetAudioClips(MusicState state, ActualLeader leader) => null;
        public virtual int GetTact() => 0;
        public virtual int GetTact(ActualLeader leader) => 0;
        public virtual int GetTact(MusicState state, AudioClip clip) => 0;

        public virtual void SetupAllDict() { }
        #endregion
}