using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Camera cam;

    float verticalMovement;
    float horizontalMovement;

    Rigidbody2D rigidbody;

    public float movementSpeed = 10f;
    public float cameraMovementSpeed = 10f;

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        rigidbody = GetComponent<Rigidbody2D>();
        transform.position = cam.transform.position;
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rigidbody.velocity = new Vector2(horizontalMovement * movementSpeed * Time.fixedDeltaTime, verticalMovement * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Door") && rigidbody.velocity.magnitude > 0)
        {
            if (other.transform.position.y > cam.transform.position.y)
            {
                Vector3 cameraTargetPosition = cam.transform.position + Vector3.up * 14;
                Vector3 playerTargetPosition = new Vector3(cameraTargetPosition.x, cameraTargetPosition.y - 2.5f, 0);
                StartCoroutine(ChangeRoomRoutine(cameraTargetPosition, playerTargetPosition));
            }
            else if (other.transform.position.y < cam.transform.position.y)
            {
                Vector3 cameraTargetPosition = cam.transform.position - Vector3.up * 14;
                Vector3 playerTargetPosition = new Vector3(cameraTargetPosition.x, cameraTargetPosition.y + 2.5f, 0);
                StartCoroutine(ChangeRoomRoutine(cameraTargetPosition, playerTargetPosition));
            }
            else if (other.transform.position.x < cam.transform.position.x)
            {
                Vector3 cameraTargetPosition = cam.transform.position - Vector3.right * 20;
                Vector3 playerTargetPosition = new Vector3(cameraTargetPosition.x + 6.5f, cameraTargetPosition.y, 0);
                StartCoroutine(ChangeRoomRoutine(cameraTargetPosition, playerTargetPosition));
            }
            else if (other.transform.position.x > cam.transform.position.x)
            {
                Vector3 cameraTargetPosition = cam.transform.position + Vector3.right * 20;
                Vector3 playerTargetPosition = new Vector3(cameraTargetPosition.x - 6.5f, cameraTargetPosition.y, 0);
                StartCoroutine(ChangeRoomRoutine(cameraTargetPosition, playerTargetPosition));
            }
        }

    }

    IEnumerator ChangeRoomRoutine(Vector3 cameraTargetPosition, Vector3 playerTargetPosition)
    {
        cam.GetComponent<Camera>().ChangeRoomView(cameraTargetPosition);
        transform.position = playerTargetPosition;
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

}
