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

    private void Start()
    {
        nickname = transform.Find("Nickname").GetComponent<TMP_InputField>();
        username = transform.Find("ID").GetComponent<TMP_InputField>();
        password = transform.Find("Password").GetComponent<TMP_InputField>();
        passwordCheck = transform.Find("PasswordCheck").GetComponent<TMP_InputField>();
        language = transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
        nicknameCheckButton = transform.Find("NicknameCheck").Find("NicknameCheckBtn").GetComponent<Button>();
        usernameCheckButton = transform.Find("IdCheck").Find("IdCheckBtn").GetComponent<Button>();
        registerButton = transform.Find("Confirm").Find("ConfirmBtn").GetComponent<Button>();
    }

    public void OnNicknameCheckButtonClicked()
    {
        string InputNickname = nickname.text;
        if (string.IsNullOrEmpty(InputNickname))
        {
            Debug.LogError("�г����� �Է����ּ���");
            return;
        }
        StartCoroutine(CheckNickname(InputNickname));
    }

    public void OnUsernameCheckButtonClicked()
    {
        string InputUsername = username.text;
        if (string.IsNullOrEmpty(InputUsername))
        {
            Debug.LogError("���̵� �Է����ּ���");
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
            Debug.LogError("�Է�ĭ�� ��� ä���ּ���");
            return;
        }


        if (registerData.password != passwordCheck.text)
        {
            Debug.LogError("��й�ȣ�� �ٽ� Ȯ�����ּ���!");
            return;
        }
        StartCoroutine(Register(registerData));
    }


    IEnumerator CheckNickname(string InputNickname)
    {

        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(new { nickname = InputNickname });


        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(serverUrl + "/nickname-check", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // �г��� üũ ���н�
                Debug.Log(www.error);
                Debug.Log("�г��� üũ ����"); 
            }
            else
            {
                // �г��� üũ ������ ó���ϴ� ���� �ۼ���
                Debug.Log("�г��� üũ ����");
            }
        }
    }

    IEnumerator CheckUsername(string InputUsername)
    {
        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(new { username = InputUsername });


        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(serverUrl + "/username-check", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // �г��� üũ ���н�
                Debug.Log(www.error);
                Debug.Log("���̵� üũ ����");
            }
            else
            {
                // �г��� üũ ������ ó���ϴ� ���� �ۼ���
                Debug.Log("���̵� üũ ����");
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
                Debug.Log("ȸ������ ����");
            }
            else
            {
                // ȸ������ ������ ó���ϴ� ���� �ۼ���
                Debug.Log("ȸ������ ����");
                SceneManager.LoadScene("GameLogin");
            }
        }
    }
}
