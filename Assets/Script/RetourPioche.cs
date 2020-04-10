using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RetourPioche : MonoBehaviour
{
    private GameObject eventSystem;
    private void Start()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
    }

    private void Update()
    {
        Collider2D obj = Physics2D.OverlapBox (transform.position, Vector2.one * 14f, 0f);
        Debug.Log(obj.gameObject);
        
        if (obj.CompareTag("Carte"))
        {
            if (!obj.GetComponent<OverLook>().inSelection)
            {
                eventSystem.GetComponent<Generator>().AddInPioche(obj.GetComponent<OverLook>().cardName);
                Destroy(obj.gameObject);
            }
        }
    }

}
