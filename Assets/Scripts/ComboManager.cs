using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class ComboManager : MonoBehaviour
{
    //Gestio del combo
    public int comboRacha;
    private int comboAnnouncers;
    public GameObject[] comboWords;
    private float cooldownAnnouncers = 0;
    public float cooldownMultipliers = 0;
    public AudioSource errorSound;
    public AudioSource correctSound;

    //Highscore
    [HideInInspector] public float highScore = 0;
    public TextMeshProUGUI highScoreText;
    public int highScoreMultiplier = 15;

    //Screenshake
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float initialShakeDuration = 0.25f;
    [SerializeField] private float shakeMagnitude = 0.7f;
    [SerializeField] private float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    //Particles
    public GameObject lowParticles;
    public GameObject midParticles;
    public GameObject highParticles;

    //PlayerMat
    public Material material1;
    public Material material2;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    private void Awake()
    {
        initialPosition = cameraTransform.localPosition;
        highScoreText.text = highScore.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        comboRacha = 0;
        cooldownAnnouncers = 0;
        GameManager.OnCorrectPos += PlayParticles;
        GameManager.OnIncorrectPos += YouFailed;
    }

    // Update is called once per frame
    void Update()
    {
        //Aqui es podria augmentar el tema filtros per fer-ho mes epic quan portin una racha queflips

        //Cooldowns imatges
        if (cooldownAnnouncers > 0)
        {
            cooldownAnnouncers -= Time.deltaTime;
            if (cooldownAnnouncers < 0)
            {
                cooldownAnnouncers = 0;
                for (int i = 0; i < comboWords.Length; i++)
                {
                    comboWords[i].SetActive(false);
                }
            }
        }

        //Failure
        if (initialShakeDuration > 0)
        {
            cameraTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            initialShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            initialShakeDuration = 0f;
            cameraTransform.localPosition = initialPosition;
        }
    }

    void PlayParticles()
    {
        PlayerPrefs.SetInt("Score", Convert.ToInt32(highScore));
        if (comboRacha < 2)
        {
            lowParticles.SetActive(false);
            lowParticles.SetActive(true);
        }
        else if (comboRacha < 4)
        {
            midParticles.SetActive(false);
            midParticles.SetActive(true);
        }
        else
        {
            highParticles.SetActive(false);
            highParticles.SetActive(true);
        }
    }

    public void MoreCombos()
    {
        if (!correctSound.isPlaying) correctSound.Play();
        comboRacha++;
        if (comboWords.Length - 1 < comboRacha) comboRacha = comboWords.Length - 1;
        highScore += comboRacha * highScoreMultiplier;
        highScoreText.text = highScore.ToString();
        switch (Random.Range(1,7))
        {
            case 1:
                comboWords[0].SetActive(true); //És important que cadascun d'aquests objectes tingui un script que faci que image enabled = false despres d'un temps d'estar enabled.
                cooldownAnnouncers = cooldownMultipliers;
                break;
            case 2:
                comboWords[1].SetActive(true);
                cooldownAnnouncers = cooldownMultipliers;
                break;
            case 3:
                comboWords[2].SetActive(true);
                cooldownAnnouncers = cooldownMultipliers;
                break;
            case 4:
                comboWords[3].SetActive(true);
                cooldownAnnouncers = cooldownMultipliers; //* 1.5f;
                break;
            case 5:
                comboWords[4].SetActive(true);
                cooldownAnnouncers = cooldownMultipliers;// * 1.5f;
                break;
            default:
                comboWords[5].SetActive(true);
                cooldownAnnouncers = cooldownMultipliers;// * 2f;
                break;
        }
    }

    public void YouFailed()
    {
        comboRacha = 0;
        errorSound.Play();
        initialShakeDuration = 0.25f;
        StartCoroutine(PlayerFailed());
    }

    public IEnumerator PlayerFailed()
    {
        Debug.Log("failedfailed");
        skinnedMeshRenderer.material = material2;
        yield return new WaitForSeconds(0.5f);
        skinnedMeshRenderer.material = material1;
        yield return new WaitForSeconds(0.1f);
        skinnedMeshRenderer.material = material2;
        yield return new WaitForSeconds(0.25f);
        skinnedMeshRenderer.material = material1;
    }
}
