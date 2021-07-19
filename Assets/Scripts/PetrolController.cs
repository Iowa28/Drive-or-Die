using UnityEngine;
using UnityEngine.UI;

public class PetrolController : MonoBehaviour
{
    [SerializeField] private float petrolMaxCount = 100f;
    [SerializeField] private float canisterPetrolCount = 25f;
    [SerializeField] private float petrolDecreasingRate = 2f;

    [SerializeField] private Slider slider;
    
    private float petrolCurrentCount;
    private bool stop;
    
    void Start()
    {
        petrolCurrentCount = petrolMaxCount;
    }

    void LateUpdate()
    {
        if (stop)
            return;

        if (petrolCurrentCount > 0f)
        {
            petrolCurrentCount -= petrolDecreasingRate * Time.deltaTime;
            slider.value = petrolCurrentCount / petrolMaxCount;
        }
        else
        {
            stop = true;
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void IncreasePetrolCount()
    {
        petrolCurrentCount += canisterPetrolCount;
        if (petrolCurrentCount > petrolMaxCount)
        {
            petrolCurrentCount = petrolMaxCount;
        }
    }

    public void SetStop(bool newValue)
    {
        stop = newValue;
    }
}
