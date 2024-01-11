using UnityEngine;

public class RandomObjectPicker : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToPickFrom;

    private void Start()
    {
        PickRandomObject();
    }

    public void PickRandomObject()
    {
        if (objectsToPickFrom.Length > 0)
        {
            int randomIndex = Random.Range(0, objectsToPickFrom.Length);
            Instantiate(objectsToPickFrom[randomIndex]);
        }
        else
        {
            Debug.LogWarning("No objects assigned to objectsToPickFrom array.");
        }
    }
}

