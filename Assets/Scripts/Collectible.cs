using UnityEngine;

public class Collectible : MonoBehaviour
{
    public LevelManager lm;

    public AudioManager audioManager;

    private void Awake()
    {
        lm = GetComponent<LevelManager>();
    }

    void Start()
    {
        lm = GetComponent<LevelManager>();
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

            Destroy(gameObject);

        }
    }
}
