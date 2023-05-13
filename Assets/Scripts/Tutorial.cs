using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject note;

    private void Start()
    {
        note.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        note.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        note.SetActive(false);
    }
}
