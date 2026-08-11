using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject uiInteract;
    [SerializeField] private GameObject uiCompass;
    [SerializeField] private TextMeshProUGUI riskText;
    [SerializeField] private TextMeshProUGUI riskBlockText;
    [SerializeField] private TextMeshProUGUI hazardBlockText;

    [Header("Artifacts")]
    [SerializeField] private List<GameObject> artifactsLevel1 = new List<GameObject>();
    [SerializeField] private List<GameObject> artifactsSpawnpointsLevel1 = new List<GameObject>();
    private Artifact currentActiveArtifact;

    [Header("Risks")]
    [SerializeField] private int riskLevel = 0;
    [SerializeField] private int maxRiskLevel = 10;
    [SerializeField] private int riskBlockCount = 0;
    [SerializeField] private bool riskTrapsActive = false;

    [SerializeField] private List<GameObject> risksLevel1 = new List<GameObject>();

    [Header("Hazards")]
    [SerializeField] private float hazardTimer = 1;
    [SerializeField] private float hazardCurrentTimer = 0;
    [SerializeField] private int hazardBlockCount = 0;

    [SerializeField] private List<Hazard> hazardsLevel1 = new List<Hazard>();
 
    // Exit
    private Collider exitTrigger;

    [Header("Audio and SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip riskAudioClip;
    [SerializeField] private AudioClip blockRiskAudioClip;
    [SerializeField] private AudioClip hazardAudioCip;
    [SerializeField] private AudioClip blockHazardAudioClip;

    // Debug Variables
    private float riskTimer = 1;
    private float riskCurrentTimer = 0;

    [Header("Other Elements")]
    [SerializeField] private PlayerController player;

    // Public References
    public PlayerController Player { get => player;}
    public Artifact CurrentActiveArtifact { get => currentActiveArtifact;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitTrigger = GetComponent<Collider>();
        SpanwRandomArtifact();

        //---- Debug Show Block Updates -----
        AddHazardBlock(3);
        AddRiskBlock(5);
        UIHazardBlockUpdate();
        UIRiskBlockUpdate();
        //----------------------------------- 
    }

    // Update is called once per frame
    void Update()
    {
        //-------- Debug Risk And Hazard Logics--------
        if(hazardCurrentTimer < hazardTimer) hazardCurrentTimer += Time.deltaTime;
        else 
        {
            hazardCurrentTimer = 0f;
            HazardUpdate(1);
        }/*
        if(riskCurrentTimer <  riskTimer) riskCurrentTimer += Time.deltaTime;
        else 
        {
            riskCurrentTimer = 0f;
            RiskUpdate(1);
        }*/
        // --------------------------------------------
    }
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
    public void AddRiskBlock(int amount) 
    {
        riskBlockCount += amount;
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
                if (riskLevel < maxRiskLevel)
                {
                    riskLevel++;
                    UIRiskUpdate();
                    audioSource.PlayOneShot(riskAudioClip);
                }
                else
                {
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
    public void AddHazardBlock(int amount) 
    {
        hazardBlockCount += amount;
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
            int randomHazard = Random.Range(0, hazardsLevel1.Count);
            Hazard temp = hazardsLevel1[randomHazard];
            temp.TryToBlock();
            hazardsLevel1.Remove(temp);
            audioSource.PlayOneShot(hazardAudioCip);
        }
    }
    //--------- Logicas de la UI ----------
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
    
    //--------- Finish Run ----------

    // Volver al menu
    public void BackToMenu() 
    {
        // Guardar Progresión
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
    // Ganar Run
    private void EndRun()
    {
        // Logica de compra de cartas
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
        // Logica temporal para volver al menu
        BackToMenu();
    }
}
