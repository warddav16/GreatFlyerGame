using UnityEngine;
using System.Collections;

public class SimpleSpawner : MonoBehaviour
{
    public GameObject[] ObjectsToSpawn;
    public Transform[] SpawnLocations;

    public float SpawnRate = 1.0f;
    private float _timer = 0;

    void Update()
    {
        _timer += Time.deltaTime;
        if( _timer >= SpawnRate )
        {
            GameObject objectToSpawn = ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Length)];
            Transform loc = SpawnLocations[Random.Range(0, SpawnLocations.Length)];
            Instantiate(objectToSpawn, loc.position, loc.rotation);
            _timer = 0;
        }
    }
}
