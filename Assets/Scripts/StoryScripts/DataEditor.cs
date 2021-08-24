using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class DataEditor : MonoBehaviour
{

    string storyText;
    string answersNum;
    string firstAns;
    string secondAns;
    string thirdAns;
    string firstAnsID;
    string secondAnsID;
    string thirdAnsID;
    string imageName;
    string backgroundName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStoryInID()
    {
        string loadedID = GameObject.Find("Canvas/Panel/MainLayout/FirstOption/StoryIDLayout/InputField").GetComponent<InputField>().text;

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Assets/Resources/XML/Data.xml");
        XmlElement xRoot = xDoc.DocumentElement;


        foreach (XmlNode xnode in xRoot)
        {
            if (xnode.Attributes.GetNamedItem("id").Value == loadedID)
            {
                XmlNode attr = xnode.Attributes.GetNamedItem("id");
                if (attr == null)
                {
                    Debug.Log("Этот ID не заполнен");
                }
                else
                {
                    foreach(XmlNode childnode in xnode.ChildNodes)
                    {
                        if(childnode.Name == "StoryText")
                        {                            
                            storyText = childnode.InnerText;
                        }
                        if (childnode.Name == "AnswersNumber")
                        {
                            answersNum = childnode.InnerText;
                        }
                        if (childnode.Name == "FirstButtonText")
                        {
                            firstAns = childnode.InnerText;
                        }
                        if (childnode.Name == "SecondButtonText")
                        {
                            secondAns = childnode.InnerText;
                        }
                        if (childnode.Name == "ThirdButtonText")
                        {
                            thirdAns = childnode.InnerText;
                        }
                        if (childnode.Name == "FirstButtonRefID")
                        {
                            firstAnsID = childnode.InnerText;
                        }
                        if (childnode.Name == "SecondButtonRefID")
                        {
                            secondAnsID = childnode.InnerText;
                        }
                        if (childnode.Name == "ThirdButtonRefID")
                        {
                            thirdAnsID = childnode.InnerText;
                        }
                        if (childnode.Name == "ImageRef")
                        {
                            imageName = childnode.InnerText;
                        }
                        if (childnode.Name == "BackgroundRef")
                        {
                            backgroundName = childnode.InnerText;
                        }
                    }
                    SetStoryText();
                }
                
            }
        } 
    }

    public void SetStoryText()
    {
        GameObject.Find("Canvas/Panel/MainLayout/InputStory").GetComponent<InputField>().text = storyText;
        GameObject.Find("Canvas/Panel/MainLayout/FirstOption/AnswersNumLayout/InputField").GetComponent<InputField>().text = answersNum;
        GameObject.Find("Canvas/Panel/MainLayout/FirstAnswerLayout/InputField").GetComponent<InputField>().text = firstAns;
        GameObject.Find("Canvas/Panel/MainLayout/SecondAnswerLayout/InputField").GetComponent<InputField>().text = secondAns;
        GameObject.Find("Canvas/Panel/MainLayout/ThirdAnswerLayout/InputField").GetComponent<InputField>().text = thirdAns;
        GameObject.Find("Canvas/Panel/MainLayout/FirstAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text = firstAnsID;
        GameObject.Find("Canvas/Panel/MainLayout/SecondAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text = secondAnsID;
        GameObject.Find("Canvas/Panel/MainLayout/ThirdAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text = thirdAnsID;
        GameObject.Find("Canvas/Panel/MainLayout/ImagesLayout/ImageName/InputField").GetComponent<InputField>().text = imageName;
        GameObject.Find("Canvas/Panel/MainLayout/ImagesLayout/BackgroundName/InputField").GetComponent<InputField>().text = backgroundName;
    }

    public void GetStoryText()
    {
        storyText = GameObject.Find("Canvas/Panel/MainLayout/InputStory").GetComponent<InputField>().text;
        answersNum = GameObject.Find("Canvas/Panel/MainLayout/FirstOption/AnswersNumLayout/InputField").GetComponent<InputField>().text;
        firstAns = GameObject.Find("Canvas/Panel/MainLayout/FirstAnswerLayout/InputField").GetComponent<InputField>().text;
        secondAns = GameObject.Find("Canvas/Panel/MainLayout/SecondAnswerLayout/InputField").GetComponent<InputField>().text;
        thirdAns = GameObject.Find("Canvas/Panel/MainLayout/ThirdAnswerLayout/InputField").GetComponent<InputField>().text;
        firstAnsID = GameObject.Find("Canvas/Panel/MainLayout/FirstAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text;
        secondAnsID = GameObject.Find("Canvas/Panel/MainLayout/SecondAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text;
        thirdAnsID = GameObject.Find("Canvas/Panel/MainLayout/ThirdAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text;
        imageName = GameObject.Find("Canvas/Panel/MainLayout/ImagesLayout/ImageName/InputField").GetComponent<InputField>().text;
        backgroundName = GameObject.Find("Canvas/Panel/MainLayout/ImagesLayout/BackgroundName/InputField").GetComponent<InputField>().text;

    }


    public void TrySaveStory()
    {
        string getID = GameObject.Find("Canvas/Panel/MainLayout/FirstOption/StoryIDLayout/InputField").GetComponent<InputField>().text;

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Assets/Resources/XML/Data.xml");
        XmlElement xRoot = xDoc.DocumentElement;

        bool didItMatch = false;

        foreach (XmlNode xnode in xRoot)
        {
            if (xnode.Attributes.GetNamedItem("id").Value == getID)
                didItMatch = true;
        }
        if (!didItMatch)
        {
            CreateNewStoryElement();
            ClearFields();
            TextMessage("Succesfully saved");
        }
        else
        {
            ConfirmSaving(getID);
        }

    }

    public void CreateNewStoryElement()
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Assets/Resources/XML/Data.xml");
        XmlElement xRoot = xDoc.DocumentElement;

        XmlNode storyNode = xDoc.CreateElement("Story");
        xRoot.AppendChild(storyNode);

        XmlAttribute idNode = xDoc.CreateAttribute("id");
        idNode.InnerText = GameObject.Find("Canvas/Panel/MainLayout/FirstOption/StoryIDLayout/InputField").GetComponent<InputField>().text;
        storyNode.Attributes.Append(idNode);

        XmlNode storyTextNode = xDoc.CreateElement("StoryText");
        storyTextNode.InnerText = GameObject.Find("Canvas/Panel/MainLayout/InputStory").GetComponent<InputField>().text;
        storyNode.AppendChild(storyTextNode);

        XmlNode answerNumNode = xDoc.CreateElement("AnswersNumber");
        answerNumNode.InnerText = GameObject.Find("Canvas/Panel/MainLayout/FirstOption/AnswersNumLayout/InputField").GetComponent<InputField>().text;
        storyNode.AppendChild(answerNumNode);

        XmlNode bcgrndRefNode = xDoc.CreateElement("BackgroundRef");
        bcgrndRefNode.InnerText = GameObject.Find("Canvas/Panel/MainLayout/ImagesLayout/BackgroundName/InputField").GetComponent<InputField>().text;
        storyNode.AppendChild(bcgrndRefNode);

        XmlNode ImgRefNode = xDoc.CreateElement("ImageRef");
        ImgRefNode.InnerText = GameObject.Find("Canvas/Panel/MainLayout/ImagesLayout/ImageName/InputField").GetComponent<InputField>().text;
        storyNode.AppendChild(ImgRefNode);

        XmlNode frstBtnTxt = xDoc.CreateElement("FirstButtonText");
        frstBtnTxt.InnerText = GameObject.Find("Canvas/Panel/MainLayout/FirstAnswerLayout/InputField").GetComponent<InputField>().text;
        storyNode.AppendChild(frstBtnTxt);

        XmlNode frstBtnID = xDoc.CreateElement("FirstButtonRefID");
        frstBtnID.InnerText = GameObject.Find("Canvas/Panel/MainLayout/FirstAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text;
        storyNode.AppendChild(frstBtnID);

        XmlNode scndBtnTxt = xDoc.CreateElement("SecondButtonText");
        scndBtnTxt.InnerText = GameObject.Find("Canvas/Panel/MainLayout/SecondAnswerLayout/InputField").GetComponent<InputField>().text;
        storyNode.AppendChild(scndBtnTxt);

        XmlNode scndBtnID = xDoc.CreateElement("SecondButtonRefID");
        scndBtnID.InnerText = GameObject.Find("Canvas/Panel/MainLayout/SecondAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text;
        storyNode.AppendChild(scndBtnID);

        XmlNode thrdBtnTxt = xDoc.CreateElement("ThirdButtonText");
        thrdBtnTxt.InnerText = GameObject.Find("Canvas/Panel/MainLayout/ThirdAnswerLayout/InputField").GetComponent<InputField>().text;
        storyNode.AppendChild(thrdBtnTxt);

        XmlNode thrdBtnID = xDoc.CreateElement("ThirdButtonRefID");
        thrdBtnID.InnerText = GameObject.Find("Canvas/Panel/MainLayout/ThirdAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text;
        storyNode.AppendChild(thrdBtnID);

        XmlNode frstBtnEvent = xDoc.CreateElement("FirstButtonEvent");
        storyNode.AppendChild(frstBtnEvent);

        XmlNode scndBtnEvent = xDoc.CreateElement("SecondButtonEvent");
        storyNode.AppendChild(scndBtnEvent);

        XmlNode thrdBtnEvent = xDoc.CreateElement("ThirdButtonEvent");
        storyNode.AppendChild(thrdBtnEvent);

        xDoc.Save("Assets/Resources/XML/Data.xml");
    }

    public void ReplaceStoryOnID(string id)
    {
        GetStoryText();
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Assets/Resources/XML/Data.xml");
        XmlElement xRoot = xDoc.DocumentElement;

        foreach (XmlNode xnode in xRoot)
        {
            if (xnode.Attributes.GetNamedItem("id").Value == id)
            {
                XmlNode attr = xnode.Attributes.GetNamedItem("id");
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "StoryText")
                    {
                        childnode.InnerText = storyText;
                    }
                    if (childnode.Name == "AnswersNumber")
                    {
                        childnode.InnerText = answersNum;
                    }
                    if (childnode.Name == "FirstButtonText")
                    {
                        childnode.InnerText = firstAns;
                    }
                    if (childnode.Name == "SecondButtonText")
                    {
                        childnode.InnerText = secondAns;
                    }
                    if (childnode.Name == "ThirdButtonText")
                    {
                        childnode.InnerText = thirdAns;
                    }
                    if (childnode.Name == "FirstButtonRefID")
                    {
                        childnode.InnerText = firstAnsID;
                    }
                    if (childnode.Name == "SecondButtonRefID")
                    {
                        childnode.InnerText = secondAnsID;
                    }
                    if (childnode.Name == "ThirdButtonRefID")
                    {
                        childnode.InnerText = thirdAnsID;
                    }
                    if (childnode.Name == "ImageRef")
                    {
                        childnode.InnerText = imageName;
                    }
                    if (childnode.Name == "BackgroundRef")
                    {
                        childnode.InnerText = backgroundName;
                    }
                }
            }
        }
        xDoc.Save("Assets/Resources/XML/Data.xml");

    }

    public void ConfirmSaving(string id)
    {
        ReplaceStoryOnID(id);
        TextMessage("Succesfully saved");
        Debug.Log("This ID already exist");
    }

    public void FindFreeID()
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Assets/Resources/XML/Data.xml");
        XmlElement xRoot = xDoc.DocumentElement;

        bool match = false;
        int x;
        for (x = 1; x<1000; x++)
        {
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes.GetNamedItem("id").Value == x.ToString())
                {
                    match = true;                    
                    break;
                }
                else match = false;
            }
            if (match == false)
            {
                GameObject.Find("Canvas/Panel/MainLayout/FirstOption/StoryIDLayout/InputField").GetComponent<InputField>().text = x.ToString();
                break;
            }
        }

    }

    public void ClearFields()
    {
        GameObject.Find("Canvas/Panel/MainLayout/InputStory").GetComponent<InputField>().text = null;
        GameObject.Find("Canvas/Panel/MainLayout/FirstOption/AnswersNumLayout/InputField").GetComponent<InputField>().text = null;
        GameObject.Find("Canvas/Panel/MainLayout/FirstAnswerLayout/InputField").GetComponent<InputField>().text = null;
        GameObject.Find("Canvas/Panel/MainLayout/SecondAnswerLayout/InputField").GetComponent<InputField>().text = null;
        GameObject.Find("Canvas/Panel/MainLayout/ThirdAnswerLayout/InputField").GetComponent<InputField>().text = null;
        GameObject.Find("Canvas/Panel/MainLayout/FirstAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text = null;
        GameObject.Find("Canvas/Panel/MainLayout/SecondAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text = null;
        GameObject.Find("Canvas/Panel/MainLayout/ThirdAnswerLayout/IDLayout/InputField").GetComponent<InputField>().text = null;
        GameObject.Find("Canvas/Panel/MainLayout/ImagesLayout/ImageName/InputField").GetComponent<InputField>().text = null;
        GameObject.Find("Canvas/Panel/MainLayout/ImagesLayout/BackgroundName/InputField").GetComponent<InputField>().text = null;
    }

    public void TextMessage(string text)
    {
        GameObject.Find("Canvas/Panel/MainLayout/MessageLayout/MessageText").GetComponent<Text>().text = text;
    }

    public void DeleteStory()
    {
        string id = GameObject.Find("Canvas/Panel/MainLayout/FirstOption/StoryIDLayout/InputField").GetComponent<InputField>().text;
        GetStoryText();
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Assets/Resources/XML/Data.xml");
        XmlElement xRoot = xDoc.DocumentElement;

        foreach (XmlNode xnode in xRoot)
        {
            if (xnode.Attributes.GetNamedItem("id").Value == id.ToString())
            {
                xRoot.RemoveChild(xnode);
            }
            
        }
        xDoc.Save("Assets/Resources/XML/Data.xml");
        ClearFields();
    }

}
