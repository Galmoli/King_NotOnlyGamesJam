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

    [SerializeField] private GameObject reset1B;
    [SerializeField] private GameObject accept1B;
    [SerializeField] private GameObject ready1B;
    [SerializeField] private GameObject reset2B;
    [SerializeField] private GameObject accept2B;
    [SerializeField] private GameObject ready2B;
    
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
            blueFront = new Color(Mathf.Clamp(blueFront.r +0.01f, 0.502f, 0.784f), blueFront.g, blueFront.b, blueFront.a);
            blueFrontFill.fillAmount += 0.01f;
        }
        if (Input.GetAxis("Joystick1Horizontal") < -0.1f)
        {
            blueFront = new Color(Mathf.Clamp(blueFront.r - 0.01f, 0.502f, 0.784f), blueFront.g, blueFront.b, blueFront.a);
            blueFrontFill.fillAmount -= 0.01f;
        }
        if (Input.GetAxis("Joystick2Horizontal") > 0.1f)
        {
            redFront = new Color(redFront.r, redFront.g, Mathf.Clamp(redFront.b + 0.01f, 0.427f, 0.667f), redFront.a);
            redFrontFill.fillAmount += 0.01f;
        }
        if (Input.GetAxis("Joystick2Horizontal") < -0.1f)
        {
            redFront = new Color(redFront.r, redFront.g, Mathf.Clamp(redFront.b - 0.01f, 0.427f, 0.667f), redFront.a);
            redFrontFill.fillAmount -= 0.01f;
        }

        if (hinput.gamepad[0].Y.justPressed || Input.GetKeyDown(KeyCode.R))
        {
            blueFront = defaultBlue;
            blueFrontFill.fillAmount = 0.5f;
        }

        if (hinput.gamepad[1].Y.justPressed || Input.GetKeyDown(KeyCode.Backspace))
        {
            redFront = defaultRed;
            redFrontFill.fillAmount = 0.5f;
        }

        if (hinput.gamepad[0].A.justPressed || Input.GetKeyDown(KeyCode.Q))
        {
            player1Confirmed = true;
            reset1B.SetActive(false);
            accept1B.SetActive(false);
            ready1B.SetActive(true);
            PlayerPrefs.SetFloat("BlueR", blueFront.r);
            PlayerPrefs.SetFloat("BlueG", blueFront.g);
            PlayerPrefs.SetFloat("BlueB", blueFront.b);
            if (player2Confirmed) LoadGamePlay();
        }
        if (hinput.gamepad[1].A.justPressed || Input.GetKeyDown(KeyCode.Return))
        {
            player2Confirmed = true;
            reset2B.SetActive(false);
            accept2B.SetActive(false);
            ready2B.SetActive(true);
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
