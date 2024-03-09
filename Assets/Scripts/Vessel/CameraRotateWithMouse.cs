using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotateWithMouse : MonoBehaviour
{

    [SerializeField]
    private Vector2 _rotateAmount;
    private Vector3 _baseEuler;
    // Start is called before the first frame update
    void Start()
    {
        _baseEuler = transform.localEulerAngles;
    }

    // Update is called once per frame  
    void Update()
    {

        Vector2 mousePos = Mouse.current.position.ReadValue();
        mousePos.x = mousePos.x / Camera.main.pixelWidth;
        mousePos.y = mousePos.y / Camera.main.pixelHeight;
        mousePos.x = Mathf.Clamp01(mousePos.x);
        mousePos.y = Mathf.Clamp01(mousePos.y);


        mousePos.x -= 0.5f;
        mousePos.y -= 0.5f;


        mousePos *= 2;


        Vector3 eulerAngles = transform.localEulerAngles;
        Vector3 targetEuler = new Vector3(_baseEuler.x + (_rotateAmount.y * mousePos.y), _baseEuler.y +(_rotateAmount.x * mousePos.x), _baseEuler.z);

        if (eulerAngles.x > 180)
            targetEuler.x += 360;

        if (eulerAngles.y > 180)
            targetEuler.y += 360;


        eulerAngles.x += (targetEuler.x - eulerAngles.x) * 0.1f;
        eulerAngles.y += (targetEuler.y - eulerAngles.y) * 0.1f;


        transform.localEulerAngles = eulerAngles;
    }


}
