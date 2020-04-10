using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovableCard : MonoBehaviour
{
    [HideInInspector] public bool isSelected = false;
    private string nameOfSprite;


    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        nameOfSprite = GetComponent<Image>().sprite.name;
    }

    private void Update()
    {
        var trans = transform;
        var pos = trans.position;
        pos.z = 0;
        trans.position = pos;

        if (!isSelected)
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = trans.position;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("RetourPioche"))
                {
                    Destroy();
                }
            }
        }
    }

    private void Destroy()
    {
        GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Generator>().AddInPioche(nameOfSprite);
        Destroy(gameObject);
        
    }

    
}
