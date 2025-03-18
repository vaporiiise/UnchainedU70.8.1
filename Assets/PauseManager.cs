using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // Assign Pause Canvas in Inspector
    public GameObject player;    // Assign Player GameObject in Inspector
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);

        // Disable/Enable player movement
        if (player != null)
        {
            player.GetComponent<Tilemovement>().enabled = !isPaused;
        }

        // Disable/Enable all pushable objects
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("ObjToPush");
        foreach (GameObject box in boxes)
        {
            if (box.GetComponent<Push>() != null)
            {
                box.GetComponent<Push>().enabled = !isPaused;
            }
        }
    }
}
