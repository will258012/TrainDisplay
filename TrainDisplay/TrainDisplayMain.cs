﻿using ICities;
using ColossalFramework.UI;
using UnityEngine;
using TrainDisplay.UI;
using TrainDisplay.Utils;

namespace TrainDisplay
{

	public class TrainDisplayMain : MonoBehaviour
	{

		public static TrainDisplayMain instance;

		VehicleManager vManager;
		public static void Initialize(LoadMode mode)
		{
			GameObject gameObject = new GameObject();
			instance = gameObject.AddComponent<TrainDisplayMain>();

		}

		public static void Deinitialize()
		{
			Destroy(instance);
		}

		void Awake()
        {
			vManager = VehicleManager.instance;
        }

		public DisplayUI displayUi;
		private bool showingDisplay = false;
		private ushort followInstance = 0;

		void Start()
		{
			displayUi = DisplayUI.Instance;
			displayUi.enabled = false;
		}

		//int debugcounter = 0;

		void Update()
        {

			// Toggle Showing
			bool newShowing = FPSCamera.FPSCamera.instance.vehicleCamera.following;
			if (newShowing != showingDisplay)
            {
				if (newShowing)
                {
					FPSCamera.VehicleCamera vCamera = FPSCamera.FPSCamera.instance.vehicleCamera;
					followInstance = CodeUtils.ReadPrivate<FPSCamera.VehicleCamera, ushort>(vCamera, "followInstance");
					newShowing = DisplayUIManager.Instance.SetTrain(followInstance);
				}
				showingDisplay = newShowing;
				displayUi.enabled = newShowing;
			}

			// When showing
			if (showingDisplay)
            {
				DisplayUIManager.Instance.updateNext();
			}
        }
	}
}
