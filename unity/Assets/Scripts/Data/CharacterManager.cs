using System.Collections;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterManager : MonoBehaviour
{
    [System.Serializable]
    private class NewCharacter
    {
        public string characterName;
        public char gender;
    }


    string serverUrl = "http://k9d210.p.ssafy.io:8081/api/character";

    public TMP_InputField characterName;
    public ToggleGroup gender;
    
    void Start()
    {
        characterName = transform.Find("Name").Find("CharacterName").GetComponent<TMP_InputField>();
        gender = transform.Find("Gender").Find("RadioBtnGroup").GetComponent<ToggleGroup>();
    }
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
        NewCharacter newCharacter = new NewCharacter();
        string activatedGender = gender.ActiveToggles().FirstOrDefault().GetComponentInChildren<Text>().text;
        if (activatedGender == "����")
        {
            newCharacter.gender = char.Parse("F");
        }
        else
        {
            newCharacter.gender = char.Parse("M");
        }
        Debug.Log(newCharacter.characterName);
        Debug.Log(newCharacter.gender);
        StartCoroutine(CreateNewCharacter(newCharacter));
    }

    IEnumerator CheckNewCharacterName(string InputCharactername)
    {

        // JSON���� ��ȯ�ϱ�
        string json = JsonUtility.ToJson(new { name = InputCharactername });


        // POST ��û ������
        using (UnityWebRequest www = new UnityWebRequest(serverUrl + "/name/check", "POST"))
        {
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
                Debug.Log("ĳ���� �̸� üũ ����");
            }
        }
    }

    IEnumerator CreateNewCharacter(NewCharacter newCharacter)
    {
        string accessToken = DataManager.instance.AccountData.accessToken;
        Debug.Log(accessToken);

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
                Debug.Log("ĳ���� ���� ����");
            }
            else
            {
                // ĳ���� ���� ������
                Debug.Log("ĳ���� ���� ����");
                SceneManager.LoadScene("GameLogin"); // ���� �̵�����
            }
        }
    }
}
