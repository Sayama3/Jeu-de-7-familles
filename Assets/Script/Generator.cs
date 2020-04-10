using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
// using UnityEngine.UIElements;
using Random = UnityEngine.Random;
    

public class Generator : MonoBehaviour
{
    [Title("Component Needed")]
    [SerializeField] private Transform parent;
    [SerializeField] private Transform positionDeDepart;
    [SerializeField] private Image piocheImage;
    [SerializeField] private Text cardLeft;
    [Title("Image to Load")]
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private GameObject emptyImage;
    private List<Sprite> actualSprite = new List<Sprite>();

    private Sprite oneShow;
    private Vector2 middle;

    private Sprite oldPioche;
    
    private void Awake()
    {
        middle = new Vector2(1920f/2,1080f/2);
        actualSprite = new List<Sprite>(sprites);
        Random.InitState((int)DateTime.Now.Ticks);
    }

    private void Update()
    {
        cardLeft.text = actualSprite.Count + " Left";
    }

    private void Start()
    {
        oldPioche = piocheImage.sprite;
    }

    [Button(ButtonSizes.Large)]
    public void ShowRandom()
    {
        if (actualSprite.Count > 0)
        {

            var newCard = Instantiate(emptyImage,positionDeDepart.position,positionDeDepart.rotation,parent);
            newCard.AddComponent<BoxCollider2D>();
            SpriteRenderer img = newCard.GetComponent<SpriteRenderer>();
            int index = RandomIndex(actualSprite.Count);
            img.sprite = actualSprite[index];
            actualSprite.RemoveAt(index);
            // Debug.Log("Affichage de l'image");
            img.color = Color.white;
            // img.GetComponentInChildren<Text>().text = "";

        }
        else
        {
            piocheImage.sprite = Sprite.Create(Texture2D.whiteTexture, 
                new Rect(1, 1, 1, 1), 
                Vector2.zero);
            piocheImage.color = Color.red;
            piocheImage.GetComponentInChildren<Text>().text = "Vide";

        }
        
    }

    [Button(ButtonSizes.Large)]
    public void ResetPaquet()
    {
        Random.InitState((int)(DateTime.Now.Ticks * Random.Range(1f,999999f)));
        foreach (var carte in GameObject.FindGameObjectsWithTag("Carte"))
        {
            Destroy(carte);
        }
        
        piocheImage.GetComponentInChildren<Text>().text = "Pioche";
        actualSprite = new List<Sprite>(sprites);
        piocheImage.sprite = oldPioche;
        piocheImage.color = Color.grey;
    }

    public void AddInPioche(string numero)
    {
        foreach (var sprite in sprites)
        {
            if (sprite.name==numero)
            {
                if (!actualSprite.Contains(sprite))
                {
                    actualSprite.Add(sprite);
                    piocheImage.GetComponentInChildren<Text>().text = "Pioche";
                    piocheImage.color = Color.grey;
                }
                break;
            }
        }
    }
    
    int RandomIndex(int tailleListe)
    {
        int index = Random.Range(0, tailleListe);
        return index;
    }
    
}
