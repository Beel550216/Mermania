using JetBrains.Annotations;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public LevelManager lm;
    public GameObject lmObject;

    public AudioManager audioManager;
    public GameObject audioManagerObject;
    public Animator anim;


    private void Awake()
    {
        //lm = GetComponent<LevelManager>();
    }

    void Start()
    {
        lm = lmObject.GetComponent<LevelManager>();
        audioManager = audioManagerObject.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //audioManager.GetComponent<AudioManager>().PlaySFX(1);
            string type = gameObject.tag.ToString();
            Debug.Log(type + " Collected");

            for(int i = 0; i < lm.collectMultiplier; i++)
            {
                if(type != "treasure")
                {
                    lm.collectibles.Add(type);
                }
            }

            if(type == "Stone")
            {
                audioManager.PlaySFX(3);
            }
            if(type == "Coconut")
            {
                audioManager.PlaySFX(5);
            }
            if(type == "WaterBottle")
            {
                audioManager.PlaySFX(7);
            }
            if(type == "Perfume")
            {
                audioManager.PlaySFX(8);
            }

            if (type != "treasure")
            {
                gameObject.SetActive(false);
            }
            else
            {
                anim.SetBool("open", true);
                TreasureItem(gameObject);
                audioManager.PlaySFX(8);
            }

            lm.UpdateInventory();

            //Destroy(gameObject);

        }
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }

    public void TreasureItem(GameObject other)
    {
        foreach (Transform child in other.transform)
        {
            if (child.gameObject.tag == "Perfume" || child.gameObject.tag == "WaterBottle" || child.gameObject.tag == "Coconut" || child.gameObject.tag == "Stone" || child.gameObject.tag == "Comb")
            {
                lm.collectibles.Add(child.gameObject.tag);
                Debug.Log("TREASURE ITEM ADDED");
            }
        }
    }
}
