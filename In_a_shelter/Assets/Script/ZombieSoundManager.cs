using System.Collections;
using UnityEngine;

public class ZombieSoundManager : MonoBehaviour
{
    public AudioClip[] defaultZombieSounds; // �Ϲ� ���� ���� �Ҹ� �迭
    public AudioClip[] chaseZombieSounds; // ���� ���� ���� �Ҹ� �迭
    private AudioSource audioSource;
    private bool isChasing = false; // ���� ���� ����
    private bool currentSoundState; // ���� ��� ���� ����

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSoundState = isChasing;
        StartCoroutine(PlayDefaultSoundsCoroutine());
    }

    void Update()
    {
        // ZombieManager�� ���¿� ��ġ�ϵ��� ������Ʈ
        bool newChasingState = ZombieManager.Instance.chasing;

        // ���� ������ ���� ��쿡�� �ڷ�ƾ�� �ٽ� ����
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
