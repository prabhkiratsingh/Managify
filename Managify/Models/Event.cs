using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Managify.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public decimal Fees { get; set; }
        
        // This property will be stored in the database and not ImageFile.
        public string ImagePath { get; set; }

        [Required]
        [NotMapped]
        [Display(Name = "Upload File")]
        public virtual HttpPostedFileBase ImageFile { set; get; }
        // ImageFile will the contain the original image uploaded by the admin while creation of events.
    }
}