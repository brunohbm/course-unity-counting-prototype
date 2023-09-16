using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] ballsPrefab;
    [SerializeField] int spawnAmount = 15;
    [SerializeField] Slider betSlider;
    [SerializeField] TextMeshProUGUI betAmountText;
    [SerializeField] TextMeshProUGUI finalBetText;
    [SerializeField] Button betButton;
    [SerializeField] GameObject betScreen;
    [SerializeField] GameObject resultScreen;

    BoxCollider spawnDelimiter;
    List<GameObject> instantiatedPrefabs = new List<GameObject>();

    void Awake()
    {
        spawnDelimiter = GetComponent<BoxCollider>();
    }

    void Start()
    {
        InstantiatePrefabs();
        betSlider.onValueChanged.AddListener(OnChangeBetSlider);
        betButton.onClick.AddListener(StartBet);
        betSlider.maxValue = spawnAmount;
    }

    void Update()
    {

    }

    void OnChangeBetSlider(float newValue)
    {
        betAmountText.text = newValue.ToString();
        finalBetText.text = "Guess: " + newValue;
    }

    void StartBet()
    {
        betScreen.SetActive(false);
        resultScreen.SetActive(true);

        foreach (GameObject prefab in instantiatedPrefabs)
        {
            prefab.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void InstantiatePrefabs()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 position = GetRandomSpawnPosition();
            GameObject prefabToInstantiate = GetRandomPrefab();      
            GameObject instantiatedPrefab = GameObject.Instantiate(prefabToInstantiate, position, prefabToInstantiate.transform.rotation);

            instantiatedPrefab.GetComponent<Rigidbody>().isKinematic = true;
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
