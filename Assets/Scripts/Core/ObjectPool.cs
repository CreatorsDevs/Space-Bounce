using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(5)]
public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObjectPool SharedInstance;
    public List<GameObject> capitalizerObjects;
    public List<GameObject> obstacleObjects;
    public GameObject[] capitalizerToPool;
    public GameObject[] obstacleToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        capitalizerObjects = new List<GameObject>();
        GameObject Capitalizer;
        GameObject Capitalizer1;
        for(int i = 0; i < amountToPool; i++)
        {
            Capitalizer = Instantiate(capitalizerToPool[0]);
            Capitalizer.SetActive(false);
            capitalizerObjects.Add(Capitalizer);

            Capitalizer1 = Instantiate(capitalizerToPool[1]);
            Capitalizer1.SetActive(false);
            capitalizerObjects.Add(Capitalizer1);
        }

        obstacleObjects = new List<GameObject>();
        GameObject Obstacle;
        GameObject Obstacle1;
        for(int i = 0; i < amountToPool; i++)
        {
            Obstacle = Instantiate(obstacleToPool[0]);
            Obstacle.SetActive(false);
            obstacleObjects.Add(Obstacle);

            Obstacle1 = Instantiate(obstacleToPool[1]);
            Obstacle1.SetActive(false);
            obstacleObjects.Add(Obstacle1);
        }
    }

    public GameObject GetCapitalizedObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!capitalizerObjects[i].activeInHierarchy)
            {
                return capitalizerObjects[i];
            }
        }
        return null;
    }

    public GameObject GetObstacleObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!obstacleObjects[i].activeInHierarchy)
            {
                return obstacleObjects[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
