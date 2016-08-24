using Core.Model;
using Core.Model.Interfaces;

namespace Web.Client.ViewModels
{
    public class DeviceViewModel : BaseDevice
    {
        public DeviceViewModel()
        {
        }

        public DeviceViewModel(IDevice device) : base(device)
        {
        }
    }
}