using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsMenu : MonoBehaviour
{
    [SerializeField] private List<SaveSlot> saveSlots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Dictionary<string, GameData> profilesGameData = DataPersistentManager.instance.GetAllProfileGameData();
        foreach(SaveSlot saveSlot in saveSlots) 
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
        }
    }

}
