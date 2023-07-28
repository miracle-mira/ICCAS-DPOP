using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public InputField idInputField;
    public Button loginButton;
    public login loginScript; // Login 스크립트의 참조를 설정

    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    public void OnLoginButtonClicked()
    {
        string id = idInputField.text;
        // 서버에 로그인 요청을 보내는 함수 호출
        StartCoroutine(loginScript.SendLoginRequest(id)); // StartCoroutine으로 수정
    }
}

