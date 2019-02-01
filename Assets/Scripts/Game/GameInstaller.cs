using Assets.Scripts.Common.Commands;
using Assets.Scripts.Common.EventAggregator;
using Assets.Scripts.Common.UI;
using Assets.Scripts.Game.Commands;
using Assets.Scripts.Game.Common;
using Assets.Scripts.Game.Events;
using Assets.Scripts.Game.Model;
using Assets.Scripts.Game.Navigation;
using Assets.Scripts.Game.UI;
using Assets.Scripts.Game.Units;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField]
		private WindowFactory _windowFactory;

		[SerializeField]
		private UnitsFactory _unitsFactory;

		[SerializeField]
		private WindowRoot _windowRoot;

		[SerializeField]
		private InputManager _inputManager;

		public override void InstallBindings()
		{
			InstallEvents();

			Container.Bind<IGameScene>().To<GameScene>().AsSingle();

			Container.Bind<IWindowRoot>().To<WindowRoot>().FromInstance(_windowRoot);
			Container.Bind<IWindowFactory>().To<WindowFactory>().FromInstance(_windowFactory);
			Container.Bind<IUIManager>().To<UIManager>().AsSingle();

			Container.Bind<IUnitsFactory>().To<UnitsFactory>().FromInstance(_unitsFactory);

			Container.Bind<IInputManager>().To<InputManager>().FromInstance(_inputManager);

			Container.Bind<ICommandsManager>().To<CommandsManager>().AsSingle();
			Container.Bind<IUnitCommandsStorage>().To<UnitCommandsStorage>().AsSingle();

			Container.Bind<IPathfinder>().To<Pathfinder>().AsSingle();

			Container.Bind<IGameBootstart>().To<GameBootstart>().AsSingle();
			Container.Resolve<IGameBootstart>().Startup();
		}

		private void InstallEvents()
		{
			Container.Bind<IPublisher>().To<Publisher>().AsSingle();
			Container.Bind<ISubscriber>().To<EventAggregator>().AsSingle();

			Container.Bind<SetInputStateEvent>().AsSingle();
			Container.Bind<SetInputStateEvent.ISubscribed>().To<SetInputStateAction>().AsSingle();

			Container.Bind<CreateUnitsEvent>().AsSingle();
			Container.Bind<CreateUnitsEvent.ISubscribed>().To<CreateUnitsAction>().AsSingle();

			Container.Bind<SceneClickEvent>().AsSingle();
			Container.Bind<SceneClickEvent.ISubscribed>().To<SceneClickAction>().AsSingle();

			Container.Bind<ClickUnitEvent>().AsSingle();
			Container.Bind<ClickUnitEvent.ISubscribed>().To<ClickUnitAction>().AsSingle();

			Container.Bind<MoveUnitByPathEvent>().AsSingle();
			//Container.Bind<MoveUnitByPathEvent.ISubscribed>().To<MoveUnitByPathAction>().AsSingle();
			Container.Bind<MoveUnitByPathEvent.ISubscribed>().To<MoveUnitByLoopAction>().AsSingle();

			Container.Bind<AttackUnitEvent>().AsSingle();
			Container.Bind<AttackUnitEvent.ISubscribed>().To<AttackUnitAction>().AsSingle();
		}
	}
}
