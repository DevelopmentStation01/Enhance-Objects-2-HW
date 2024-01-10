using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackExample : MonoBehaviour
{
    public Stack<GameObject> stack = new Stack<GameObject>();
    public GameObject prefab;
    private GameObject tempObj;

    void Start()
    {
        
    }

    void Update()
    {
        //Push element to the stack
        if (Input.GetKeyDown(KeyCode.A))
        {
            tempObj = Instantiate(prefab, transform);
            tempObj.transform.position = new Vector2(0, stack.Count); //calculate Y position based on the stack elements

            tempObj.name = $"Stacked - {stack.Count}";
            tempObj.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            stack.Push(tempObj);
            Debug.Log($"Pushed - {tempObj.name}");
        }

        //remove TOP element from the stack not destroying
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log($"Popped from the stack: {stack.Pop().name}");
        }

        //remove TOP element from the stack and destroy
        if (Input.GetKeyDown(KeyCode.X))
        {
            var remObj = stack.Pop();
            Destroy(remObj);
            Debug.Log($"Popped from the stack: {remObj}");
        }

        //Peek the TOP element of the stack
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"Object at the top is: {stack.Peek().name}");
        }
    }
}
