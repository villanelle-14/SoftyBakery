using UnityEngine;

namespace Logic.VIew2D
{
    public class MealController : MonoBehaviour
    {
        private MealSequenceView selfMeal;
        public void SetMeal(MealSequenceView meal)
        {
            if (selfMeal != null)
            {
                Destroy(selfMeal.gameObject);
            }

            selfMeal = meal;
            if (meal != null)
            {
                selfMeal.transform.parent = transform;                
            }

        }
        public void Hit(float progress)
        {
            selfMeal.FillingProgress = progress;
        }
    }
}