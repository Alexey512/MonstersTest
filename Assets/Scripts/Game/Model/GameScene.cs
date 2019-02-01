using System.Collections.Generic;
using Assets.Scripts.Game.Units;

namespace Assets.Scripts.Game.Model
{
	public sealed class GameScene: IGameScene
	{
		public eInputState InputState { get; set; }

		public List<IUnit> Units { get; private set; } = new List<IUnit>();
		public IUnit SelectedUnit { get; set; }
	}
}
