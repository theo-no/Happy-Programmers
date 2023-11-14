using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject gameQuest;
    public GameObject gameSave;
    public GameObject gameSetting;

    private GameObject gameQuestInstance;
    private GameObject gameSaveInstance;
    private GameObject gameSettingInstance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        gameQuestInstance = Instantiate(gameQuest);
        gameSaveInstance = Instantiate(gameSave);
        gameSettingInstance = Instantiate(gameSetting);

        // ó������ ��� ��Ȱ��ȭ�մϴ�.
        gameQuestInstance.SetActive(false);
        gameSaveInstance.SetActive(false);
        gameSettingInstance.SetActive(false);
    }

    void Update()
    {
        // "I" Ű�� ������ �κ��丮 â Ȱ��ȭ/��Ȱ��ȭ
        //if (Input.GetKeyDown(KeyCode.I))
        //{

        //}

        // "Q" Ű�� ������ ����Ʈ â Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameQuestInstance.SetActive(!gameQuestInstance.activeSelf);
        }

        // "ESC" Ű�� ������ ���� â Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameSettingInstance.SetActive(!gameSettingInstance.activeSelf);
        }

        // "CTRL + S" Ű�� ������ ���� â Ȱ��ȭ/��Ȱ��ȭ
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.S))
        {
            gameSaveInstance.SetActive(!gameSaveInstance.activeSelf);
        }
    }
}

