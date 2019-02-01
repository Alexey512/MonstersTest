using Assets.Scripts.Common.EventAggregator;

namespace Assets.Scripts.Common.UI.Context
{
	public class WindowContext: IWindowContext
	{
		public IUIManager UIManager { get; private set; }
		public IPublisher Publisher { get; private set; }
		public CustomObject Data { get; private set; }

		public WindowContext(IUIManager uiManager, IPublisher publisher, CustomObject data)
		{
			UIManager = uiManager;
			Publisher = publisher;
			Data = data;
		}
	}
}
