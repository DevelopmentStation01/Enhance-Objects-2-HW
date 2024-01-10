using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueExamplle : MonoBehaviour
{
    public Queue<GameObject> queue = new Queue<GameObject>();
    public GameObject prefab;

    private GameObject tempObj;

    Vector2 lastEnqueuePoistion = Vector2.zero;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            tempObj = Instantiate(prefab, transform);
            tempObj.transform.position = new Vector2(lastEnqueuePoistion.x + 1, 0);

            tempObj.name = $"Queue-{queue.Count}";
            tempObj.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            queue.Enqueue(tempObj);
            lastEnqueuePoistion = tempObj.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            var remObj = queue.Dequeue();
            Destroy(remObj);
            Debug.Log($"Dequeued from the queue {remObj}");
        }

        //Peek the next queue object
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"Object at the front is {queue.Peek().name}");
        }
    }
}
