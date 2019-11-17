using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Animator NG_Anim;
    [SerializeField] private Animator Exit_Anim;

    private bool newGameSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Joystick1Vertical") > 0.1f)
        {
            NG_Anim.SetBool("Idle", false);
            Exit_Anim.SetBool("Selected", false);
            newGameSelected = true;
        }

        if (Input.GetAxis("Joystick1Vertical") < -0.1f)
        {
            NG_Anim.SetBool("Idle", true);
            Exit_Anim.SetBool("Selected", true);
            newGameSelected = false;
        }

        if (hinput.gamepad[0].A.justPressed)
        {
            if (newGameSelected) SceneManager.LoadScene(1);
            else Application.Quit();
        }
    }
}
