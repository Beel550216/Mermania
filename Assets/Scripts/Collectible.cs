using UnityEngine;

public class Collectible : MonoBehaviour
{
    public LevelManager lm;
    public GameObject lmObject;

    public AudioManager audioManager;
    public GameObject audioManagerObject;

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
            lm.collectibles.Add(type);
            //lm.UpdateInventory();

            if(type == "Stone")
            {
                audioManager.PlaySFX(3);
            }
            if(type == "Coconut")
            {
                audioManager.PlaySFX(5);
            }

            Destroy(gameObject);

        }
    }
}
