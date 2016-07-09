using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipController))]
public class EnemyState : MonoBehaviour
{
    public GameObject Target { get; set; }

    public AIBehavior CurrentBehavior;
    public float AIUpdateFreq = .33f;
    private float _aiUpdateTimer = 0;

    void Awake()
    {
        // Should tick once immediately
        _aiUpdateTimer = AIUpdateFreq;
    }

    public void SetCurrentBehavior(AIBehavior newBehavior)
    {
        CurrentBehavior.enabled = false;
        newBehavior.enabled = true;
        CurrentBehavior = newBehavior;
    }

    void Update()
    {
        _aiUpdateTimer += Time.deltaTime;
        if (_aiUpdateTimer >= AIUpdateFreq)
        {
            if (CurrentBehavior != null)
            {
                CurrentBehavior.AIUpdate(_aiUpdateTimer);
            }
            _aiUpdateTimer = 0;
        }
    }
}
