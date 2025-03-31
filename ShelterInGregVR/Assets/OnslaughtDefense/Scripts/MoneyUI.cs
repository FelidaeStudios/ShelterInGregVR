using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text moneyText;

    void Update()
    {
        moneyText.text = "$" + PlayerStats.money.ToString();
    }
}
