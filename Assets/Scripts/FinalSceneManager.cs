using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
        
    // Start is called before the first frame update
    void Start()
    {
        score.text = Convert.ToString(PlayerPrefs.GetInt("Score"));
    }

    // Update is called once per frame
    void Update()
    {
        if (hinput.anyGamepad.A.justPressed || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
    }
}
