using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(IPlayerMovement))]
public class PlayerController : MonoBehaviour
{
    public int PlayerId { get; set; }
    [SerializeField] private float speedMovement;
    [SerializeField] private float speedRotate;

    private IPlayerMovement _movement;
    private Player _player;

    private void Move() 
    {
        _movement.Rotate(speedRotate * Time.deltaTime * _player.GetAxis("Horizontal"));
        _movement.Move(speedMovement * Time.deltaTime * _player.GetAxis("Vertical"));
    }

    protected void Awake()
    {
        _movement = GetComponent<IPlayerMovement>();
    }

    protected void Start()
    {
        _player = ReInput.players.Players[PlayerId];
        Invoke("SetupPlayerController", 0.5f);
    }

    protected void Update()
    {
        Move();
    }
}
