
using System;
using System.Collections.Generic;
using Assets.Scripts.Common.EventAggregator;
using Assets.Scripts.Common.UI;
using Assets.Scripts.Game.Common;
using Assets.Scripts.Game.Events;
using Assets.Scripts.Game.UI.Controller;
using UnityEngine;

namespace Assets.Scripts.Game
{
	public class GameBootstart: IGameBootstart
	{
		private readonly IUIManager _uiManager;

		private readonly IPublisher _publisher;

		private readonly IInputManager _inputManager;

		public GameBootstart(IUIManager uiManager, IPublisher publisher, IInputManager inputManager)
		{
			_uiManager = uiManager;
			_publisher = publisher;
			_inputManager = inputManager;

			_inputManager.OnClick += OnSceneClick;
		}

		public void Startup()
		{
			_uiManager.ShowWindow<LeftPanelWindow>(null);

			//LoadUnits();
		}

		private void LoadUnits()
		{
			var unitsData = new List<Tuple<string, Vector3>>
			{
				new Tuple<string, Vector3>("unit_0", new Vector3(0, 0, 1)),
				new Tuple<string, Vector3>("unit_0", new Vector3(0, 0, 2)),
				new Tuple<string, Vector3>("unit_0", new Vector3(0, 0, 3)),
			};

			_publisher.Publish<CreateUnitsEvent, List<Tuple<string, Vector3>>>(unitsData);
		}

		private void OnSceneClick(RaycastHit hit)
		{
			_publisher.Publish<SceneClickEvent, RaycastHit>(hit);
		}
	}
}
