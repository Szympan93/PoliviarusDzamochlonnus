using Music.Settings;

namespace Music.Layer
{
    public interface ILayer
    {
        void StartPlayingMusic(MusicState state = MusicState.None);
        void StopPlayingMusic();
        void MutePlayingMusic();
    }
}