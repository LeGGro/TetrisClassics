using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KeyWordUI : MonoBehaviour
{
    [Header("Login")]
    [SerializeField]
    private TMP_InputField loginEmail;
    [SerializeField]
    private TMP_InputField loginPassword;
    
    [Space(5f)]
    [Header("Register")]
    [SerializeField]
    private TMP_InputField registerEmail;
    [SerializeField]
    private TMP_InputField registerPassword;
    [SerializeField]
    private TMP_InputField registerConfirmPassword;
    [SerializeField]
    private TMP_InputField registerUserName;
    
    public void RegEmailPaste()
    {
        registerEmail.text = GUIUtility.systemCopyBuffer;
    
    }
    public void LogEmailPaste()
    {
        loginEmail.text = GUIUtility.systemCopyBuffer;

    }
    public void RegPasswordPaste()
    {
        registerPassword.text = GUIUtility.systemCopyBuffer;

    }
    public void RegPasswordConfirmPaste()
    {
        registerConfirmPassword.text = GUIUtility.systemCopyBuffer;

    }
    public void UsernamePaste()
    {
        registerUserName.text = GUIUtility.systemCopyBuffer;

    }
    public void LoginPasswordPaste()
    {
        loginPassword.text = GUIUtility.systemCopyBuffer;

    }
}
