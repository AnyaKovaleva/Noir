using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public GameObject Health;
    public GameObject Status;
    public GameObject Money;
    public GameObject Bullets;

    private void Start()
    {
        RefreshStatus();
    }

    public void RefreshStatus()
    {
        int health = DataTransferScript.health;
        string status = DataTransferScript.status;
        int money = DataTransferScript.money;
        int bullets = DataTransferScript.bullets;
        Health.GetComponent<Text>().text = "Здоровье: " + health.ToString();
        Status.GetComponent<Text>().text = "Статус: " + status;
        Money.GetComponent<Text>().text = "Деньги: " + money.ToString() + "$";
        Bullets.GetComponent<Text>().text = "Патроны: " + bullets.ToString();
    }
}
