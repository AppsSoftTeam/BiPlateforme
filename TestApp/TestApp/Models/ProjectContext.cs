using System.Data.Entity;


namespace TestApp.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("name=GlobalnetReporting") { }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<ObjectifSection> ObjectifSections { get; set; }
        public DbSet<ObjectifContrat> ObjectifContrats { get; set; }
        public DbSet<ObjectifType> ObjectifTypes { get; set; }
        public DbSet<ProfileTypeObj> ProfileTypeObjs { get; set; }

        public DbSet<ProfileCategories> Profil_Roles { get; set; }
        public DbSet<UserProfiles> User_Profils { get; set; }

        public DbSet<Gouvernorat> Gouvernorats { get; set; }


    }
}