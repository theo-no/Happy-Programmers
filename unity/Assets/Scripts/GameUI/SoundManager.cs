using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public List<SceneBGM> sceneBGMs;
    public AudioSource musicSource;
    public Slider volumeSlider;

    [System.Serializable]
    public class SceneBGM
    {
        public string sceneName; // ���� �̸�
        public AudioClip bgm; // ���� �ش��ϴ� �������
    }

    private void Start()
    {
       if (volumeSlider != null && musicSource != null)
        {
            // ����� ������ ������ �ҷ��ͼ� ����, ������ �⺻ ������ ����
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            volumeSlider.value = savedVolume;
            SetMusicVolume(savedVolume);
        }

        PlayBGM();
    }

    private void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }

    public void OnVolumeSliderChanged()
    {
        if (volumeSlider != null)
        {
            SetMusicVolume(volumeSlider.value);
            // ������ ������ ����
            PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
        }
    }

    private void PlayBGM()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // ���� ���� �ش��ϴ� ��������� ã�� ���
        SceneBGM currentSceneBGM = sceneBGMs.Find(sceneBGM => sceneBGM.sceneName == currentSceneName);

        if (currentSceneBGM != null && musicSource != null)
        {
            musicSource.clip = currentSceneBGM.bgm;
            musicSource.Play();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
