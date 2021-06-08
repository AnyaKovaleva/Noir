using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public struct Dialogue
{
    public string person;
    public string dialogueText;
    public string cloud;

}

[Serializable]
public struct StoryStruct
{
    public int storyID;
    public string authorText;
    public Dialogue dialogue;
    public int buttonNumber;
    public string background;
    public string firstButtonText;
    public string firstButtonRef;
    public string secondButtonText;
    public string secondButtonRef;
    public string thirdButtonText;
    public string thirdButtonRef;
}

[Serializable]
public class Stories
{
    public StoryStruct[] stories;
}

public class DataBase : MonoBehaviour
{
    public Text storyText; // canvas content
    public Button button1;
    public Button button2;
    public Button button3;
    public GameObject image; //canvas/Image


    public GameObject Transfer;
    public GameObject status;
    private int firstButtonRefID;
    private int secondButtonRefID;
    private int thirdButtonRefID;

    public int mstoryID;

    public string FBgameIndex;
    public string SBgameIndex;
    public string TBgameIndex;

    private string FBevent = "";
    private string SBevent = "";
    private string TBevent = "";

   

    private Stories stories;

    void Start()
    {
        TextAsset jasonText = Resources.Load<TextAsset>("JSON/Data");

        string s = jasonText.text;
        Debug.Log(s);

        stories = JsonUtility.FromJson<Stories>(s);

        //Instantiate(Transfer);
        mstoryID = DataTransferScript.savedStoryID;

        mstoryID = 1; 


        GetStoryInID(mstoryID);
    }

    //Отображает историю по индексу (стартовая история и переходы)
    public void GetStoryInID(int storyID)
    {
        DataTransferScript.savedStoryID = storyID;
        //XmlDocument xDoc = new XmlDocument();
        //xDoc.LoadXml(Resources.Load<TextAsset>("XML/Data").text);
        //XmlElement xRoot = xDoc.DocumentElement;

        foreach (StoryStruct story in stories.stories)
        {
            if(story.storyID == storyID)
            {
                SetFirstButtonText(story.firstButtonText);
                SetSecondButtonText(story.secondButtonText);
                SetThirdButtonText(story.thirdButtonText);
                break;
            }
        }

        //foreach (XmlNode xnode in xRoot)
        //{
        //    if (xnode.Attributes.GetNamedItem("id").Value == storyID.ToString())
        //    {
        //        foreach (XmlNode childnode in xnode.ChildNodes)
        //        {
        //            Debug.Log(childnode.InnerText);
        //            if (childnode.Name == "StoryText")
        //            {
        //                Debug.Log("got past if");

        //                SetStoryText(childnode.InnerText);
        //            }
        //            if (childnode.Name == "FirstButtonText")
        //            {
        //                SetFirstButtonText(childnode.InnerText);
        //            }
        //            if (childnode.Name == "SecondButtonText")
        //            {
        //                SetSecondButtonText(childnode.InnerText);
        //            }
        //            if (childnode.Name == "ThirdButtonText")
        //            {
        //                SetThirdButtonText(childnode.InnerText);
        //            }
        //            if (childnode.Name == "FirstButtonRefID" && childnode.InnerText != "")
        //            {
        //                if (childnode.InnerText.Contains("game"))
        //                {
        //                    FBgameIndex = childnode.InnerText;
        //                }
        //                else
        //                {
        //                    firstButtonRefID = int.Parse(childnode.InnerText);
        //                }
        //            }
        //            if (childnode.Name == "SecondButtonRefID" && childnode.InnerText != "")
        //            {
        //                if (childnode.InnerText.Contains("game"))
        //                {
        //                    SBgameIndex = childnode.InnerText;
        //                }
        //                else
        //                {
        //                    secondButtonRefID = int.Parse(childnode.InnerText);
        //                }

        //            }
        //            if (childnode.Name == "ThirdButtonRefID" && childnode.InnerText != "")
        //            {
        //                if (childnode.InnerText.Contains("game"))
        //                {
        //                    TBgameIndex = childnode.InnerText;
        //                }
        //                else
        //                {
        //                    thirdButtonRefID = int.Parse(childnode.InnerText);
        //                }
        //            }
        //            if (childnode.Name == "AnswersNumber")
        //            {
        //                SetAnswerNumber(int.Parse(childnode.InnerText));
        //            }
        //            if (childnode.Name == "ImageRef")
        //            {
        //                SetImage(childnode.InnerText);
        //            }
        //            if (childnode.Name == "FirstButtonEvent" && childnode.InnerText != "")
        //            {
        //                FBevent = childnode.InnerText;
        //            }
        //            if (childnode.Name == "SecondButtonEvent" && childnode.InnerText != "")
        //            {
        //                SBevent = childnode.InnerText;
        //            }
        //            if (childnode.Name == "ThirdButtonEvent" && childnode.InnerText != "")
        //            {
        //                TBevent = childnode.InnerText;
        //            }
        //        }
        //    }
        //}
    }

