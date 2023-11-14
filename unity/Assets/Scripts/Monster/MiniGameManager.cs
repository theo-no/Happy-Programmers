using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 5 * 60f;
    public bool isLive;

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150 };
    public float health;
    public float maxHealth = 100;
    public float mp;
    public float maxMp = 100;
    public float nowExp;

    public GameObject uiResult;
    

    

    [Header("# Game Object")]
    public PoolManager pool;
    public CharacterInput player;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;


        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }


    public void GameStart()
    {
        health = maxHealth;
        mp = maxMp;
        isLive = true;

    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());

    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.SetActive(true);
        Stop(); 
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("MiniGameMap");
        
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }

    public void MonstersSkill(int monsterType)
    {
        switch (monsterType)
        {
            case 0:
                return;
            case 1:
                // ���⿡ ���Ÿ� �����̳� ���������� �ǵ��� �־��... �ϴµ�~
                // Set ��¼��� �ִϸ��̼� �ٲٰ�
                // ���� �Լ� �����ͼ� ĳ���Ϳ��� ����� ������
                return;
        }
    }


    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
