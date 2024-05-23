using Mirror;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        // [SerializeField] private string connectButtonQuery = "client-connect-localhost-game-server";
        [SerializeField] private string btnQuickPlayQuery = "btn_quick_play";
        [SerializeField] private string btnFindAGameQuery = "btn_find_a_game";
        [SerializeField] private string btnServerBrowserQuery = "btn_server_browser";
        [SerializeField] private string btnSettingsQuery = "btn_settings";
        [SerializeField] private NetworkManager networkManager;
        // public UnityEvent onClientConnectToLocalhost;
        private Button _clientConnectToLocalhostButton;
        private int _currentSelectionIndex = 0;
        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>();
            if (uiDocument == null)
            {
                Debug.LogError($"{name} | no ui document found");
                enabled = false;
                return;
            }
            networkManager ??= FindObjectOfType<NetworkManager>();
            uiDocument.rootVisualElement.Q<Button>(btnQuickPlayQuery);
            uiDocument.rootVisualElement.Q<Button>(btnFindAGameQuery);
            uiDocument.rootVisualElement.Q<Button>(btnServerBrowserQuery);
            uiDocument.rootVisualElement.Q<Button>(btnSettingsQuery);
            uiDocument.rootVisualElement.Q<VisualElement>("[focusable=true]");
            /*
            _clientConnectToLocalhostButton = uiDocument.rootVisualElement.Q<Button>(connectButtonQuery);
            if (_clientConnectToLocalhostButton == null)
            {
                Debug.LogError($"{name} | no 'connect to localhost' button found (using query: {connectButtonQuery})");
                enabled = false;
                return;
            }
            _clientConnectToLocalhostButton.clicked += ConnectToLocalhostGameServer;
            */
        }
        /*
        private void ConnectToLocalhostGameServer()
        {
            Debug.Log($"{name} | connecting to localhost game server...");
            onClientConnectToLocalhost?.Invoke();
        }
        */
        private void OnNavigate(InputValue inputValue)
        {
            Debug.Log($"{name} | On navigate {inputValue.Get<Vector2>()} focused element: {uiDocument.rootVisualElement.focusController.focusedElement}");
        }
        private void OnSubmit(InputValue inputValue)
        {
            Debug.Log($"{name} | On submit {inputValue.isPressed}");
        }
    }
}
