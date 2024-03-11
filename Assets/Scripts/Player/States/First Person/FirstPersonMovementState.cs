using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovementState : FirstPersonBaseState
{
    private CharacterController _controller;
    private FirstPersonPlayerControls _playerControls;
    private PlayerData _playerData;
    private FirstPersonPlayerManager _player;


    private GameObject _highlightedObject;
    private bool _pressedLastFrame;
    private bool _pressedInteractable = false;
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
        LookY();
        _highlightedObject = CheckInteractable();


        if (_highlightedObject != null && !_pressedInteractable)
        {
            if (!_pressedLastFrame && _playerControls.SelectPressed)
            {
                _pressedInteractable = true;
                _highlightedObject.SendMessage("OnInteracted", _playerControls);
            }
        }
        _pressedLastFrame = _playerControls.SelectPressed;
    }

    private void Move()
    {
        Vector3 MoveDirection = _player.transform.forward * _playerControls.MoveInput.y + _player.transform.right * _playerControls.MoveInput.x;
        MoveDirection *= _playerData.MoveSpeed;
        MoveDirection.y += _playerData.Gravity;
        _controller.Move(MoveDirection * Time.deltaTime);
    }

    private void LookX()
    {
        Vector3 curRot = _player.transform.eulerAngles;
        curRot.y += _playerData.LookSpeed * _playerControls.LookInput.x;
        _player.transform.eulerAngles = curRot;
    }

    private void LookY()
    {
        Vector3 curRot = _player.HeadPos.eulerAngles;
        curRot.x += _playerData.LookSpeed * _playerControls.LookInput.y;
        if(curRot.x > 180)
        {
            curRot.x = Mathf.Clamp(curRot.x, 270f, 361f);

        }
        else
        {
            curRot.x = Mathf.Clamp(curRot.x, -1, 90f);

        }
        _player.HeadPos.eulerAngles = curRot;

    }

    private GameObject CheckInteractable()
    {
        RaycastHit hit;
        if(Physics.Raycast(_player.HeadPos.position, _player.HeadPos.forward, out hit,_playerData.InteractionRange, _playerData.InteractionMask))
        {
            return hit.transform.gameObject;
        }
        _pressedInteractable = false;
        return null;
    }
}
