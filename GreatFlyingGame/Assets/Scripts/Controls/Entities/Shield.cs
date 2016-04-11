using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class Shield : Damageable<float>
{
    public int _regenTime = 3;
    public int _regenWait = 1;
    private float _regenWaitTimer = 0;
    public bool IsRegen { get; set; }
    private float _regenSpeed;
    void Awake()
    {
        Curr = Max;
        _regenSpeed = Max / _regenTime;
    }

    // Update is called once per frame
    void Update()
    {
        _regenWaitTimer += Time.deltaTime;
        if (_regenWaitTimer > _regenWait && Curr < Max )
        {
            Regen();
        }
    }

    public override void TakeDamage(float damage)
    {
        if(IsRegen)
        {
            ResetTimer();
        }
        
        Debug.Log(string.Format("{0} shield took {1} damage", gameObject.name, damage));
        if (Curr - damage < 0)
        {
            var returnDamge = Mathf.Abs(Mathf.FloorToInt(Curr - damage));
            Curr = 0;
            GetComponent<Health>().TakeDamage(returnDamge);
        }
    }
    public void ResetTimer()
    {
        _regenWaitTimer = 0;
    }
    private void Regen()
    {
        IsRegen = true;
        if(Curr + _regenSpeed * Time.deltaTime > Max)
        {
            Curr = Max;
            IsRegen = false;
            return;
        }
        Curr += _regenSpeed * Time.deltaTime;
    }
}
