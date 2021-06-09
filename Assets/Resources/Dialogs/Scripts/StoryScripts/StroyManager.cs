using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System;
using System.Text;

public class StroyManager : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField]
    private Canvas storyCanvas;

    [SerializeField]
    private Image backGround;

    [SerializeField]
    private Text AuthorStoryText;

    [SerializeField]
    private GameObject ButtonLayout;

    [SerializeField]
    private GameObject DialogueLayout;

    [SerializeField]
    private List<GameObject> Anchors;
    #endregion

    private XmlDocument xDoc;
    private XmlElement xRoot;
    private List<GameObject> buttonList = new List<GameObject>();
    private List<GameObject> personList = new List<GameObject>();
    private List<GameObject> cloudList = new List<GameObject>();
    private int buttonsNumber;
    private string[] dialogueArray;

    private Stories stories;


    private void Start()
    {
        TextAsset jasonText = Resources.Load<TextAsset>("JSON/Data");     
        string s = jasonText.text;
        Debug.Log(s);
        stories = JsonUtility.FromJson<Stories>(s);

       // LoadXml();
        ShowStoryPage(1);
    }

    #region Parse XML
    //Парсит XML и открывает историю по ID
    private void ShowStoryPage(int id)
    {
        ClearPage();

        foreach (StoryStruct story in stories.stories)
        {
            if(story.storyID == id)
            {
                SetAuthorText(story.authorText);
                SetBackground(story.background);
                buttonsNumber = story.buttonNumber;
                AddButton(ParseButton(story.firstButtonText));
                AddButtonEvent(story.firstButtonRef, 0);
                if(buttonsNumber > 1)
                {
                    AddButton(ParseButton(story.secondButtonText));
                    AddButtonEvent(story.secondButtonRef, 1);
                }
                if(buttonsNumber > 2)
                {
                    AddButton(ParseButton(story.thirdButtonText));
                    AddButtonEvent(story.thirdButtonRef, 2);
                }
                
                if(story.dialogue.person != "")
                {
                    Debug.Log("person = " + story.dialogue.person);
                    SetPersons(story.dialogue.person);
                }
                if(story.dialogue.dialogueText != "")
                {
                    Debug.Log("dialogueText = " + story.dialogue.dialogueText);
                    ParseDialogue(story.dialogue.dialogueText);
                }
                if(story.dialogue.cloud != "")
                {
                    SetClouds(story.dialogue.cloud);
                }
                break;
            }
        }

        //foreach (XmlNode xNode in xRoot)
        //{
        //    if (xNode.Attributes.GetNamedItem("id").Value == id.ToString())
        //    {
        //        foreach(XmlNode childNode in xNode.ChildNodes)
        //        {
        //            switch (childNode.Name)
        //            {                        
        //                case "AuthorText":
        //                    SetAuthorText(childNode.InnerText);
        //                    break;
        //                case "Background":
        //                    SetBackground(childNode.InnerText);
        //                    break;
        //                case "ButtonsNumber":
        //                    buttonsNumber = int.Parse(childNode.InnerText);
        //                    break;
        //                case "FirstButtonText":
        //                    AddButton(ParseButton(childNode.InnerText));
        //                    break;
        //                case "FirstButtonRef":
        //                    AddButtonEvent(childNode.InnerText, 0);
        //                    break;
        //                case "SecondButtonText":
        //                    if(buttonsNumber>1)
        //                    {
        //                        AddButton(ParseButton(childNode.InnerText));                                
        //                    }
        //                    break;
        //                case "SecondButtonRef":
        //                    if (buttonsNumber > 1)
        //                    {
        //                        AddButtonEvent(childNode.InnerText, 1);                                
        //                    }
        //                    break;                            
        //                case "ThirdButtonText":
        //                    if (buttonsNumber > 2)
        //                    {
        //                        AddButton(ParseButton(childNode.InnerText));                                                                
        //                    }
        //                    break;
        //                case "ThirdButtonRef":
        //                    if (buttonsNumber > 2)
        //                    {
        //                        AddButtonEvent(childNode.InnerText, 2);                                
        //                    }
        //                    break;
        //                case "Dialogue":
        //                    foreach(XmlNode subChild in childNode.ChildNodes)
        //                    {
        //                        switch (subChild.Name)
        //                        {
        //                            case "Persons":
        //                                if(subChild.InnerText != "")
        //                                {
        //                                    SetPersons(subChild.InnerText);
        //                                }                                        
        //                                break;
        //                            case "Dialogues":
        //                                if(subChild.InnerText != "")
        //                                {
        //                                    ParseDialogue(subChild.InnerText);
        //                                }
        //                                break;
        //                            case "Clouds":
        //                                if (subChild.InnerText != "")
        //                                {
        //                                    SetClouds(subChild.InnerText);
        //                                }
        //                                break;
        //                        }
        //                    }
        //                    break;
        //            }
        //        }
        //    }
        //}
    }

    #endregion

    //Загружает XML
    private void LoadXml()
    {
        xDoc = new XmlDocument();
        xDoc.LoadXml(Resources.Load<TextAsset>("XML/Data").text);
        xRoot = xDoc.DocumentElement;
    }

    //Изменяет Бэкграунд
    private void SetBackground(string _backgroundName)
    {
        backGround.sprite = Resources.Load<Sprite>("Sprites/StorySprites/Background/" + _backgroundName);
    }

    //Добавляет текст "от Автора" на канвас
    private void SetAuthorText(string _text)
    {
        AuthorStoryText.text = _text.Replace("\\n","\n");
    }

    //Добавляет кнопки в лэйаут
    private void AddButton(GameObject _button)
    {
        GameObject createdButton;
        createdButton = Instantiate(_button, ButtonLayout.transform);
        buttonList.Add(createdButton);
    }

    private void ParseDialogue(string _dialogue)
    {
        dialogueArray = _dialogue.Split('#');
    }

    //Расшифровка диалога
    private void SetClouds(string _bubble)
    {
        int _num = 0;
        string[] _clouds = _bubble.Split('$');
        foreach(string cloud in _clouds)
        {
            string[] cloudOptions = cloud.Split('#');
            GameObject cloudObject = Resources.Load<GameObject>("Dialogs/Prefabs/DialogueClouds/" + cloudOptions[0] + "Cloud" + cloudOptions[1]);
            
            GameObject instCloud = Instantiate(cloudObject, DialogueLayout.transform);
            
            switch (cloudOptions[2])
            {
                case "Left":
                    DialogueLayout.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleLeft;
                    break;
                case "Right":
                    DialogueLayout.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleRight;
                    break;
            }
            switch (cloudOptions[3])
            {
                case "Left":
                    instCloud.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleLeft;
                    break;
                case "Right":
                    instCloud.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleRight;
                    break;
            }
            instCloud.GetComponentInChildren<Text>().text = dialogueArray[_num].Replace("\\n", "\n");
            cloudList.Add(instCloud);

            _num += 1;
        }
    }

    private void SetPersons(string _persons)
    {
        int _num = 0;
        string[] _characters = _persons.Split('$');
        foreach (string person in _characters)
        {
            string[] personOptions = person.Split('#');
            GameObject character;
            character = Resources.Load<GameObject>("Dialogs/Prefabs/Person");
            GameObject dispChar = Instantiate(character, Anchors[_num].transform);
            dispChar.GetComponent<Image>().sprite = Resources.Load<Sprite>("Dialogs/Images/Persons/" + personOptions[0]);
            dispChar.transform.localPosition = new Vector2(0, float.Parse(personOptions[1]));                       
            personList.Add(dispChar);
            _num += 1;
        }
    }


    #region buttonTypes parser
    //Расшифровывает название клавиши из XML и возвращаяет префаб кнопки 
    private GameObject ParseButton(string _buttonType)
    {
        GameObject buttonPrefab;
        string[] _types = _buttonType.Split('#');
        //Debug.Log("Dialogs/Prefabs/" + _types[1] + "Button" + _types[2]);
        buttonPrefab = Resources.Load<GameObject>("Dialogs/Prefabs/Buttons/" + _types[1] + "Button" + _types[2]);
        buttonPrefab.GetComponentInChildren<Text>().text = _types[0].Replace("\\n","\n");
        return buttonPrefab;
    }

    //Добавляет Листенер к кнопке
    private void AddButtonEvent(string _buttonEvent, int buttonID)
    {
        GameObject _button = buttonList[buttonID];
        string[] events = _buttonEvent.Split(' ');

        _button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
        _button.GetComponent<Button>().onClick.AddListener(() =>
        {
            switch (events[0])
            {
                case "Page":
                    ShowStoryPage(int.Parse(events[1]));
                    break;
                case "Intuition":                    
                    RunGame("Intuition", int.Parse(events[1]));
                    break;
            }            
        });
    }
    #endregion

    private void RunGame(string gameName, int gameID)
    {
        Debug.Log("Run Game");
    }

    #region Clear Page
    //Удаляет кнопки и облачка диалогов
    private void ClearPage()
    {
        KillButtons();
        KillPersons();
        KillClouds();
    }
    private void KillButtons()
    {
        foreach (GameObject button in buttonList)
        {
            Destroy(button);            
        }
        buttonList.Clear();
    }    

    private void KillPersons()
    {
        foreach(GameObject _person in personList)
        {
            Destroy(_person);
        }
        personList.Clear();
    }

    private void KillClouds()
    {
        foreach (GameObject _cloud in cloudList)
        {
            Destroy(_cloud);
        }
        cloudList.Clear();
        
    }
    #endregion

}
