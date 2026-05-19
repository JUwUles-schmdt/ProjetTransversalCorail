using UnityEngine;

public class OceanWorld : MonoBehaviour
{
    [Header("Physics")]
    public float currentSpeed = 3f;
    public bool isFishingNet = false;

    [Header("Chimique")]
    public float nutrimentLvl = 10f;
    public float waterPH = 7f;
    public float microPlasticLvl = 10f;
}