using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboManager : MonoBehaviour
{
    //Gestio del combo
    public int comboRacha;
    private int comboAnnouncers;
    public Image[] comboWords;
    private float cooldownAnnouncers = 0;
    public float cooldownMultipliers = 0;

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
    [SerializeField] public ParticleSystem BaseCelebrationL; //Es podrien instanciar les grans amb els combos
    [SerializeField] public ParticleSystem BaseCelebrationR;
    [SerializeField] public ParticleSystem MidCelebrationL;
    [SerializeField] public ParticleSystem MidCelebrationR;
    [SerializeField] public ParticleSystem LargeCelebrationL;
    [SerializeField] public ParticleSystem LargeCelebrationR;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Aqui es podria augmentar el tema filtros per fer-ho mes epic quan portin una racha queflips

        //Cooldowns imatges
        if (cooldownAnnouncers > 0)
        {
            cooldownAnnouncers -= Time.deltaTime;
            Debug.Log(cooldownAnnouncers);
            if (cooldownAnnouncers < 0)
            {
                cooldownAnnouncers = 0;
                for (int i = 0; i < comboWords.Length - 1; i++)
                {
                    comboWords[i].enabled = false;
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

    public void MoreCombos()
    {
        comboRacha++;
        if (comboWords.Length - 1 < comboRacha) comboRacha = comboWords.Length - 1;
        highScore += comboRacha * highScoreMultiplier;
        highScoreText.text = highScore.ToString();
        switch (comboRacha)
        {
            case 0:
                break;
            case 1:
                comboWords[0].enabled = true; //És important que cadascun d'aquests objectes tingui un script que faci que image enabled = false despres d'un temps d'estar enabled.
                cooldownAnnouncers = cooldownMultipliers;
                break;
            case 2:
                comboWords[1].enabled = true;
                cooldownAnnouncers = cooldownMultipliers;
                break;
            case 3:
                comboWords[2].enabled = true;
                cooldownAnnouncers = cooldownMultipliers;
                break;
            case 4:
                comboWords[3].enabled = true;
                cooldownAnnouncers = cooldownMultipliers * 1.5f;
                break;
            case 5:
                comboWords[4].enabled = true;
                cooldownAnnouncers = cooldownMultipliers * 1.5f;
                break;
            case 6:
                comboWords[5].enabled = true;
                cooldownAnnouncers = cooldownMultipliers * 2f;
                break;
            default:
                break;
        }
    }

    public void YouFailed()
    {
        comboRacha = 0;
        Debug.Log("Awful :(");
        initialShakeDuration = 0.25f;
    }
}
