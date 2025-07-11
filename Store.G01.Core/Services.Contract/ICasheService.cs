using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core.Services.Contract
{
    public interface ICasheService
    {
        Task SetCasheKeyAsync(string Key, object response, TimeSpan expireTime);
        Task<string> GetCasheKeyAsync(string key);
    }
}
