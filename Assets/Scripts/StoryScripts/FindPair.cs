using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System.Xml;
using System.Text;
using System.IO;

public class FindPair : MonoBehaviour
{
    [SerializeField] private GameObject card;
    private Sprite[] participants = new Sprite[5];
    private List<GameObject> cardStack = new List<GameObject>();
    [SerializeField] private Vector2[] cardPlace;

    private GameObject card1;
    private GameObject card2;

    private int gameID;
    private string sprPath = "Images/Cards/Players/";

    private string suspect;

    private bool allFlipped = false;
    private int turns = 3;


    void Start()
    {
        gameID = DataTransferScript.gameID;

        Debug.Log(gameID);

        Debug.Log(participants.Length);
        FillParticipants();

        gameObject.GetComponentInChildren<Text>().text = "У тебя " + turns + " попытки";

    }

    private struct MemoGame
    {
        public int GameID;
        public string GameType;
        public string LevelName;
        public string[] suspects;
        public string villain;
        public int turns;
        public int StoryIdTransfer;
    }
    public void FillParticipants()
    {
        //XmlDocument xDoc = new XmlDocument();
        //xDoc.LoadXml(Resources.Load<TextAsset>("XML/MiniGameMeta")/.text);

        //XmlElement xRoot = xDoc.DocumentElement;

        //TextAsset jasonText = Resources.Load<TextAsset>("JSON/MiniGameMeta");

        //        string s = jasonText.text;
        //      Debug.Log(s);
        TextAsset jasonText = Resources.Load<TextAsset>("JSON/MiniGameMeta");
        string s = jasonText.text;
        Debug.Log(s);

        MemoGame memoGame = JsonUtility.FromJson<MemoGame>(s);
        Debug.Log(memoGame.GameID + memoGame.GameType);

       // gameID = 1;


        if (memoGame.GameID == gameID)
        {
            Debug.Log("passed if");


            for (int i = 0; i < participants.Length; i++)
            {
                Debug.Log("i = " + i);
                if (i < memoGame.suspects.Length)
                {
                    participants[i] = Resources.Load<Sprite>(sprPath + memoGame.suspects[i]);
                    Debug.Log(participants[i].name);
                }
                else
                {
                    participants[i] = Resources.Load<Sprite>(sprPath + memoGame.villain);
                    Debug.Log(participants[i].name);
                    break;
                }

            }

            for(int i =0; i < participants.Length; i++)
            {
                Debug.Log("part " + i + "  = " + participants[i].name);
            }

            turns = memoGame.turns;

            //foreach(XmlNode childnode in xnode.ChildNodes)
            //{
            //    Debug.Log("childnode name = " + childnode.Name);
            //    if (childnode.Name == "suspect1")
            //    {
            //        participants[0] = Resources.Load<Sprite>(sprPath + childnode.InnerText);
            //        Debug.Log(participants[0].name);
            //    }
            //    if (childnode.Name == "suspect2")
            //    {
            //        participants[1] = Resources.Load<Sprite>(sprPath + childnode.InnerText);
            //        Debug.Log(participants[1].name);
            //    }
            //    if (childnode.Name == "suspect3")
            //    {
            //        participants[2] = Resources.Load<Sprite>(sprPath + childnode.InnerText);
            //    }
            //    if (childnode.Name == "suspect4")
            //    {
            //        participants[3] = Resources.Load<Sprite>(sprPath + childnode.InnerText);
            //    }
            //    if (childnode.Name == "villain")
            //    {
            //        participants[4] = Resources.Load<Sprite>(sprPath + childnode.InnerText);
            //    }
            //    if (childnode.Name == "turns")
            //    {
            //        turns = int.Parse(childnode.InnerText);
            //    }


            //}
        }

        CreateStack();

    }

    public void CreateStack()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject sCard = GetCardName(participants[i]);
            cardStack.Add(sCard);
            if (cardStack.Count < 9)
            {
                sCard = GetCardName(participants[i]);
                cardStack.Add(sCard);
            }

        }


        for (int x = 0; x < 20; x++)
        {
            int rnd = Random.Range(0, 9);
            GameObject temp = cardStack[rnd];
            cardStack.RemoveAt(rnd);
            cardStack.Add(temp);
        }
        SpawnCards();

    }

    public void SpawnCards()
    {
        for (int i = 0; i < cardPlace.Length; i++)
        {
            cardStack[i].transform.localPosition = new Vector3(cardPlace[i].x, cardPlace[i].y, 0f);
        }
    }

    public GameObject GetCardName(Sprite part)
    {
        GameObject pCard = GameObject.Instantiate(card, gameObject.transform);
        pCard.GetComponent<CardOption>().SetCardImage(part);
        return pCard;
    }

    public async void CheckPair(GameObject card)
    {
        if (card1 == null)
        {
            card1 = card;

            BoolEqualizer(710);
        }
        else if (card2 == null)
        {
            card2 = card;

            await Task.Delay(700);

            if (card1.GetComponent<Image>().sprite.name == card2.GetComponent<Image>().sprite.name)
            {
                card1 = null;
                card2 = null;
            }
            else
            {
                card1.GetComponent<CardOption>().FlipBack();
                card2.GetComponent<CardOption>().FlipBack();
                card1 = null;
                card2 = null;


                BoolEqualizer(0);
                TurnCounter();
            }

        }
        else
        {
            card.GetComponent<CardOption>().FlipBack();
        }


        //Debug.Log(allFlipped);


        if (false)
        {

        }
    }

    public async void BoolEqualizer(int time)
    {
        GameObject suspect = null;
        await Task.Delay(time);

        bool Flipped = true;
        foreach (GameObject xCard in cardStack)
        {
            Flipped = (Flipped && xCard.GetComponent<CardOption>().isFlipped);
            if (xCard.GetComponent<Image>().sprite.name == this.suspect)
            {
                suspect = xCard;
                Debug.Log(suspect.GetComponent<Image>().sprite.name);
            }
        }
        allFlipped = Flipped;

        if (allFlipped)
        {
            if (suspect != null)
            {
                suspect.GetComponent<Image>().color = new Color(0.21f, 1.0f, 0.28f, 1.0f);
            }
            gameObject.GetComponentInChildren<Text>().text = "Gotcha bitch!";

            GameOver();
        }

    }

    public void TurnCounter()
    {
        turns -= 1;

        gameObject.GetComponentInChildren<Text>().text = "У тебя осталось " + turns + " попытки";

        if (turns == 1)
        {
            gameObject.GetComponentInChildren<Text>().text = "У тебя осталось последняя попытка";
        }

        if (turns == 0)
        {
            gameObject.GetComponentInChildren<Text>().text = "Ты проиграл! Злодей сбежал. Сдай свой значок, детектив.";

            foreach (GameObject xCard in cardStack)
            {
                xCard.GetComponent<CardOption>().BlockInput();
            }

            GameOver();
        }
    }

    public async void ReloadLevel()
    {
        await Task.Delay(3000);

        SceneManager.LoadScene("IntuitionScene");
    }

    public async void GameOver()
    {
        await Task.Delay(3000);

        SceneManager.LoadScene("DialogueScene");
    }
}
