using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip idleMusic;  // �⺻ IDLE BGM
    public AudioClip chaseMusic; // �߰� BGM
    private AudioSource audioSource;
    private bool isChasing = false; // ���� ���� ���� ����
    public GameObject PausePanel;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayIdleMusic(); // ó������ IDLE ������ ���
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
        }
        bool newIsChasing = ZombieManager.Instance.chasing;
        if (newIsChasing != isChasing) // ���°� ����� ���� ó��
        {
            isChasing = newIsChasing;
            SwitchBGM(); // BGM ��ȯ
        }
    }

    private void SwitchBGM()
    {
        if (isChasing)
        {
            PlayChaseMusic();
        }
        else
        {
            PlayIdleMusic();
        }
    }

    private void PlayIdleMusic()
    {
        audioSource.clip = idleMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void PlayChaseMusic()
    {
        audioSource.clip = chaseMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}
