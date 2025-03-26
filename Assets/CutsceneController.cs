using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector timeline; // Assign your Timeline
    public GameObject videoUI; // Assign UI Panel with RawImage & VideoPlayer
    public VideoPlayer videoPlayer; // Assign VideoPlayer component
    public string nextSceneName; // Set the name of the next scene

    private void Start()
    {
        timeline.stopped += OnTimelineEnd;
        videoUI.SetActive(false); // Hide video UI initially
    }

    void OnTimelineEnd(PlayableDirector director)
    {
        if (director == timeline)
        {
            PlayVideo();
        }
    }

    void PlayVideo()
    {
        videoUI.SetActive(true); // Show the UI with the video
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName); // Load the next scene
    }
}