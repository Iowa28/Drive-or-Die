using System;
using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [Header("Invincibility")] 
    [SerializeField] private float invincibilityRespawnLenght;
    [SerializeField] private Renderer bodyRenderer;
    [SerializeField] private Renderer wheel1;
    [SerializeField] private Renderer wheel2;
    [SerializeField] private Renderer wheel3;
    [SerializeField] private Renderer wheel4;
    [SerializeField] private float flashLenght = .1f;
    
    [Header("Respawn")]
    [SerializeField] private float respawnOffsetX = 5f;
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private float correctY = 1f;
    [SerializeField] private float correctZ;

    [Header("Rotating border")] 
    [SerializeField] private float defaultRotationY = 270f;
    [SerializeField] private float maxOffset = 90f;

    private float invincibilityCounter;
    private float flashCounter;
    private bool isRespawning;
    
    void Update()
    {
        if (IsInvincible())
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0f)
            {
                bodyRenderer.enabled = !bodyRenderer.enabled;
                wheel1.enabled = !wheel1.enabled;
                wheel2.enabled = !wheel2.enabled;
                wheel3.enabled = !wheel3.enabled;
                wheel4.enabled = !wheel4.enabled;
                flashCounter = flashLenght;
            }

            if (invincibilityCounter <= 0f)
            {
                bodyRenderer.enabled = true;
                wheel1.enabled = true;
                wheel2.enabled = true;
                wheel3.enabled = true;
                wheel4.enabled = true;
                gameManager.SetPetrolStopValue(false);
            }
        }

        CheckRotationStuck();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        StartCoroutine(MakeRespawn());
    }

    IEnumerator MakeRespawn()
    {
        isRespawning = true;
        MakeInvincibility(invincibilityRespawnLenght);
        
        yield return new WaitForSeconds(respawnDelay);
        
        transform.rotation = Quaternion.Euler(0, -90, 0);
        transform.position += new Vector3( respawnOffsetX, correctY, correctZ);
        isRespawning = false;
    }
    
    public void MakeInvincibility(float invincibilityLenght)
    {
        invincibilityCounter = invincibilityLenght;
        
        bodyRenderer.enabled = false;
        wheel1.enabled = false;
        wheel2.enabled = false;
        wheel3.enabled = false;
        wheel4.enabled = false;
        
        flashCounter = flashLenght;
        
        gameManager.SetPetrolStopValue(true);
    }

    private void CheckRotationStuck()
    {
        if (Math.Abs(transform.rotation.eulerAngles.y - defaultRotationY) > maxOffset && !isRespawning)
        {
            Respawn();
        }
    }
    
    public bool IsInvincible()
    {
        return invincibilityCounter > 0f;
    }

    public bool IsRespawning()
    {
        return isRespawning;
    }
}
