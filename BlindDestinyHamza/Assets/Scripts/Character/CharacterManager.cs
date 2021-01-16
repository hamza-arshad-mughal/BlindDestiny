using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterManager : Bolt.EntityBehaviour<IPlayerStateForAll>
{
    public Material myPlatformMaterial;
    public string myPlatformTag;
 
    public TextMeshProUGUI NameText;

    public Canvas hostCanvas;
    public Text roomidText;

  
    private string playerName;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Attached()
    {
        if (entity.IsOwner)
        {
            playerName = GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<GAMEMANAGER>().PlayerName;
            state.MyName = playerName;        
        }
       
        NameText.text = state.MyName;
    }

    // Update is called once per frame
    void Update()
    {
        if(string.IsNullOrEmpty(NameText.text))
        {
            NameText.text = state.MyName;
        }
    }
 
}
