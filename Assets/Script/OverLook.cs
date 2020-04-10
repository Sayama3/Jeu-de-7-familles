using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class OverLook : MonoBehaviour
{

    [SerializeField, MinMaxSlider(-90f, 90f,true)]
    private Vector2 ScreenWidth,ScreenHeight;
    [HideInInspector]public bool inSelection = false;

    public string cardName;

    // private GameObject card;
    [SerializeField,Range(0.1f,5f)] float timer = 2f;
    private float timePassed = 0;
    private bool hasUpdate = false;
    [SerializeField] private float sizeMultiplicator = 4f;


    // private Vector2 oldMousePosition;
    private Vector2 MousePosition;

    private Camera cam;

    private GameObject eventSystem;
    
    
    private void Start()
    {
        cam = Camera.main;
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
        cardName = GetComponent<SpriteRenderer>().sprite.name;
    }



    private void OnMouseDown()
    {
        // oldMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        inSelection = true;
        Reduire();
        timePassed = 0;
    }

    private void OnMouseDrag()
    {
        MousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        // var deplacement = MousePosition - oldMousePosition;
        var nexPos = MousePosition;
        if (nexPos.x > ScreenHeight.y)
        {
            nexPos.x = ScreenHeight.y;
        }
        else if (nexPos.x < ScreenHeight.x)
        {
            nexPos.x = ScreenHeight.x;
        }
        
        if (nexPos.y > ScreenWidth.y)
        {
            nexPos.y = ScreenWidth.y;
        }
        else if (nexPos.y < ScreenWidth.x)
        {
            nexPos.y = ScreenWidth.x;
        }

        transform.position = nexPos;
        
    }

    private void OnMouseUp()
    {
        inSelection = false;
    }

    private void OnMouseExit()
    {
        // Debug.Log("Mouse Exit");
        Reduire();
        timePassed = 0;
    }
    
    private void OnMouseOver()
    {
        if (!inSelection)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= timer)
            {
                Agrandire();
            }
        }
    }
    private void Agrandire()
    {
        // if(card==null) return;
        var vec = new Vector3(sizeMultiplicator,sizeMultiplicator,sizeMultiplicator);
        transform.localScale = vec;
        // Debug.Log(vec + " - " + transform.localScale);

    }

    private void Reduire()
    {
        // if(card==null) return;
        var vec = new Vector3(1,1,1);
        transform.localScale = vec;
    }

}
