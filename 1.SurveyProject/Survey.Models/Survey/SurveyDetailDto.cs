using System;
using Survey.Entities.Survey;

namespace Survey.Models.Survey
{
    public class SurveyDetailDto
    {
        public Guid SurveyDetailId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public static SurveyDetailDto ConvertToDto(SurveyDetail surveyDetail)
        {
            return new SurveyDetailDto
            {
                SurveyDetailId = surveyDetail.SurveyDetailId,
                Title = surveyDetail.Title,
                Description = surveyDetail.Description
            };
        }
    }
}