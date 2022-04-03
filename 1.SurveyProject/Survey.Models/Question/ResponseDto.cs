using Survey.Entities.Question;

namespace Survey.Models.Question
{
    public class ResponseDto
    {
        public int ResponseId { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }

        public static ResponseDto ConvertToDto(Response response)
        {
            return new ResponseDto
            {
                ResponseId = response.ResponseId,
                Text = response.Text,
                Order = response.Order
            };
        }
    }
}