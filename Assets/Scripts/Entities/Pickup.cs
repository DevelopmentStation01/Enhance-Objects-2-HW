using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip onPickClip; //Sound implementation for pickups
    public virtual void OnPicked()
    {
        SoundManager.soundManager.PlaySound(onPickClip, transform, 0.4f);
        Destroy(gameObject);
    }
}
