using UnityEngine;

public class CrateTower : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] GameObject cratePrefab;
    [SerializeField] int minCrates = 5;
    [SerializeField] int maxCrates = 15;
    [SerializeField] float crateSize = 1f; // match this to your crate prefab size

    void Start()
    {
        BuildTower();
    }

    void BuildTower()
    {
        int crateCount = Random.Range(minCrates, maxCrates + 1);

        BoxCollider crateCollider = cratePrefab.GetComponent<BoxCollider>();
        float detectedSize = crateCollider != null ? crateCollider.size.y * cratePrefab.transform.localScale.y : crateSize;

        for (int i = 0; i < crateCount; i++)
        {
            Vector3 spawnPos = transform.position + new Vector3(0, (i * detectedSize) + (detectedSize * 0.5f), 0);
            Instantiate(cratePrefab, spawnPos, Quaternion.identity, transform);
        }
    }
}