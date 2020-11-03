using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PowerUpClass
{
    public Action<GameObject> PowerUpStartCommand;
    public Action<GameObject> PowerUpEndCommand;
    public float Duration;

    public bool Stopped;

    public PowerUpClass(Action<GameObject> powerUpStartCommand, Action<GameObject> powerUpEndCommand, int duration)
    {
        PowerUpStartCommand = powerUpStartCommand;
        PowerUpEndCommand = powerUpEndCommand;
        Duration = duration;
    }

    public PowerUpClass() {
        
    }
    // public async void RunPowerUp(GameObject gameObject)
    // {
    //     PowerUpStartCommand.Invoke(gameObject);
    //     await Task.Delay(Duration);
    //     PowerUpEndCommand.Invoke(gameObject);
    // }


    public IEnumerator RunPowerUpCoroutine(GameObject gameObject)
    {
        PowerUpStartCommand.Invoke(gameObject);
        yield return new WaitForSeconds(Duration);
        PowerUpEndCommand.Invoke(gameObject);
    }
}