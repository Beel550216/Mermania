using Player;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public GameObject npcDialogue;
        public TMP_Text npcDialogueText;
        public TMP_Text npcNameText;

        public bool inInteractionZone;
        public bool atKiosk;
        public bool atNPC;
        public bool fishing;

        public bool npcFound;

        public LevelManager lm;
        public GameObject levelManagerGO;

        private ButtonNav buttonNav;

        public int fishingCount;
        public TMP_Text fishingCounterText;

        public bool interaction = false;

        public GameObject nextButton;

        public List<string> npcList = new List<string>();
        public List<string> npcDialogues = new List<string>();
        public List<string> npcName = new List<string>();

        private void Awake()
        {
            npcList.Add("NPC1");
            npcList.Add("NPC2");
            npcList.Add("NPCnotlost");
            npcList.Add("NPC3");

            npcDialogues.Add("Wow, the heat wave is strong today");
            npcDialogues.Add("If only I had something to drink...");
            npcDialogues.Add("Bring 1x water bottle");

            npcDialogues.Add("It's been 10 minutes, where's Savanna?");
            npcDialogues.Add("She's wearing yellow, how did we lose track of her?!");
            npcDialogues.Add("I don't know, we lost her whilst leaving the bandstand..."); //Girl2
            npcDialogues.Add("Hey, can you help us find her?"); //Girl2
            npcDialogues.Add("We can offer you clams in return.");
            npcDialogues.Add("Talk to Savanna (the girl in yellow)"); //her friend

            npcDialogues.Add("Am I Savanna? What? No, you must be looking for a different girl in yellow.");

            npcDialogues.Add("Am I Savanna? Yes, why?");
            npcDialogues.Add("They asked you to look for me?");
            npcDialogues.Add("Thanks, I lost them back at the bandstand!");
            npcDialogues.Add("Take this, as a thank you payment");

            npcDialogues.Add("Hey, you know that old myth that mermaids exist around this island?");
            npcDialogues.Add("Yeah, you don't actually believe that, right, ____?"); //Girl2
            npcDialogues.Add("No of course not!");
            npcDialogues.Add("Have you heard the one about this rock being a portal, though?");
            npcDialogues.Add("What? Wow they really come up with anything these days!");  //Girl2
            npcDialogues.Add("A portal! Come on. They need to accept that life is just boring"); //Girl2
            npcDialogues.Add("I don't know, __. It does look rather magical...");
            npcDialogues.Add("If I look too long, it almost seems like its glowin-");
            npcDialogues.Add("___! Don't tell me you're going crazy too!"); //Girl2
            npcDialogues.Add("There could never be magic. Life is disappointing."); //Girl2
            npcDialogues.Add("There's no harm in believing for better though...");
            npcDialogues.Add("Another world...");
            npcDialogues.Add("Whatever you say, ___."); //Girl2


            npcName.Add("Girl A");
            npcName.Add("Girl B");
            npcName.Add("Not Savanna");
            npcName.Add("Savanna?");

            npcName.Add("Conspiror");
            npcName.Add("Emma");
            npcName.Add("Not believing");
            npcName.Add("Jane");
        }

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
            ActivateNPC();
            DialoguePress();
            //LeaveKiosk();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                //Time.timeScale = 0;
                Debug.Log("PLAYER ENTERED TRADING ZONE");
                inInteractionZone = true;

                if(gameObject.tag != "Portal")
                {
                    interact.SetActive(true);
                }

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

        private void ActivateNPC()
        {
            if (inInteractionZone && npcList.Contains(gameObject.tag) && lm.interaction == true)
            {
                //Time.timeScale = 0;
                interact.SetActive(false);
                //inInteractionZone = true;

                Debug.Log("TALKING TO NPC");

                npcDialogue.gameObject.SetActive(true);
                atNPC = true;

                lm.SetButton(nextButton);

                if (gameObject.tag == npcList[0])
                {
                    dialogueNum = 0;
                    npcDialogueText.text = npcDialogues[dialogueNum];
                    npcNameText.text = npcName[0];
                }
                if (gameObject.tag == npcList[1])
                {
                    dialogueNum = 3;
                    npcDialogueText.text = npcDialogues[dialogueNum];
                    npcNameText.text = npcName[1];
                }
                if (gameObject.tag == npcList[2])
                {
                    dialogueNum = 9;
                    npcDialogueText.text = npcDialogues[dialogueNum];
                    npcNameText.text = npcName[2];
                }
                if (gameObject.tag == npcList[3])
                {
                    Debug.Log("NPC FOUND!");
                    npcFound = true;
                    dialogueNum = 10;
                    npcDialogueText.text = npcDialogues[dialogueNum];
                    npcNameText.text = npcName[3];
                    lm.AwardClams(1000);
                }

                //NavJumpTo("Buy");

            }
        }

        public int dialogueNum;

        public void DialoguePress()
        {
            if(lm.interaction == true && npcList.Contains(gameObject.tag) && inInteractionZone)
            {
                npcDialogueText.text = npcDialogues[dialogueNum];
                dialogueNum++;
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
                npcDialogue.gameObject.SetActive(false);
                atNPC = false;
                lm.portalText.SetActive(false);

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