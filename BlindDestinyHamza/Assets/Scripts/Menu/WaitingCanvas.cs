using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public int numOfplayers = 0;
    public Text roomIdText;

    public GameObject InGameplayerSelectionCanvas;
    void Start()
    {
        if (BoltNetwork.IsServer)
        {
            roomIdText.text = "ROOM ID : " + GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<GAMEMANAGER>().roomIdHost;
        }
        else
        {
            roomIdText.text = "";
        }
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            numOfplayers = GameObject.FindObjectsOfType<MoveCharacter>().Length;
            if (numOfplayers > 1)
            {
                MoveCharacter[] _playersInScene = GameObject.FindObjectsOfType<MoveCharacter>();

                string _tempPlayerName = "";
                bool _samePlayerFound = false;
                for(int i = 0; i < _playersInScene.Length; i++)
                {
                    if (i == 0)
                    {
                        _tempPlayerName = _playersInScene[i].name;
                    }
                    else
                    {
                        if(_tempPlayerName == _playersInScene[i].name)
                        {
                          _samePlayerFound = true;
                        }
                    }
                }


                if (!_samePlayerFound)
                {
                    gameObject.SetActive(false);
                    InGameplayerSelectionCanvas.SetActive(false);
                }
                else
                {
                    if (BoltNetwork.IsServer)
                    {
                        gameObject.SetActive(true);
                    }
                    else
                    {
                        InGameplayerSelectionCanvas.SetActive(true);
                    }
                }
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }

    public void SendReloadSceneEvent()
    {
        var reloadSceneEvent = ReloadSceneEvent.Create(Bolt.GlobalTargets.Everyone, Bolt.ReliabilityModes.ReliableOrdered);
        reloadSceneEvent.Send();
    }
}
