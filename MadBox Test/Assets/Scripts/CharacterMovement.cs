using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Rigidbody characterRigidbody;
    [SerializeField] Vector3 movementTarget;
    [SerializeField] bool reached = false;
    [SerializeField] float speed = 0.1f;
    [SerializeField] bool collided = false;
    [SerializeField] CameraMovement cameraPlayer;

    Vector3 firstPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (characterController == null) Debug.LogWarning("There is not character controller in this gameObject");
        if (characterRigidbody == null) characterRigidbody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(0f,0f,0f);
        firstPosition = transform.position;
    }

    // Controls the players movement;
    void Update()
    {
        if (!reached && !collided) {
            float movement = Input.GetAxis("Vertical");
            movement = Mathf.Max(movement, 0f);
            int index;
            Vector3 movementDirection = PathFollowing.instance.RuteDirection(false, transform.position, ref movementTarget, out index);
            if (movementDirection != Vector3.zero)
            {
                characterController.Move(movementDirection * movement * 0.1f);
                if(Vector3.Distance(transform.position,movementTarget) > 0.2f) transform.LookAt(movementTarget);
            }
            else
            {
                reached = true;
                characterRigidbody.velocity = Vector3.zero;
            }
        }
    }

    // Code to detect the collision of the player with a mortal obstacle, causing death
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            collided = true;
            characterRigidbody.useGravity = true;
            characterRigidbody.freezeRotation = false;
            characterRigidbody.AddForce(collision.impulse);
            StartCoroutine(Death());
        }
    }

    // Code to detect that player have reach the end of the game
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Final")
        {
            cameraPlayer.Final();
        }
    }

    //Code executed when player's death is detected
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        collided = false;
        characterRigidbody.useGravity = false;
        characterRigidbody.freezeRotation = true;
        transform.position = firstPosition;
        movementTarget = transform.position;
        cameraPlayer.Death();
    }
}
