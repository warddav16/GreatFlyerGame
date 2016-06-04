using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipController))]
public abstract class EnemyBase : MonoBehaviour
{
    private ShipController _shipController;
    public float FiringThreshold = 10.0f;

    void Awake()
    {
        _shipController = GetComponent<ShipController>();
    }

    void Update()
    {
        Player player = Player.Get();
        Vector3 playerPos = player.transform.position;
        Vector3 dirToPlayer = (playerPos - transform.position).normalized;

        Quaternion dRot = Quaternion.LookRotation(dirToPlayer, transform.up);
        Vector3 rot = (dRot * Quaternion.Inverse(transform.rotation)).eulerAngles;

        _shipController.UpDown = rot.x;
        _shipController.RightLeft = -rot.y;

        RaycastHit hitinfo;
        bool shouldFire = false;
        Debug.DrawRay(transform.position, (dRot * Quaternion.Inverse(transform.rotation)) * transform.forward * 1000.0f, Color.white );
        if( Physics.Raycast(transform.position, dirToPlayer, out hitinfo) )
        {
            if( hitinfo.transform.gameObject.tag == "Player")
            {
                if ( Vector3.Dot(transform.forward, dirToPlayer) >= FiringThreshold)
                { 
                    shouldFire = true;
                }
            }
        }
        _shipController.IsFirePrimary = shouldFire;
    }
}
