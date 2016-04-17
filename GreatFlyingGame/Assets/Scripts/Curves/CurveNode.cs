using UnityEngine;
using System.Collections;

using UnityEditor;

public class CurveNode : MonoBehaviour
{
    public Vector3 position { get { return transform.position; } }

    protected virtual void OnDrawGizmos()
    {
        Color temp = Gizmos.color;
        Gizmos.color = new Color(1.0f, 0, 0, .75f);
        Gizmos.DrawSphere(transform.position, 1.0f);
        Gizmos.color = temp;
    }
}
