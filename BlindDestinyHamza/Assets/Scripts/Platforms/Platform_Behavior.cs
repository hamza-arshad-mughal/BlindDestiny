using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Behavior : MonoBehaviour
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
        if(gameObject.tag == "Platform_Neutral")
        {
            gameObject.tag = col.gameObject.GetComponent<CharacterManager>().myPlatformTag;
            gameObject.GetComponent<Renderer>().material = col.gameObject.GetComponent<CharacterManager>().myPlatformMaterial;
        }
        else
        {
            if (!gameObject.CompareTag(col.gameObject.GetComponent<CharacterManager>().myPlatformTag))
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
            }
        }
        
    }
}
