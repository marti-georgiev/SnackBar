using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBar.Core.Contracts
{
    public interface IAdmin
    {
        public Task AddNewKey(string key);
        public Task<bool> IsKeyValid(string key_attempt);

    }
}
