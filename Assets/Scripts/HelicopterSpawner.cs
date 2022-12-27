using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private GameObject helicopterPrefab;
    
    private void Start()
    {
        GameManager.GameOverSequence += DestroyIt;
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        if(spawnLocations==null || helicopterPrefab == null)
        {
            Debug.LogError("SpawnLocations or helicopterPrefab null");
            yield return null;
        }
        else
        {
            Vector3 eulerRotation = new Vector3(0,180,0);
            int randomValue;

            while (this.isActiveAndEnabled)
            {
                randomValue = Random.Range(0,spawnLocations.Length);
                Instantiate(helicopterPrefab, spawnLocations[randomValue]);
                yield return CouroutineManager._Instance.WaitFor2Sec();
            }
        }
    }

    private void DestroyIt()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameManager.GameOverSequence -= DestroyIt;
    }
}
