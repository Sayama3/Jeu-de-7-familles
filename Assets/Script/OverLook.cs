using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class OverLook : MonoBehaviour
{
    private bool inSelection = false;
    
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private GameObject card;
    [SerializeField,Range(0.1f,5f)] float timer = 2f;
    private float timePassed = 0;
    private bool hasUpdate = false;
    [SerializeField] private float sizeMultiplicator = 4f;
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (!inSelection)
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
            bool found = false;
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Carte"))
                {
                    found = true;
                    if (card != null && card == result.gameObject)
                    {
                        timePassed += Time.deltaTime;
                        break;
                    }
                    else
                    {
                        if (card != null)
                        {
                            Reduire();
                        }
                        card = result.gameObject;
                        timePassed = 0;
                        hasUpdate = false;
                        break;
                    }

                }
            }

            if (!found && card != null && timePassed >= timer)
            {
                Reduire();
                
                card = null;
                timePassed = 0;
                hasUpdate = false;
            }

            if (timePassed >= timer && !hasUpdate && card != null)
            {
                Agrandire();
                hasUpdate = true;
            }
        }
    }

    private void Agrandire()
    {
        var vec = new Vector2(100 * sizeMultiplicator,160 * sizeMultiplicator);
        card.GetComponent<RectTransform>().sizeDelta = vec;
    }

    private void Reduire()
    {
        var vec = new Vector2(100,160);
        card.GetComponent<RectTransform>().sizeDelta = vec;
    }

    public void IsSelecter(bool variable)
    {
        inSelection = variable;
        if (variable)
        {
            if (hasUpdate && card != null)
            {
                Reduire();
            }
            card = null;
            hasUpdate = false;
            timePassed = 0;
        }
        
    }
}
