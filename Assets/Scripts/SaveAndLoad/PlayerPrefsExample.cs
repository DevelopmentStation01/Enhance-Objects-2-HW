using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] TMP_Text textDisplay;

    public void SaveData()
    {
        PlayerPrefs.SetString("SAVED_DATA", input.text);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("SAVED_DATA"))
        {
            textDisplay.SetText(PlayerPrefs.GetString("SAVED_DATA"));
        }
        else
        {
            Debug.Log($"Noa data to load");
        }
    }
}
