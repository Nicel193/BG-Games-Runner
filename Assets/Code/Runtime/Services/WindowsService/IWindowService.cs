using Code.Runtime.UI;
using Code.Runtime.UI.Windows;

namespace Code.Runtime.Services.WindowsService
{
    public interface IWindowService
    {
        void Open(WindowType windowType, bool returnPage = false);
        void Close();
        void Initialize();
    }
}