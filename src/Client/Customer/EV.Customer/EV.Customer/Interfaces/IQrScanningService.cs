using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Customer.Interfaces
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
