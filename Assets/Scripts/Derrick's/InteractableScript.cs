using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableTrigger : MonoBehaviour
{
    public Transform player;
    public Transform triggerZone;
    public float interactionRadius = 2f;
    public Transform promptObject;
    public string nextSceneName;
    public float scaleSpeed = 5f;
    public Vector3 targetScale = new Vector3(1, 1, 1);
    public AudioClip popUpSound;

    private bool canTriggerPopup = true;
    private bool isPromptVisible = false;
    private Coroutine scalingCoroutine;
    private AudioSource audioSource;

    void Start()
    {
        if (promptObject != null) promptObject.localScale = Vector3.zero;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector2.Distance(player.position, triggerZone.position);

        if (distance >= interactionRadius) canTriggerPopup = true;

        if (distance < interactionRadius && canTriggerPopup && Input.GetKeyDown(KeyCode.E) && !isPromptVisible)
        {
            ShowPrompt();
        }

        if (isPromptVisible)
        {
            if (Input.GetKeyDown(KeyCode.F)) SceneManager.LoadScene(nextSceneName);
            else if (Input.GetKeyDown(KeyCode.C)) HidePrompt();
        }
    }

    void ShowPrompt()
    {
        if (promptObject != null && !isPromptVisible)
        {
            isPromptVisible = true;
            if (scalingCoroutine != null) StopCoroutine(scalingCoroutine);
            scalingCoroutine = StartCoroutine(ScaleObject(promptObject, Vector3.zero, targetScale));

            if (popUpSound != null) audioSource.PlayOneShot(popUpSound);
        }
    }

    void HidePrompt()
    {
        if (promptObject != null && isPromptVisible)
        {
            isPromptVisible = false;
            canTriggerPopup = false;
            if (scalingCoroutine != null) StopCoroutine(scalingCoroutine);
            scalingCoroutine = StartCoroutine(ScaleObject(promptObject, targetScale, Vector3.zero));
        }
    }

    IEnumerator ScaleObject(Transform obj, Vector3 startScale, Vector3 endScale)
    {
        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * scaleSpeed;
            obj.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }
        obj.localScale = endScale;
    }

    void OnDrawGizmos()
    {
        if (triggerZone != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(triggerZone.position, interactionRadius);
        }
    }
}
