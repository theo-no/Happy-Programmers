using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CharacterSaveLoadManager : MonoBehaviour {

    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/character";
    public GameObject checkWindow;
    public TextMeshProUGUI windowText;
    public GameObject uiManagerPrefab;
    string accessToken;

    // ĳ���� ���� ������ ����
    public IEnumerator SaveCharacterData()
    {
        // ĳ���� �����͸� JSON���� ��ȯ
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
                windowText.text = "���忡 �����Ͽ����ϴ�.";
                checkWindow.SetActive(true);
            }
            else
            {
                windowText.text = "ĳ���� ���� ������ �����Ͽ����ϴ�.";
                checkWindow.SetActive(true);
            }
        }
    }

    // ĳ���� �������� �ҷ�����
    public IEnumerator LoadCharacterData()
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
                windowText.text = "ĳ���͸� ���Ӱ� �������ּ���!";
                checkWindow.SetActive(true);

            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log(response);
                CharacterData character = JsonUtility.FromJson<CharacterData>(response);
                Debug.Log(character);
                DataManager.instance.CharacterData = character;
                // UIManager �ν��Ͻ� ����
                if (UIManager.instance == null)
                {
                    Instantiate(uiManagerPrefab);
                }
                SceneManager.LoadScene(DataManager.instance.CharacterData.savepoint);
                Debug.Log("ĳ���� �ҷ����� ����");

            }
        }
    }
 }