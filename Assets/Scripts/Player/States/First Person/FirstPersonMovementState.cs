using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FirstPersonMovementState : FirstPersonBaseState
{
    private CharacterController _controller;
    private FirstPersonPlayerControls _playerControls;
    private PlayerData _playerData;
    private FirstPersonPlayerManager _player;


    private bool _pressedLastFrame;
    private bool _pressedInteractable = false;
    private float _distanceWalked;
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
        _distanceWalked = 0;
    }

    public override void OnExitState(FirstPersonPlayerManager player)
    {
    }

    public override void OnUpdateState(FirstPersonPlayerManager player)
    {
        if (Time.timeScale == 0) return;
        Move();
        LookX();
        LookY();
        player._highlightedObject = (_playerControls.IsPosessing || PlayerSubHealth.Instance.Health <=0)? null : CheckInteractable();

        if (_distanceWalked > player.PlayerData.MinimumFootStepDistance)
        {
            _distanceWalked = 0;
            player.FootstepSource.PlayOneShot(player.Footsteps[Random.Range(0, player.Footsteps.Length)]);
        }

        if (player._highlightedObject != null && !_pressedInteractable)
        {
            if (!_pressedLastFrame && _playerControls.SelectPressed)
            {
                _pressedInteractable = true;
                player._highlightedObject.OnInteracted(_playerControls);
            }
        }
        _pressedLastFrame = _playerControls.SelectPressed;
    }

    private void Move()
    {
        Vector3 MoveDirection = _player.transform.forward * _playerControls.MoveInput.y + _player.transform.right * _playerControls.MoveInput.x;
        MoveDirection *= _playerData.MoveSpeed;
        _distanceWalked += MoveDirection.magnitude * Time.deltaTime;

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

    private IInteractable CheckInteractable()
    {
        RaycastHit hit;
        if(Physics.Raycast(_player.HeadPos.position, _player.HeadPos.forward, out hit,_playerData.InteractionRange, _playerData.InteractionMask))
        {
            IInteractable foundComponent = (IInteractable)hit.transform.GetComponent(typeof(IInteractable));

            if (foundComponent == null) return null;

            if(!foundComponent.CheckInteractable(hit.distance)) return null;

            return foundComponent;
        }
        _pressedInteractable = false;
        return null;
    }
}
