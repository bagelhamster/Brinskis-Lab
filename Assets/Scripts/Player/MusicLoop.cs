using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    [SerializeField] private AudioClip[] music = default;
    [SerializeField] private AudioSource Player = default;
    public GameObject player;

    void Update()
    {
        if(!Player.isPlaying)Player.PlayOneShot(music[UnityEngine.Random.Range(0, music.Length - 1)]);
        if (Time.timeScale == 0)
        {
            player.GetComponent<FPSController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }
    private void Awake()
    {
        player.GetComponent<FPSController>().enabled = true;
    }


}
