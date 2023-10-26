using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferScences : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�

    public void TranferScene()
    {
        if (transferMapName == "GameSetting")
        {
            BackButton backButton = new BackButton();
            backButton.SavePreviousSceneName();
        }

        SceneManager.LoadScene(transferMapName);
    }
}
