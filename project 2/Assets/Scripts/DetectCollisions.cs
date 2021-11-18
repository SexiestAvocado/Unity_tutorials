using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCollisions : MonoBehaviour
{
    public Slider hungerSlider;
    public int amountToBeFed;

    private int currentFedAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        hungerSlider.maxValue = amountToBeFed;
        hungerSlider.value = 0;
        hungerSlider.fillRect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PlayerController.livesPlayer > 1)
            {
                PlayerController.livesPlayer -= 1;
                Debug.Log($"Lives: {PlayerController.livesPlayer}   Score: {PlayerController.score}");
            }
            else
            {
                Debug.Log("termino");
            }
            
        }
        else
        {
            FeedAnimal(1);

            //Destroy(gameObject);
            Destroy(other.gameObject);
            PlayerController.score += 1;
            Debug.Log($"Lives: { PlayerController.livesPlayer}      Score: { PlayerController.score}");
        }
        
    }
    public void FeedAnimal(int amount)
    {
        currentFedAmount += amount;
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentFedAmount;

        if (currentFedAmount >= amountToBeFed)
        {
            amountToBeFed +=1;
            Destroy(gameObject, 0.1f);
        }
    }
}
