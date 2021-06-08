using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtonMeta : MonoBehaviour
{
    GameObject canvas;
    public Text buttonText;
    string saveName; 

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        //canvas.GetComponent<SaveLoadStory>().
    }

    public void Load()
    {
        saveName = buttonText.GetComponent<Text>().text;
        canvas.GetComponent<SaveLoadStory>().LoadGame(saveName);
        Debug.Log(saveName);
    }
}
