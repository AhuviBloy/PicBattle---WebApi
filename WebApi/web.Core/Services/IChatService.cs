using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.Services
{
    public interface IChatService
    {
        Task<string> GetResponseAsync(string prompt);

    }
}
