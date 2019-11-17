using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Animator NG_Anim;
    [SerializeField] private Animator Exit_Anim;
    [SerializeField] private AudioClip menuSelected;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject player;
    [SerializeField] private float changeSceneDelay = 0.6f;
    private Animator anim;

    private bool newGameSelected;
    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        newGameSelected = true;
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
            if (newGameSelected)
            {
                anim.SetBool("Ready", true);
                audioSource.PlayOneShot(menuSelected);
                StartCoroutine(ChangeScene());
            }
            else Application.Quit();
        }
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(changeSceneDelay);
        while (fadeImage.color.a < 1)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + 1f * Time.deltaTime);
            audioSource.volume -= 1f * Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}
