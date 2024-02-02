using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private Vector2 input;
    private Vector2 movement;
    private Animator animator;
    private Rigidbody2D rb;
    public Timer timer;
    public EndDialog dialog;
    public Object enemy;
    [SerializeField] HighScoreHandler highScoreHandler;
    private PlayerInput playerInput;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        timer.running = true;
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * playerSpeed);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        timer.running = false;
        playerInput.DeactivateInput();
        Debug.Log($"hit by enemy, timer: {timer.currentTime}");
        dialog.gameObject.SetActive(true);
        dialog.tryAgain.onClick.AddListener(TryAgainClicked);
        dialog.submitButton.onClick.AddListener(SubmitClicked);
        
        
    }
    
    private void TryAgainClicked()
    {
        rb.position = new Vector2(38, 4);
        enemy.GameObject().gameObject.transform.position = new Vector3(22, -1);
        playerInput.ActivateInput();
        timer.currentTime = 0;
        timer.running = true;
	dialog.gameObject.SetActive(false);
    }

	private void SubmitClicked()
	{
dialog.submitButton.onClick.RemoveListener(SubmitClicked);
		highScoreHandler.AddHighScore(new HighScoreElement(dialog.playerNameInput.text, timer.currentTime.ConvertTo<int>()));

		
	}
}
