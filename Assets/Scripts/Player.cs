using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    public int Health { get; set; }

    Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    Rigidbody2D rigidBody2D;

    public float movementSpeed = 10f;

    bool canMove = true;
    Animator animator;

    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        Health = 3;
        cam = FindObjectOfType<Camera>().GetComponent<Camera>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.position = cam.transform.position;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (dead)
        {
            UIManager.Instance.ActivateDeathScreen();
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.RestartGame();
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            ManagePlayerRotation();
            rigidBody2D.velocity = movement * movementSpeed * Time.fixedDeltaTime;
        }
        else
        {
            rigidBody2D.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Door") && rigidBody2D.velocity.magnitude > 0)
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
        Vector2 lookDir = mousePos - rigidBody2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rigidBody2D.rotation = angle;
    }

    public void TakeDamage()
    {
        Health--;
        UIManager.Instance.UpdateHealthUI(Health);
        if (Health < 1)
        {
            StartCoroutine(DeathRoutine());
            animator.SetTrigger("Die");
            AudioManager.instance.Play("Death");
            canMove = false;
            gameObject.GetComponent<Shooting>().Death();
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            AudioManager.instance.Play("Ouch");
        }
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1f);
        dead = true;
    }

}
