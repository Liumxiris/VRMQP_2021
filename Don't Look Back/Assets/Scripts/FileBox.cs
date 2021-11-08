using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileBox : MonoBehaviour
{
    [SerializeField] Transform FileCreationLocation;
    [SerializeField] GameObject[] Files;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateFile() {
        List<GameObject> files = new List<GameObject>(GameObject.FindGameObjectsWithTag("File"));
        if (files.Count != 0) return;

        Instantiate(Files[Random.Range(0, Files.Length)], FileCreationLocation.position, Quaternion.Euler(Vector3.zero));
    }
    
}
