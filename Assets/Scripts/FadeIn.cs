using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    private Image mb;

    void Start()
    {
        mb = GetComponent<Image>();
        Destroy(gameObject, 1);
    }

    void Update()
    {
        mb.color -= Color.black * Time.deltaTime;
    }
}