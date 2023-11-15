using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CharacterLoadManager : MonoBehaviour {

    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/character";
    public GameObject checkWindow;
    public TextMeshProUGUI windowText;
    public GameObject uiManagerPrefab;
    public GameObject questManagerPrefab;
    public GameObject cameraManagerPrefab;
    public GameObject characterPrefab;
    string accessToken;

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
                if (CharacterMovement.Instance == null)
                {
                    Instantiate(characterPrefab);
                }
                // QuestManager �ν��Ͻ� ����
                if (QuestManager.instance == null)
                {
                    Instantiate(questManagerPrefab);
                }
                // CamaeraManager �ν��Ͻ� ����
                if (CameraManager.instance == null)
                {
                    Instantiate(cameraManagerPrefab);
                }
                SceneManager.LoadScene(DataManager.instance.CharacterData.savepoint);
                Debug.Log("ĳ���� �ҷ����� ����");

            }
        }
    }
 }