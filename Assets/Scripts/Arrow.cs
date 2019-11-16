using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject arrow;
    [SerializeField] private PlayerController.ArrowDirection direction;
    private bool onMove;
    private int playerInt;
    [HideInInspector] public RectTransform rect;
    [HideInInspector] public bool instanced;
    [HideInInspector] public Arrow[] arrowVector;
    // Start is called before the first frame update

    private void OnEnable()
    {
        rect = GetComponent<RectTransform>();
        instanced = true;
    }

    private void Update()
    {
        if (!onMove && AreAllArrowsInstanced())
        {
            onMove = true;
            StartCoroutine(Move());
        }
        if(transform.localPosition.y <= -130) 
        {
            if (playerInt == 0)
            {
                if(direction == PlayerController.Instance.GetPlayer0Dir()) GameManager.Instance.SetPlayer0State(true);    
                else GameManager.Instance.SetPlayer0State(false);
            }
            if (playerInt == 1)
            {
                if(direction == PlayerController.Instance.GetPlayer1Dir()) GameManager.Instance.SetPlayer1State(true);
                else GameManager.Instance.SetPlayer1State(false);
            }
            DisableArrow();
        }
    }

    private IEnumerator Move()
    {
        while (onMove)
        {
            rect.position = new Vector2(rect.position.x , rect.position.y - speed * Time.deltaTime);
            yield return null;
        }
    }

    private void DisableArrow()
    {
        onMove = false;
        gameObject.SetActive(false);
    }

    private bool AreAllArrowsInstanced() //Sees if all cubes are instantiated. It makes them start at the same time.
    {
        foreach (var c in arrowVector)
        {
            if (!c.instanced) return false;
        }
        return true;
    }

    public void SetPlayer(int n)
    {
        playerInt = n;
        if (n == 0) //Blue
        {
            GetComponent<Image>().color = GameManager.Instance.blueBK;
            arrow.GetComponent<Image>().color = GameManager.Instance.blueColor;
        }
        else //Red
        {
            GetComponent<Image>().color = GameManager.Instance.redBK;
            arrow.GetComponent<Image>().color = GameManager.Instance.redColor;
        }
    }
}
