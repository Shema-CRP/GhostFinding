using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBehaviour : MonoBehaviour
{
    public enum EGeneratorState { Charged, Uncharged };
    [SerializeField] EGeneratorState GeneratorState;
    [SerializeField] float EnergyMax;
    [SerializeField] float CurrentEnergy;
    [SerializeField] float EnergyReloadSpeed;
    Renderer GeneratorColor;

    private void Start()
    {
        CurrentEnergy = 0;
        GeneratorState = EGeneratorState.Uncharged;
        // Search the component in the child GameObject
        GeneratorColor = this.transform.Find("ProgressBar").gameObject.GetComponent<Renderer>();
        // put the screen generator in red when he instantiate
        this.transform.Find("ProgressBar").gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void IncrementEnergy()
    {
        if(GeneratorState == EGeneratorState.Uncharged)
        {
            this.transform.Find("ProgressBar").gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            CurrentEnergy += EnergyReloadSpeed;
            if (CurrentEnergy > EnergyMax)
            {
                GeneratorState = EGeneratorState.Charged;
                this.transform.Find("ProgressBar").gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }
}