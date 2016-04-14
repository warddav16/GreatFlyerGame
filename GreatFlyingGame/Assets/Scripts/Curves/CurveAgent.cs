using UnityEngine;
using System.Collections;

public class CurveAgent : MonoBehaviour
{
    public ICurve FollowCurve;
    public Vector3 Offset = Vector3.zero;

    private float CurrentT = 0.0f;

    public float Speed = 10.0f;

    public void Move(float speed)
{
        CurrentT += FollowCurve.GetTIncrement(CurrentT, speed);
        Vector3 newPos = FollowCurve.Move(CurrentT);
        transform.position = newPos + Offset;
        transform.forward = FollowCurve.GetForward(CurrentT);
    }

    void Update()
    {
        Move(Speed * Time.deltaTime);
    }
}
