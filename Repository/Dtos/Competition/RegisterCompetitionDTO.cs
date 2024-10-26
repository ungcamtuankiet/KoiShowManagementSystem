using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.Dtos.Competition
{
    public class RegisterCompetitionDTO
    {
        [Required(ErrorMessage = "Competition name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string Description { get; set; }
    }
}