using UnityEngine;
using System.Collections;

public class Health : Damageable<int>
{
    
    void Awake()
    {
        Curr = Max;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
