using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyObject : MonoBehaviour, IPointerDownHandler
{
    private bool clicked = false;
    private float timer = 0f;
    private float maxTime = 10f; // Maximum time before generating popup

    // Prefab for the popup message
    public GameObject popupPrefab;
    private GameObject popupInstance;

    private void Start()
    {
        AddPhysics2DRaycaster();
    }

    private void Update()
    {
        if (!clicked)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                GeneratePopup();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked = true;
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        DestroyPopup();
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    private void GeneratePopup()
    {
        if (popupPrefab != null)
        {
            // Instantiate the popup message prefab
            popupInstance = Instantiate(popupPrefab, Vector3.zero, Quaternion.identity);

            // Set the parent of the popup to the main camera
            popupInstance.transform.SetParent(Camera.main.transform, false);

            // Set the position of the popup in front of the camera
            popupInstance.transform.localPosition = new Vector3(0f, 0f, 2f); // Adjust the z-value as needed

            // Set the text of the popup
            Text popupText = popupInstance.GetComponentInChildren<Text>();
            if (popupText != null)
            {
                popupText.text = "A dangerous situation is happening, try to identify it!";
            }
            else
            {
                Debug.LogWarning("Popup prefab does not contain a Text component!");
            }
        }
        else
        {
            Debug.LogWarning("Popup prefab is not assigned!");
        }
    }

    private void DestroyPopup()
    {
        if (popupInstance != null)
        {
            Destroy(popupInstance);
        }
    }
}
