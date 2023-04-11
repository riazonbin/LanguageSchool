using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Components
{
    public partial class ClientService
    {
        public TimeSpan TimeBeforeRecordStart => StartTime.Subtract(DateTime.Now);
    }
}
