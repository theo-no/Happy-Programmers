using System.Collections;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public Button loginButton;


    private void Start()
    {
        username = transform.Find("InputID").GetComponent<TMP_InputField>();
        password = transform.Find("InputPW").GetComponent<TMP_InputField>();
        loginButton = transform.Find("Login").Find("LoginBtn").GetComponent<Button>();
    }

    public void OnLoginButtonClicked()
    {
        string username = this.username.text;
        string password = this.password.text;
        StartCoroutine(Login(username, password));
    }

    IEnumerator Login(string username, string password)
    {
        if (username == "" || password == "")
        {
            Debug.LogError("���̵�� �н����带 ��� �Է����ּ���");
            yield break;
        }

        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(new { username = username, password = password });


        string url = "http://k9d210.p.ssafy.io:8081/api/account/login";

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
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
                Debug.Log("�α��� ����");
            }
            else
            {
                // �α��� ������ ó���ϴ� ����
                Debug.Log("�α��� ����");
                string response = www.downloadHandler.text;
                var data = JsonUtility.FromJson<Dictionary<string, string>>(response);
                string accountId = data["acocuntId"];
                StartCoroutine(ServerManager.instance.LoadCharacterData(accountId));
            }
        }
    }


   

}
