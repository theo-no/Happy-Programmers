using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    [System.Serializable]
    private class LoginData
    {
        public string username;
        public string password;
    }

    [System.Serializable]
    private class AccountFirstResponse
    {
        public long accountId;
        public string accessToken;
        public string refreshToken;
    }

    [System.Serializable]
    private class AccountSecondResponse
    {
        public string username;
        public string nickname;
        public string language;
    }


    private string url = "http://k9d210.p.ssafy.io:8081/api/account";

    public TMP_InputField username;
    public TMP_InputField password;
    public Button loginButton;
    public GameObject checkWindow;
    public TextMeshProUGUI windowText;
    public GameObject dataManagerPrefab;


    public void OnLoginButtonClicked()
    {
        string InputUsername = username.text;
        string InputPassword = password.text;
        if (string.IsNullOrEmpty(InputUsername) || string.IsNullOrEmpty(InputPassword))
        {
            windowText.text = "���̵�� ��й�ȣ�� ��� �Է����ּ���";
            checkWindow.SetActive(true);
            return;
        }
        LoginData loginData = new LoginData();
        loginData.username = InputUsername;
        loginData.password = InputPassword;
        StartCoroutine(Login(loginData));
    }

    IEnumerator Login(LoginData loginData)
    {
        
        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(loginData);

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(url + "/login", "POST"))
        {
            // JSON �ùٸ��� Ȯ���غ���
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // �α��� ���н�
                Debug.Log(www.error);
                windowText.text = "���̵� �Ǵ� ��й�ȣ�� Ʋ�Ƚ��ϴ�.";
                checkWindow.SetActive(true);
            }
            else
            {
                // �α��� ������ ó���ϴ� ����
                string response = www.downloadHandler.text;
                AccountFirstResponse account = JsonUtility.FromJson<AccountFirstResponse>(response);
                Instantiate(dataManagerPrefab);
                DataManager.instance.AccountData.accessToken = account.accessToken;
                DataManager.instance.AccountData.refreshToken = account.refreshToken;
                DataManager.instance.AccountData.accountId = account.accountId.ToString();
                StartCoroutine(GetAccountInfo(account.accessToken));
                SceneManager.LoadScene("GameStart");
            }
        }
    }

    IEnumerator GetAccountInfo(string accessToken)
    {
        UnityWebRequest www = UnityWebRequest.Get(url + "/my/account");
        www.SetRequestHeader("Authorization", "Bearer " + accessToken);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            Debug.Log("���� ������ �������� �Ϳ� �����߽��ϴ�.");
        }
        else
        {
            string response = www.downloadHandler.text;
            AccountSecondResponse account = JsonUtility.FromJson<AccountSecondResponse>(response);
            DataManager.instance.AccountData.username = account.username;
            DataManager.instance.AccountData.nickname = account.nickname;
            DataManager.instance.AccountData.language = account.language;
            Debug.Log("���� ������ �������� �Ϳ� �����߽��ϴ�.");
        }
    }
}
