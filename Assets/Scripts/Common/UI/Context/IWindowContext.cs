
using Assets.Scripts.Common.EventAggregator;

namespace Assets.Scripts.Common.UI.Context
{
	public interface IWindowContext
	{
		IUIManager UIManager { get; }

		IPublisher Publisher { get; }

		CustomObject Data { get; }

		//IUserData { get; }
	}
}
