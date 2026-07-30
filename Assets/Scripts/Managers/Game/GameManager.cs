using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject uiInteract;
    [SerializeField] private List<GameObject> firstLevelArtifacts = new List<GameObject>();
    [SerializeField] private List<GameObject> firstLevelArtifactSpawnPoints = new List<GameObject>();
    private Artifact currentActiveArtifact;
    private Collider exitTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitTrigger = GetComponent<Collider>();
        int randomArtifact = Random.Range(0, firstLevelArtifacts.Count);
        int randomSpawnPoint = Random.Range(0, firstLevelArtifactSpawnPoints.Count);
        firstLevelArtifacts[randomArtifact].SetActive(true);
        firstLevelArtifacts[randomArtifact].transform.position = firstLevelArtifactSpawnPoints[randomSpawnPoint].transform.position;
        currentActiveArtifact = firstLevelArtifacts[randomArtifact].GetComponent<Artifact>();
        currentActiveArtifact.GameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PickUpArtifact() 
    {
        exitTrigger.enabled = true;
    }
    public void ShowInteract(bool value) 
    {
        uiInteract.SetActive(value);
    }
    private void OnTriggerEnter(Collider other)
    {
        EndRun();
        // Logica temporal para volver al menu
        BackToMenu();
    }
    private void EndRun() 
    {
        //Logica de compra de cartas

    }
    public void BackToMenu() 
    {
        //Volver al menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
    public void LossRun() 
    {
        // Logica de perder

        BackToMenu();
    }
}
