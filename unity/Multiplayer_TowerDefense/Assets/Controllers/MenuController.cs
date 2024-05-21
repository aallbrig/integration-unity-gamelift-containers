using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private string clientConnectToLocalhostButtonName = "client-connect-localhost-game-server";
        public UnityEvent onClientConnectToLocalhost;
        private Button _clientConnectToLocalhostButton;
        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>();
            if (uiDocument == null)
            {
                Debug.LogError($"{name} | no ui document found");
                enabled = false;
                return;
            }
            _clientConnectToLocalhostButton = uiDocument.rootVisualElement.Q<Button>(clientConnectToLocalhostButtonName);
            if (_clientConnectToLocalhostButton == null)
            {
                Debug.LogError($"{name} | no 'connect to localhost' button found (using name: {clientConnectToLocalhostButtonName})");
                enabled = false;
                return;
            }
            _clientConnectToLocalhostButton.clicked += ConnectToLocalhostGameServer;
        }
        private void ConnectToLocalhostGameServer()
        {
            Debug.Log($"{name} | connecting to localhost game server...");
            onClientConnectToLocalhost?.Invoke();
        }
    }
}
