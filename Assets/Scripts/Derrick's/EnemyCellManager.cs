using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCellManager : MonoBehaviour
{
    public List<GameObject> referencedGameObjects;

    public List<GameObject> targetGameObjects;

    public AudioClip destructionSound;
    private AudioSource audioSource;

    private int destroyedCount = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = destructionSound;

        foreach (var obj in referencedGameObjects)
        {
            if (obj != null)
            {
                var watcher = obj.AddComponent<DestroyWatcher>();
                watcher.OnDestroyed += HandleReferencedDestroyed;
            }
        }
    }

    private void HandleReferencedDestroyed(GameObject destroyedObject)
    {
        destroyedCount++;
        if (destroyedCount >= 3)
        {
            StartCoroutine(DestroyTargetObjectsOneByOne());
        }
    }

    private IEnumerator DestroyTargetObjectsOneByOne()
    {
        foreach (var obj in targetGameObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log("DestroyingCells");

                if (audioSource != null && destructionSound != null)
                {
                    audioSource.Play();
                }

                yield return new WaitForSeconds(0.5f); 
            }
        }
    }
}

public class DestroyWatcher : MonoBehaviour
{
    public delegate void DestroyedAction(GameObject destroyedObject);
    public event DestroyedAction OnDestroyed;

    void OnDestroy()
    {
        OnDestroyed?.Invoke(gameObject);
    }
}