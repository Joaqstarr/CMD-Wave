using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObstacle : MonoBehaviour
{
    [SerializeField]
    private Transform _movePos;
    [SerializeField]
    private float _moveDuration;

    private Vector2 _startPos;
    private float _moveTimer;
    private bool _moving;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_moving)
        {
            _moveTimer += Time.deltaTime;
            transform.position = new Vector2(Mathf.Lerp(_startPos.x, _movePos.position.x, _moveTimer), Mathf.Lerp(_startPos.y, _movePos.position.y, _moveTimer));
            if (_moveTimer >= _moveDuration )
            {
                _moving = false;
            }
        }
    }

    public void ActivateDoor()
    {
        _moving = true;
    }
}
