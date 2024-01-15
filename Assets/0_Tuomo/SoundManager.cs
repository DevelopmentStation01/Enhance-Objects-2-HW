using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager; //Access Soundmanager outside of class
    [SerializeField] private AudioSource soundFxObj; //AudioSource object reference that will be used to spawn new audio objects

    [Header("ButtonFx")]
    [SerializeField] private AudioClip buttonOnClick;
    [SerializeField] private AudioClip buttonOnEnter;
    [SerializeField] private AudioClip buttonOnExit;

    [Header("EnemyFx")]
    [SerializeField] private AudioClip[] explosionFx;

    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, Transform spawnTransform, float volume)
    {
        //spawn in gameobject
        AudioSource audioSource = Instantiate(soundFxObj, spawnTransform.position, Quaternion.identity);

        //assign audio clip
        audioSource.clip = clip;
        
        //assign volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //Clip length
        float clipLength = audioSource.clip.length;

        //Destroy clip
        Destroy(audioSource.gameObject, clipLength);
    }

    public AudioSource StartLoopedSound(AudioClip clip, float volume)
    {
        //spawn in gameobject
        AudioSource audioSource = Instantiate(soundFxObj, soundFxObj.transform.position, Quaternion.identity);

        //assign audio clip
        audioSource.clip = clip;

        //assign volume
        audioSource.volume = volume;

        //Loop clip
        audioSource.loop = true;

        //play sound
        audioSource.Play();

        //Update current song
        return audioSource;
    }

    public void PlayRandomExplosion()
    {
        //Plays a random explosion sound from array 
        int randint = Random.Range(0, explosionFx.Length);
        PlaySound(explosionFx[randint], soundFxObj.transform, 0.3f);
    }

    public void PlayerDeath(AudioSource movementFx)
    {
        //Destroys movementFx object. Plays Player explosion sound and changes to menu music.
        Destroy(movementFx.gameObject, 0f);
        PlaySound(explosionFx[1], soundFxObj.transform, 0.6f);
        MusicManager.musicManager.GameToMenu();
    }

    // Event Trigger Sound Effects
    public void PlayButtonClick()
    {
        PlaySound(buttonOnClick, soundFxObj.transform, 0.8f);
        MusicManager.musicManager.MenuToGame();
    }

    public void PlayButtonEnter()
    {
        PlaySound(buttonOnEnter, soundFxObj.transform, 0.8f);
    }
    public void PlayButtonExit()
    {
        PlaySound(buttonOnExit, soundFxObj.transform, 0.8f);
    }
}
