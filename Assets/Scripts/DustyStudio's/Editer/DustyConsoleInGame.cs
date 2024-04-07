using UnityEngine;
using UnityEngine.UI;

namespace DustyStudios
{
    public class DustyConsoleInGame : MonoBehaviour
    {
        [SerializeField] Text input, output;
        void Start()
        {
            Application.logMessageReceived += DustyConsole.LogCallback;
            DustyConsole.onPrint += Print;
            Print(null);
#if !UNITY_EDITOR
            DustyConsole.Print("Dusty Console is ready. Print help [Page] for help");
#endif
        }
        private void OnDestroy()
        {
            Application.logMessageReceived -= DustyConsole.LogCallback;
            DustyConsole.onPrint -= Print;   
        }
        void Print(string line)
        {
            output.text = DustyConsole.Output;
        }
        public void CommandPlayerInput()=>Command(input.text);
        public void Command(string command)=>DustyConsole.ExecuteCommandWithString(command);
    }
}