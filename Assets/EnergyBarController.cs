using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyBarController : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slider;
    public float MaxEnergy = 10f;
    public  float EnergyNow = 10f; 

    public float ShootCost = 2f;

    public float TimeBeforeStartReloadEnergyRemember = 1.6f;
    public float TimeBeforeStartReloadEnergy = 2f;
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = MaxEnergy;
        slider.value = MaxEnergy;
        EnergyNow = MaxEnergy;

        TimeBeforeStartReloadEnergy = TimeBeforeStartReloadEnergyRemember;
    }

    void SetEnergy(float energy){
        slider.value = energy;
    }

    public void OnShoot(){
        EnergyNow = EnergyNow - ShootCost;
        SetEnergy(EnergyNow);
        TimeBeforeStartReloadEnergy = TimeBeforeStartReloadEnergyRemember;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeBeforeStartReloadEnergy<=0 && EnergyNow<MaxEnergy){
             EnergyNow = EnergyNow + 1;
             SetEnergy(EnergyNow);
        }

    }

    private void FixedUpdate() {
        TimeBeforeStartReloadEnergy = TimeBeforeStartReloadEnergy - 1 * Time.fixedDeltaTime; 
    }
}
