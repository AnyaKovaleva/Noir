using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas loadMenu;
    public GameObject loadButton;

    private void Start()
    {
        
    }

    public void NewGame()
    {
        mainMenu.gameObject.SetActive(false);
        DataTransferScript.savedStoryID = 1;
        DataTransferScript.health = 100;
        DataTransferScript.money = 25;
        DataTransferScript.bullets = 3;
        gameObject.GetComponent<DataBase>().GetStoryInID(1);

        //mainMenu.enabled = false;
    }

    public void CreateLoadMenu()
    {
        XmlDocument saveDoc = new XmlDocument();
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            saveDoc.LoadXml(Resources.Load<TextAsset>("XML/Saves").text);
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            saveDoc.Load(Application.persistentDataPath + "/Resources/XML/Saves.xml");
        }
        //saveDoc.LoadXml(Resources.Load<TextAsset>("XML/Saves").text);
        XmlElement xRoot = saveDoc.DocumentElement;

        //Debug.Log(xRoot.ChildNodes.Count);

        foreach (XmlNode xSave in xRoot.ChildNodes)
        {
            GameObject button = Instantiate(loadButton, GameObject.Find("LoadGameDisplay/ButtonsLayout").transform);
            button.GetComponentInChildren<Text>().text = xSave.Attributes.GetNamedItem("name").Value;
        }
        
        
    }

    public void LoadGame()
    {
        mainMenu.gameObject.SetActive(false);
        loadMenu.gameObject.SetActive(true);
        CreateLoadMenu();
    }

}
