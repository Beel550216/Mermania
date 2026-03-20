using UnityEngine;

public class Collectible : MonoBehaviour
{
    public LevelManager lm;
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
            string type = gameObject.tag.ToString();
            lm.collectibles.Add(type);
            Destroy(gameObject);

        }
    }
}
