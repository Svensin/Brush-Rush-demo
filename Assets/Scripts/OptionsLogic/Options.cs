using System;
using UnityEngine;

namespace OptionsLogic
{
    public class Options : SingletonComponent<Options>
    {
        [SerializeField] private OptionsUi optionsUi;

        public OptionsUi OptionsUi => optionsUi;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}