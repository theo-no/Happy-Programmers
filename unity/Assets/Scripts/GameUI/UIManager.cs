using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject gameQuest;
    public GameObject gameSave;
    public GameObject gameSetting;
    public GameObject gameInventory;

    private void Awake()
    {
        #region �̱���
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        #endregion
    }

    void Update()
    {
        //"I" Ű�� ������ �κ��丮 â Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.I))
        {
            instance.gameInventory.SetActive(!instance.gameInventory.activeSelf);

            // �ٸ� â���� ���� �ִٸ� �ݱ�
            if (instance.gameQuest.activeSelf)
                instance.gameQuest.SetActive(false);

            if (instance.gameSetting.activeSelf)
                instance.gameSetting.SetActive(false);

            if (instance.gameSave.activeSelf)
                instance.gameSave.SetActive(false);
        }

        // "Q" Ű�� ������ ����Ʈ â Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            instance.gameQuest.SetActive(!instance.gameQuest.activeSelf);

            // �ٸ� â���� ���� �ִٸ� �ݱ�
            if (instance.gameInventory.activeSelf)
                instance.gameInventory.SetActive(false);

            if (instance.gameSetting.activeSelf)
                instance.gameSetting.SetActive(false);

            if (instance.gameSave.activeSelf)
                instance.gameSave.SetActive(false);
        }

        // "ESC" Ű�� ������ ���� â Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            instance.gameSetting.SetActive(!instance.gameSetting.activeSelf);

            // �ٸ� â���� ���� �ִٸ� �ݱ�
            if (instance.gameInventory.activeSelf)
                instance.gameInventory.SetActive(false);

            if (instance.gameQuest.activeSelf)
                instance.gameQuest.SetActive(false);

            if (instance.gameSave.activeSelf)
                instance.gameSave.SetActive(false);
        }

        // "F5" Ű�� ������ ���� â Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.F5))
        {
            instance.gameSave.SetActive(!instance.gameSave.activeSelf);

            // �ٸ� â���� ���� �ִٸ� �ݱ�
            if (instance.gameInventory.activeSelf)
                instance.gameInventory.SetActive(false);

            if (instance.gameQuest.activeSelf)
                instance.gameQuest.SetActive(false);

            if (instance.gameSetting.activeSelf)
                instance.gameSetting.SetActive(false);
        }
    }
}

