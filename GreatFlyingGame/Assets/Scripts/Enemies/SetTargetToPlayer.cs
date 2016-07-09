using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyState))]
public class SetTargetToPlayer : MonoBehaviour
{
    void Start ()
    {
        GetComponent<EnemyState>().Target = Player.Get().gameObject;
	}
}
