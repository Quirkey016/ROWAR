using System.Collections;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float hori;
        public float moveSpeed = 7f;
        public float currentSpeed = 7;
        public float jump = 5f;
        public float dodgeSpeed = 14f;
        public bool isGrounded = false;
        public float groundCheckRange = 5.56f;
        public float iFrameLength = 0.25f;
        public bool isDodging = false;
        public Transform groundCheckOrigin;
        public PlayerHealth playerHealth;
        public float downwardForce = 1.2f;
        [SerializeField] private Animator animator;
        [SerializeField] LayerMask ground;
        private SpriteRenderer spriteRenderer;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            playerHealth = gameObject.GetComponent<PlayerHealth>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Gravity()
        {
            if (isGrounded) return;
           
            rb.AddForce(Vector2.down * downwardForce, (ForceMode2D)ForceMode.Force);
        }

        // Update is called once per frame
        void Update()
        {
        
            switch (isDodging)
            {
                case true: currentSpeed = dodgeSpeed;
                    break;
                case false: currentSpeed = moveSpeed;
                    break;
            }

            if (!Pause.IsPaused)
            {
                Inputs();
                GroundCheck();
                Controls();
                Gravity();

                spriteRenderer.flipX = hori switch
                {
                    -1 => true,
                    1 => false,
                    _ => spriteRenderer.flipX
                };
            }
        }

        void Inputs()
        {
            //gets A and D and makes them equal 1 and -1
            hori = Input.GetAxisRaw("Horizontal");
        }

        void Controls()
        {
            if (isGrounded)
            {
            
                //moves right if D is pressed
                if (hori == 1)
                {
                    animator.SetBool("IsRunning", true);
                    rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
                }
                //moves left if A is pressed
                if (hori == -1)
                {
                    animator.SetBool("IsRunning", true);
                    rb.linearVelocity = new Vector2(-currentSpeed, rb.linearVelocity.y);
                }
                if (hori == 0)
                {
                    animator.SetBool("IsRunning", false);
                    rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                }
            }

            if (!isGrounded)
            {
            
            
                //adds movement in the air but less
                if (hori == 1)
                {
                    rb.AddForce((transform.right * 10) * (moveSpeed * Time.deltaTime), ForceMode2D.Force);
                }

                //adds movement in the air but less
                if (hori == -1)
                {
                    rb.AddForce((transform.right * 10) * (-moveSpeed * Time.deltaTime), ForceMode2D.Force);
                }
            }
       
            //jumps if space is pressed but only while the player is grounded
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) 
            {
                rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
            }

            //stationary dodge
            if (Input.GetKeyDown(KeyCode.LeftShift) && hori == 0)
            {
                StartCoroutine(IFrame());
            }

            //moving right dodge
            if (Input.GetKeyDown(KeyCode.LeftShift) && hori == 1)
            {
                StartCoroutine(IFrame());
            }

            //moving left dodge
            if (Input.GetKeyDown(KeyCode.LeftShift) && hori == -1)
            {
                StartCoroutine(IFrame());
            }
        }

        //checks if the player is grounded
        void GroundCheck()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckOrigin.position, groundCheckRange, ground);
        }

        //draws gizmos so can see in scene
        //gizmo is wireframe thing around a thing
        //yellow if not ground
        //green if ground
        private void OnDrawGizmos()
        {
            Gizmos.color = isGrounded ? Color.yellow : Color.green;
            Gizmos.DrawWireSphere(groundCheckOrigin.position, groundCheckRange);
        }

        public IEnumerator IFrame()
        {
            Debug.Log("dodge");
            isDodging = true;
            playerHealth.isInvincible = true;
            yield return new WaitForSeconds(iFrameLength);
            playerHealth.isInvincible = false;
            isDodging = false;
        }
    }
}