using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RewiredDemoPlayer : MonoBehaviour
{
    [SerializeField] private int playerId;
    [SerializeField] private float speed;

    private Player _player;

    protected void Start()
    {
        _player = ReInput.players.Players[playerId];
    }

    protected void Update()
    {
        var input = _player.GetAxis2D("Horizontal", "Vertical");
        transform.position += speed * Time.deltaTime * new Vector3(input.x, 0, input.y);
    }
}
