using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(IMovement))]
public class PlayerController : IController
{
    public int PlayerId { get; set; }
    private Player _player;

    protected void Start()
    {
        _player = ReInput.players.Players[PlayerId];
        Invoke("SetupPlayerController", 0.5f);
    }
    protected override void Move()
    {
        _movement.Rotate(speedRotate * Time.deltaTime * _player.GetAxis("Horizontal"));
        _movement.Move(speedMovement * Time.deltaTime * _player.GetAxis("Vertical"));
    }
}
