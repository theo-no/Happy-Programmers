using System.Collections;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewCharacterManager : MonoBehaviour
{
    [System.Serializable]
    private class NewCharacter
    {
        public string name;
        public string gender;
        public string savepoint = "LobbyMap";
        public string imgPath = "images/character/character_m.png";
    }


    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/character";

    public TMP_InputField characterName;
    public ToggleGroup gender;
    public bool characterNameCheck;
    public GameObject checkWindow;
    public TextMeshProUGUI windowText;
    public CharacterLoadManager characterLoadManager;

    public void OnCharacterNameCheckButtonClicked()
    {
        string InputCharactername = characterName.text;
        if (string.IsNullOrEmpty(InputCharactername))
        {
            windowText.text = "ĳ���� �̸��� �Է����ּ���!";
            checkWindow.SetActive(true);
            return;
        }
        StartCoroutine(CheckNewCharacterName(InputCharactername));
    }

    public void OnCreateNewCharacterButtonClicked()
    {
        if (!characterNameCheck)
        {
            windowText.text = "ĳ���� �̸� �ߺ�Ȯ���� ���ּ���";
            checkWindow.SetActive(true);
            return;
        }

        NewCharacter newCharacter = new NewCharacter();
        newCharacter.name = characterName.text;
        string activatedGender = gender.ActiveToggles().FirstOrDefault().GetComponentInChildren<Text>().text;
        if (activatedGender == "����")
        {
            newCharacter.gender = "F";
        }
        else
        {
            newCharacter.gender = "M";
        }
        StartCoroutine(CreateNewCharacter(newCharacter));
    }

    IEnumerator CheckNewCharacterName(string inputCharacterName)
    {
        string accessToken = DataManager.instance.AccountData.accessToken;

        // �Ķ���� ���� URL�� ���Խ�Ű��
        string urlWithParameter = serverUrl + "/check/nickname/" + UnityWebRequest.EscapeURL(inputCharacterName);

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(urlWithParameter, "GET"))
        {
            www.SetRequestHeader("Authorization", "Bearer " + accessToken);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // �г��� üũ ���н�
                Debug.Log(www.error);
                windowText.text = "�ߺ��� ĳ���� �̸��Դϴ�.";
                checkWindow.SetActive(true);
            }
            else
            {
                // �г��� üũ ������ ó���ϴ� ���� �ۼ���
                characterNameCheck = true;
                windowText.text = "���� �� �ִ� ĳ���� �̸��Դϴ�.";
                checkWindow.SetActive(true);
            }
        }
    }

    IEnumerator CreateNewCharacter(NewCharacter newCharacter)
    {
        string accessToken = DataManager.instance.AccountData.accessToken;

        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(newCharacter);
        Debug.Log(json);

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(serverUrl + "/save", "POST"))
        {
            www.SetRequestHeader("Authorization", "Bearer " + accessToken);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // ĳ���� ���� ���н�
                Debug.Log(www.error);
                windowText.text = "ĳ���� ������ �����Ͽ����ϴ�.";
                checkWindow.SetActive(true);
            }
            else
            {
                // ĳ���� ���� ������
                windowText.text = "ĳ���� ������ �����Ͽ����ϴ�. �� ������ �����մϴ�.";
                checkWindow.SetActive(true);

                float delayTime = 2.0f; // ������ �ð� ���� (��: 2��)
                yield return new WaitForSeconds(delayTime);
                characterLoadManager.LoadCharacterData();
                
                SceneManager.LoadScene("LobbyMap"); // ���� �̵�����
            }
        }
    }
}