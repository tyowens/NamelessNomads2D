using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private Fields
    // Actor number of player who shot this bullet
    private int playerId;

    private Vector2 trajectory;
    #endregion

    #region Private Serializable Fields
    [SerializeField]
    private int speed;
    #endregion

    #region Public Methods
    public void SetTrajectory(Vector2 vector)
    {
        this.trajectory = vector;
    }

    public void SetPlayerId(int id)
    {
        this.playerId = id;
    }
    #endregion

    #region MonoBehavior Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += (Vector3)(trajectory * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if hit player
        if(collider.gameObject.TryGetComponent(out PlayerManager playerManager))
        {
            if(playerManager.playerId == playerId)
            {
                return;
            }

            PhotonNetwork.Destroy(gameObject);
        }else if(collider.gameObject.TryGetComponent(out BoxManager boxManager))
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    #endregion

    #region MonoBehavior PUN Callbacks
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
    #endregion
}
