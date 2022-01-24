using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Public Fields
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    #endregion


    #region Private Fields
    private float spriteColorRed;
    private float spriteColorGreen;
    private float spriteColorBlue;
    private Rigidbody2D rb;
    private Vector2 velocity;
    #endregion


    #region Private Serializable Fields
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject boxPrefab;
    #endregion


    #region MonoBehaviour Callbacks
    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(spriteColorRed, spriteColorGreen, spriteColorBlue);

        // If the Player Prefab belongs to another user, then do not make modifications to its location
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        ProcessPlayerInput();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * speed * Time.deltaTime);
    }

    void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion


    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(spriteColorRed);
            stream.SendNext(spriteColorGreen);
            stream.SendNext(spriteColorBlue);
        }
        else
        {
            // Network player, receive data
            this.spriteColorRed = (float)stream.ReceiveNext();
            this.spriteColorGreen = (float)stream.ReceiveNext();
            this.spriteColorBlue = (float)stream.ReceiveNext();
        }
    }
    #endregion


    #region Public Methods
    void ProcessPlayerInput()
    {
        // Movement
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 tempVect = new Vector2(h, v);
        velocity = tempVect.normalized * speed * Time.deltaTime;

        // Color Changing
        if (Input.GetKeyUp("g"))
        {
            PickRandomSpriteColor();
        }

        // Spawn Ball
        if (Input.GetKeyUp("b"))
        {
            PhotonNetwork.Instantiate(this.boxPrefab.name, new Vector3(5f, 0f, 0f), Quaternion.identity, 0);
        }
    }

    public void PickRandomSpriteColor()
    {
        this.spriteColorRed = Random.Range(0f, 1f);
        this.spriteColorGreen = Random.Range(0f, 1f);
        this.spriteColorBlue = Random.Range(0f, 1f);
    }
    #endregion
}