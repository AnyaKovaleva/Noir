using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardOption : MonoBehaviour
{
    private Sprite cardImage;
    [SerializeField] private Sprite cardBack;
    public bool isFlipped = false;
    private bool blocked = false;


    public void Start()
    {
        gameObject.GetComponent<Image>().sprite = cardBack;
    }
    public void SetCardImage(Sprite image)
    {
        cardImage = image;
    }

    public void FlipCard()
    {
        if ((!isFlipped) && (!blocked))
        {
            gameObject.GetComponent<Image>().sprite = cardImage;
            gameObject.GetComponentInChildren<SetName>().ShowName();
            gameObject.GetComponentInParent<FindPair>().CheckPair(gameObject);
            isFlipped = !isFlipped;
        }
    }

    public void FlipBack()
    {
        gameObject.GetComponent<Image>().sprite = cardBack;
        gameObject.GetComponentInChildren<SetName>().HideName();
        isFlipped = !isFlipped;
    }

    public void BlockInput()
    {
        blocked = true;
    }

}
