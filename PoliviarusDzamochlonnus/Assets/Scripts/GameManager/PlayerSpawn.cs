using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private int numPlayers;
    [SerializeField] private Disease playerPrefab;
    [SerializeField] private Transform parentPlayer;

    void SetCamera(Camera playerCam, int num) {
        Camera _camera = playerCam.GetComponent<Camera>();
        if(_camera == null) return;
        switch (num)
        {
            case 0:
                playerCam.GetComponent<AudioListener>().enabled = true;
                _camera.rect = new Rect(0, 0, 0.5f, 1);
                break;
            case 1:
                _camera.rect = new Rect(0.5f, 0, 0.5f, 1);
                break;
            default:
                Debug.Log("Undefined player...");
                break;
        }
    }

    void SetPlayer(Transform pos, int num) {
        Disease player = Instantiate(playerPrefab, pos.position, pos.rotation);
        player.transform.SetParent(parentPlayer);
        player.name = "Player_" + (num + 1);
        player.GetComponent<PlayerController>().PlayerId = num;
        SetCamera(player.GetComponentInChildren<Camera>(), num);

    }

    void GameSetup() 
    {
        var spawnedPlayers = 0;
        var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPosition").Select(e => e.transform).DefaultIfEmpty().ToList();
        while(spawnPoints.Count > 0 && spawnedPlayers < numPlayers)
        {
            var point = spawnPoints[Random.Range(0, spawnPoints.Count)];
            spawnPoints.Remove(point);
            SetPlayer(point, spawnedPlayers);
            spawnedPlayers++;
        }
    }

    void Start()
    {
        GameSetup();
    }
}
