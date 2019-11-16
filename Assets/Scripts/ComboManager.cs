using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboManager : MonoBehaviour
{
    //Gestio del combo
    private int comboRacha;
    private int comboAnnouncers;
    public Image[] comboWords;

    //Highscore
    public float highScore = 0;
    public TextMeshProUGUI highScoreText;
    public int highScoreMultiplier = 15;

    //Screenshake
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float initialShakeDuration = 0.25f;
    [SerializeField] private float shakeMagnitude = 0.7f;
    [SerializeField] private float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = cameraTransform.localPosition;
        highScoreText.text = highScore.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        comboRacha = 0;
        //for (int i = 0; i < comboWords.Length - 1; i++)
        //{
        //    comboWords[i]
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Aqui es podria augmentar el tema filtros per fer-ho mes epic quan portin una racha queflips
        //Failure


        if (initialShakeDuration > 0)
        {
            cameraTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            Debug.Log(transform.localPosition);
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
        if (comboWords.Length - 1 > comboRacha) comboRacha = comboWords.Length - 1;
        highScore += comboRacha * highScoreMultiplier;
        highScoreText.text = highScore.ToString();
        switch (comboRacha)
        {
            case 0:
                break;
            case 1:
                comboWords[0].enabled = true;
                break;
            case 2:
                comboWords[1].enabled = true;
                break;
            case 3:
                comboWords[2].enabled = true;
                break;
            case 4:
                comboWords[3].enabled = true;
                break;
            case 5:
                comboWords[4].enabled = true;
                break;
            case 6:
                comboWords[5].enabled = true;
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
