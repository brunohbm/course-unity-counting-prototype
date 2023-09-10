using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] ballsPrefab;
    [SerializeField] int spawnAmount = 15;

    BoxCollider spawnDelimiter;
    List<GameObject> instantiatedPrefabs = new List<GameObject>();

    void Awake()
    {
        spawnDelimiter = GetComponent<BoxCollider>();
    }

    void Start()
    {
        InstantiatePrefabs();
    }

    void Update()
    {

    }

    void InstantiatePrefabs()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 position = GetRandomSpawnPosition();
            GameObject prefabToInstantiate = GetRandomPrefab();      
            GameObject instantiatedPrefab = GameObject.Instantiate(prefabToInstantiate, position, prefabToInstantiate.transform.rotation);

            instantiatedPrefab.GetComponent<Collider>().isTrigger = true;
            instantiatedPrefabs.Add(instantiatedPrefab);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 minBound = spawnDelimiter.bounds.min;
        Vector3 maxBound = spawnDelimiter.bounds.max;
        float minBoundX = Random.Range(minBound.x, maxBound.x);
        float minBoundY = Random.Range(minBound.y, maxBound.y);
        float minBoundZ = Random.Range(minBound.z, maxBound.z);

        return new Vector3(minBoundX, minBoundY, minBoundZ);
    }

    GameObject GetRandomPrefab()
    {
        int prefabPos = Random.Range(0, ballsPrefab.Length - 1);
        return ballsPrefab[prefabPos];
    }
}
