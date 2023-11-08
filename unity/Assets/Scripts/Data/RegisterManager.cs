using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterManager : MonoBehaviour
{
    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/account";

    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField nickname;
    public TMP_InputField language;
    public Button registerButton;

    private void Start()
    {
        username = transform.Find("InputID").GetComponent<TMP_InputField>();
        password = transform.Find("InputPW").GetComponent<TMP_InputField>();
        registerButton = transform.Find("Button").Find("Button").GetComponent<Button>();
    }

    public void OnRegisterButtonClicked()
    {
        string username = this.username.text;
        string password = this.password.text;
        string nickname = this.nickname.text;
        string language = this.language.text;
        StartCoroutine(Register(username, password, nickname, language));
    }
    IEnumerator Register(string username, string password, string nickname, string language)
    {

        if (username == "" || password == "" || nickname == "" || language == "")
        {
            Debug.LogError("�Է�ĭ�� ��� �Է����ּ���");
        }


        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(new { username = username, password = password });


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
            }
            else
            {
                // ȸ������ ������ ó���ϴ� ���� �ۼ���
                Debug.Log("Register Success");
            }
        }
    }
}
