using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static OVRInput;

namespace HeneGames.DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        #region Singleton

        public static DialogueUI instance { get; private set; }
        abutton abut;
        bbutton bbut;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            //Hide dialogue and interaction UI at awake
            dialogueWindow.SetActive(false);
            interactionUI.SetActive(false);
            abut = GameObject.Find("Abutton").GetComponent<abutton>();
            bbut = GameObject.Find("bbutton").GetComponent<bbutton>();
        }

        #endregion

        public DialogueManager currentDialogueManager;
        private bool typing;
        private string currentMessage;
        private float startDialogueDelayTimer;
        private float escapeTimer;

        [Header("References")]
        [SerializeField] private Image portrait;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private GameObject dialogueWindow;
        [SerializeField] private GameObject interactionUI;

        [Header("Settings")]
        [SerializeField] private bool animateText = true;

        [Range(0.1f, 1f)]
        [SerializeField] private float textAnimationSpeed = 0.5f;

        public VRInputSystem vrInput;

        [Header("Action input")]
        public KeyCode actionInput = KeyCode.Space;

        [Header("Force skip input")]
        public KeyCode skipInput = KeyCode.Escape;

        public GameObject buoyObject;
        public buoy buoyScript;
        public Swimmer swimmer;

        private int buoyTextIndex = 3;
        private int whistleTextIndex = 5;
        private int escapeIndex = 6;

        private void Update()
        {
            //Delay timer
            if(startDialogueDelayTimer > 0f)
            {
                startDialogueDelayTimer -= Time.deltaTime;
            }
            //Escape delay timer
            if(escapeTimer >= 0f && currentDialogueManager.GetSentenceIndex() == whistleTextIndex)
            {
                escapeTimer -= Time.deltaTime;
            }

            OVRInput.Update();
            InputUpdate();
        }

        public virtual void InputUpdate()
        {
            //Next dialogue input
            int sentenceIndex = currentDialogueManager.GetSentenceIndex();
            if ((Input.GetKeyDown(actionInput) || vrInput.primaryButtonDown || OVRInput.GetDown(OVRInput.Button.One) || abut.IsGrabbed()) && sentenceIndex != whistleTextIndex && sentenceIndex != escapeIndex)
            {
                NextSentenceSoft();
            }
            else if(Input.GetKeyDown(skipInput) || vrInput.secondaryButtonDown || OVRInput.GetDown(OVRInput.Button.Two) || bbut.IsGrabbed())
            {
                NextSentenceHard();
            }
            else if(escapeTimer <= 0f && sentenceIndex == whistleTextIndex || OVRInput.GetDown(OVRInput.Button.Two) || bbut.IsGrabbed())
            {
                NextSentenceHard();
            }
        }

        /// <summary>
        /// If a sentence is being written and this function is called, the sentence is completed instead of immediately moving to the next sentence.
        /// This function needs to be called twice if you want to switch to a new sentence.
        /// </summary>
        public void NextSentenceSoft()
        {
            if (startDialogueDelayTimer <= 0f)
            {
                if (!typing)
                {
                    NextSentenceHard();
                }
                else
                {
                    StopAllCoroutines();
                    typing = false;
                    messageText.text = currentMessage;
                }
            }
        }

        /// <summary>
        /// Even if a sentence is being written, with this function immediately moves to the next sentence.
        /// </summary>
        public void NextSentenceHard()
        {
            //Continue only if we have dialogue
            if (currentDialogueManager == null)
                return;

            //Tell the current dialogue manager to display the next sentence. This function also gives information if we are at the last sentence
            currentDialogueManager.NextSentence(out bool lastSentence);

            //If last sentence remove current dialogue manager
            if (lastSentence)
            {
                currentDialogueManager = null;
            }
        }

        public void NextSentenceIfBuoy()
        {
            if (currentDialogueManager == null)
                return;

            swimmer.setDrown(false);

            //Hardcoding the index of the buoy sentence, change if necessary
            if(currentDialogueManager.GetSentenceIndex() == buoyTextIndex)
            {
                NextSentenceHard();
            }
        }

        public void StartDialogue(DialogueManager _dialogueManager)
        {
            //Delay timer
            startDialogueDelayTimer = 0.1f;

            //Store dialogue manager
            currentDialogueManager = _dialogueManager;

            //Start displaying dialogue
            currentDialogueManager.StartDialogue();
        }

        public void ShowSentence(DialogueCharacter _dialogueCharacter, string _message)
        {
            StopAllCoroutines();

            dialogueWindow.SetActive(true);

            portrait.sprite = _dialogueCharacter.characterPhoto;
            nameText.text = _dialogueCharacter.characterName;
            currentMessage = _message;

            if(currentDialogueManager.GetSentenceIndex() == buoyTextIndex)
            {
                swimmer.setDrown(true);
            }

            if(currentDialogueManager.GetSentenceIndex() == whistleTextIndex)
            {
                escapeTimer = 20f;
            }

            if (animateText)
            {
                StartCoroutine(WriteTextToTextmesh(_message, messageText));
            }
            else
            {
                messageText.text = _message;
            }
        }

        public void ClearText()
        {
            dialogueWindow.SetActive(false);
        }

        public void ShowInteractionUI(bool _value)
        {
            interactionUI.SetActive(_value);
        }

        public bool IsProcessingDialogue()
        {
            if(currentDialogueManager != null)
            {
                return true;
            }

            return false;
        }

        public bool IsTyping()
        {
            return typing;
        }

        public int CurrentDialogueSentenceLenght()
        {
            if (currentDialogueManager == null)
                return 0;

            return currentDialogueManager.CurrentSentenceLenght();
        }

        IEnumerator WriteTextToTextmesh(string _text, TextMeshProUGUI _textMeshObject)
        {
            typing = true;

            _textMeshObject.text = "";
            char[] _letters = _text.ToCharArray();

            float _speed = 1f - textAnimationSpeed;

            foreach(char _letter in _letters)
            {
                _textMeshObject.text += _letter;

                if(_textMeshObject.text.Length == _letters.Length)
                {
                    typing = false;
                }

                yield return new WaitForSeconds(0.1f * _speed);
            }
        }
    }
}