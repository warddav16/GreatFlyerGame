using UnityEngine;
using System.Collections;
using System;

public class FleeTargetBehavior : AIBehavior
{
    public float FiringThreshold = 10.0f; 

    public override void AIUpdate(float aiDT)
    {
        GameObject target = _state.Target;
        Vector3 targetPos = target.transform.position;
        Vector3 dirToTarget = (targetPos - transform.position).normalized;

        base.SetDirection(-dirToTarget);

        RaycastHit hitinfo;
        bool shouldFire = false;
        if (Physics.Raycast(transform.position, dirToTarget, out hitinfo))
        {
            if (hitinfo.transform.gameObject.tag == "Player")
            {
                if (Vector3.Dot(transform.forward, dirToTarget) >= FiringThreshold)
                {
                    shouldFire = true;
                }
            }
        }
        _shipController.IsFirePrimary = shouldFire;
    }
}
