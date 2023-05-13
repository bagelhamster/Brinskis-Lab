using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject menu;
    public GameObject dead;
    public GameObject win;
    void Start()
    {
        menu.SetActive(false);
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            dead.SetActive(false);
            win.SetActive(true);
        }
    }
}
