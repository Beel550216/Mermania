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

        public LevelManager lm;
        public GameObject levelManagerGO;

        private ButtonNav buttonNav;

        public int fishingCount;
        public TMP_Text fishingCounterText;

        public bool interaction = false;

        public List<string> npcList = new List<string>();
        public List<string> npcDialogues = new List<string>();
        public List<string> npcName = new List<string>();

        private void Awake()
        {
            npcList.Add("NPC1");
            npcList.Add("NPC2");

            npcDialogues.Add("Weather dialogue");
            npcDialogues.Add("Allude to wanting water");
            npcDialogues.Add("Bring 2x water bottles");

            npcDialogues.Add("It's been 10 minutes, where's ____?");
            npcDialogues.Add("Hey, can you help me find her?");
            npcDialogues.Add("Talk to ____"); //her friend

            npcName.Add("Girl A");
            npcName.Add("Girl B");
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

                if(gameObject.tag == npcList[0])
                {
                    dialogueNum = 0;
                    npcDialogueText.text = npcDialogues[dialogueNum];
                    npcNameText.text = npcName[0];
                }
                if (gameObject.tag == npcList[1])
                {
                    dialogueNum = 3;
                    npcDialogueText.text = npcDialogues[dialogueNum];
                    npcNameText.text = npcName[0];
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