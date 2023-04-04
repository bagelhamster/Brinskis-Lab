using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPres2 : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            button.onClick.Invoke();
        }
    }
}
