using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    // The sound effects for the game
    public AudioClip Button, Fall, Flap, Hit, Score;

    // The Audio Listener to play the sound effects
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayButton()
    {
        play(Button);
    }

    public void PlayFall()
    {
        play(Fall);
    }

    public void PlayFlap()
    {
        play(Flap);
    }

    public void PlayHit()
    {
        play(Hit);
    }

    public void PlayScore()
    {
        play(Score);
    }

    private void play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
