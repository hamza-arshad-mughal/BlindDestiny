using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform_Acid : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Contains("_Player"))
        {
            if (BoltNetwork.IsServer)
            {   
                Invoke("LoadCurrentScene", 2f);
                
            }
            
        }
    }

    public void LoadCurrentScene()
    {

        BoltNetwork.LoadScene(SceneManager.GetActiveScene().name);
    }
}
