using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenuBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public bool SoundPlaying = true;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Button SoundButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SoundPlaying)
        {
            SoundButton.GetComponent<Image>().sprite = soundOnSprite;
        }
        else
        {
            SoundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
    }

    public void soundButtonClicked()
    {
        if (SoundPlaying)
        {
            AudioSource[] sources = GameObject.FindObjectsOfType<AudioSource>();
            foreach (AudioSource source in sources)
            {
                source.enabled = false;
            }
            SoundPlaying = false;
        }
        else
        {
            AudioSource[] sources = GameObject.FindObjectsOfType<AudioSource>();
            foreach (AudioSource source in sources)
            {
                source.enabled = true;
            }
            SoundPlaying = true;
        }
    }
    public void exitButtonClicked()
    {
        BoltNetwork.ShutdownImmediate();
        SceneManager.LoadScene(0);
    }
}
