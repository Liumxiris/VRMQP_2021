using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileBox : MonoBehaviour
{
    [SerializeField] Transform FileCreationLocation;
    [SerializeField] GameObject FileSeal1;
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
        Instantiate(FileSeal1, FileCreationLocation.position, Quaternion.identity);
        Debug.Log("Selected, Creating file");
    }
    
}
