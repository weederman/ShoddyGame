using System.Collections;
using UnityEngine;

public class ZombieSoundManager : MonoBehaviour
{
    public AudioClip[] defaultZombieSounds; // 일반 상태 좀비 소리 배열
    public AudioClip[] chaseZombieSounds; // 추적 상태 좀비 소리 배열
    private AudioSource audioSource;
    private bool isChasing = false; // 현재 추적 상태
    private bool currentSoundState; // 사운드 재생 상태 추적

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSoundState = isChasing;
        StartCoroutine(PlayDefaultSoundsCoroutine());
    }

    void Update()
    {
        // ZombieManager의 상태와 일치하도록 업데이트
        bool newChasingState = ZombieManager.Instance.chasing;

        // 상태 변경이 있을 경우에만 코루틴을 다시 시작
        if (newChasingState != currentSoundState)
        {
            currentSoundState = newChasingState;
            StopAllCoroutines();
            if (currentSoundState)
            {
                StartCoroutine(PlayChaseSoundsCoroutine());
            }
            else
            {
                StartCoroutine(PlayDefaultSoundsCoroutine());
            }
        }
    }

    IEnumerator PlayDefaultSoundsCoroutine()
    {
        while (!currentSoundState)
        {
            audioSource.PlayOneShot(defaultZombieSounds[Random.Range(0, defaultZombieSounds.Length)]);
            yield return new WaitForSeconds(Random.Range(4f, 6f));
        }
    }

    IEnumerator PlayChaseSoundsCoroutine()
    {
        while (currentSoundState)
        {
            audioSource.PlayOneShot(chaseZombieSounds[Random.Range(0, chaseZombieSounds.Length)]);
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }
}
