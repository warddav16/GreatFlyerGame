using UnityEngine;
using System.Collections;

public class OnTriggerEnableObjects : MonoBehaviour
{
    public bool ToSet = true;
    public GameObject[] Targets;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == Player.Get().gameObject)
        {
            foreach( GameObject g in Targets )
            {
                g.SetActive(ToSet);
            }
        }
    }
}
