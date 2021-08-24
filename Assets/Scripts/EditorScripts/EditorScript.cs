using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown editorOptionsDropdown;

    [SerializeField]
    private List<GameObject> editors;

    [SerializeField]
    private GameObject background;

    [SerializeField]
    private TMP_Dropdown backgroundDropdown;

    [SerializeField]
    private GameObject buttonEditor;

    private string backgroundSpritesPath = "Sprites/StorySprites/Background/";


    private void Start()
    {
        ChooseEditor();
        SetBackgroundFromDropdown();
    }

    private void HideAllEditors()
    {
        foreach (GameObject editor in editors)
        {
            editor.SetActive(false);
        }
    }

    public void ChooseEditor()
    {
        HideAllEditors();
        editors[editorOptionsDropdown.value].SetActive(true);
    }

    public void SetBackgroundFromDropdown()
    {
        int optionNum = backgroundDropdown.value;
        background.GetComponent<Image>().sprite = Resources.Load<Sprite>(backgroundSpritesPath + backgroundDropdown.options[optionNum].text);
    }
}
