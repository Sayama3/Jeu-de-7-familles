using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using UnityEngine.EventSystems;

public class MoveCard : MonoBehaviour
{
    public bool isAlreadyPressed = false;
    public Camera cam;
    // private Vector3 previousPosition;
    public GameObject card;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private OverLook overLook;
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        overLook = GetComponent<OverLook>();
    }

    void Update()
    {
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAlreadyPressed)
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                // foreach (RaycastResult result in results)
                // {
                //     Debug.Log("Hit " + result.gameObject.name);
                // }
                if (results.Count >0)
                {
                    card = results[0].gameObject;
                    if (card.CompareTag("Carte"))
                    {
                        isAlreadyPressed = true;
                    }
                    else
                    {
                        card = null;
                    }
                }

            }


        }
        if (Input.GetMouseButton(0))
        {
            // Debug.Log(Input.mousePosition);

            if (isAlreadyPressed)
            {
                var mousePos = Input.mousePosition;
                mousePos.z = 1;
                mousePos.x = (mousePos.x > 0) ? ((mousePos.x < 1920) ? mousePos.x : 1920) : 0;
                mousePos.y = (mousePos.y > 0) ? ((mousePos.y < 1080) ? mousePos.y : 1080) : 0;
                
                card.GetComponent<RectTransform>().localPosition = mousePos;
                // Debug.Log(mousePos);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isAlreadyPressed)
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                // For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                 foreach (RaycastResult result in results)
                 {    
                     Debug.Log("Hit " + result.gameObject.tag);

                     if (result.gameObject.CompareTag("RetourPioche"))
                     {
                         GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Generator>().AddInPioche(card.GetComponent<Image>().sprite.name);
                         Destroy(card);
                         
                     }
                 }

                 card = null;
                 isAlreadyPressed = false;
            }
            
        }

        overLook.IsSelecter(isAlreadyPressed);

    }

    public void SendCard(GameObject card)
    {
        // if (!isAlreadyPressed)
        // {
        //     this.card = card;
        //     isAlreadyPressed = true;
        //     this.card.GetComponent<MovableCard>().isSelected = true;
        // }
        
    }
}
