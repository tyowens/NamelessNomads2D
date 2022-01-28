using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private Fields
    private Vector2 trajectory;

    [SerializeField]
    private float rangeRemaining;
    #endregion

    #region Private Serializable Fields
    [SerializeField]
    private int speed;
    #endregion

    #region Public Fields
    // Actor number of player who shot this bullet
    public int playerId;
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

    public void DestroyBullet()
    {
        Debug.Log("Entering DestroyBullet()");
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
        rangeRemaining -= 1 * Time.deltaTime;
        if(rangeRemaining <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
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

            collider.transform.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 10);

            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        else if(collider.gameObject.TryGetComponent(out BoxManager boxManager))
        {
            collider.transform.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 10);

            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
    #endregion

    #region MonoBehavior PUN Callbacks
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    #endregion
}
