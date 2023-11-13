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
        public string characterName;
        public string gender;
    }


    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/character";

    public TMP_InputField characterName;
    public ToggleGroup gender;
    public bool characterNameCheck;

    public void OnCharacterNameCheckButtonClicked()
    {
        string InputCharactername = characterName.text;
        if (string.IsNullOrEmpty(InputCharactername))
        {
            Debug.LogError("ĳ���� �̸��� �Է����ּ���");
            return;
        }
        StartCoroutine(CheckNewCharacterName(InputCharactername));
    }

    public void OnCreateNewCharacterButtonClicked()
    {
        if (!characterNameCheck)
        {
            Debug.Log("ĳ���� �̸� �ߺ�Ȯ���� ���ּ���");
            return;
        }

        NewCharacter newCharacter = new NewCharacter();
        newCharacter.characterName = characterName.text;
        string activatedGender = gender.ActiveToggles().FirstOrDefault().GetComponentInChildren<Text>().text;
        if (activatedGender == "����")
        {
            newCharacter.gender = "F";
        }
        else
        {
            newCharacter.gender = "M";
        }
        Debug.Log(newCharacter.characterName);
        Debug.Log(newCharacter.gender);
        StartCoroutine(CreateNewCharacter(newCharacter));
    }

    IEnumerator CheckNewCharacterName(string inputCharacterName)
    {
        string accessToken = DataManager.instance.AccountData.accessToken;

        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(new { name = inputCharacterName });

        // �Ķ���� ���� URL�� ���Խ�Ű��
        string urlWithParameter = serverUrl + "/check/nickname/" + UnityWebRequest.EscapeURL(inputCharacterName);

        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(urlWithParameter, "POST"))
        {
            www.SetRequestHeader("Authorization", "Bearer " + accessToken);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // �г��� üũ ���н�
                Debug.Log(www.error);
                Debug.Log("ĳ���� �̸� üũ ����");
            }
            else
            {
                // �г��� üũ ������ ó���ϴ� ���� �ۼ���
                characterNameCheck = true;
                Debug.Log("ĳ���� �̸� üũ ����");
            }
        }
    }

    IEnumerator CreateNewCharacter(NewCharacter newCharacter)
    {
        string accessToken = DataManager.instance.AccountData.accessToken;

        CharacterData characterData = new CharacterData();

        characterData.name = newCharacter.characterName;
        characterData.gender = newCharacter.gender;
        characterData.exp = 0;
        characterData.level = 1;
        characterData.savepoint = "LobbyMap";
        characterData.point = 0;
        characterData.storyProgress = 0;

        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(characterData);
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
                Debug.Log("ĳ���� ���� ����");
            }
            else
            {
                // ĳ���� ���� ������
                float delayTime = 2.0f; // ������ �ð� ���� (��: 2��)
                yield return new WaitForSeconds(delayTime);

                CharacterSaveLoadManager characterSaveLoadManager = new CharacterSaveLoadManager();
                characterSaveLoadManager.LoadCharacterData();
                Debug.Log("ĳ���� ���� ����");
                SceneManager.LoadScene("LobbyMap"); // ���� �̵�����
            }
        }
    }
}