using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Managify.Models
{
    public class RegisteredEvent
    {
        // Just two columns that are composite primary keys and also happen to be the foreign keys.
        public virtual UserAccount User { get; set; }

        [Required]
        [Key]
        [Column(Order = 1)]
        public int UserAccountId { get; set; }

        public virtual Event Event{ get; set; }

        [Required]
        [Key]
        [Column(Order = 2)]
        public int EventId { get; set; }
    }
}