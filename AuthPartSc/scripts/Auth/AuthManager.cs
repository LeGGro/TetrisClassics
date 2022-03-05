using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AuthManager : MonoBehaviour
{
    [Header("Login")]
    [SerializeField]
    private TMP_InputField loginEmail;
    [SerializeField]
    private TMP_InputField loginPassword;
    [SerializeField]
    private TMP_Text loginOutput;
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
    [SerializeField]
    private TMP_Text registerOutput;
    [Space(5f)]
    [Header("Recovery")]
    [SerializeField]
    private TMP_InputField resetEmail;

    [SerializeField]
    private string _HIDDEN_PASS;
    [SerializeField]
    private string _HIDDEN_EMAIL;
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _HIDDEN_EMAIL = PlayerPrefs.GetString("HIDEMAIL");
        _HIDDEN_PASS = PlayerPrefs.GetString("HIDPASS");
        if (_HIDDEN_PASS != string.Empty && _HIDDEN_EMAIL != string.Empty)
        {
            var request = new LoginWithEmailAddressRequest();
            request.Password = _HIDDEN_PASS;
            request.Email = _HIDDEN_EMAIL;

            PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginError);
        }
    }



    public void ClearInputFields()
    {
        loginEmail.text = "";
        loginPassword.text = "";
        loginOutput.text = "";
        registerUserName.text = "";
        registerPassword.text = "";
        registerOutput.text = "";
        registerEmail.text = "";
        registerConfirmPassword.text = "";
    }
    private string Encrypt(string pass)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x =
        new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();
    }
    public void SendCustomAccountRecoveryEmail()
    {
        if (resetEmail.text != null)
        {
            var request = new SendAccountRecoveryEmailRequest
            {
                //Email = resetEmail.text,
                //EmailTemplateId = "910397F24A7DB132",
                //TitleId = "F1BF1"
            };

            PlayFabClientAPI.SendAccountRecoveryEmail(request, res =>
            {
                Debug.Log("An account recovery email has been sent to the player's email address.");
            }, FailureCallback);
        }
    }

    private void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
    public void SignIn()
    {
        Debug.Log("logging");
        var request = new LoginWithEmailAddressRequest();
        request.Password = Encrypt(loginPassword.text);
        request.Email = loginEmail.text;

        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginError);


    }
    private void LoginError(PlayFabError obj)
    {
        registerOutput.color = Color.red;
        if (obj.Error == PlayFabErrorCode.AccountNotFound)
            loginOutput.text = "Account not found";
        if (obj.Error == PlayFabErrorCode.InvalidEmailOrPassword)
            loginOutput.text = "Invalid email or password";
    }
    private void LoginSuccess(LoginResult obj)
    {
        Debug.Log("Success logging");
        if (_HIDDEN_EMAIL != string.Empty && _HIDDEN_PASS != string.Empty)
        {
            _HIDDEN_PASS = Encrypt(loginPassword.text);
            _HIDDEN_EMAIL = loginEmail.text;
        }
        PlayerPrefs.SetString("HIDPASS", _HIDDEN_PASS);
        PlayerPrefs.SetString("HIDEMAIL", _HIDDEN_EMAIL);
        SceneManager.LoadScene(1);

    }


    public void SignUp()
    {
        if (registerConfirmPassword.text != registerPassword.text)
        {
            registerOutput.text = "Passwords does not match";
        }
        else
        {
            var registerRequest = new RegisterPlayFabUserRequest
            {
                Email = registerEmail.text,
                Password = Encrypt(registerPassword.text),
                Username = registerUserName.text,
                DisplayName = registerUserName.text

            };
            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterError);
        }
    }
    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {

        var AddOrUpdateContactEmail = new AddOrUpdateContactEmailRequest { EmailAddress = registerEmail.text };
        PlayFabClientAPI.AddOrUpdateContactEmail(AddOrUpdateContactEmail, AddSuccess, AddError);
    }
    private void AddSuccess(AddOrUpdateContactEmailResult result)
    {
        registerOutput.color = Color.green;
        registerOutput.text = "Register Succsess, please verify your email";

    }
    private void AddError(PlayFabError result)
    {
        Debug.Log("all not nice");
    }
    private void RegisterError(PlayFabError result)
    {
        registerOutput.color = Color.red;
        if (result.Error == PlayFabErrorCode.EmailAddressNotAvailable)
            registerOutput.text = "Email Address Not Available";
        if (result.Error == PlayFabErrorCode.InvalidEmailAddress)
            registerOutput.text = "Invalid email";
        if (result.Error == PlayFabErrorCode.InvalidPassword)
            registerOutput.text = "Invalid password";
    }
}
