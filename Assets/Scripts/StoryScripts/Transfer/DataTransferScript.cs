using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransferScript : MonoBehaviour
{
    public static int gameID;
    public static string gameType;

    public static int health = 100;
    public static string status = "Отлично";
    public static int money = 25;
    public static int bullets = 3;

    public static string[] inventory;

    public static int savedStoryID = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
    }

}
