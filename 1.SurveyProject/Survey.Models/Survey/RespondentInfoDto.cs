using System.ComponentModel.DataAnnotations;

namespace Survey.Models.Survey
{
    public class RespondentInfoDto
    {
        [Required]
        public string UserAccountId { get; set; }

        [Required]
        public string SurveyDetailId { get; set; }
    }
}