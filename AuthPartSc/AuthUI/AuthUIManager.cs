using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class AuthUIManager : MonoBehaviour
{
    public static AuthUIManager instance;

    [Header("references")]
    [SerializeField]
    private GameObject LoginUI;
    [SerializeField]
    private GameObject RegisterUI;
    [SerializeField]
    private GameObject resetPasswordUI;
    [SerializeField]
    private GameObject buttonToSendResetEmail;
    [SerializeField]
    private TMP_Text VerifyEmailText;


    private void Awake()
    {
        if (instance == null)
        { instance = this; }
        else if (instance != this)
        { Destroy(gameObject);}

    }

    private void ClearUI()
    {
        LoginUI.SetActive(false);
        RegisterUI.SetActive(false);
        resetPasswordUI.SetActive(false);
    }
    public void ResetScreen()
    {
        ClearUI();
        resetPasswordUI.SetActive(true);
    }
    public void LoginScreen()
    {
        ClearUI();
        LoginUI.SetActive(true);
    }

    public void RegisterScreen()
    {
        ClearUI();
        RegisterUI.SetActive(true);
            
    }
    public void OpenKeyword()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    
    }
    
}
