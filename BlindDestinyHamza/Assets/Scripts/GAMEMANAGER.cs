using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GAMEMANAGER : MonoBehaviour
{
    public int PlayerMoney;
    public string PlayerName;

    public string roomIdHost;
    public InputField PlayerNameInputFeild;
    public InputField SessionIdJoinPanel;
    public Button Host1stPanel;
    public Button Join1stPanel;
    public Button DoneJoinPanel;

    public string CharacterSelected = "";
    public string CharacterSpawned = "";

    public static GAMEMANAGER instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (PlayerNameInputFeild.text.ToString().Length <= 2)
            {
                Host1stPanel.interactable = false;
                Join1stPanel.interactable = false;
            }
            else
            {
                Host1stPanel.interactable = true;
                Join1stPanel.interactable = true;
            }

            if (SessionIdJoinPanel.text.ToString().Length < 4)
            {
                DoneJoinPanel.interactable = false;
            }
            else
            {
                DoneJoinPanel.interactable = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "EndGame")
        {
            Destroy(gameObject);
        }
    }

    public void playernameChanged()
    {
        PlayerName = PlayerNameInputFeild.text.ToString();
    }

    
}
