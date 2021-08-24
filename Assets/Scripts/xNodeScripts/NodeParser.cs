using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;

public class NodeParser : MonoBehaviour
{
    public StoryGraph graph;

    [SerializeField]
    private Image background;
    [SerializeField]
    private Text authorText;
    [SerializeField]
    private GameObject buttonLayout;
    [SerializeField]
    private GameObject dialogueLayout;
    [SerializeField]
    private GameObject anchorRight;
    [SerializeField]
    private GameObject anchorLeft;


    private string buttonPrefabsPath = "Dialogs/Prefabs/Buttons/";
    private string personPrefabPath = "Dialogs/Prefabs/Person";
    private string cloudsPath = "Dialogs/Prefabs/DialogueClouds/";

    private List<GameObject> buttons = new List<GameObject>();
    private List<GameObject> clouds = new List<GameObject>();
    private List<GameObject> people = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        graph.SetCurrentNodeToStart();
        graph.StartStory();
        ProcessCurrentNode();
    }
 
    private void ProcessCurrentNode()
    {
        BaseNode current = graph.GetCurrentNode();
        switch (current.GetNodeType())
        {
            case NodeType.DIALOGUE_NODE: ProcessDialogueNode(current as DialogueNode);
                break;
            case NodeType.AUTHOR_NODE: ProcessAuthorNode(current as AuthorNode);
                break;
            case NodeType.MINIGAME_NODE: ProcessMinigameNode();
                break;
        }
    }

    private void ProcessAuthorNode(AuthorNode node)
    {
        SetBackground(node.background);
        SetAuthorText(node.authorText);
        AddButtons();
    }

    private void ProcessDialogueNode(DialogueNode node)
    {
        SetBackground(node.background);
        SetPeople(graph.GetCurrentNode() as DialogueNode);
        SetDialogue(graph.GetCurrentNode() as DialogueNode);
        AddButtons();
    }

    private void ProcessMinigameNode()
    {

    }

    private void SetBackground(Sprite backgroundSprite)
    {
        background.sprite = backgroundSprite;
    }

    private void SetAuthorText(string text)
    {
        authorText.text = text;
    }

    private void SetDialogue(DialogueNode node)
    {
        bool hasTwoPeople = node.addLeftPerson;
        GameObject cloudRightPerson = Resources.Load<GameObject>(cloudsPath + CalculateCloudSiazeAndType(hasTwoPeople, node.rightPersonText));
        GameObject instantiatedCloudRight = Instantiate(cloudRightPerson, dialogueLayout.transform);
        instantiatedCloudRight.GetComponentInChildren<Text>().text = node.rightPersonText;
        clouds.Add(instantiatedCloudRight);

        RawImage person = instantiatedCloudRight.GetComponentInChildren<RawImage>();

        if (person != null)
        {
            //Vector2 newPos = personAnchor.transform.position;
            people[0].SetActive(false);
            instantiatedCloudRight.GetComponentInChildren<RawImage>().texture = node.rightPersonSprite.texture;
        }
        else
        {
            people[0].transform.position = new Vector2(people[0].transform.position.x, instantiatedCloudRight.transform.position.y + instantiatedCloudRight.GetComponent<RectTransform>().rect.height);
        }
           


        if(node.addAdditionalRightPersonText)
        {
            cloudRightPerson = Resources.Load<GameObject>(cloudsPath + CalculateCloudSiazeAndType(hasTwoPeople, node.additionalRightPersonText));
            instantiatedCloudRight = Instantiate(cloudRightPerson, dialogueLayout.transform);
            instantiatedCloudRight.GetComponentInChildren<Text>().text = node.additionalRightPersonText;
            clouds.Add(instantiatedCloudRight);
        }

        if(hasTwoPeople)
        {
            GameObject cloudLeftPerson = Resources.Load<GameObject>(cloudsPath + CalculateCloudSiazeAndType(hasTwoPeople, node.leftPersonText));
            GameObject instantiatedCloudLeft = Instantiate(cloudLeftPerson, dialogueLayout.transform);
            instantiatedCloudLeft.GetComponentInChildren<Text>().text = node.leftPersonText;
            clouds.Add(instantiatedCloudLeft);

            person = instantiatedCloudLeft.GetComponentInChildren<RawImage>();

            if(person!= null)
            {
                //Vector2 newPos = personAnchor.transform.position;
                people[1].SetActive(false);
                instantiatedCloudLeft.GetComponentInChildren<RawImage>().texture = node.leftPersonSprite.texture;
            }
            else
            {
                Debug.Log("Pos is null");
                people[1].transform.position = new Vector2(people[1].transform.position.x, instantiatedCloudLeft.transform.position.y + instantiatedCloudLeft.GetComponent<RectTransform>().rect.height);
            }

           

            if (node.addAdditionalLeftPersonText)
            {
                cloudLeftPerson = Resources.Load<GameObject>(cloudsPath + CalculateCloudSiazeAndType(hasTwoPeople, node.additionalLeftPersonText));
                instantiatedCloudLeft = Instantiate(cloudLeftPerson, dialogueLayout.transform);
                instantiatedCloudLeft.GetComponentInChildren<Text>().text = node.additionalLeftPersonText;            
                clouds.Add(instantiatedCloudLeft);
            }
        }

    }

    private string CalculateCloudSiazeAndType(bool hasTwoPeople, string cloudText)
    {
        string type = "Mind";
        string cloud = "Cloud";
        int size = 1;

        if(hasTwoPeople)
        {
            type = "Dialogue";
        }

        Debug.Log("cloud text length = " + cloudText.Length);

        size = cloudText.Length / 20;

        Debug.Log("Size = " + size);

        if (size == 1)
        {
            size++;
        }

        if (size == 0)
        {
            size = 1;
        }        

        return type + cloud + size.ToString();
    }


    private void SetPeople(DialogueNode node)
    {
        SetPersonRight(node);
        if(node.addLeftPerson)
        {
            SetPersonLeft(node);
        }

    }

    private void SetPersonRight(DialogueNode node)
    {
        GameObject person = Resources.Load<GameObject>(personPrefabPath);
        person.GetComponent<Image>().sprite = node.rightPersonSprite;
        GameObject instantiatedPerson = Instantiate(person, anchorRight.transform);
        people.Add(instantiatedPerson);
    }

    private void SetPersonLeft(DialogueNode node)
    {
        GameObject person = Resources.Load<GameObject>(personPrefabPath);
        person.GetComponent<Image>().sprite = node.leftPersonSprite;
        GameObject instantiatedPerson = Instantiate(person, anchorLeft.transform);
        people.Add(instantiatedPerson);
    }

    private void AddButtons()
    {
        BaseNode node = graph.GetCurrentNode();
        Debug.Log("add buttons node " + node.GetNodeType());
        if(graph.IsConnectedOption_1())
        {
            Debug.Log("adding 1st button");
            GameObject button = Resources.Load<GameObject>(buttonPrefabsPath + ChooseButtonColour(node.GetNodeType()) + "Button" + CalculateButtonSize(node.GetButton_1_Text()));
            if(button == null)
            {
                Debug.Log("button is null");
            }
            button.GetComponentInChildren<Text>().text = node.GetButton_1_Text();

            GameObject instantiatedButton = Instantiate(button, buttonLayout.transform);
            instantiatedButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            instantiatedButton.GetComponent<Button>().onClick.AddListener(delegate { OnButtonOnePress(); });
            buttons.Add(instantiatedButton);
            Debug.Log("button list count = " + buttons.Count.ToString());
        }
        if(graph.IsConnectedOption_2())
        {
            Debug.Log("adding 2nd button");

            GameObject button = Resources.Load<GameObject>(buttonPrefabsPath + ChooseButtonColour(node.GetNodeType()) + "Button" + CalculateButtonSize(node.GetButton_2_Text()));
            button.GetComponentInChildren<Text>().text = node.GetButton_2_Text();

            GameObject instantiatedButton = Instantiate(button, buttonLayout.transform);
            instantiatedButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            instantiatedButton.GetComponent<Button>().onClick.AddListener(delegate { OnButtonTwoPress(); });
            buttons.Add(instantiatedButton);

        }

        if (graph.IsConnectedOption_3())
        {
            GameObject button = Resources.Load<GameObject>(buttonPrefabsPath + ChooseButtonColour(node.GetNodeType()) + "Button" + CalculateButtonSize(node.GetButton_3_Text()));
            button.GetComponentInChildren<Text>().text = node.GetButton_3_Text();

            GameObject instantiatedButton = Instantiate(button, buttonLayout.transform);
            instantiatedButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            instantiatedButton.GetComponent<Button>().onClick.AddListener(delegate { OnButtonThreePress(); });
            buttons.Add(instantiatedButton);

        }

    }

    private string ChooseButtonColour(NodeType nodeType)
    {
        if(nodeType == NodeType.AUTHOR_NODE)
        {
            return "Black";
        }

        return "White";
    }

    private int CalculateButtonSize(string buttonText)
    {
        if(buttonText.Length > 35)
        {
            return 3;
        }

        if(buttonText.Length > 20)
        {
            return 2;
        }

        return 1;
    }

    public void OnButtonOnePress()
    {
        Debug.Log("pressed button 1");
        ClearScreen();
        graph.MoveToOption_1();
        ProcessCurrentNode();      
    }

    public void OnButtonTwoPress()
    {
        ClearScreen();
        graph.MoveToOption_2();
        ProcessCurrentNode();
    }

    public void OnButtonThreePress()
    {
        ClearScreen();
        graph.MoveToOption_3();
        ProcessCurrentNode();
    }

    private void ClearScreen()
    {
        RemoveAllButtons();
        RemoveAuthorText();
        RemovePeople();
        RemoveClouds();
    }

    private void RemoveAllButtons()
    {
        Debug.Log("removing buttons");
       foreach(GameObject button in buttons)
        {
            Destroy(button);
        }
        buttons.Clear();
    }

    private void RemoveAuthorText()
    {
        authorText.text = "";
    }

    private void RemovePeople()
    {
        foreach (GameObject person in people)
        {
            Destroy(person);
        }
        people.Clear();
    }

    private void RemoveClouds()
    {
        foreach(GameObject cloud in clouds)
        {
            Destroy(cloud);
        }
        clouds.Clear();
    }

}
