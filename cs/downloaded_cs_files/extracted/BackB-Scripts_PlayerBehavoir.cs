using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerUIEvent : UnityEvent { }

[System.Serializable]
public class PlayerMovedEvent : UnityEvent { }

public class PlayerBehavoir : MonoBehaviour
{
    public Rigidbody rb;
    public ObjectPooler pool;
    public static int spawnCount;
    public static int numberOfMoves;
    public IngameUI gameUI;
    private Vector3 oneUnitVector = new Vector3(0.5f, 0.5f, 0.5f);
    public PlayerMovedEvent playerMovedEvent;

    public void Division()
    {
        if (BallsLeft() > 0)
        {
            numberOfMoves++;
            playerMovedEvent.Invoke();
            GameObject DividedBall = pool.GetPooledObject(0);
            DividedBall.transform.position = gameObject.transform.position;
            Vector3 spawnOffset = rb.velocity.normalized / 100.0f;
            spawnOffset = new Vector3(spawnOffset.y, spawnOffset.x, spawnOffset.z);
            DividedBall.transform.position = gameObject.transform.position + spawnOffset;
            Rigidbody rigidbody1 = DividedBall.GetComponent<Rigidbody>();
            DividedBall.SetActive(true);
            rigidbody1.velocity = Quaternion.Euler(0, 0, 90) * rb.velocity;
            rb.velocity = Quaternion.Euler(0, 0, -90) * rb.velocity;
            iTween.ScaleTo(this.gameObject, iTween.Hash(
                "scale", oneUnitVector,
                "time", 0.3f));
            iTween.ScaleTo(DividedBall, iTween.Hash(
                "scale", oneUnitVector,
                "time", 0.3f));
            // TO DO THIS NEED REFACTORY ; this for adding listeners when dividing to balls that havent been made by level generator
            DividedBall.GetComponent<PlayerBehavoir>().playerMovedEvent.RemoveAllListeners();
            DividedBall.GetComponent<PlayerBehavoir>().playerMovedEvent.AddListener(FindObjectOfType<LevelGenerator>().CheckPlayerMoves);


        }
    }

    public static int BallsLeft()
    {
        return LevelGenerator.ballsToUse - spawnCount;
    }

    public static void MoveBalls(TKSwipeRecognizer swipeRecognizer)
    {
        PlayerBehavoir.numberOfMoves++;
        Debug.Log("num of mov: " + PlayerBehavoir.numberOfMoves);
        GameObject[] playerSpawns = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerSpawn in playerSpawns)
        {
            Rigidbody rb = playerSpawn.GetComponent<Rigidbody>();
            switch (swipeRecognizer.completedSwipeDirection)
            {
                case TKSwipeDirection.Right:
                    rb.AddForce(new Vector3(3, 0, 0), ForceMode.Impulse);
                    break;
                case TKSwipeDirection.Left:
                    rb.AddForce(new Vector3(-3, 0, 0), ForceMode.Impulse);
                    break;
                case TKSwipeDirection.Up:
                    rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
                    break;
                case TKSwipeDirection.Down:
                    rb.AddForce(new Vector3(0, -3, 0), ForceMode.Impulse);
                    break;
            }
        }
        //this is the worst solution 
        FindObjectOfType<PlayerBehavoir>().playerMovedEvent.Invoke();
    }

    private void Awake()
    {
        GameObject UI =  GameObject.FindGameObjectWithTag("IngameUI");
        gameUI = UI.GetComponent<IngameUI>();
    }

    void OnEnable()
    {
        pool = GetComponentInParent<ObjectPooler>();
        rb = gameObject.GetComponent<Rigidbody>();
        ++spawnCount;
       // Debug.Log(" LOG: " + spawnCount + " level shit: " + LevelGenerator.ballsToUse);
        gameUI.UpdateBallLeftText();
        
        //if (angle < 360.0f)
        //    angle += 45.0f;
        // else
        //     angle = 0.0f;
        //rb.AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
        // rb.velocity = Quaternion.AngleAxis(angle, Vector3.right) * new Vector3(1, -1, 0);
        //rb.velocity = Vector3.zero;
        // Debug.Log("PrintOnEnable: script was enabled");
    }

    private void OnDisable()
    {
        --spawnCount;

        //IngameUI.playerUIEvent.Invoke();
        gameUI.UpdateBallLeftText();
        //animation of destrofying
    }

    private void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 playerPos = transform.position;
            Vector3 camPosition = Input.mousePosition;
            camPosition.z = playerPos.z - Camera.main.transform.position.z;
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(camPosition);
            touchPos.z = 0; playerPos.z = 0;
            // if((touchPos - playerPos).magnitude < 0.5f)
            //   Division();
            //Debug.Log("touch: " + touchPos + " player: " + playerPos);
        }
    }

    public void Deactivate()
    {
        iTween.ScaleTo(this.gameObject, iTween.Hash(
                "scale", Vector3.zero,
                "time", 0.5f
                ));
        SetUnactive();
    }

    private void SetUnactive()
    {
        gameObject.SetActive(false); 
    }
}
