using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateForm : MonoBehaviour
{
    private Transform endPoint;
    public SpriteRenderer spriteRenderer;

    public GameObject plateform;
    public GameObject spriteGO;
    public Transform player;

    public float maxDistX;
    public float minDistX;

    public float maxDistY;
    public float minDistY;

    public float maxSize;
    public float minSize;

    private Vector2 dist;
    private Vector3 initialPosition;
    private bool turn = true;
    private bool animated = false;
    private bool alreadyCreate = false;

    public float moveSpeed;
    public float maxMove;
    public float minMove;

    public Color normal;
    public Color animatedcol;
    public Color bounce;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player").GetComponent<Transform>();
        spriteGO.tag = "ground";
        moveSpeed = Random.Range(0.5f, 2);
        transform.localScale = new Vector3(Random.Range(minSize, maxSize), 1, 1);
        endPoint = GameObject.Find("endpoint").GetComponent<Transform>();
        Debug.Log(endPoint.position.y);
        initialPosition = transform.position;
        Animated();
    }

    void Update()
    {
        if(animated)
        {
            if (transform.position.y < (initialPosition.y + maxMove) && turn)
            {
                transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;

            }else if(transform.position.y > (initialPosition.y + maxMove))
            {
                turn = false;
            }

            if (transform.position.y > (initialPosition.y + minMove) && !turn)
            {
                transform.position -= new Vector3(0, moveSpeed, 0) * Time.deltaTime;

            }else if(transform.position.y < (initialPosition.y + minMove))
            {
                turn = true;
            }
        } 

        if(Mathf.Abs(Mathf.Pow(player.position.x - this.transform.position.x,2)) * Mathf.Abs(Mathf.Pow(player.position.x - this.transform.position.x, 2)) <= Mathf.Pow(10,2) && !alreadyCreate)
        {
            StartCoroutine(CreatePlateform());
            alreadyCreate = true;
        }
    }
   
   public IEnumerator CreatePlateform()
   {
       yield return new WaitForSeconds(0.5f);
       dist = new Vector2(transform.position.x + Random.Range(minDistX, maxDistX), initialPosition.y + Random.Range(minDistY, maxDistY));
       Instantiate(plateform, new Vector3(dist.x, dist.y, 0), Quaternion.identity);
   }

    public void Animated()
    {
        float amIAnimated = Mathf.Floor(Random.Range(0, 2.99f));

        if(amIAnimated == 0)
        {
            spriteRenderer.color = normal;
            return;
        }
        else if(amIAnimated == 1)
        {
            Debug.Log("animated");
            animated = true;
            spriteRenderer.color = animatedcol;
        }
        else if (amIAnimated == 2)
        {
            Debug.Log("bounce");
            spriteRenderer.color = bounce;
            spriteGO.tag = "bounce";
        }
    }

    
}
