using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRingSpawner : MonoBehaviour
{
    [SerializeField]
    private float minValue = -5f;
    [SerializeField]
    private float maxValue = 5f;

    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private GameObject noRingObj;

    [SerializeField]    
    private float timer = 0f;
    [SerializeField]    
    private float limit = 18f;

    private void Update()
    {
        if(timer < limit)
        {
            timer += Time.deltaTime;

            if(timer >= limit)
            {
                timer = 0f;
                float spawnPosY = Random.Range(minValue, maxValue);
                Instantiate(noRingObj, new Vector3(spawnPos.position.x, spawnPosY, 0f), Quaternion.identity);
            }
        }
    }
}
