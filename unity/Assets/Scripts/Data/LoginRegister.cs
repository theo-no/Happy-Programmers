using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginRegister : MonoBehaviour
{
    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/account";

    public InputField username;
    public InputField password;
    public Button registerButton;

    void Awake()
    {

    }
    public void OnLoginButtonClicked()
    {
        string username = this.username.text;
        string password = this.password.text;
        StartCoroutine(Login(username, password));
    }

    public void OnRegisterButtonClicked()
    {
        string username = this.username.text;
        string password = this.password.text;
        StartCoroutine(Register(username, password));
    }


    IEnumerator Login(string username, string password)
    {
        if (username == null || password == null)
        {
            
        }
        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(new { username = username, password = password });


        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(serverUrl + "/login", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // �α��� ���н�
                Debug.Log(www.error);
            }
            else
            {
                // �α��� ������ ó���ϴ� ���� �ۼ���
                Debug.Log("Login Success");
            }
        }
    }

    IEnumerator Register(string username, string password)
    {

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
                // �α��� ���н�
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
