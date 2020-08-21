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
        public bool IsCurrentLeaderChange { get; set; }
        public bool CanEndMenuLoop { get; set; }
        public bool CanStartLayers { get; set; }
        public bool CanEndGameStart { get; set; }
        public bool CanEndGameLoop { get; set; }
        public List<ILayer> MusicLayers { get; private set; }
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
            CanEndMenuLoop = false;
            CanStartLayers = false;
            CanEndGameStart = false;
            CanEndGameLoop = false;
        }

        public void StartMenuLoop()
        {
            foreach (var layer in MusicLayers)
            {
                if (layer is MenuLoop)
                    layer.StartPlayingMusic();
            }
        }

        public void StartLayers(MusicState state)
        {
            foreach (var layer in MusicLayers)
            {
                if (layer is MenuLoop) continue;
                layer.StartPlayingMusic(state);
            }
        }

        public void MuteLayers(MusicState state = MusicState.None)
        {
            foreach (var layer in MusicLayers)
            {
                if (layer is StartGame) continue;
                layer.MutePlayingMusic();
            }
        }

        public void StopLayers(MusicState state)
        {
            foreach (var layer in MusicLayers)
            {
                layer.StopPlayingMusic();
            }
        }
        #endregion
    }
}