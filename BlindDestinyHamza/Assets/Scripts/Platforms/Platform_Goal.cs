using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform_Goal : MonoBehaviour
{
    // Start is called before the first frame update
    public string tag1 = null;
    public string tag2 = null;

    public int numOfplayers = 0;

    public bool Player1Arrived = false;
    public bool Player2Arrived = false;

    private bool checkPlayers = true;

    public AudioSource player;
    public AudioClip levelCompleteClip;
    public string nextSceneName;



    private bool _LoadNextSceneCalled = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkPlayers)
        {
            numOfplayers = GameObject.FindObjectsOfType<MoveCharacter>().Length;
        }



        if (numOfplayers > 1)
        {
            checkPlayers = false;
            MoveCharacter[] players = GameObject.FindObjectsOfType<MoveCharacter>();
            tag1 = players[0].gameObject.tag;
            tag2 = players[1].gameObject.tag;
        }else if(numOfplayers <= 1)
        {
            checkPlayers = true;
        }



        if (Player1Arrived && Player2Arrived)
        {
            Invoke("LoadnextScene", 2f);
            player.clip = levelCompleteClip;
            player.Play();
            
        }


    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(tag1))
        {
            Player1Arrived = true;
        }
        else if (col.gameObject.CompareTag(tag2))
        {
            Player2Arrived = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(tag1))
        {
            Player1Arrived = false;
        }
        else if (col.gameObject.CompareTag(tag2))
        {
            Player2Arrived = false;
        }
    }

    public void LoadnextScene()
    {
        if (Player1Arrived && Player2Arrived && !_LoadNextSceneCalled)
        {
            GAMEMANAGER.instance.PlayerMoney += 100;
            PlayFabManager.instance.SaveData();
            _LoadNextSceneCalled = true;
            BoltNetwork.LoadScene(nextSceneName);
            Debug.LogWarning("CALED");
        }
 
    }
}
