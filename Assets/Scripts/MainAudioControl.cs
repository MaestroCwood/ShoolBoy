
using System.Collections;
using UnityEngine;

public class MainAudioControl : MonoBehaviour
{
    public AudioClip[] audioClips; // Массив аудиоклипов
    public Vector3[] speakerPositions; // Позиции громкоговорителей
    bool isPlayingSound = false;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        // Пример: воспроизведение звука на всех позициях через 1 секунду

        Invoke("PlaySoundOnAllPositions", 10f);

    }

 

    void PlaySoundOnAllPositions()
    {
        StartCoroutine(StartSoundRepeat());
    }

    public void PlayDangerAudioAtPositions(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < audioClips.Length)
        {
            foreach (Vector3 position in speakerPositions)
            {
                AudioSource.PlayClipAtPoint(audioClips[clipIndex], position);
            }
        }
        else
        {
            Debug.LogError("Индекс аудиоклипа вне диапазона!");
        }
    }

    public IEnumerator StartSoundRepeat()
    {
        while (true)
        {
            if (!isPlayingSound && !audioSource.isPlaying)
            {
                isPlayingSound = true;
                PlayDangerAudioAtPositions(0);
                yield return new WaitForSeconds(35f);
                isPlayingSound = false;
            }

            yield return null;
        }
        
    }
}

