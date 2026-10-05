using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DataPersistentManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEnctryprion;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string selectedProfileId = "test";

    public static DataPersistentManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEnctryprion);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        // Temporal
        //LoadGame();
    }
    //Temporal
    private void OnApplicationQuit()
    {
        //SaveGame();
    }
    public void NewGame() 
    {
        gameData = new GameData();
    }
    public void LoadGame() 
    {
        gameData = dataHandler.Load(selectedProfileId);
        if(gameData == null) 
        {
            NewGame();
        }
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData,selectedProfileId);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() 
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    public Dictionary<string,GameData> GetAllProfileGameData() 
    {
        return dataHandler.LoadAllData();
    }
}
