using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.General
{
    class GeneralScheduleEntityConfiguration : BaseEntityConfiguration<GeneralSchedule>
    {
        GeneralScheduleEntityConfiguration() {
            MakeRequired(fk => fk.GeneralStatus);
        }
    }
}
