using UnityEngine;
using System.Collections;

public abstract class Damageable<T> : MonoBehaviour
{
    public abstract void TakeDamage(T damage);
    public virtual void Kill()
    {
        Destroy(gameObject);
    }
    public T Max;
    public T Curr { get; set; }
    public T DefaultDamageTaken;
}
