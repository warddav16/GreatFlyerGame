using UnityEngine;
using System.Collections;

public class StaticBomb : Health
{
    public float RadiusOfDoom = 40f;
    public int DamageToDeal = 10;

    public override void Kill()
    {
        Explode();
        base.Kill();
    }

    void Explode()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, RadiusOfDoom);
        foreach (Collider c in col)
        {
            Damageable<int> d = c.gameObject.GetComponent<Damageable<int>>();
            if (d && d != this)
            {
                d.TakeDamage(DamageToDeal);
            }
        }
    }

    void Update()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, RadiusOfDoom);
        foreach (Collider c in col)
        {
            if( c.gameObject == Player.Get().gameObject)
            {
                Kill();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Color col = Gizmos.color;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RadiusOfDoom);
        Gizmos.color = col;
    }
}
