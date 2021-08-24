using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadStory : MonoBehaviour
{
    public Text testText;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        XmlDocument saveDoc = new XmlDocument();
        XmlElement xRoot = saveDoc.CreateElement("Saves");
        saveDoc.AppendChild(xRoot);

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (!File.Exists(Application.dataPath + "/Resources/XML/Saves.xml"))
            {
                FileStream file = File.Create(Application.dataPath + "/Resources/XML/Saves.xml");
                saveDoc.Save(Application.dataPath + "/Resources/XML/Saves.xml"); 
            }

        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (!File.Exists(Application.persistentDataPath + "/Resources/XML/Saves.xml"))
            {
                FileStream file = File.Create(Application.persistentDataPath + "/Resources/XML/Saves.xml");
                saveDoc.Save(Application.persistentDataPath + "/Resources/XML/Saves.xml");
                testText.GetComponent<Text>().text = "поидее создан"; 
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Сохранено " + GetDate());
            i++;
            SaveGame();
        }
    }

    public void TestSave()
    {
        
    }

    public void SaveGame()
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

        XmlNode saveNode = saveDoc.CreateElement("Save");
        xRoot.AppendChild(saveNode);

        XmlAttribute saveName = saveDoc.CreateAttribute("name");
        saveName.InnerText =  "Сохранение  " + GetDate();
        saveNode.Attributes.Append(saveName);

        XmlNode storyID = saveDoc.CreateElement("StoryID");
        storyID.InnerText = DataTransferScript.savedStoryID.ToString();
        saveNode.AppendChild(storyID);

        XmlNode attributesNode = saveDoc.CreateElement("Attributes");
        saveNode.AppendChild(attributesNode);

        XmlNode hlthAttrib = saveDoc.CreateElement("Health");
        hlthAttrib.InnerText = DataTransferScript.health.ToString();
        attributesNode.AppendChild(hlthAttrib);

        XmlNode bultsAttrib = saveDoc.CreateElement("Bullets");
        bultsAttrib.InnerText = DataTransferScript.bullets.ToString();
        attributesNode.AppendChild(bultsAttrib);

        XmlNode moneyAttrib = saveDoc.CreateElement("Money");
        moneyAttrib.InnerText = DataTransferScript.money.ToString();
        attributesNode.AppendChild(moneyAttrib);

        XmlNode inventoryNode = saveDoc.CreateElement("Inventory");
        /*string inventory = null;
        for (int i = 0; i < DataTransferScript.inventory.Length; i++)
        {
            if (DataTransferScript.inventory.Length > 0)
            {
                if (i == DataTransferScript.inventory.Length - 1)
                {
                    inventory += DataTransferScript.inventory[i];
                }
                else
                {
                    inventory += (DataTransferScript.inventory[i] + ".");
                }
            }
            
        }
        inventoryNode.InnerText = inventory;*/
        saveNode.AppendChild(inventoryNode);

        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            saveDoc.Save(Application.dataPath + "/Resources/XML/Saves.xml");
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            saveDoc.Save(Application.persistentDataPath + "/Resources/XML/Saves.xml");
        }

        //Debug.Log(Application.persistentDataPath + "/XML/Saves.xml");
    }

    public void LoadGame(string saveName)
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
            
        XmlElement xRoot = saveDoc.DocumentElement;

        int storyID = 1;

        foreach(XmlNode xNode in xRoot)
        {
            if (xNode.Attributes.GetNamedItem("name").Value == saveName)
            {
                foreach(XmlNode childnode in xNode.ChildNodes)
                {
                    if(childnode.Name == "StoryID")
                    {
                        DataTransferScript.savedStoryID = int.Parse(childnode.InnerText);
                        storyID = int.Parse(childnode.InnerText);
                    }
                    if(childnode.Name == "Attributes")
                    {
                        foreach(XmlNode subchild in childnode.ChildNodes)
                        {
                            if(subchild.Name == "Health")
                            {
                                DataTransferScript.health = int.Parse(subchild.InnerText);
                            }
                            if (subchild.Name == "Money")
                            {
                                DataTransferScript.money = int.Parse(subchild.InnerText);
                            }
                            if (subchild.Name == "Bullets")
                            {
                                DataTransferScript.bullets = int.Parse(subchild.InnerText);
                            }
                        }
                    }
                    /*if(childnode.Name == "Inventory")
                    {

                    }*/
                }
            }
        }

        gameObject.GetComponent<DataBase>().GetStoryInID(storyID);
        gameObject.GetComponent<MainMenu>().loadMenu.gameObject.SetActive(false);

    }

    public string GetDate()
    {
        string date = System.DateTime.Now.ToString();
        return (date);
    }

}
