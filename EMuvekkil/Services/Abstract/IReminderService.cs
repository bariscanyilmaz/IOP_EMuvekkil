using System.Collections.Generic;
using EMuvekkil.Models;

namespace EMuvekkil.Services.Abstract
{
    public interface IReminderService
    {
        string AddReminder(EventViewModel ev, IList<UserViewModel> users);
        bool DeleteReminder(string jobId);
    }
}