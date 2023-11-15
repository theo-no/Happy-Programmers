using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterData
{
    public string username;
    public string password;
    public string nickname;
    public string language;
}

public class RegisterManager : MonoBehaviour
{
    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/account";

    public TMP_InputField nickname;
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField passwordCheck;
    public TMP_Dropdown language;
    public Button registerButton;
    public Button nicknameCheckButton;
    public Button usernameCheckButton;
    public GameObject checkWindow;
    public TextMeshProUGUI windowText;

    public void OnNicknameCheckButtonClicked()
    {
        string InputNickname = nickname.text;
        if (string.IsNullOrEmpty(InputNickname))
        {
            windowText.text = "�г����� �Է����ּ���";
            checkWindow.SetActive(true);
            return;
        }
        StartCoroutine(CheckNickname(InputNickname));
    }

    public void OnUsernameCheckButtonClicked()
    {
        string InputUsername = username.text;
        if (string.IsNullOrEmpty(InputUsername))
        {
            windowText.text = "���̵� �Է����ּ���";
            checkWindow.SetActive(true);
            return;
        }

        StartCoroutine(CheckUsername(InputUsername));
    }

    public void OnRegisterButtonClicked()
    {
          
        RegisterData registerData = new RegisterData();
        registerData.username = username.text;
        registerData.password = password.text;
        registerData.nickname = nickname.text;
        registerData.language = language.options[language.value].text;

        if (string.IsNullOrEmpty(registerData.username) ||
            string.IsNullOrEmpty(registerData.password) ||
            string.IsNullOrEmpty(registerData.nickname) ||
            string.IsNullOrEmpty(registerData.language))
        {
            windowText.text = "�Է�ĭ�� ��� ä���ּ���";
            checkWindow.SetActive(true);
            return;
        }


        if (registerData.password != passwordCheck.text)
        {
            windowText.text = "��й�ȣ�� �ٽ� Ȯ�����ּ���!";
            checkWindow.SetActive(true);    
            return;
        }
        StartCoroutine(Register(registerData));
    }


    IEnumerator CheckNickname(string InputNickname)
    {
        string urlWithParameter = serverUrl + "/check/nickname/" + UnityWebRequest.EscapeURL(InputNickname);

        using (UnityWebRequest www = new UnityWebRequest(urlWithParameter, "GET"))

        {
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                windowText.text = "�ߺ��� �г����Դϴ�.";
                checkWindow.SetActive(true);

            }
            else
            {
                windowText.text = "���� �� �ִ� �г����Դϴ�.";
                checkWindow.SetActive(true);
            }
        }
    }

    IEnumerator CheckUsername(string InputUsername)
    {
        string urlWithParameter = serverUrl + "/check/username/" + UnityWebRequest.EscapeURL(InputUsername);

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(urlWithParameter, "GET"))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                windowText.text = "�ߺ��� ���̵��Դϴ�.";
                checkWindow.SetActive(true);
            }
            else
            {
                windowText.text = "���� �� �ִ� ���̵��Դϴ�.";
                checkWindow.SetActive(true);
            }
        }
    }


    IEnumerator Register(RegisterData registerData)
    {

        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(registerData);
        Debug.Log(json);

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(serverUrl + "/sign-up", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // ȸ������ ���н�
                Debug.Log(www.error);
                windowText.text = "ȸ�����Կ� �����Ͽ����ϴ�.";
                checkWindow.SetActive(true);
            }
            else
            {
                // ȸ������ ������ ó���ϴ� ���� �ۼ���
                windowText.text = "���������� ȸ�������� �Ǿ����ϴ�.";
                checkWindow.SetActive(true);
                SceneManager.LoadScene("GameLogin");
            }
        }
    }
}
