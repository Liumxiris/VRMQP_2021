using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileBox : MonoBehaviour
{
    [SerializeField] Transform FileCreationLocation;
    [SerializeField] GameObject[] Files;
    [SerializeField] float FileSpawnTime;
    [SerializeField] float FileSpawnVariation;

    float SpawnTimeCount = 0;
    float SpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = FileSpawnTime + Random.Range(-FileSpawnVariation, FileSpawnVariation);
        Instantiate(Files[Random.Range(0, Files.Length)], FileCreationLocation.position, FileCreationLocation.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimeCount += Time.deltaTime;
        if(SpawnTimeCount >= FileSpawnTime)
        {
            Instantiate(Files[Random.Range(0, Files.Length)], FileCreationLocation.position, FileCreationLocation.rotation);
            SpawnTime = FileSpawnTime + Random.Range(-2.0f, 2.0f);
            SpawnTimeCount = 0;
        }
    }
    
}
