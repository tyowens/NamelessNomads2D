using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    #region Public Fields
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    #endregion


    #region Private Fields
    private float xPos;
    private float yPos;
    #endregion


    #region Private Serializable Fields
    [SerializeField]
    private float speed;
    #endregion


    #region MonoBehaviour Callbacks
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // If the Player Prefab belongs to another user, then do not make modifications to its location
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        UpdatePlayerPosition();
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


    #region Public Methods
    void UpdatePlayerPosition()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;

        this.gameObject.transform.position += tempVect;
    }
    #endregion
}