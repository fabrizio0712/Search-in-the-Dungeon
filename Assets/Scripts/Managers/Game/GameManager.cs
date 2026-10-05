using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Containers")]
    [SerializeField] private GameObject uIGameplay;
    [SerializeField] private GameObject uIPause;
    [SerializeField] private GameObject uIRewards;

    [Header("UI Gameplay Elements")]
    [SerializeField] private GameObject uiInteract;
    [SerializeField] private GameObject uiCompass;
    [SerializeField] private TextMeshProUGUI riskText;
    [SerializeField] private TextMeshProUGUI riskBlockText;
    [SerializeField] private TextMeshProUGUI hazardBlockText;

    [Header("Artifacts")]
    [SerializeField] private List<GameObject> artifactsLevel1 = new List<GameObject>();
    [SerializeField] private List<GameObject> artifactsSpawnpointsLevel1 = new List<GameObject>();
    private Artifact currentActiveArtifact;

    [Header("Embers & Tresure")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject crownPrefab;
    [SerializeField] private GameObject emberPrefab;
    [SerializeField] private List<GameObject> treasureSpawnpointsLevel1 = new List<GameObject>();
    [SerializeField] private List<GameObject> coinsPool = new List<GameObject>();
    [SerializeField] private List<GameObject> crownsPool = new List<GameObject>();
    [SerializeField] private List<GameObject> embersPool = new List<GameObject>();
    [SerializeField] private int aditionalEmbers = 0;

    [Header("Healings")]
    [SerializeField] private GameObject healingPrefab;
    [SerializeField] private List<GameObject> healingPool = new List<GameObject>();
    [SerializeField] private List<GameObject> healingSpawnPoints = new List<GameObject>();
    [SerializeField] private float spawnHealTime = 1;
    [SerializeField] private float spawnHealCurrentTime;

    [Header("Risks")]
    [SerializeField] private int riskLevel = 0;
    [SerializeField] private int maxRiskLevel = 10;
    [SerializeField] private int riskBlockCount = 0;
    [SerializeField] private bool riskTrapsActive = false;
    [SerializeField] private List<GameObject> risksLevel1 = new List<GameObject>();
    [SerializeField] private float stumbleTimer = 1;
    [SerializeField] private float stumbleCurrentTimer = 0;

    [Header("Hazards")]
    [SerializeField] private float hazardTimer = 1;
    [SerializeField] private float hazardCurrentTimer = 0;
    [SerializeField] private int hazardBlockCount = 0;
    [SerializeField] private List<Hazard> hazardsLevel1 = new List<Hazard>();
 
    [Header("Audio and SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip riskAudioClip;
    [SerializeField] private AudioClip blockRiskAudioClip;
    [SerializeField] private AudioClip hazardAudioCip;
    [SerializeField] private AudioClip blockHazardAudioClip;

    [Header("Other Elements")]
    [SerializeField] private PlayerController player;
    [SerializeField] private DeckManager deckManager;
    [SerializeField] private bool isPaused = false;
    [SerializeField] private bool isRunEnded = false;
    private Collider exitTrigger;

    // Public References
    public PlayerController Player { get => player;}
    public Artifact CurrentActiveArtifact { get => currentActiveArtifact;}
    public int AditionalEmbers { get => aditionalEmbers; }

    void Start()
    {
        exitTrigger = GetComponent<Collider>();
        SpanwRandomArtifact();
        UIHazardBlockUpdate();
        UIRiskBlockUpdate();
    }

    void Update()
    {
        if (!isRunEnded)
        {
            // --------- Spawn Heal Timer Logic --------------
            if (spawnHealCurrentTime < spawnHealTime)
            {
                spawnHealCurrentTime += Time.deltaTime;
            }
            else
            {
                spawnHealCurrentTime = 0f;
                SpawnHealObject();
            }
            // -----------------------------------------------

            // ------------ Hazard Timer Logic ---------------
            if (hazardCurrentTimer < hazardTimer)
            {
                hazardCurrentTimer += Time.deltaTime;
            }
            else
            {
                hazardCurrentTimer = 0f;
                int flag = Random.Range(0, 3);
                if(flag > 1) HazardUpdate(1);
            }
            // -----------------------------------------------

            // --------------- Stumble Logic -----------------
            if (stumbleCurrentTimer < stumbleTimer)
            { 
                stumbleCurrentTimer += Time.deltaTime;
            }
            else
            {
                stumbleCurrentTimer = 0f;
                deckManager.AddStumbleToDeck();
            }
            // -----------------------------------------------

            // ------------ Pause Input Logic ----------------
            if (Input.GetButtonDown("Cancel"))
            {
                if (!isPaused)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
            // ----------------------------------------------
        }  
    }
    // ----------------------------------------------------

    // -------------- Healing Functions -------------------
    public void SpawnHealObject() 
    {
        int randomPosition = Random.Range(0,healingSpawnPoints.Count);
        Vector3 directionDisplacement = new Vector3(Random.Range(0.1f, 1f), 0, Random.Range(0.1f, 1f));
        if (healingPool.Count > 0) 
        {
            GameObject spawnedHeal = healingPool[0];
            spawnedHeal.SetActive(true);
            spawnedHeal.transform.position = healingSpawnPoints[randomPosition].transform.position;
            spawnedHeal.GetComponent<Rigidbody>().AddForce(directionDisplacement, ForceMode.Impulse);
            healingPool.Remove(spawnedHeal);
        }
        else 
        {
            GameObject spawnedHeal = Instantiate(healingPrefab);
            spawnedHeal.GetComponent<HealingLogic>().SetGameManager(this);
            spawnedHeal.transform.position = healingSpawnPoints[randomPosition].transform.position;
            spawnedHeal.GetComponent<Rigidbody>().AddForce(directionDisplacement, ForceMode.Impulse);
        }
    }
    public void AddHealObjectToPool(GameObject go) 
    {
        healingPool.Add(go);
    }
    // ----------------------------------------------------

    // -------------- Embers Functions --------------------
    public void SpawnEmbers(int amount) 
    {
        if (riskLevel == 10) return;
        for(int i = 0; i < amount; i++)
        {
            int randomPosition = Random.Range(0, treasureSpawnpointsLevel1.Count);
            Vector3 directionDisplacement = new Vector3(Random.Range(0.1f, 1f), 0, Random.Range(0.1f, 1f));
            if (embersPool.Count > 0) 
            {
                GameObject spawnedEmber = embersPool[0];
                spawnedEmber.SetActive(true);
                spawnedEmber.transform.position = treasureSpawnpointsLevel1[randomPosition].transform.position;
                spawnedEmber.GetComponent<Rigidbody>().AddForce(directionDisplacement,ForceMode.Impulse);
                embersPool.Remove(spawnedEmber);
            }
            else 
            {
                GameObject spawnedEmber = Instantiate(emberPrefab);
                spawnedEmber.GetComponent<EmberLogic>().SetGameManager(this);
                spawnedEmber.transform.position = treasureSpawnpointsLevel1[randomPosition].transform.position;
                spawnedEmber.GetComponent<Rigidbody>().AddForce(directionDisplacement, ForceMode.Impulse);
            }
        }
    }
    public void AddEmberToPool(GameObject go) 
    {
        embersPool.Add(go);
        aditionalEmbers++;
    }
    // ----------------------------------------------------

    // -------------- Artifacts Functions -----------------
    private void SpanwRandomArtifact()
    { 
        int randomArtifact = Random.Range(0, artifactsLevel1.Count);
        int randomSpawnPoint = Random.Range(0, artifactsSpawnpointsLevel1.Count);
        artifactsLevel1[randomArtifact].SetActive(true);
        artifactsLevel1[randomArtifact].transform.position = artifactsSpawnpointsLevel1[randomSpawnPoint].transform.position;
        currentActiveArtifact = artifactsLevel1[randomArtifact].GetComponent<Artifact>();
        currentActiveArtifact.GameManager = this;
    }
    public void PickUpArtifact()
    {
        RiskUpdate(3);
        exitTrigger.enabled = true;
        uiCompass.SetActive(false);
    }
    // ------------------------------------------------

    // ----------- Risk Functions --------------------- 
    public void AddRiskBlock(int amount) 
    {
        riskBlockCount += amount;
        UIRiskBlockUpdate();
    }
    public void RiskUpdate(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (riskBlockCount > 0)
            {
                riskBlockCount--;
                UIRiskBlockUpdate();
                audioSource.PlayOneShot(blockRiskAudioClip);
            }
            else
            {
                if (riskLevel < maxRiskLevel - 1)
                {
                    riskLevel++;
                    UIRiskUpdate();
                    audioSource.PlayOneShot(riskAudioClip);
                }
                else
                {
                    if (riskLevel == 9) 
                    {
                        riskLevel++;
                        UIRiskUpdate();
                    }
                    if (!riskTrapsActive) 
                    {
                        foreach(GameObject go in risksLevel1) 
                        {
                            go.SetActive(true);
                        }
                        riskTrapsActive = true;
                    }
                    HazardUpdate(1);
                }
            }
        }
    }
    // ------------------------------------------------

    // ----------- Hazard Functions -------------------
    public void AddHazardBlock(int amount) 
    {
        hazardBlockCount += amount;
        UIHazardBlockUpdate();
    }
    public void HazardUpdate(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (hazardBlockCount > 0)
            {
                hazardBlockCount--;
                UIHazardBlockUpdate();
                audioSource.PlayOneShot(blockHazardAudioClip);
            }
            else ActivateRandomHazard();
        }
    }
    private void ActivateRandomHazard()
    {
        if (hazardsLevel1.Count < 1)
        {
            Debug.Log("No more Hazards To Activate");
        }
        else
        {
            int randomHazard = UnityEngine.Random.Range(0, hazardsLevel1.Count);
            Hazard temp = hazardsLevel1[randomHazard];
            temp.TryToBlock();
            hazardsLevel1.Remove(temp);
            audioSource.PlayOneShot(hazardAudioCip);
        }
    }
    // ------------------------------------------------

    // --------- UI Gameplay Functions ----------------
    public void UIShowInteract(bool value) 
    {
        uiInteract.SetActive(value);
    }
    public void UIRiskUpdate() 
    {
        riskText.text = riskLevel.ToString();
    }
    public void UIRiskBlockUpdate()
    {
        riskBlockText.text = riskBlockCount.ToString();
    }
    public void UIHazardBlockUpdate() 
    {
        hazardBlockText.text = hazardBlockCount.ToString();
    }
    // ------------------------------------------------

    // --------- UI Pause Functions -------------------
    public void Resume() 
    {
        isPaused = false;
        uIPause.SetActive(false);
        uIGameplay.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        audioSource.UnPause();
    }
    public void Pause() 
    {
        isPaused = true;
        uIPause.SetActive(true);
        uIGameplay.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        audioSource.Pause();
    }
    public void Options() 
    {

    }
    public void ExitRun() 
    {
        BackToMenu();
    }
    // ------------------------------------------------

    //--------- Finish Run ----------

    // Volver al menu
    public void BackToMenu() 
    {
        // Guardar Progresión
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    // Ganar Run
    private void EndRun()
    {
        // Logica de compra de cartas
        isRunEnded = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        uIGameplay.SetActive(false);
        uIRewards.SetActive(true);
        Time.timeScale = 0;
    }
    // Perder Run
    public void LossRun() 
    {
        // Logica de perder
        BackToMenu();
    }
    // Trigger de Victoria
    private void OnTriggerEnter(Collider other)
    {
        EndRun();
    }
}
