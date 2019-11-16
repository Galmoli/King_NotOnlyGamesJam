using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorCalibrator : MonoBehaviour
{
    public Color blueFront;
    public Color redFront;
    [SerializeField] private Image blueFrontImg;
    [SerializeField] private Image redFrontImg;
    [SerializeField] private Image blueFrontFill;
    [SerializeField] private Image redFrontFill;
    private Color defaultBlue;
    private Color defaultRed;

    private bool player1Confirmed;
    private bool player2Confirmed;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultBlue = blueFront;
        defaultRed = redFront;
        player1Confirmed = false;
        player2Confirmed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Joystick1Horizontal") > 0.1f)
        {
            blueFront = new Color(blueFront.r + 0.01f, blueFront.g, blueFront.b, blueFront.a);
            blueFrontFill.fillAmount += 0.01f;
        }
        if (Input.GetAxis("Joystick1Horizontal") < -0.1f)
        {
            blueFront = new Color(blueFront.r - 0.01f, blueFront.g, blueFront.b, blueFront.a);
            blueFrontFill.fillAmount -= 0.01f;
        }
        if (Input.GetAxis("Joystick2Horizontal") > 0.1f)
        {
            redFront = new Color(redFront.r, redFront.g, redFront.b + 0.01f, redFront.a);
            redFrontFill.fillAmount += 0.01f;
        }
        if (Input.GetAxis("Joystick2Horizontal") < -0.1f)
        {
            redFront = new Color(redFront.r, redFront.g, redFront.b - 0.01f, redFront.a);
            redFrontFill.fillAmount -= 0.01f;
        }

        if (Input.GetButtonDown("Y button 1"))
        {
            blueFront = defaultBlue;
            blueFrontFill.fillAmount = 0.5f;
        }

        if (Input.GetButtonDown("Y button 2"))
        {
            redFront = defaultRed;
            redFrontFill.fillAmount = 0.5f;
        }

        if (Input.GetButtonDown("A button 1"))
        {
            player1Confirmed = true;
            PlayerPrefs.SetFloat("BlueR", blueFront.r);
            PlayerPrefs.SetFloat("BlueG", blueFront.g);
            PlayerPrefs.SetFloat("BlueB", blueFront.b);
            if (player2Confirmed) LoadGamePlay();
        }
        if (Input.GetButtonDown("A button 2"))
        {
            player2Confirmed = true;
            PlayerPrefs.SetFloat("RedR", redFront.r);
            PlayerPrefs.SetFloat("RedG", redFront.g);
            PlayerPrefs.SetFloat("RedB", redFront.b);
            if(player1Confirmed) LoadGamePlay();
        }

        blueFrontImg.color = blueFront;
        redFrontImg.color = redFront;
    }

    private void LoadGamePlay()
    {
        SceneManager.LoadScene(2);
    }
}
