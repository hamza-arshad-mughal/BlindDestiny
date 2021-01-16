using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayFabLogin : MonoBehaviour
{
    public GameObject LogSignPanel;
    public GameObject CharacterSelectionPanel;
    public GameObject ErrorLoginPanel;
    public GameObject ErrorSingupPanel;
    public GameObject LoginPanel;
    public GameObject SignUpPanel;
    public GameObject GuidelinePanel;
    //public InputField usernameSignup;
    //public InputField emailSignup;
    //public InputField passwordSignUp;
    //public InputField emailLogIn;
    //public InputField passwordLogIn;

    public string emailLog;
    public string passwordLog;
    public string emailSign;
    public string passwordSign;
    public string usernameSign;

    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "D94EC";
        }
    }

    public void LoginClicked()
    {
        var request = new LoginWithEmailAddressRequest { Email = emailLog, Password = passwordLog };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        LogSignPanel.SetActive(false);
       GuidelinePanel.SetActive(true);
        PlayFabManager.instance.GetDataAndAssignValues();
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = usernameSign }, OnDisplayNameSuccess, OnRegisterFailure);
    }

   

    private void OnLoginFailure(PlayFabError error)
    {
        LoginPanel.SetActive(false);
        ErrorLoginPanel.SetActive(true);
    }

    public void GetEmailLogIn(string emailInLog)
    {
        emailLog = emailInLog;
    }
    public void GetPasswordLogIn(string passwordInLog)
    {
        passwordLog = passwordInLog;
    }

    public void SignUpClicked()
    {
        var request = new RegisterPlayFabUserRequest { Email = emailSign, Password = passwordSign, Username = usernameSign };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        
    }

    public void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult res)
    {
        Debug.Log("DISPLAYNAME UPDATED");
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        SignUpPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        SignUpPanel.SetActive(false);
        ErrorSingupPanel.SetActive(true);
    }
    public void GetUsernameSign(string usernameSignIn)
    {
        usernameSign = usernameSignIn;
    }
    public void GetEmailSign(string emailSignIn)
    {
        emailSign = emailSignIn;
    }
    public void GetPasswordSign(string passwordSignIn)
    {
        passwordSign = passwordSignIn;
    }

    public void retryClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
