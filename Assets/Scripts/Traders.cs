using Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{

    public class Traders : MonoBehaviour
    {
        public PlayerInput playerInput;

        public GameObject tradingScreen;
        public GameObject interact;
        public GameObject fishingText;
        public GameObject caughtFish;

        public bool inInteractionZone;
        public bool atKiosk;
        public bool fishing;

        public LevelManager lm;
        public GameObject levelManagerGO;

        private ButtonNav buttonNav;

        public int fishingCount;
        public TMP_Text fishingCounterText;

        public bool interaction = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //buttonNav.GetComponent<ButtonNav>();

            GameObject levelManagerGO = GameObject.FindGameObjectWithTag("Level Manager");
            lm = levelManagerGO.GetComponent<LevelManager>();

        }

        // Update is called once per frame
        void Update()
        {
            ActivateKiosk();
            ActivateFishing();
            Fishing();
            //LeaveKiosk();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                //Time.timeScale = 0;
                Debug.Log("PLAYER ENTERED TRADING ZONE");
                inInteractionZone = true;
                interact.SetActive(true);

                //tradingScreen.gameObject.SetActive(true);

            }
        }

        private void ActivateKiosk()
        {
            if (inInteractionZone && gameObject.tag == "Kiosk" && lm.interaction == true)
            {
                Time.timeScale = 0;
                interact.SetActive(false);
                //inInteractionZone = true;

                tradingScreen.gameObject.SetActive(true);
                atKiosk = true;

                NavJumpTo("Buy");

            }
        }


        public void NavJumpTo(string tag)
        {
            GameObject button = GameObject.FindGameObjectWithTag(tag);
            lm.SetButton(button);
            buttonNav.JumpToSpecificElement(tag);
        }

        private void ActivateFishing()
        {
            if (inInteractionZone && gameObject.tag == "FishingZone" && lm.interaction == true)
            {
                interact.SetActive(false);
                fishingText.SetActive(true);
                //inInteractionZone = true;

                //tradingScreen.gameObject.SetActive(true);
                fishing = true;

            }
        }

        private void Fishing()
        {
            if (fishing && Input.GetKeyDown(KeyCode.F))
            {
                fishingCount++;

            }
            if (fishingCount == 12)
            {
                caughtFish.SetActive(true);
                fishingCount = 0;


            }

            //fishingCounterText.text = fishingCount.ToString();
        }


        /* private void LeaveKiosk()
         {
             if (atKiosk && inInteractionZone && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X)))
             {
                 Time.timeScale = 1;
                 tradingScreen.SetActive(false);

                 inInteractionZone = false;

             }
         }*/

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                Time.timeScale = 1;

                inInteractionZone = false;
                fishing = false;
                interact.SetActive(false);
                fishingText.SetActive(false);
                tradingScreen.SetActive(false);

            }
        }


        /*public bool OnInteract(InputValue value)
        {

            if (value.isPressed)
            {
                interaction = true;
                return interact;
            }
            else
            {
                interaction = false;
                return interact;
            }
        }*/

    }
}