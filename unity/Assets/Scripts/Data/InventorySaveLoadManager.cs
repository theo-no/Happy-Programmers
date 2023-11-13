using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class InventorySaveLoadManager
{

    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/inventory";
    string accessToken;

    // ĳ������ �κ��丮 ���� ������ ����
    public IEnumerator SaveInventoryData()
    {
        // �κ��丮 �����͸� JSON���� ��ȯ
        string json = JsonUtility.ToJson(DataManager.instance.CharacterData);

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(serverUrl + "/save", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("ĳ������ �κ��丮 ������ �����Ͽ����ϴ�.");
            }
        }
    }

    // ĳ������ �κ��丮 �������� �ҷ�����
    public IEnumerator LoadInventoryData()
    {
        accessToken = DataManager.instance.AccountData.accessToken;

        using (UnityWebRequest www = new UnityWebRequest(serverUrl + "/my", "GET"))
        {
            www.SetRequestHeader("Authorization", "Bearer " + accessToken);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Debug.Log("ĳ���� �ҷ����� ����");
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log(response);
                CharacterData character = JsonUtility.FromJson<CharacterData>(response);
                DataManager.instance.CharacterData = character;
                SceneManager.LoadScene(DataManager.instance.CharacterData.savepoint);

                Debug.Log(DataManager.instance.CharacterData.name + "�� �����Ͽ����ϴ�.");
                Debug.Log("ĳ���� �ҷ����� ����");
            }
        }
    }
}