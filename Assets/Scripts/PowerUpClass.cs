using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PowerUpClass
{
    public Action<GameObject> PowerUpStartCommand;
    public Action<GameObject> PowerUpEndCommand;
    public int Duration;

    public bool Stopped;

    public PowerUpClass(Action<GameObject> powerUpStartCommand, Action<GameObject> powerUpEndCommand, int duration)
    {
        PowerUpStartCommand = powerUpStartCommand;
        PowerUpEndCommand = powerUpEndCommand;
        Duration = duration;
    }

    public async void RunPowerUp(GameObject gameObject)
    {
        PowerUpStartCommand.Invoke(gameObject);
        await Task.Delay(Duration);
        PowerUpEndCommand.Invoke(gameObject);
    }
}