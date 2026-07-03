using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Using standard UI instead of TMPro

public class DoorButtonScript : MonoBehaviour
{
    public Transform door;
    public Vector3 openPositionOffset;
    public float transitionTime = 3f;
    public Text promptText; // Changed to Legacy Text

    private bool isNear = false;
    private bool isOpen = false;
    private bool isMoving = false;
    private Vector3 closedPosition;

    void Start()
    {
        closedPosition = door.position;
        promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isNear && !isMoving && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(MoveDoor(closedPosition + openPositionOffset));
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            promptText.text = "Press P to open the Door";
            promptText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            promptText.gameObject.SetActive(false);
        }
    }

    IEnumerator MoveDoor(Vector3 targetPos)
    {
        isMoving = true;
        promptText.gameObject.SetActive(false);

        // 1. Slide Open
        float elapsedTime = 0;
        Vector3 startPos = door.position;
        while (elapsedTime < transitionTime)
        {
            door.position = Vector3.Lerp(startPos, targetPos, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        door.position = targetPos;
        isOpen = true;

        // 2. Wait 3 seconds
        yield return new WaitForSeconds(3f);

        // 3. Slide Closed
        elapsedTime = 0;
        startPos = door.position;
        while (elapsedTime < transitionTime)
        {
            door.position = Vector3.Lerp(startPos, closedPosition, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        door.position = closedPosition;

        isOpen = false;
        isMoving = false;

        if (isNear) promptText.gameObject.SetActive(true);
    }
}