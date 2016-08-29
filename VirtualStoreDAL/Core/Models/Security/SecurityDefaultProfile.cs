namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Associates an SecurityProfileType with its default profile
    /// When a user is created, the profile assigned will be the default indicated here
    /// Only administrator could change this
    /// Administrators don't have a profile, they can access everything
    /// </summary>
    public partial class SecurityDefaultProfile : BaseAuditableModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public SecurityProfileType Id { get; set; }

        [NotMapped]
        public int IdSecurityProfile { get; set; }

        public virtual SecurityProfile SecurityProfileDefault { get; set; }
    }
}
