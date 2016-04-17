using UnityEngine;
using System.Collections;

public abstract class ICurve : MonoBehaviour
{
    public CurveNode[] Nodes;

    public abstract Vector3 GetValueAt(float tValue);
    public abstract float GetTIncrement(float tValue, float speed);
    public abstract Vector3 GetForward(float tValue);

    public bool Loops = false;

    public bool DebugDraw = false;
}
