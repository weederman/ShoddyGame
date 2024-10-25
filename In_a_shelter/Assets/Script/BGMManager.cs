using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip idleMusic;  // 기본 IDLE BGM
    public AudioClip chaseMusic; // 추격 BGM
    private AudioSource audioSource;
    private bool isChasing = false; // 현재 추적 상태 여부

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayIdleMusic(); // 처음에는 IDLE 음악을 재생
    }

    void Update()
    {
        bool newIsChasing = ZombieManager.Instance.chasing;
        if (newIsChasing != isChasing) // 상태가 변경될 때만 처리
        {
            isChasing = newIsChasing;
            SwitchBGM(); // BGM 전환
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
