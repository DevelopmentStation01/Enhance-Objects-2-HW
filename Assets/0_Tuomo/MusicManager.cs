using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager musicManager; //Access musicManager outside of class
    [SerializeField] private AudioSource musicFxObj; //AudioSource object reference that will be used to spawn new audio objects

    [Header("Music")]
    [SerializeField] private AudioClip menuSong;
    [SerializeField] private AudioClip[] gameplaySongs; //Array of gameplay songs that get picked at random

    private AudioSource currentSong;
    public float musicVolume = 0.5f;


    private void Awake()
    {
        if (musicManager == null)
        {
            musicManager = this;
            DontDestroyOnLoad(gameObject);
        }
        currentSong = StartLoopedSong(menuSong, musicVolume);
    }

    public AudioSource StartLoopedSong(AudioClip clip, float volume)
    {
        //Spawn music gameobject
        AudioSource audioSource = Instantiate(musicFxObj, musicFxObj.transform.position, Quaternion.identity);

        //assign audio clip
        audioSource.clip = clip;
        
        //assign volume
        audioSource.volume = volume;

        //Loop song
        audioSource.loop = true;

        //play song
        audioSource.Play();

        //Update current song
        return audioSource;
    }

    public void TransitionNextSong(AudioClip clip)
    {
        //Changes music to input clip
        Destroy(currentSong.gameObject);
        currentSong = StartLoopedSong(clip, musicVolume);
    }

    public void MenuToGame()
    {
        //Transition from menu to randomly selected gameplay music
        TransitionNextSong(gameplaySongs[Random.Range(0,gameplaySongs.Length)]);
    }

    public void GameToMenu()
    {
        //Transition from gameplay music to menu music
        TransitionNextSong(menuSong);
    }
}
