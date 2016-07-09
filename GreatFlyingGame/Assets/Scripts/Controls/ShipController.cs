using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(WeaponBase))]
public class ShipController : MonoBehaviour
{
    public float Speed = 10.0f;
    public float RotationSpeed = 20.0f;
    public float Acceleration = 15.0f;
    public float FastSpeedMod = 1.5f;
    public float SlowSpeedMod = .5f;
    private Health _health;
    private Shield _shield;
    private Rigidbody _rigidbody;
    public WeaponBase PrimaryWeapon;
    public WeaponBase SecondaryWeapon;
    

    private float _currSpeed;
    public float UpDown { get; set; }
    public float RightLeft { get; set; }
    public float RotateZ { get; set; }
    public float Accel { get; set; }

    public bool IsFirePrimary;
    public bool IsFireSecondary;

    void Awake()
    {
        _currSpeed = Speed;
        _rigidbody = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        _shield = GetComponent<Shield>();
    }

    void Update()
    {
        //if(IsFirePrimary)
        //{
        PrimaryWeapon.IsFiring = IsFirePrimary;
        //}

        if (SecondaryWeapon != null)
        {
            SecondaryWeapon.IsFiring = IsFireSecondary;
        }
    }
    void FixedUpdate()
    {
        Quaternion newRot = _rigidbody.rotation;
        newRot *= Quaternion.Euler(Vector3.up * UpDown);
        newRot *= Quaternion.Euler(Vector3.right * RightLeft);
        newRot *= Quaternion.Euler(Vector3.forward * RotateZ);

        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, newRot, Time.fixedDeltaTime * RotationSpeed);

        float desiredSpeed = Speed;
        if( Accel > 0 )
        {
            desiredSpeed += Accel * Speed * FastSpeedMod;
        }
        else if( Accel < 0 )
        {
            desiredSpeed -= Mathf.Abs( Accel ) * Speed * SlowSpeedMod;
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
        if (_shield == null)
        {
            _health.TakeDamage(/*col.GetComponent<DealsDamage>().Damage ||*/ _health.DefaultDamageTaken);
        }
        else
        {
            _shield.TakeDamage(/*col.GetComponent<DealsDamage>().Damage ||*/ _shield.DefaultDamageTaken);
        }
    }
}
