using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    public float Speed = 10.0f;
    public float RotationSpeed = 20.0f;
    public float Acceleration = 15.0f;
    public float FastSpeedMod = 1.5f;
    public float SlowSpeedMod = .5f;

    private Rigidbody _rigidbody;

    private float _currSpeed;

    void Awake()
    {
        _currSpeed = Speed;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float upDown = Input.GetAxis("Horizontal");
        float rightLeft = Input.GetAxis("Vertical");
        float rotateZ = Input.GetAxis("RightH");
        float accel = -Input.GetAxis("RightV");

        Quaternion newRot = _rigidbody.rotation;
        newRot *= Quaternion.Euler(Vector3.up * upDown);
        newRot *= Quaternion.Euler(-Vector3.right * rightLeft);
        newRot *= Quaternion.Euler(-Vector3.forward * rotateZ);

        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, newRot, Time.fixedDeltaTime * RotationSpeed);

        float desiredSpeed = Speed;
        if( accel > 0 )
        {
            desiredSpeed += accel * Speed * FastSpeedMod;
        }
        else if( accel < 0 )
        {
            desiredSpeed -= Mathf.Abs( accel ) * Speed * SlowSpeedMod;
        }

        if( desiredSpeed > _currSpeed )
        {
            _currSpeed += Acceleration * Time.fixedDeltaTime;
        }
        else if( desiredSpeed < _currSpeed )
        {
            _currSpeed -= Acceleration * Time.fixedDeltaTime;
        }

        _rigidbody.velocity = transform.forward * _currSpeed;
    }

    void OnCollisionEnter(Collision col)
    {
        transform.forward = Vector3.Reflect(transform.forward, col.contacts[0].normal);
        _rigidbody.velocity = transform.forward * _rigidbody.velocity.magnitude;
    }
}
