using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueEditor : MonoBehaviour
{
    [SerializeField]
    private GameObject personAnchorRight;
    [SerializeField]
    private GameObject personAnchorLeft;
    [SerializeField]
    private GameObject dialogueLayout;

    [SerializeField]
    [Range(1, 4)]
    private int maxNumberOfClouds;

    [SerializeField]
    private TMP_Dropdown numberOfPeopleDropdown;
    [SerializeField]
    private GameObject rightSide;
    [SerializeField]
    private GameObject leftSide;

    [SerializeField]
    private TMP_Dropdown personDropdown;
    [SerializeField]
    private Button addPersonButton;
    [SerializeField]
    private Button removePersonButton;
    [SerializeField]
    private TMP_Dropdown cloudTypeDropdown;
    [SerializeField]
    private TMP_Dropdown dialogueCloudsDropdown;
    [SerializeField]
    private TMP_Dropdown mindCloudsDropdown;
    [SerializeField]
    private Button addCloudButton;
    [SerializeField]
    private Button removeCloudButton;

    [SerializeField]
    private TMP_Dropdown personDropdown_Left;
    [SerializeField]
    private Button addPersonButton_Left;
    [SerializeField]
    private Button removePersonButton_Left;
    [SerializeField]
    private TMP_Dropdown cloudTypeDropdown_Left;
    [SerializeField]
    private TMP_Dropdown dialogueCloudsDropdown_Left;
    [SerializeField]
    private TMP_Dropdown mindCloudsDropdown_Left;
     [SerializeField]
    private Button addCloudButton_Left;
    [SerializeField]
    private Button removeCloudButton_Left;

    private int numberOfPeople;
    private bool isExistingPersonRight;
    private bool isExistingPersonLeft;
    private string personPrefabPath = "Dialogs/Prefabs/Person";
    private Sprite rightPersonSprite;
    private Sprite leftPersonSprite;
    private string personSpritePath = "Images/Persons/";

    private bool isAddingDialogueCloud;
    private int currentlyExistingClouds;
    private string pathToCloudPrefabs = "Dialogs/Prefabs/DialogueClouds/";
    private string cloudText;

    private void Start()
    {
        isAddingDialogueCloud = true;
        numberOfPeople = 1;
        isExistingPersonLeft = false;
        isExistingPersonRight = false;
        SetNumberOfPeople();
        SetPersonRigth();
        SetPersonLeft();
    }

    public void SetNumberOfPeople()
    {
        numberOfPeople = numberOfPeopleDropdown.value + 1;
        if(numberOfPeople == 1)
        {
            leftSide.SetActive(false);
            if(isExistingPersonLeft)
            {
                RemovePersonLeft();
            }
        }
        else
        {
            leftSide.SetActive(true);
        }

    }

    public void SetPersonRigth()
    {
        rightPersonSprite = Resources.Load<Sprite>(personSpritePath + personDropdown.options[personDropdown.value].text);
    }

    public void SetPersonLeft()
    {
        leftPersonSprite = Resources.Load<Sprite>(personSpritePath + personDropdown_Left.options[personDropdown_Left.value].text);
    }

    public void AddPersonRight()
    {
        if(isExistingPersonRight)
        {
            return;
        }
        isExistingPersonRight = true;
        GameObject newPerson = Instantiate<GameObject>(Resources.Load<GameObject>(personPrefabPath), personAnchorRight.transform);
        newPerson.GetComponent<Image>().sprite = rightPersonSprite;
    }

    public void AddPersonLeft()
    {
        if (isExistingPersonLeft)
        {
            return;
        }
        isExistingPersonLeft = true;
        GameObject newPerson = Instantiate<GameObject>(Resources.Load<GameObject>(personPrefabPath), personAnchorLeft.transform);
        newPerson.GetComponent<Image>().sprite = leftPersonSprite;
    }

    public void RemovePersonRight()
    {
        if(!isExistingPersonRight)
        {
            return;
        }
        isExistingPersonRight = false;
        Image person = personAnchorRight.GetComponentInChildren<Image>();
        Destroy(person.gameObject);
    }

    public void RemovePersonLeft()
    {
        if(!isExistingPersonLeft)
        {
            return;
        }
        isExistingPersonLeft = false;
        Image person = personAnchorLeft.GetComponentInChildren<Image>();
        Destroy(person.gameObject);
    }

}
