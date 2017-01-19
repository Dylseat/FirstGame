using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    [SerializeField]
    Sprite[] HeartSprites;
    [SerializeField]
    Image HeartUI;

    private PlayerCharacter player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        HeartUI.sprite = HeartSprites[player.currentHealth]; // Changes the hearts sprite according to the player's health
    }
}