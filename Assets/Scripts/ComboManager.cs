using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public PlayerController pController;
    public GameManager gManager;

    //Gestio del combo
    private int comboRacha;
    public string[] comboWords;

    //Screenshake
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float initialShakeDuration = 0.25f;
    [SerializeField] private float shakeMagnitude = 0.7f;
    [SerializeField] private float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = cameraTransform.localPosition;
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
        //Aqui es podria augmentar el tema filtros per fer-ho mes epic quan portin una racha queflips//Failure

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
    }

    public void YouFailed()
    {
        comboRacha = 0;
        initialShakeDuration = 0.25f;
    }
}
