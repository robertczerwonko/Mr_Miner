using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMananger : MonoBehaviour {

    public static SaveMananger instance;
    public SaveState state;
    private string save = "save4";

    public void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        instance = this;
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetString(save, Helper.Serialize<SaveState>(state));
    }
	

    public void Load()
    {
        if(PlayerPrefs.HasKey(save))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString(save));
        }
        else
        {
            state = new SaveState();
            Save();
        }
    }
}
