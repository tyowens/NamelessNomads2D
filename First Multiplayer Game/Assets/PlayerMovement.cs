using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
        UpdatePlayerPosition();
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