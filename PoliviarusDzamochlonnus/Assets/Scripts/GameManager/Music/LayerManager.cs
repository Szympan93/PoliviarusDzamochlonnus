using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Music.Layer;
using Music.Settings;

namespace Music
{
    public class LayerManager : MonoBehaviour
    {
        #region Fields
        public List<ILayer> MusicLayers { get; private set; }
        public bool IsCurrentLeaderChange { get; set; }
        public bool StartGameplay { get; set; }
        public bool CanStartLayers { get; set; }
        public bool CanEndGameStart { get; set; }
        public bool CanEndGameLoop { get; set; }
        #endregion

        #region Unity methods
        private void Awake()
        {
            MusicLayers = GameObject.FindObjectsOfType<LayerSettings>().OfType<ILayer>().ToList();
        }
        #endregion

        #region Non-Unity methods
        public void SetupLayerState()
        {
            IsCurrentLeaderChange = false;
            StartGameplay = false;
            CanEndGameStart = false;
            CanEndGameLoop = false;
        }

        public void StartLayers(MusicState state)
        {
            CanStartLayers = false;
            foreach (var layer in MusicLayers)
            {
                layer.StartPlayingMusic(state);
            }
        }

        public void MuteLayers(MusicState state = MusicState.None)
        {
            foreach (var layer in MusicLayers)
            {
                if (layer is StartGame) continue;
                if (layer is Winner) continue;
                if (layer is ColorChange) continue;
                layer.MutePlayingMusic();
            }
        }

        public void StopLayers()
        {
            foreach (var layer in MusicLayers)
            {
                layer.StopPlayingMusic();
            }
        }
        #endregion
    }
}