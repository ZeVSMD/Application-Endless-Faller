using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //[SerializeField] private Transform rPivot, lPivot;
    //public float moveChance;

    void Start()
    {

        /*moveChance = Random.Range(0, 10); //This is needed to set the chance of a moving platform to spawn       
        var distance = Random.Range(1f, 9f);

        rPivot.localScale += Vector3.right * distance;
        lPivot.localScale += Vector3.left * distance;*/
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * LevelManager.instance.platformSpeed;

        /*if (moveChance >= 7)
        {
            rPivot.localScale += Vector3.right * 0.05f * Mathf.Sin(Time.time);
            lPivot.localScale += Vector3.left* 0.05f * Mathf.Sin(Time.time);
        }*/
    }
}
