using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipController))]
[RequireComponent(typeof(EnemyState))]
public abstract class AIBehavior : MonoBehaviour
{
    public abstract void AIUpdate(float aiDT);

    protected ShipController _shipController;
    protected EnemyState _state;

    public float AvoidanceDistance = 100.0f;

    protected virtual void Awake()
    {
        _shipController = GetComponent<ShipController>(); 
        _state = GetComponent<EnemyState>(); 
    }

    public void SetDirection(Vector3 dir)
    {
        dir = CheckForCollision(dir);

        Vector3 finalDir = (dir - transform.forward).normalized;

        _shipController.UpDown = -finalDir.x;
        _shipController.RightLeft = finalDir.y;
    }

    Vector3 CheckForCollision(Vector3 in_dir)
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, in_dir, out hitinfo, AvoidanceDistance ))
        {
            if( hitinfo.collider.tag == "Player")
            {
                if (Vector3.Dot(hitinfo.collider.transform.forward, (transform.position - hitinfo.transform.position).normalized) > .1f)
                {
                    //Debug.Log("Avoiding: " + hitinfo.collider.gameObject.name + "   InDir: " + in_dir + "   OutDir: " + (in_dir + hitinfo.normal));
                    return CheckForCollision(in_dir = hitinfo.normal);
                }
            }
            else
            {
                //Debug.Log("Avoiding: " + hitinfo.collider.gameObject.name + "   InDir: " + in_dir + "   OutDir: " + (in_dir + hitinfo.normal));
                return CheckForCollision(in_dir = hitinfo.normal);
            }            
        }

        return in_dir;
    }
}
