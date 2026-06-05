using UnityEngine;
using System.Collections;

public class RagdollPile : MonoBehaviour
{
    [Header("Pile Settings")]
    [SerializeField] GameObject npcPrefab;
    [SerializeField] int pileCount = 15;
    [SerializeField] float pileRadius = 3f;

    [Header("Falling Bodies")]
    [SerializeField] float minDropInterval = 2f;
    [SerializeField] float maxDropInterval = 5f;
    [SerializeField] float dropHeight = 20f;

    void Start()
    {
        SpawnPile();
        StartCoroutine(DropBodyRoutine());
    }

    void SpawnPile()
    {
        for (int i = 0; i < pileCount; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * pileRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, i * 0.5f, randomCircle.y);

            GameObject npc = Instantiate(npcPrefab, spawnPos, Random.rotation);
            NPCController controller = npc.GetComponent<NPCController>();

            if (controller != null)
                StartCoroutine(EnableRagdollNextFrame(controller, silent: true));
        }
    }

    IEnumerator DropBodyRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minDropInterval, maxDropInterval);
            yield return new WaitForSeconds(waitTime);
            DropBody();
        }
    }

    void DropBody()
    {
        Vector2 randomCircle = Random.insideUnitCircle * pileRadius;
        Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, dropHeight, randomCircle.y);

        GameObject npc = Instantiate(npcPrefab, spawnPos, Random.rotation);
        NPCController controller = npc.GetComponent<NPCController>();

        if (controller != null)
            StartCoroutine(EnableRagdollNextFrame(controller, silent: true));
    }

    IEnumerator EnableRagdollNextFrame(NPCController controller, bool silent)
    {
        yield return new WaitForSeconds(0.1f); // slightly longer wait
        controller.EnableRagdoll(silent);
    }
}