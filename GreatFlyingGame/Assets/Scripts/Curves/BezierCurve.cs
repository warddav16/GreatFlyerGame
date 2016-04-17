using UnityEngine;
using System.Collections;

public class BezierCurve : ICurve
{
    public override Vector3 GetValueAt(float tValue)
    {
        float t = tValue % ( Nodes.Length / 2 );

        int currNode = ( (int)Mathf.Floor(t) ) * 2;

        if( currNode >= Nodes.Length - 2 && !Loops )
        {
            return Nodes[Nodes.Length - 2].position;
        }

        int nextNode = currNode + 2;

        if (Loops)
        {
            currNode %= Nodes.Length;
            nextNode %= Nodes.Length;
        }

        Vector3 p0 = Nodes[currNode].position;
        Vector3 p1 = Nodes[currNode + 1].position;
        Vector3 p2 = Nodes[nextNode].position + (Nodes[nextNode].position - Nodes[nextNode + 1].position); // inferred node, asserts continuous curve
        Vector3 p3 = Nodes[nextNode].position;

        t -= Mathf.Floor(t);
        
        float tt = t * t;
        float ttt = tt * t;
        float u = 1 - t;
        float uu = u * u;
        float uuu = uu * u;

        return uuu * p0 + 3 * uu * t * p1 + 3 * u * tt * p2 + ttt * p3;
    }

    public override float GetTIncrement(float tValue, float speed)
    {
        return speed / GetDerivative(tValue).magnitude;
    }

    public override Vector3 GetForward(float tValue)
    {
        return GetDerivative(tValue).normalized;
    }

    private Vector3 GetDerivative(float tValue)
    {
        float t = tValue % (Nodes.Length / 2);

        int currNode = ((int)Mathf.Floor(t)) * 2;

        if (currNode >= Nodes.Length - 2 && !Loops)
        {
            return Nodes[Nodes.Length - 2].position;
        }

        int nextNode = currNode + 2;

        if (Loops)
        {
            currNode %= Nodes.Length;
            nextNode %= Nodes.Length;
        }

        Vector3 p0 = Nodes[currNode].position;
        Vector3 p1 = Nodes[currNode + 1].position;
        Vector3 p2 = Nodes[nextNode].position + (Nodes[nextNode].position - Nodes[nextNode + 1].position); // inferred node, asserts continuous curve
        Vector3 p3 = Nodes[nextNode].position;

        t -= Mathf.Floor(t);

        float tt = t * t;
        float u = 1 - t;
        float uu = u * u;

        return -3 * p0 * uu + 3 * p1 * (uu - 2 * t * u) + 3 * p2 * (-tt + u * 2 * t) + 3 * p3 * tt;
    }

    void OnDrawGizmos()
    {
        if (!DebugDraw)
            return;

        Color temp = Gizmos.color;
        Gizmos.color = Color.yellow;

        for (float t = 0; t < Nodes.Length / 2 - .2f; t+= .1f)
        {
            Vector3 p = GetValueAt(t);
            Vector3 p1 = GetValueAt(t + .1f);
            Gizmos.DrawLine(p, p1);
        }

        Gizmos.color = temp;
    }
}
