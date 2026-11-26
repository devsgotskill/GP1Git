using UnityEngine;

//Script created by: Damian Zuniga
public class DetectionVolume : MonoBehaviour
{
    [SerializeField] public string tag_to_detect;
    public bool is_detecting = false;
    public Vector3 point_of_collision;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(tag_to_detect))
        {
            point_of_collision = other.transform.position;
            is_detecting = true;
        }
    }
}
