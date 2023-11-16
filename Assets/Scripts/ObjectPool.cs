using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefabObject;
    public int objectNumberOnStart;

    private List<GameObject> poolObjects = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < objectNumberOnStart; i++)
        {
            CreateNewObject();
        }
    }
    /// <summary>
    /// Instanciate a new Object and add it to the list
    /// </summary>
    /// <returns>Object to pool</returns>
    private GameObject CreateNewObject()
    {
        GameObject gameObject = Instantiate(prefabObject);
        gameObject.SetActive(false); //Deactivate
        poolObjects.Add(gameObject); //Add it to the list
        return gameObject;
    }


    /// <summary>
    /// Get any abvailable object from the list
    /// Create a new one if none are abvailable
    /// </summary>
    /// <returns> Requested Object</returns>
    public GameObject GetGameObject()
    {
        //find any deactivated object
        GameObject gameObject = poolObjects.Find(x => x.activeInHierarchy == false);

        //create a new one if none are found
        if (gameObject == null)
        {
            gameObject = CreateNewObject();
        }
        gameObject.SetActive(true);
        
        return gameObject;
    }
}
