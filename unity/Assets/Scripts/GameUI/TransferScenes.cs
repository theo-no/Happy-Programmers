using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransferScences : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�

    private CharacterMovement thePlayer;

    void Start()
    {
        thePlayer = FindAnyObjectByType<CharacterMovement>(); // ��� ��ü ����
        // GetComponent ���� ��ü
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Character")
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }

    public void TranferScene()
    {
        SceneManager.LoadScene(transferMapName);
    }
}
