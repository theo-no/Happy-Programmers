using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransferScences : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�

    private CharacterMovement thePlayer;
    private FadeManager theFade;

    void Start()
    {
        thePlayer = FindObjectOfType<CharacterMovement>(); // ��� ��ü ����
        // GetComponent ���� ��ü
        theFade = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Character")
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    IEnumerator TransferCoroutine()
    {
        theFade.FadeOut();

        yield return new WaitForSeconds(1f);
        thePlayer.currentMapName = transferMapName;
        SceneManager.LoadScene(transferMapName);
        theFade.FadeIn();
    }

    public void TranferScene()
    {
        SceneManager.LoadScene(transferMapName);
    }
}
