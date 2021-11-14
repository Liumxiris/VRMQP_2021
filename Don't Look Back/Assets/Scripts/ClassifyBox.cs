using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassifyBox : MonoBehaviour
{
    public int Type;

    [SerializeField]
    Transform FilePosition;

    [SerializeField]
    int maxNumFiles = 9;
    int numFiles = 0;


    public void StashFile(GameObject file)
    {
        if (numFiles > maxNumFiles)
        {
            Destroy(file);
        }
        else
        {
            // Destroy all component except Transform
            foreach(Component co in file.GetComponents(typeof(Component))){
                if (co.GetType() != typeof(Transform))
                {
                    Destroy(co);
                }
            }
            file.transform.parent = transform;
            file.transform.rotation = FilePosition.transform.rotation;
            file.transform.localPosition = new Vector3(FilePosition.localPosition.x + numFiles * 0.01f, FilePosition.localPosition.y, FilePosition.localPosition.z);
            file.tag = "ClassifiedFile";
            numFiles++;
        }
    }
}
