using UnityEngine;
using System.Collections;

public abstract class WeaponBase : MonoBehaviour
{
    public float FireRate = 1.0f;
    private float _fireTimer = 0.0f;

    protected virtual void Awake()
    {
        _fireTimer = 0.0f;
    }

    public abstract void Fire();

    protected virtual void Update()
    {
        _fireTimer += Time.deltaTime;
        float isPressed = Input.GetAxis("RightTrigger");
        if( isPressed < 0.0f ) // -1 is right thumbstick
        {
            if( _fireTimer >= FireRate )
            {
                _fireTimer = 0;
                Fire();
            }
        }
    }
}
