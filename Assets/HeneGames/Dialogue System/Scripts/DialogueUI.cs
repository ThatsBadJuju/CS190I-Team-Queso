using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HeneGames.DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        #region Singleton

        public static DialogueUI instance { get; private set; }

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
        }

        #endregion

        public DialogueManager currentDialogueManager;
        private bool typing;
        public bool completed = false;
        private string currentMessage;
        private float startDialogueDelayTimer;
        private float escapeTimer;
        bool wrong = false;

        [Header("References")]
        [SerializeField] private Image portrait;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private GameObject dialogueWindow;
        [SerializeField] private GameObject interactionUI;
        [SerializeField] private GameObject incorrectUI;

        [Header("Settings")]
        [SerializeField] private bool animateText = true;

        [Range(0.1f, 1f)]
        [SerializeField] private float textAnimationSpeed = 0.5f;

        public VRInputSystem vrInput;

        [Header("Action input")]
        public KeyCode actionInput = KeyCode.Space;

        [Header("Force skip input")]
        public KeyCode skipInput = KeyCode.Escape;

        public Swimmer swimmer;
        public Waypoints runner;

        private int buoyGrabIndex = 2;
        private int buoyTextIndex = 3;
        private int whistleGrabIndex = 4;
        private int whistleTextIndex = 5;
        private int escapeIndex = 6;

        private int urWrongIndex = 8;
        private int wrongIndex1 = 9;
        private int drownAgainIndex = 11;
        private int wrongIndex2 = 12;
        private int lastIndex =14;

        private void Update()
        {
            if (completed) return;
            //Delay timer
            if(startDialogueDelayTimer > 0f)
            {
                startDialogueDelayTimer -= Time.deltaTime;
            }
            InputUpdate();
        }

        public virtual void InputUpdate()
        {
            //Next dialogue input
            int sentenceIndex = currentDialogueManager.GetSentenceIndex();
            if ((Input.GetKeyDown(actionInput) || vrInput.primaryButtonDown) 
            && sentenceIndex != buoyGrabIndex && sentenceIndex != buoyTextIndex && sentenceIndex != whistleGrabIndex
            && sentenceIndex != whistleTextIndex && sentenceIndex != escapeIndex && sentenceIndex != wrongIndex1 && 
            sentenceIndex != drownAgainIndex && sentenceIndex != wrongIndex2 && sentenceIndex != lastIndex)
            {
                Debug.Log("IN");
                NextSentenceSoft();
            }
            else if((Input.GetKeyDown(skipInput) || vrInput.secondaryButtonDown) && sentenceIndex == escapeIndex)
            {
                NextSentenceHard();
            } else if(wrong) {
                NextSentenceHard();
                wrong = false;
                incorrectUI.SetActive(false);

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

            //Hardcoding the index of the buoy sentence, change if necessary
            if(currentDialogueManager.GetSentenceIndex() == buoyTextIndex 
            || currentDialogueManager.GetSentenceIndex() == drownAgainIndex)
            {
                NextSentenceHard();
            }
        }

        public void NextSentenceIfBuoyGrab()
        {
            if (currentDialogueManager == null)
                return;

            //Hardcoding the index of the buoy sentence, change if necessary
            if (currentDialogueManager.GetSentenceIndex() == buoyGrabIndex)
            {
                NextSentenceHard();
            }
        }

        public void NextSentenceIfWhistle()
        {
            if (currentDialogueManager == null)
                return;

            //Hardcoding the index of the buoy sentence, change if necessary
            if (currentDialogueManager.GetSentenceIndex() == whistleTextIndex)
            {
                NextSentenceHard();
            }
        }

        public void NextSentenceIfWhistleGrab()
        {
            if (currentDialogueManager == null)
                return;

            //Hardcoding the index of the buoy sentence, change if necessary
            if (currentDialogueManager.GetSentenceIndex() == whistleGrabIndex)
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

            if (currentDialogueManager == null)
                return;

            if (currentDialogueManager.GetSentenceIndex() == buoyTextIndex
            || currentDialogueManager.GetSentenceIndex() == drownAgainIndex)
            {
                swimmer.setDrown(true);
            }

            if(currentDialogueManager.GetSentenceIndex() == whistleTextIndex)
            {
                runner.StartRunning();
            }

            if(currentDialogueManager.GetSentenceIndex() == urWrongIndex ||
            currentDialogueManager.GetSentenceIndex() == wrongIndex2)
            {
                incorrectUI.SetActive(true);
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
        public void setWrong() {
            wrong = true;
        }
    }
}