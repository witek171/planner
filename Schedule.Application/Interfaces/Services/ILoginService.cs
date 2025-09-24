using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Application.Interfaces.Services;

public interface ILoginService
{
	Task<string> LoginAsync(Guid comapny_id, string username, string password);
}
