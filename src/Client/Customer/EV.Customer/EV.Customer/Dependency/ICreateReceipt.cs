using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Customer.Dependency
{
    public interface ICreateReceipt
    {
        Task<bool> SavePhotoAsync(byte[] data, string folder, string filename);
    }
}
