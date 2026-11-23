using System.Collections.Generic;
using UnityEngine;

public class BushLogic : MonoBehaviour
{
    [SerializeField] private GameObject healingPrefab;
    [SerializeField] private List<GameObject> healingPool = new List<GameObject>();
    [SerializeField] private float spawnHealTime = 1;
    [SerializeField] private float spawnHealCurrentTime;
    [SerializeField] private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnHealCurrentTime = Random.Range(0, 25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnHealCurrentTime < spawnHealTime)
        {
            spawnHealCurrentTime += Time.deltaTime;
        }
        else
        {
            if (Random.Range(0, 6) > 4)
            {
                SpawnHealObject();
            }
            spawnHealCurrentTime = 0f;
        }
    }
    public void SpawnHealObject()
    {
        Vector3 directionDisplacement = new Vector3(Random.Range(0.1f, 1f), 0, Random.Range(0.1f, 1f));
        if (healingPool.Count > 0)
        {
            Debug.Log("Get from pool");
            GameObject spawnedHeal = healingPool[0];
            spawnedHeal.SetActive(true);
            spawnedHeal.transform.position = transform.position;
            spawnedHeal.GetComponent<Rigidbody>().AddForce(directionDisplacement, ForceMode.Impulse);
            healingPool.Remove(spawnedHeal);
        }
        else
        {
            Debug.Log("Spawn New");
            GameObject spawnedHeal = Instantiate(healingPrefab);
            spawnedHeal.GetComponent<HealingLogic>().SetParentBush(this);
            spawnedHeal.transform.position = transform.position;
            spawnedHeal.GetComponent<Rigidbody>().AddForce(directionDisplacement, ForceMode.Impulse);
        }
        audioSource.Play();
    }
    public void AddHealObjectToPool(GameObject go)
    {
        healingPool.Add(go);
    }
}
