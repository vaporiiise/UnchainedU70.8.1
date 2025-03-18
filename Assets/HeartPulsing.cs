using UnityEngine;

public class HeartPulsing : MonoBehaviour
{
    private GameManager gameManager;
    private NHPlayerHealth playerHealth;
    public AudioSource audioSource;

    [Header("Health Settings")]
    public int warningHealth = 50;   // Adjust in Inspector
    public int criticalHealth = 25;  // Adjust in Inspector

    [Header("Audio Settings")]
    public float heartbeatVolume = 1f; // Adjust in Inspector (0 - 1)
    public float slowHeartbeatSpeed = 1f; // Adjust in Inspector
    public float fastHeartbeatSpeed = 1.5f; // Adjust in Inspector

    [Header("Heartbeat Sounds")]
    public AudioClip slowHeartbeatClip;
    public AudioClip fastHeartbeatClip;

    private bool isSlowPlaying = false;
    private bool isFastPlaying = false;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager instance not found!");
            return;
        }

        playerHealth = FindObjectOfType<NHPlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("NHPlayerHealth script not found!");
            return;
        }

        // Ensure volume is set
        audioSource.volume = heartbeatVolume;
    }

    private void Update()
    {
        if (gameManager == null || playerHealth == null)
            return;

        int currentHealth = gameManager.savedPlayerHealth; // Get current health from GameManager

        if (currentHealth > warningHealth)
        {
            StopHeartbeat();
        }
        else if (currentHealth > criticalHealth)
        {
            PlayHeartbeat(slowHeartbeatClip, ref isSlowPlaying, ref isFastPlaying, slowHeartbeatSpeed);
        }
        else if (currentHealth > 0)
        {
            PlayHeartbeat(fastHeartbeatClip, ref isFastPlaying, ref isSlowPlaying, fastHeartbeatSpeed);
        }
        else
        {
            StopHeartbeat();
        }
    }

    private void PlayHeartbeat(AudioClip clip, ref bool isPlaying, ref bool stopOther, float pitch)
    {
        if (!isPlaying || audioSource.clip != clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.pitch = pitch; //the speed to adjust
            audioSource.volume = heartbeatVolume;
            audioSource.Play();
            isPlaying = true;
            stopOther = false;
        }
    }

    private void StopHeartbeat()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            isSlowPlaying = false;
            isFastPlaying = false;
        }
    }
}
