using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator biganimation;
    [SerializeField]
    private float gunCooldown = 1.2f;
    private float cooldownTime;
    private bool ReadyToPlay = true;
    
    void Start()
    {
        biganimation = GetComponent<Animator>();
    }

    void Update()
    {
        cooldownTime += Time.deltaTime;
        if (cooldownTime > gunCooldown)
        {
            ReadyToPlay = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && ReadyToPlay == true)
        {
            biganimation.SetBool("boom", true);
            ReadyToPlay = false;
            cooldownTime = 0;
        }
        else
        {
            biganimation.SetBool("boom", false);
        }
    }
}