    //Отображение текста истории
    public void SetStoryText(string settext)
    {
        Debug.Log("Settext = " + settext);
        storyText.text = settext;
        //GameObject.Find("Canvas/ScrollView/Viewport/Content").GetComponent<Text>().text = settext;
    }

    //Текст и эвенты кнопок
    public void SetFirstButtonText(string text)
    {
        button1.GetComponentInChildren<Text>().text = text;
        //GameObject.Find("Canvas/Layout1/Button1/Text").GetComponent<Text>().text = text;
    }

    public void FirstButtonPressed()
    {
        if (FBgameIndex.Length > 1)
        {
            LoadGame(FBgameIndex);
        }
        else
        {
            if (FBevent != "")
            {
                ParseEvent(FBevent);
                FBevent = "";
            }
            GetStoryInID(firstButtonRefID);
        }
    }

    public void SetSecondButtonText(string text)
    {
        button2.GetComponentInChildren<Text>().text = text;
        //GameObject.Find("Canvas/Layout1/Button2/Text").GetComponent<Text>().text = text;
    }

    public void SecondButtonPressed()
    {
        if (SBgameIndex.Length > 1)
        {
            LoadGame(SBgameIndex);
        }
        else
        {
            if (SBevent != "")
            {
                ParseEvent(SBevent);
                SBevent = "";
            }
            GetStoryInID(secondButtonRefID);
        }
    }

    public void SetThirdButtonText(string text)
    {
        button3.GetComponentInChildren<Text>().text = text;
       // GameObject.Find("Canvas/Layout1/Button3/Text").GetComponent<Text>().text = text;
    }

    public void ThirdButtonPressed()
    {
        if (TBgameIndex.Length > 1)
        {
            LoadGame(TBgameIndex);
        }
        else
        {
            if (TBevent != "")
            {
                ParseEvent(TBevent);
                TBevent = "";
            }
            GetStoryInID(thirdButtonRefID);
        }

    }

    public void SetAnswerNumber(int num)
    {
        if (num == 1)
        {
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
            // GameObject.Find("Canvas/Layout1/Button2").SetActive(false);
            // GameObject.Find("Canvas/Layout1/Button3").SetActive(false);
        }
        else if (num == 2)
        {
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(false);

            //GameObject.Find("Canvas/Layout1/Button2").SetActive(true);
            //GameObject.Find("Canvas/Layout1/Button3").SetActive(false);
        }
        else if (num == 3)
        {
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);

            // GameObject.Find("Canvas/Layout1/Button2").SetActive(true);
            //GameObject.Find("Canvas/Layout1/Button3").SetActive(true);
        }
    }

    public void SetImage(string refer)
    {
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/" + refer);
        //GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/" + refer);
    }

    //Загрузить миниигру
    public void LoadGame(string id)
    {
        DataTransferScript.savedStoryID = mstoryID;
        int gameId = int.Parse(id.Trim(new char[] { 'g', 'a', 'm', 'e' }));

        DataTransferScript.gameID = gameId;

        XmlDocument aDoc = new XmlDocument();
        aDoc.LoadXml(Resources.Load<TextAsset>("XML/MiniGameMeta").text);
        XmlElement xRoot = aDoc.DocumentElement;

        foreach (XmlNode xnode in xRoot)
        {
            if (xnode.Attributes.GetNamedItem("id").Value == gameId.ToString())
            {
                Debug.Log("level " + xnode.Attributes.GetNamedItem("id").Value);
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "LevelName" && childnode.InnerText != "")
                    {
                        SceneManager.LoadScene(childnode.InnerText);
                    }
                }
            }
        }
    }

    //Расшифровка контрольной команды из XML
    public void ParseEvent(string evnt)
    {
        string[] events = evnt.Split(' ');

        for (int i = 0; i < events.Length; i++)
        {
            if (i % 2 == 0)
            {
                SendMessage(events[i], int.Parse(events[i + 1]));
            }
        }
        
    }


    //Использование ресурсов
    public void UseMoney(int count)
    {
        DataTransferScript.money += count;
        status.GetComponent<StatusController>().RefreshStatus();
    }

    public void UsePistol(int count)
    {
        DataTransferScript.bullets += count;
        status.GetComponent<StatusController>().RefreshStatus();
        Debug.Log("Bang");
    }

    public void HealthUpdate(int count)
    {
        int health = DataTransferScript.health;
        string _status = DataTransferScript.status;
        health = Mathf.Clamp(health + count, 0, 100);
        if (health == 100)
        {
            _status = "Отлично";
        }
        else if (health < 100 && health >= 75)
        {
            _status = "Легкое ранение";
        }
        else if (health < 75 && health >= 50)
        {
            _status = "Ранения средней тяжести";
        }
        else if (health < 50 && health >= 25)
        {
            _status = "Нужно бы найти аптечку";
        }
        else
        {
            _status = "Ходячий труп";
        }
        status.GetComponent<StatusController>().RefreshStatus();
    }

    public void CloseStoryMode(int zero)
    {
        gameObject.GetComponent<MainGame>().ReturnToMainMenu();
    }
}

