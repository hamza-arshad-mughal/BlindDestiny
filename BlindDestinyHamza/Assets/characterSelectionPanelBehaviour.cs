using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class characterSelectionPanelBehaviour : MonoBehaviour
{
    public GameObject CharacterSelectionPanel;
    public GameObject CharacterScrollViewParent;
    public GameObject SelectedCharacter;
    public GameObject CharacterDetailsPrefab;
    public GameObject ConfirmationPanel;
    public GameObject InsufficentMoneyPanel;
    public GameObject PlayerMoneyText;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ConfigureCharactersInScrollView();
        UpdatePlayerMoney();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConfigureCharactersInScrollView()
    {
        for (int i = 0; i < CharacterScrollViewParent.transform.childCount; i++)
        {
            Destroy(CharacterScrollViewParent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < CharacterSelectionManager.instance.AllCharcters.Count; i++)
        {
            bool _characterAutoSelectCalled = false;
            GameObject newOBJ = Instantiate(CharacterDetailsPrefab, CharacterScrollViewParent.transform, false);
            newOBJ.transform.GetChild(0).GetComponent<Image>().sprite = CharacterSelectionManager.instance.AllCharcters[i].icon;
            newOBJ.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = CharacterSelectionManager.instance.AllCharcters[i].name;
            newOBJ.transform.GetChild(2).gameObject.SetActive(false);
            newOBJ.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = CharacterSelectionManager.instance.AllCharcters[i].price.ToString();
            int _index = i;
            newOBJ.GetComponent<Button>().onClick.AddListener(() => OnCharacterClicked(_index));
            if (!CharacterSelectionManager.instance.AllCharcters[i].isUnlocked)
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
        if (CharacterSelectionManager.instance.AllCharcters[_index].isUnlocked)
        {
            SelectedCharacter.GetComponent<Image>().sprite = CharacterSelectionManager.instance.AllCharcters[_index].icon;
            SelectedCharacter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = CharacterSelectionManager.instance.AllCharcters[_index].name;
            GAMEMANAGER.instance.CharacterSelected = CharacterSelectionManager.instance.AllCharcters[_index].name;
        }
        else
        {
            ConfirmationPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Unlock this character for $" + CharacterSelectionManager.instance.AllCharcters[_index].price;
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
        Character _currentChar = CharacterSelectionManager.instance.AllCharcters[_index];
        if (GAMEMANAGER.instance.PlayerMoney >= _currentChar.price)
        {
            _currentChar.isUnlocked = true;
            CharacterSelectionManager.instance.AllCharcters[_index] = _currentChar;
            GAMEMANAGER.instance.PlayerMoney -= _currentChar.price;
            UpdatePlayerMoney();
            PlayFabManager.instance.SaveData();
            ConfirmationPanel.SetActive(false);
            OnCharacterClicked(_index);
            ConfigureCharactersInScrollView();
        }
        else
        {
            InsufficentMoneyPanel.SetActive(true);
        }
    }

    private void UpdatePlayerMoney()
    {
        PlayerMoneyText.GetComponent<TextMeshProUGUI>().text = GAMEMANAGER.instance.PlayerMoney.ToString();
    }
}
