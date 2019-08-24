using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDrink : MonoBehaviour, IDrinkWater
{
    bool isDrinking = false;
    public void DrinkWater(IHaveMass source)
    {
        if (!isDrinking)
        {
            isDrinking = true;
            IHaveDrinkSize _drinkSize = GetComponent<IHaveDrinkSize>();
            IHaveWater _water = GetComponent<IHaveWater>();
            if (_drinkSize != null)
            {
                if (source.Mass - _drinkSize.DrinkSize >= 0)
                {
                    source.Mass -= _drinkSize.DrinkSize;
                    if (_water.Water < _water.MaxWater)
                    {
                        _water.Water += _drinkSize.DrinkSize;
                    }
                }
            }
            StartCoroutine(Interval());
        }
    }
    IEnumerator Interval()
    {
        yield return new WaitForSeconds(3);
        isDrinking = false;
    }
}
