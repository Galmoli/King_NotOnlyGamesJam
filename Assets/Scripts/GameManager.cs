using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action OnCorrectPos = delegate { };
    public static Action OnIncorrectPos = delegate { };

    [SerializeField] private float gameLength = 60f;
    private float currentGameTime = 0;
    private static GameManager instance;
    private bool player0PosCorrect;
    private bool player1PosCorrect;
    private bool player0Checked;
    private bool player1Checked;
    public Color blueBK;
    public Color blueColor;
    public Color redBK;
    public Color redColor;

    public ComboManager l_comboManager;
    [SerializeField] private Animator playerAnim;
    public Animator cameraAnim;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private void Start()
    {
        l_comboManager = GetComponent<ComboManager>();
        GetColors();
    }

    private void Update()
    {
        currentGameTime += Time.deltaTime;
        if (currentGameTime >= gameLength)
        {
            StartCoroutine(ChangeScene());
        }
    }

    public void SetPlayer0State(bool state)
    {
        player0PosCorrect = state;
        player0Checked = true;
        if (player1Checked)
        {
            if(player0PosCorrect && player1PosCorrect) CorrectPos();
            else IncorrectPos();
        }
    }
    public void SetPlayer1State(bool state)
    {
        player1PosCorrect = state;
        player1Checked = true;
        if (player0Checked)
        {
            if(player0PosCorrect && player1PosCorrect) CorrectPos();
            else IncorrectPos();
        }
    }

    private void CorrectPos()
    {
        OnCorrectPos();
        playerAnim.SetTrigger("Success");
        l_comboManager.MoreCombos();
        CleanPos();
    }

    private void IncorrectPos()
    {
        OnIncorrectPos();
        playerAnim.SetTrigger("Fail");
        hinput.anyGamepad.Vibrate();
        l_comboManager.YouFailed();
        CleanPos();
    }

    private void CleanPos()
    {
        player0Checked = false;
        player1Checked = false;
    }

    private void GetColors()
    {
        blueColor = new Color(PlayerPrefs.GetFloat("BlueR"), PlayerPrefs.GetFloat("BlueG"), PlayerPrefs.GetFloat("BlueB"), 1);
        redColor = new Color(PlayerPrefs.GetFloat("RedR"), PlayerPrefs.GetFloat("RedG"), PlayerPrefs.GetFloat("RedB"), 1);
    }

    private IEnumerator ChangeScene()
    {
        cameraAnim.enabled = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(4);
    }
}
