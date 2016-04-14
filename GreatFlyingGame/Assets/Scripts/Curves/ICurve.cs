using UnityEngine;
using System.Collections;

public abstract class ICurve : MonoBehaviour
{
    public Transform[] Nodes;

    public abstract Vector3 Move(float tValue);
    public abstract float GetTIncrement(float tValue, float speed);
    public abstract Vector3 GetForward(float tValue);

    public bool Loops = false;

    public bool DebugDraw = false;

    void OnDrawGizmos()
    {
        if (!DebugDraw)
            return;

        Color temp = Gizmos.color;

        for( int i = 0; i < Nodes.Length - 1; ++i )
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(Nodes[i].position, Nodes[i + 1].position);
        }

        Gizmos.color = new Color(1.0f, 0, 0, .75f);
        Gizmos.DrawSphere(Nodes[Nodes.Length-1].transform.position, 1.0f);

        Gizmos.color = temp;
    }
}
