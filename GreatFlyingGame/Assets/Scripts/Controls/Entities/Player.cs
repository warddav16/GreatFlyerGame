using UnityEngine;
using System.Collections;
[RequireComponent(typeof(ShipController))]

public class Player : MonoBehaviour
{
    private ShipController _controller;
    
    void Awake()
    {
        _controller = GetComponent<ShipController>();
        
        
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            _controller.UpDown = Input.GetAxis("Horizontal");
            _controller.RightLeft = Input.GetAxis("Vertical");
            _controller.RotateZ = Input.GetAxis("RightH");
            _controller.Accel = Input.GetAxis("RightV");
        }
        else
        {
            _controller.UpDown = Input.GetAxis("Mouse X");
            _controller.RightLeft = Input.GetAxis("Mouse Y");
            _controller.RotateZ = Input.GetAxis("Horizontal");
            _controller.Accel = Input.GetAxis("Vertical");
            _controller.IsFirePrimary = Input.GetMouseButton(0);
        }
    }

    void OnCollisionEnter(Collision col)
    {
    }
}
