using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public static string previousScene;

    public void SavePreviousSceneName()
    {
        // ���� �� ���� ���� �����մϴ�.
        previousScene = SceneManager.GetActiveScene().name;
    }

    public void OnBackButtonClick()
    {
        // ���� ������ ���ư��ϴ�.
        SceneManager.LoadScene(previousScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
