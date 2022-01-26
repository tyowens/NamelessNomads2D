using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    #region Private Fields
    public int health = 40;
    #endregion

    #region MonoBehavior Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestruction();

        // Color Updates
        Color boxColor;
        if(health <= 10)
        {
            boxColor = Color.black;
        }
        else if(health <= 30)
        {
            boxColor = Color.grey;
        }
        else
        {
            boxColor = Color.white;
        }
        gameObject.GetComponent<SpriteRenderer>().color = boxColor;

        // Movement Updates
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    #endregion

    #region Public Methods
    [PunRPC]
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }

    public void CheckDestruction()
    {
        if(health <= 0 && gameObject.GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    #endregion
}
