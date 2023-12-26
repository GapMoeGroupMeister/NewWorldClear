using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingObject
{
    public string objectName;
    public int objectAmount;
    public GameObject objectPrefab;
    public Transform poolLocation;
}

public class PoolManager : MonoSingleton<PoolManager>
{
    public Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
    public PoolingObjectList poolingObjectList;
    public bool readyPool = false;

    private void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);


        foreach (PoolingObject @object in poolingObjectList.poolingObjects)
        {
            pools.Add(@object.objectName, new Queue<GameObject>());

            CreateObjectInPool(@object.objectName, @object.objectAmount, @object.objectPrefab, @object.poolLocation);
        }

        readyPool = true;
    }

    private void CreateObjectInPool(string objectName, int count, GameObject objectPrefab, Transform location)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObj = Instantiate(objectPrefab, location);
            newObj.SetActive(false);
            pools[objectName].Enqueue(newObj);

        }
    }

    private GameObject CreateObject(string objectName, Transform location = null)
    {
        foreach (PoolingObject @object in poolingObjectList.poolingObjects)
        {
            if (@object.objectName == objectName)
            {
                GameObject newObj = Instantiate(@object.objectPrefab, location);
                return newObj;
            }
        }

        return null;
    }

    public GameObject GetObject(string objectName)
    {
        if (!pools.ContainsKey(objectName)) return null;

        if (pools[objectName].Count > 0)
        {
            GameObject outObj = pools[objectName].Dequeue();
            outObj.SetActive(true);
            return outObj;
        }
        else
        {
            GameObject outObj = CreateObject(objectName);
            outObj.SetActive(true);
            return outObj;
        }
    }

    public GameObject GetObject(string objectName, Vector3 position)
    {
        if (!pools.ContainsKey(objectName)) return null;

        if (pools[objectName].Count > 0)
        {
            GameObject outObj = pools[objectName].Dequeue();
            outObj.transform.position = position;
            outObj.SetActive(true);
            return outObj;
        }
        else
        {
            GameObject outObj = CreateObject(objectName);
            outObj.transform.position = position;
            outObj.SetActive(true);
            return outObj;
        }

    }

    public GameObject GetObject(string objectName, Vector3 position, Transform parent)
    {
        if (!pools.ContainsKey(objectName)) return null;

        if (pools[objectName].Count > 0)
        {
            GameObject outObj = pools[objectName].Dequeue();
            outObj.transform.SetParent(parent);
            outObj.transform.position = position;
            outObj.SetActive(true);
            return outObj;
        }
        else
        {
            GameObject outObj = CreateObject(objectName, parent);
            outObj.transform.SetParent(parent);
            outObj.transform.position = position;
            outObj.SetActive(true);
            return outObj;
        }
    }

    public GameObject GetObjectUI(string objectName, Vector3 position, Transform parent)
    {
        if (!pools.ContainsKey(objectName)) return null;

        if (pools[objectName].Count > 0)
        {
            GameObject outObj = pools[objectName].Dequeue();
            outObj.transform.SetParent(parent);
            RectTransform rect = outObj.GetComponent<RectTransform>();
            rect.anchoredPosition3D = position;
            outObj.SetActive(true);
            return outObj;
        }
        else
        {
            GameObject outObj = CreateObject(objectName, parent);
            outObj.transform.SetParent(parent);
            RectTransform rect = outObj.GetComponent<RectTransform>();
            rect.anchoredPosition3D = position;
            outObj.SetActive(true);
            return outObj;
        }
    }

    public void ReturnObject(string objectName, GameObject inObj)
    {
        inObj.SetActive(false);
        inObj.transform.position = Vector3.zero;

        pools[objectName].Enqueue(inObj);
    }
}
