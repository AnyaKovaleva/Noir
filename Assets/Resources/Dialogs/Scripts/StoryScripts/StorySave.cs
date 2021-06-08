using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class StorySave : MonoBehaviour
{
    public GameObject popUp;
    public Button loadButton;
    public GameObject mainMenu;

    private void Start()
    {
        if (!(File.Exists(Application.persistentDataPath + "/SaveData.dat")))
        {
            loadButton.GetComponent<Button>().interactable = false;
        }
    }

    public void TryNewGame()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            popUp.SetActive(true);
            
        }
        else 
        {
            NewGame();
        }
    }

    public void NewGame() 
    {
        popUp.SetActive(false);
        mainMenu.SetActive(false);
        
        ResetData();
        DataTransferScript.savedStoryID = 1;
        DataTransferScript.health = 100;
        DataTransferScript.money = 25;
        DataTransferScript.bullets = 3;       
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveData data = new SaveData();
        data.savedStoryID = DataTransferScript.savedStoryID;
        data.savedMoney = DataTransferScript.money;
        data.savedHealth = DataTransferScript.health;
        data.savedBullets = DataTransferScript.bullets;
        bf.Serialize(file, data);
        file.Close();
        loadButton.GetComponent<Button>().interactable = true;
        loadButton.GetComponentInChildren<Text>().text = "Загрузить историю";
        Debug.Log("GameData Saved");
    }

    public void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            DataTransferScript.savedStoryID = data.savedStoryID;
            DataTransferScript.money = data.savedMoney;
            DataTransferScript.health = data.savedHealth;
            DataTransferScript.bullets = data.savedBullets;
            Debug.Log("GameLoaded");
            gameObject.GetComponent<DataBase>().GetStoryInID(DataTransferScript.savedStoryID);

        }
        else
        {
            Debug.LogError("There is no saved Data");
        }
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData.dat");
        }
        else
        {
            Debug.Log("No Data to delete");
        }
    }

}

[Serializable]
class SaveData
{
    public int savedStoryID;
    public int savedHealth;
    public int savedBullets;
    public int savedMoney;  
}

