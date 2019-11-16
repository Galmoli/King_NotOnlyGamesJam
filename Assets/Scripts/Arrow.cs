using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;

    private bool onMove;
    [HideInInspector] public RectTransform rect;
    [HideInInspector] public bool instanced;
    [FormerlySerializedAs("cubeOrderVector")] [HideInInspector] public Arrow[] arrowVector;
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
        if(transform.position.y <= -180) DisableArrow();
    }

    private IEnumerator Move()
    {
        while (onMove)
        {
            rect.position = new Vector2(rect.position.x , rect.position.y - speed * Time.deltaTime);
            yield return null;
        }
    }

    public void DisableArrow()
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
}
