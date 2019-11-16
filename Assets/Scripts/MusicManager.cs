using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource firstPlay;
    [SerializeField] private AudioSource songLoop;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (firstPlay.isPlaying)
        {

        }
        else if (!songLoop.isPlaying)
        {
            songLoop.Play();
        }
    }
}
