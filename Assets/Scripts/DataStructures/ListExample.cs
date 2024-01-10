using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListExample : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> list = new List<GameObject>();

    void Start()
    {
        GameObject tempObj;

        tempObj = Instantiate(prefab, transform);
        tempObj.transform.position = new Vector2(0, 1);
        list.Add(tempObj);

        tempObj = Instantiate(prefab, transform);
        tempObj.transform.position = new Vector2(1, 1);
        list.Add(tempObj);

        tempObj = Instantiate(prefab, transform);
        tempObj.transform.position = new Vector2(2, 1);
        list.Add(tempObj);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            list[Random.Range(0, list.Count)].GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            int index = Random.Range(0, list.Count);
            Destroy(list[index]);
            list.RemoveAt(index);
        }
    }
}
