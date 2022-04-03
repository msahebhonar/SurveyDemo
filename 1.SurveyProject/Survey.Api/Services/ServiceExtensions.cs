using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Survey.DataAccess;
using Survey.Services;
using Survey.Services.Implementation;

namespace Survey.Api.Services
{
    public static  class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Survey Api",
                    Version = "v1",
                    Description = "M.Sahebhonar"
                });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services.TryAddScoped<IUserAccountRepository, UserAccountRepository>();
            services.TryAddScoped<ISurveyDetailRepository, SurveyDetailRepository>();
            services.TryAddScoped<IQuestionBankRepository, QuestionBankRepository>();
            services.TryAddScoped<ISurveyDetailQuestionRepository, SurveyDetailQuestionRepository>();
            services.TryAddScoped<IRespondentRepository, RespondentRepository>();
            services.TryAddScoped<IRespondentAnswerRepository, RespondentAnswerRepository>();
        }
    }
}