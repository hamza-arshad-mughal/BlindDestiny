using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Character
{
    public int index;
    public string name;
    public Sprite icon;
    public GameObject characterprefab;
    public bool isUnlocked;
    public int price;
}
[System.Serializable] 

public struct PlayerLeaderBoardData
{
    public string displayName;
    public int playerMoney;
}