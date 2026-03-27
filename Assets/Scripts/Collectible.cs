using UnityEngine;

public class Collectible : MonoBehaviour
{
    public LevelManager lm;

    public AudioManager audioManager;
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
           // audioManager.GetComponent<AudioManager>().PlaySFX(1);
            string type = gameObject.tag.ToString();
            lm.collectibles.Add(type);
            Destroy(gameObject);

        }
    }
}
