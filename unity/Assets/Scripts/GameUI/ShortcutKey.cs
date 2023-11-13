using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutKey : MonoBehaviour
{

    public GameObject gameSettingPanel; // ���������� ���� ���� ���� �г�
    public GameObject gameSave; // �������� ���� ���� ���� �г�

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameSettingPanel != null)
            {
                gameSettingPanel.SetActive(!gameSettingPanel.activeSelf);
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            if (gameSave != null)
            {
                gameSave.SetActive(!gameSave.activeSelf);
            }
        } else if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftControl))
        {
            if (gameSave != null)
            {
                gameSave.SetActive(!gameSave.activeSelf);
            }
        }
    }
}
