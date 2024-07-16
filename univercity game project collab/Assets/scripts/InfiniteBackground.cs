using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject background1;
    public GameObject background2;
    public float backgroundSpeed = 150f;
    [SerializeField] private float backgroundWidth = 2000f;
    void Update()
    {
        MoveBackground(background1);
        MoveBackground(background2);

        if (background1.transform.position.x <= -backgroundWidth/2)
        {
            RepositionBackground(background1, background2);
        }
        if (background2.transform.position.x <= -backgroundWidth/2)
        {
            RepositionBackground(background2, background1);
        }
    }

    void MoveBackground(GameObject background)
    {
        background.transform.Translate(Vector3.left * backgroundSpeed * Time.deltaTime);
    }

    void RepositionBackground(GameObject background, GameObject otherBackground)
    {
        background.transform.position = new Vector3(otherBackground.transform.position.x + backgroundWidth, background.transform.position.y, background.transform.position.z);
    }
}
