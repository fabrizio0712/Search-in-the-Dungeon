using TMPro;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";
    [SerializeField] private TextMeshProUGUI text;

    public void SetData(GameData data) 
    {
        if(data == null) 
        {
            text.SetText("Empty");
        }
        else 
        {
            text.SetText("Continue");
        }
    }
    public string GetProfileId() 
    {
        return profileId;
    }

}
