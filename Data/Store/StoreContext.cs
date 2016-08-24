using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Data.Store.Models;

namespace Data.Store
{
    public class StoreContext : DbContext
    {
        public DbSet<Temperature> Temperatures { get; set; }

        public StoreContext() : base("StoreContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var temperature = modelBuilder.Entity<Temperature>();
            temperature.HasKey(m => m.Id);
            temperature.Property(m => m.Date)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
        }
    }
}