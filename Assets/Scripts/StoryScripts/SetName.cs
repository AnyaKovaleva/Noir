using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetName : MonoBehaviour
{    
    public void ShowName()
    {
        gameObject.GetComponent<Text>().text = gameObject.GetComponentInParent<Image>().sprite.name;        
    }

    public void HideName()
    {
        gameObject.GetComponent<Text>().text = "";
    }
}
