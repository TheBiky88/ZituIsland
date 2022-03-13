using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Gameplay;

namespace nickCode
{
    public class PillarManager : MonoBehaviour
    {
        public static PillarManager instance;

        public Door door;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }

        public Orientation[] solution = new Orientation[3];
        public Pillar[] pillars;

        public void OnPillarChanged()
        {
            int correctPillars = 0;

            for (int i = 0; i < solution.Length; i++)
            {
                if (pillars[i].Orientation == solution[i])
                {
                    correctPillars++;
                }
            }

            if (correctPillars == 3)
            {
                for (int i = 0; i < pillars.Length; i++)
                {
                    pillars[i].Solve();
                }

                door.OpenDoor();

            }
        }      
    }
}
