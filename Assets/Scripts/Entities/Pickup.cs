using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Pickup : MonoBehaviour
{
    public virtual void OnPicked()
    {
        Destroy(gameObject);
    }
}
