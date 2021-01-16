using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public Transform sploc1;
    public Transform sploc2;

    private int numloc;
    private GameObject char1inScene = null;

    public string characterSelected;
    

    [System.Obsolete]
    public override void SceneLoadLocalDone(string scene)
    {

        sploc1 = GameObject.FindGameObjectWithTag("sploc1").transform;
        sploc2 = GameObject.FindGameObjectWithTag("sploc2").transform;
        Invoke("spawnchar", 2f);

    }
    

    public void spawnchar()
    {
        Vector3 _myloc;
        if (BoltNetwork.IsServer)
        {
            _myloc = sploc1.position;
        }
        else
        {
            _myloc = sploc2.position;
        }
        characterSelected = GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<GAMEMANAGER>().CharacterSelected;
        if (string.IsNullOrEmpty(characterSelected))
        {
         //this section is obselete since characterselected is never null now bcoz of charcter selectionPanel
            char1inScene = GameObject.FindGameObjectWithTag("PinkMan_Player");
            if (char1inScene == null)
            {
                char1inScene = GameObject.FindGameObjectWithTag("NinjaFrog_Player");
                if (char1inScene == null)
                {
                    numloc = (int)Random.Range(0, 21);
                    if (numloc <= 10)
                    {
                        BoltNetwork.Instantiate(BoltPrefabs.PinkMan, sploc1.position, Quaternion.identity);
                        GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<GAMEMANAGER>().CharacterSelected = "PinkMan";
                    }
                    else
                    {
                        BoltNetwork.Instantiate(BoltPrefabs.NinjaFrog, sploc1.position, Quaternion.identity);
                        GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<GAMEMANAGER>().CharacterSelected = "NinjaFrog";
                    }
                }
            }
            if (char1inScene)
            {
                numloc = (int)Random.Range(0, 21);

                if (numloc <= 10)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.MaskDude, sploc2.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<GAMEMANAGER>().CharacterSelected = "MaskDude";
                    
                }
                else
                {
                    BoltNetwork.Instantiate(BoltPrefabs.VirtualGuy, sploc2.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<GAMEMANAGER>().CharacterSelected = "VirtualGuy";
                }

            }
        }
        else
        {
            if(characterSelected == "PinkMan")
            {
                BoltNetwork.Instantiate(BoltPrefabs.PinkMan, _myloc, Quaternion.identity);
                GAMEMANAGER.instance.CharacterSpawned = "PinkMan";
            }
            else if(characterSelected == "NinjaFrog")
            {
                BoltNetwork.Instantiate(BoltPrefabs.NinjaFrog, _myloc, Quaternion.identity);
                GAMEMANAGER.instance.CharacterSpawned = "NinjaFrog";
            }
            else if(characterSelected == "MaskDude")
            {
                BoltNetwork.Instantiate(BoltPrefabs.MaskDude, _myloc, Quaternion.identity);
                GAMEMANAGER.instance.CharacterSpawned = "MaskDude";
            }
            else if(characterSelected == "VirtualGuy")
            {
                BoltNetwork.Instantiate(BoltPrefabs.VirtualGuy, _myloc, Quaternion.identity);
                GAMEMANAGER.instance.CharacterSpawned = "VirtualGuy";
            }
        }
    }

    public override void OnEvent(ReloadSceneEvent evnt)
    {
        if (BoltNetwork.IsServer)
        {
            BoltNetwork.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
