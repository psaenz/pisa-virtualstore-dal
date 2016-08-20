namespace Pisa.VirtualStore.Dal.Core.Models.General
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GeneralSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralSchedule()
        {
        }

        /***
         * This class supports the following schedules:
         * Each 9 days.
         * - Frequency = Daily
         * - FrequencyValue = 9
         * Each 2 weeks on mondays and frindays:
         * - Frequency = Weekly
         * - FrequencyValue = 2
         * - SpecificWeekday = L,M
         * Each 2 months on 20th of each month:
         * - Frequency = Montly
         * - FrequencyValue = 2
         * - SpecificMonthDay = 20
         * Each year every 20/08:
         * - Frequency = Yearly
         * - FrequencyValue = 1
         * - StartDate = 20/08/2016
         */
        public int Id { get; set; }

        public string Frequency { get; set; } // i.e: Daily, Weekly, Monthly, working days, Weekends, Specific Weekday

        public string SpecificWeekday { get; set; } // i.e: L, M, K, J, V, S, D | Weekly could define the days it was scheduled

        public int SpecificMonthDay { get; set; } // i.e: 1,2,3...30

        public int FrequencyValue { get; set; } // i.e: when Frequency is Daily, it indicates each 15 days. If weekly then each 1 week 

        public DateTime StartDate { get; set; }

        public string EndWhen { get; set; } // i.e: Never, SpecificDate, Xruns

        public DateTime EndDate { get; set; } // when EndWhen is "SpecificDate"

        public int EndInRuns { get; set; } // when EndWhen is "Xruns"

        public virtual GeneralStatus GeneralStatus { get; set; }
    }
}
