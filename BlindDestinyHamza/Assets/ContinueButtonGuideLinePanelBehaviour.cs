using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonGuideLinePanelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Button>().interactable == false)
        {
            if (PlayFabManager.instance.hasDataBeenSet)
            {
                gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }
}
