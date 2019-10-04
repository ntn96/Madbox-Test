using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] CharacterController cameraController;
    [SerializeField] Transform player;
    [SerializeField] Vector3 movementTarget;
    [SerializeField] float speed = 0.1f;
    [SerializeField] int actualIndex = 0;
    bool reached = false;
    bool final = false;
    float initialSpeed;
    [SerializeField] Text victoryText;

    Vector3 firstPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (cameraController == null) cameraController = GetComponent<CharacterController>();
        if (cameraController == null) Debug.LogWarning("There is not character controller in this gameObject");
        firstPosition = transform.position;
        initialSpeed = speed;
    }

    // Controls the movement of the camera according to player movement;
    void Update()
    {
        if (!reached)
        {
            float movement = 1f;
            if (!final) movement = Input.GetAxis("Vertical");
            movement = Mathf.Max(movement, 0f);
            Vector3 movementDirection = PathFollowing.instance.RuteDirection(true, transform.position, ref movementTarget, out actualIndex);
            if (movementDirection != Vector3.zero)
            {
                if (actualIndex == 3) speed = 0.3f;
                else speed = initialSpeed;
                cameraController.Move(movementDirection * movement * speed);
                transform.LookAt(player);
            } else
            {
                reached = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

        }
    }

    public void Death()
    {
        movementTarget = firstPosition;
        transform.position = firstPosition;
        actualIndex = 0;
        speed = initialSpeed;
    }

    //It is executed when end of game caused by victory is detected;
    public void Final()
    {
        final = true;
        victoryText.gameObject.SetActive(true);
    }
}
