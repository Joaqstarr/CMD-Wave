using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovementState : FirstPersonBaseState
{
    private CharacterController _controller;
    private FirstPersonPlayerControls _playerControls;
    private PlayerData _playerData;
    private FirstPersonPlayerManager _player;

    public override void OnEnterState(FirstPersonPlayerManager player)
    {
        #region Global Variable Setup
        if (_controller == null)
            _controller = player.CharacterController;
        if (_playerControls == null)
            _playerControls = player.FirstPersonControls;
        if (_playerData == null)
            _playerData = player.PlayerData;

        if (_player == null)
            _player = player;
        #endregion


    }

    public override void OnExitState(FirstPersonPlayerManager player)
    {
    }

    public override void OnUpdateState(FirstPersonPlayerManager player)
    {
        Move();
        LookX();
    }

    private void Move()
    {
        Vector3 MoveDirection = _player.transform.forward * _playerControls.MoveInput.y + _player.transform.right * _playerControls.MoveInput.x;
        _controller.Move(MoveDirection * _playerData.MoveSpeed * Time.deltaTime);
    }

    private void LookX()
    {
        Vector3 curRot = _player.transform.eulerAngles;
        curRot.y += _playerData.LookSpeed * _playerControls.LookInput.x;
        _player.transform.eulerAngles = curRot;
    }
}
