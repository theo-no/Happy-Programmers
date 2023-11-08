using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    string serverUrl = "http://k9d210.p.ssafy.io:8081/save-file";

    public Button saveButton;
    public Button loadButton;

    
    // �̱��� ���� ����
    public static ServerManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
    }

    private void Start()
    {
        // ��ư�� onClick �̺�Ʈ�� �޼ҵ带 �Ҵ�
        saveButton.onClick.AddListener(() => StartCoroutine(SaveCharacterData("accountId")));
        loadButton.onClick.AddListener(() => StartCoroutine(LoadCharacterData("accountId")));
    }

    public IEnumerator LoadCharacterData(string accountId)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(serverUrl + "/" + accountId))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                DataManager.instance.characterData = JsonUtility.FromJson<CharacterData>(json);

                Debug.Log("ĳ���� ���� �ҷ����⸦ �����Ͽ����ϴ�.");
            }
        }
    }

    public IEnumerator SaveCharacterData(string accountId)
    {
        // ĳ���� �����͸� JSON���� ��ȯ
        string json = JsonUtility.ToJson(DataManager.instance.characterData);

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(serverUrl, "POST"))
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
                Debug.Log("ĳ���� ���� ������ �����Ͽ����ϴ�.");
            }
        }
    }
}
