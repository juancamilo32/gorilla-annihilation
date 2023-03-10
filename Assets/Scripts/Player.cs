using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    Rigidbody2D rigidbody;

    public float movementSpeed = 10f;

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>().GetComponent<Camera>();
        rigidbody = GetComponent<Rigidbody2D>();
        transform.position = cam.transform.position;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        ManagePlayerRotation();
        if (canMove)
        {
            rigidbody.velocity = movement * movementSpeed * Time.fixedDeltaTime;
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
        cam.GetComponent<CameraController>().ChangeRoomView(cameraTargetPosition);
        transform.position = playerTargetPosition;
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    void ManagePlayerRotation()
    {
        Vector2 lookDir = mousePos - rigidbody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rigidbody.rotation = angle;
    }

}
