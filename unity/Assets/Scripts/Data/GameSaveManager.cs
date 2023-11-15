using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GameSaveManager : MonoBehaviour
{
    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/character";
    string accessToken;
    public void OnGameSaveButtonClicked()
    {
        StartCoroutine(SaveCharacterData());
    }

    // ĳ���� ���� ������ ����
    public IEnumerator SaveCharacterData()
    {

        accessToken = DataManager.instance.AccountData.accessToken;

        // ĳ���� �����͸� JSON���� ��ȯ
        string json = JsonUtility.ToJson(DataManager.instance.CharacterData);

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(serverUrl + "/save", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.SetRequestHeader("Authorization", "Bearer " + accessToken);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Debug.Log("���忡 �����Ͽ����ϴ�.");
            }
            else
            {
                Debug.Log("ĳ���� ���� ������ �����Ͽ����ϴ�.");
            }
        }
    }
}
