using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterSelectionManager : MonoBehaviour
{
 
    public static CharacterSelectionManager instance { get; private set; }
    /*public GameObject CharacterSelectionPanel;
    public GameObject CharacterScrollViewParent;
    public GameObject SelectedCharacter;
    public GameObject CharacterDetailsPrefab;
    public GameObject ConfirmationPanel;
    public GameObject InsufficentMoneyPanel;*/

    public List<Character> AllCharcters;

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

    void Start()
    {
        //ConfigureCharactersInScrollView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void ConfigureCharactersInScrollView()
    {
        for(int i=0; i < CharacterScrollViewParent.transform.childCount; i++)
        {
            Destroy(CharacterScrollViewParent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < AllCharcters.Count; i++)
        {
            bool _characterAutoSelectCalled = false;
            GameObject newOBJ = Instantiate(CharacterDetailsPrefab, CharacterScrollViewParent.transform, false);
            newOBJ.transform.GetChild(0).GetComponent<Image>().sprite = AllCharcters[i].icon;
            newOBJ.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = AllCharcters[i].name;
            newOBJ.transform.GetChild(2).gameObject.SetActive(false);
            newOBJ.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = AllCharcters[i].price.ToString();
            int _index = i;
            newOBJ.GetComponent<Button>().onClick.AddListener(() => OnCharacterClicked(_index));
            if (!AllCharcters[i].isUnlocked)
            {
                newOBJ.transform.GetChild(1).gameObject.SetActive(false);
                newOBJ.transform.GetChild(2).gameObject.SetActive(true);
                newOBJ.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                if (!_characterAutoSelectCalled)
                {
                    OnCharacterClicked(_index);
                    _characterAutoSelectCalled = true;
                }
            }
        }
    }

    public void OnCharacterClicked(int _index)
    {
        if (AllCharcters[_index].isUnlocked)
        {
            SelectedCharacter.GetComponent<Image>().sprite = AllCharcters[_index].icon;
            SelectedCharacter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = AllCharcters[_index].name;
            GAMEMANAGER.instance.CharacterSelected = AllCharcters[_index].name;
        }
        else
        {
            ConfirmationPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Unlock this character for $" + AllCharcters[_index].price;
            ConfirmationPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnYesClickedFromConfirmationPanel(_index));
            ConfirmationPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnNoClickedFromConfirmationPanel());
            ConfirmationPanel.SetActive(true);
        }
    }

    public void OnNoClickedFromConfirmationPanel()
    {
        ConfirmationPanel.SetActive(false);
    }
    public void OnYesClickedFromConfirmationPanel(int _index)
    {
        Character _currentChar = AllCharcters[_index];
        if(GAMEMANAGER.instance.PlayerMoney >= _currentChar.price)
        {
            _currentChar.isUnlocked = true;
            AllCharcters[_index] = _currentChar;
            GAMEMANAGER.instance.PlayerMoney -= _currentChar.price;
            GAMEMANAGER.instance.UpdatePlayerMoney();
            ConfirmationPanel.SetActive(false);
            OnCharacterClicked(_index);
            ConfigureCharactersInScrollView();
        }
        else
        {
            InsufficentMoneyPanel.SetActive(true);
        }
    }*/
}
