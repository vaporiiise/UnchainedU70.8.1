using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ResetCutscene : MonoBehaviour
{
    public VideoPlayer cutsceneVideo;  
    public GameObject cutsceneCanvas;
    public GameObject rawImageUnactive;

    private bool sceneResetting = false;

    void Start()
    {
        cutsceneVideo.loopPointReached += EndCutscene; 
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5)) && !sceneResetting)
        {
            rawImageUnactive.SetActive(true);
            GameManager.Instance.SavePlayerPosition(transform.position);
            PlayCutsceneAndResetScene();
        }
    }

    public void PlayCutsceneAndResetScene()
    {
        if (!sceneResetting)
        {
            sceneResetting = true;

            cutsceneCanvas.SetActive(true);

            cutsceneVideo.Play();

            StartCoroutine(ResetSceneWhileCutscenePlays());
        }
    }

    private IEnumerator ResetSceneWhileCutscenePlays()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    
    private void EndCutscene(VideoPlayer vp)
    {
        cutsceneCanvas.SetActive(false);
    }
}
