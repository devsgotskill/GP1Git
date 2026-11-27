using UnityEngine;

public class testanimatn : MonoBehaviour
{
    [SerializeField]
    private Animator biganimation;
    void Start()
    {
        biganimation = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            biganimation.SetBool("boom", true);
        }
        else
        {
            biganimation.SetBool("boom", false);
        }
    }
}
