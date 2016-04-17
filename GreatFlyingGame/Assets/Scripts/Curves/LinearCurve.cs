using UnityEngine;
using System.Collections;
using System;

public class LinearCurve : ICurve
{
    public override Vector3 GetValueAt(float tValue)
    {
        float t = tValue % Nodes.Length;

        int currNode = (int)Mathf.Floor(t);

        if(currNode >= Nodes.Length - 1 && !Loops)
        {
            return Nodes[Nodes.Length - 1].position;
        }

        int nextNode = currNode + 1;

        if (Loops)
        {
            currNode %= Nodes.Length;
            nextNode %= Nodes.Length;
        }

        t -= Mathf.Floor(t);

        Vector3 ret =  (1 - t) * Nodes[currNode].position + t *  Nodes[nextNode].position;
        return ret;
    }

    public override float GetTIncrement(float tValue, float speed)
    {
        float t = tValue % Nodes.Length;
        int currNode = (int)Mathf.Floor(t);

        if (currNode >= Nodes.Length - 1 && !Loops)
        {
            return 0;
        }

        int nextNode = currNode + 1;

        if (Loops)
        {
            currNode %= Nodes.Length;
            nextNode %= Nodes.Length;
        }

        float len = Vector3.Distance(Nodes[currNode].position, Nodes[nextNode].position);

        return speed / len;
    }

    public override Vector3 GetForward(float tValue)
    {
        float t = tValue % Nodes.Length;

        int currNode = (int)Mathf.Floor(t);

        if (currNode >= Nodes.Length - 1 && !Loops)
        {
            if (Nodes.Length > 1)
            {
                return (Nodes[Nodes.Length - 1].position -Nodes[Nodes.Length - 2].position ).normalized;
            }
            else
            {
                return Vector3.forward;
            }
        }

        int nextNode = currNode + 1;

        if(Loops)
        {
            currNode %= Nodes.Length;
            nextNode %= Nodes.Length;
        }

        return (Nodes[nextNode].position - Nodes[currNode].position).normalized;
    }

    void OnDrawGizmos()
    {
        if (!DebugDraw)
            return;

        Color temp = Gizmos.color;

        for (int i = 0; i < Nodes.Length - 1; ++i)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(Nodes[i].position, Nodes[i + 1].position);
        }

        Gizmos.color = new Color(1.0f, 0, 0, .75f);
        Gizmos.DrawSphere(Nodes[Nodes.Length - 1].transform.position, 1.0f);

        Gizmos.color = temp;
    }
}
