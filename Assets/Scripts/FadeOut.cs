using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    Image mb;

    void Start()
    {
        mb = GetComponent<Image>();
    }

    private void Update()
    {
       mb.color += Color.black * Time.deltaTime;
    }
}