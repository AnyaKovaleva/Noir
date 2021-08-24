using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonEditor : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonLayout;
    [SerializeField]
    private TMP_Dropdown numberOfButtonsDropdown;
    [SerializeField]
    private TMP_Dropdown buttonPrefabDropdown;
    [SerializeField]
    private TMP_InputField buttonTextIF;
    [SerializeField]
    private TMP_Dropdown buttonEventDropdown;
    [SerializeField]
    private TMP_Text nextPageIDText;
    [SerializeField]
    private TMP_Dropdown minigameDropdown;
    [SerializeField]
    private Button addButton;
    [SerializeField]
    private Button removeButton;


    private int numberOfButtons;
    private int currentlyExistingButtons;
    private string buttonPrefabsPath = "Dialogs/Prefabs/Buttons/";
    private GameObject buttonPrefab;
    private string buttonText;


    private void Start()
    {
        numberOfButtons = 1;
        currentlyExistingButtons = 0;
    }

    public void SetNumberOfButtons()
    {
        numberOfButtons = numberOfButtonsDropdown.value + 1;
        if (currentlyExistingButtons > numberOfButtons)
        {
            RemoveButton();
            if (currentlyExistingButtons > numberOfButtons)
            {
                RemoveButton();
            }
        }
    }

    public void SetButtonPrefab()
    {
        Debug.Log(buttonPrefabsPath + buttonPrefabDropdown.options[buttonPrefabDropdown.value].text);
        buttonPrefab = Resources.Load<GameObject>(buttonPrefabsPath + buttonPrefabDropdown.options[buttonPrefabDropdown.value].text);

        if (buttonPrefab == null)
        {
            Debug.Log("prefab not found");
        }
    }

    public void SetButtonText(string text)
    {
        buttonText = text;
    }

    public void AddButton()
    {
        Debug.Log("Button number = " + numberOfButtons);
        Debug.Log("Current buttons = " + currentlyExistingButtons);
        if (currentlyExistingButtons >= numberOfButtons)
        {
            return;
        }
        currentlyExistingButtons++;
        GameObject newButton = Instantiate(buttonPrefab, buttonLayout.transform);
        newButton.GetComponentInChildren<Text>().text = buttonText;
    }

    public void RemoveButton()
    {
        if (currentlyExistingButtons == 0)
        {
            return;
        }
        currentlyExistingButtons--;
        Button[] buttons = buttonLayout.GetComponentsInChildren<Button>();
        Destroy(buttons[currentlyExistingButtons].gameObject);
    }

}
