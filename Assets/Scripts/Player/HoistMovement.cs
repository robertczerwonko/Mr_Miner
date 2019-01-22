using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoistMovement : MonoBehaviour {


    
    [SerializeField]
    private float rightPushRange = 0.1f;
    [SerializeField]
    private float leftPushRange = -0.1f;
    [SerializeField]
    private float velocityTreshold = 50f;
    [SerializeField] private GameObject line;
    [SerializeField] private float MovingDownTonextLevelSpeed;

    private Rigidbody2D rb2d;
    private Renderer rend;

    public static HoistMovement instance;
    void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
       
    }


    void Start()
    {
        rb2d.angularVelocity = velocityTreshold;
    }


    public float speed = 9f;

    public bool GameStarted = false;
    public bool GoToNextLevel = false;
    public bool GoToNextMasterLevel = false;
    public bool MadeTwoMoves = false;
    public int countMoves;

    private bool wantToPull = false;
    private bool movingDown;
    private bool calledTreshold = true;
    private bool WentUp;
    private float OldVelocity;

    void Update()
    {

        if(GoToNextMasterLevel)
        {
            if (Input.GetMouseButtonDown(0)) GoToNextMasterLevel = false;
            line.SetActive(false);
        }
        else if (GoToNextLevel)
        {
            line.SetActive(false);
            rb2d.freezeRotation = true;
            transform.Translate(Vector3.down * MovingDownTonextLevelSpeed);
            if (!rend.isVisible && !WentUp)
            {
                WentUp = true;
                transform.position = new Vector3(0, 4.6f, 0);
            }
            else if (WentUp && transform.position.y < 1.8f)
            {
                WentUp = false;
                rb2d.freezeRotation = false;
                GoToNextLevel = false;
            }
        }
        else if(GameStarted)
        {


            if (Input.GetMouseButtonDown(0) && !wantToPull)
            {

                OldVelocity = rb2d.angularVelocity;
                wantToPull = true;
                movingDown = true;
                line.SetActive(false);
            }

            //Jezeli nie chce wcyiagac, lub jest w trakcie to buja chwytakiem
            if (!wantToPull)
            {
                line.SetActive(true);
                if (!calledTreshold)
                {
                    if (OldVelocity < 0 || OldVelocity == 0)
                    {
                        rb2d.angularVelocity = velocityTreshold * -1;
                    }
                    else
                    {
                        rb2d.angularVelocity = velocityTreshold;
                    }
                    calledTreshold = true;
                }
                SwingHoist();
            }
            else if (movingDown)
            {
                rb2d.freezeRotation = true;
                transform.Translate(-Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Blocker" && wantToPull)
        {
            rb2d.freezeRotation = false;
            wantToPull = false;

            #region bonus level counter
            //NEED LIMITS FOR BONUS LEVEL, DOESNT INVOLVE NORMAL STAGES!
            if (BonusStage.instance.isPlaying)
            {
                countMoves++;
                BonusStage.instance.madeMove();
                if (countMoves >= 2)
                {
                    countMoves = 0;
                    MadeTwoMoves = true;
                }
            }
            else
            {
                MadeTwoMoves = false;
                countMoves = 0;
            }
            #endregion
        }

        if (col.gameObject.tag=="Ground" || col.gameObject.tag == "wall")
        {
            movingDown = false;
            calledTreshold = false;
        }
    }

    private void SwingHoist()
    {
        if (transform.rotation.z > 0 && transform.rotation.z < rightPushRange && rb2d.angularVelocity > 0 && rb2d.angularVelocity < velocityTreshold)
        {
            rb2d.angularVelocity = velocityTreshold;
        }
        else if (transform.rotation.z < 0 && transform.rotation.z > leftPushRange && rb2d.angularVelocity < 0 && rb2d.angularVelocity > velocityTreshold * -1)
        {
            rb2d.angularVelocity = velocityTreshold * -1;
        }
    }

    public bool ReturnedToDefaultPosition()
    {
        if (!wantToPull && !rb2d.freezeRotation) return true;
        return false;
    }
    
}
