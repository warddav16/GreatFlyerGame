using UnityEngine;
using System.Collections;

public class BasicShooter : WeaponBase
{
    public GameObject Bullet;

    public override void Fire()
    {
        GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
        bullet.transform.forward = transform.forward;
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), this.GetComponent<Collider>(), true);
        bullet.GetComponent<ProjectileBase>().SetDirection(transform.forward);
    }
}
