using System;
using System.Collections.Generic;
using Assets.Scripts.Common.EventAggregator;
using Assets.Scripts.Game.Model;
using Assets.Scripts.Game.Units;
using UnityEngine;

namespace Assets.Scripts.Game.Events
{
	public sealed class SceneClickAction: SceneClickEvent.ISubscribed
	{
		private readonly IGameScene _gameScene;

		private readonly IPublisher _publisher;

		public SceneClickAction(IGameScene gameScene, IPublisher publisher)
		{
			_gameScene = gameScene;
			_publisher = publisher;
		}

		public void OnEvent(RaycastHit hit)
		{
			IUnit unit = hit.transform.GetComponentInParent<IUnit>();
			switch (_gameScene.InputState)
			{
				case eInputState.Create:
					if (unit == null)
					{
						CreateUnit(hit.point);
					}
					else
					{
						ClickUnit(unit);
					}
					break;
				case eInputState.Move:
					if (unit == null)
					{
						if (_gameScene.SelectedUnit != null)
						{
							MoveUnit(_gameScene.SelectedUnit, hit.point);
						}
					}
					else
					{
						ClickUnit(unit);
					}
					break;
				case eInputState.Attack:
					if (_gameScene.SelectedUnit != null && unit != null)
					{
						_publisher.Publish<AttackUnitEvent, IUnit, IUnit>(_gameScene.SelectedUnit, unit);
					}
					break;
			}
		}

		private void MoveUnit(IUnit unit, Vector3 position)
		{
			_publisher.Publish<MoveUnitByPathEvent, IUnit, Vector3>(unit, position);
		}

		private void ClickUnit(IUnit unit)
		{
			_publisher.Publish<ClickUnitEvent, IUnit>(unit);
		}

		private void CreateUnit(Vector3 position)
		{
			var unitsData = new List<Tuple<string, Vector3>>
			{
				new Tuple<string, Vector3>("unit_0", position)
			};
			_publisher.Publish<CreateUnitsEvent, List<Tuple<string, Vector3>>>(unitsData);
		}
	}
}
