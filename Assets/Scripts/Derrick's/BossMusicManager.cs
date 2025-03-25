using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMusicManager : MonoBehaviour
{
    public static BossMusicManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource dialogueMusicSource;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1.5f;

    private float bgmVolume = 1f; // Default volume

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChanging;

            // Load saved BGM volume
            bgmVolume = PlayerPrefs.GetFloat("BGM_Volume", 1f);

            // Ensure BGM loops
            backgroundMusicSource.loop = true; 

            PlayBackgroundMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanging;
    }

    private void OnSceneChanging(Scene current, Scene next)
    {
        StopAllCoroutines();

        if (next.name == "BossHall") // Make sure BGM will only play in this scene
        {
            PlayBackgroundMusic();
        }
        else
        {
            backgroundMusicSource.Stop();
        }

        //StopAllCoroutines();
        //StartCoroutine(RestartBackgroundMusic());
    }

    private IEnumerator RestartBackgroundMusic()
    {
        yield return new WaitForSeconds(0.5f); // Small delay to allow the scene to load
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        if (!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.volume = bgmVolume;
            backgroundMusicSource.Play();
        }
        else
        {
            backgroundMusicSource.volume = bgmVolume; // Ensure volume is updated
        }
    }

    public void OnDialogueStart()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusic(backgroundMusicSource));
        StartCoroutine(FadeInMusic(dialogueMusicSource));
    }

    public void OnDialogueEnd()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusic(dialogueMusicSource));
        StartCoroutine(FadeInMusic(backgroundMusicSource));
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("BGM_Volume", bgmVolume);
        PlayerPrefs.Save();

        if (backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.volume = bgmVolume;
        }
    }

    private IEnumerator FadeOutMusic(AudioSource source)
    {
        if (source.isPlaying)
        {
            float startVolume = source.volume;
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
                yield return null;
            }
            source.Stop();
            source.volume = startVolume;
        }
    }

    private IEnumerator FadeInMusic(AudioSource source)
    {
        if (!source.isPlaying)
        {
            source.volume = 0f;
            source.Play();
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(0f, bgmVolume, t / fadeDuration);
                yield return null;
            }
        }
    }
}
