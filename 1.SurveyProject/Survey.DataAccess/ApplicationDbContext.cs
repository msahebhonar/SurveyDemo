using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Survey.Entities.Question;
using Survey.Entities.Survey;
using Survey.Entities.User;

namespace Survey.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<QuestionBank> QuestionBank { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<SurveyDetail> SurveyDetails { get; set; }

        public DbSet<SurveyDetailQuestion> SurveyDetailQuestions { get; set; }

        public DbSet<Respondent> Respondents { get; set; }

        public DbSet<RespondentAnswer> RespondentAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.AddAuditableProperites();

            base.OnModelCreating(modelBuilder);
        }
    }
}
