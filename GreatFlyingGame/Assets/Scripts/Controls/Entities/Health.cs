using UnityEngine;
using System.Collections;

public class Health : Damageable<int>
{
    void Awake()
    {
        Curr = Max;
    }

    public override void TakeDamage(int damage)
    {
        if (Curr - damage < 0)
        {
            Debug.Log("You dead");
            Kill();
        }
        Curr -= damage;
        Debug.Log(string.Format("{0} health took {1} damage", gameObject.name, damage));

    }
}
