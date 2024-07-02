using WebAppForSmartBusiness.Repositories;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddScoped<IAbsenceReasonRepository, AbsenceReasonRepository>(provider => new AbsenceReasonRepository(connectionString));
            builder.Services.AddScoped<IApprovalRequestRepository, ApprovalRequestRepository>(provider => new ApprovalRequestRepository(connectionString));
            builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>(provider => new LeaveRequestRepository(connectionString));
            builder.Services.AddScoped<IPositionRepository, PositionRepository>(provider => new PositionRepository(connectionString));
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>(provider => new ProjectRepository(connectionString));
            builder.Services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>(provider => new ProjectTypeRepository(connectionString));
            builder.Services.AddScoped<ISubdivisionRepository, SubdivisionRepository>(provider => new SubdivisionRepository(connectionString));
            builder.Services.AddScoped<IUserRepository, UserRepository>(provider => new UserRepository(connectionString));
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(provider => new EmployeeRepository(connectionString));
            builder.Services.AddScoped<ICreatecommandForFilterRepository, CreateCommandForFilterRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
