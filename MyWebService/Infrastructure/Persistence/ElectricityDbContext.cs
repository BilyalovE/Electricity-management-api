using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Persistence
{
    public class ElectricityDbContext : DbContext
    {
        public ElectricityDbContext(DbContextOptions<ElectricityDbContext> options) : base(options)
        {
        }
        public DbSet<Organization> Organizations { get; set; } = null!; 
        public DbSet<ChildOrganization> ChildOrganizations { get; set; } = null!; 
        public DbSet<ConsumptionObject> ConsumptionObjects { get; set; } = null!; 
        public DbSet<MeasurementPoint> MeasurementPoints { get; set; } = null!; 
        public DbSet<ElectricityMeter> ElectricityMeters { get; set; } = null!; 
        public DbSet<CurrentTransformer> CurrentTransformers { get; set; } = null!; 
        public DbSet<VoltageTransformer> VoltageTransformers { get; set; } = null!; 
        public DbSet<SupplyPoint> SupplyPoints { get; set; } 
        public DbSet<CalculationMeter> CalculationMeters { get; set; }  = null!; 
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<ChildOrganization>() 
               .HasOne(co => co.OrganizationEntity ) 
               .WithMany(o => o.ChildOrganizations) 
               .HasForeignKey(co => co.OrganizationId);
            
            modelBuilder.Entity<ConsumptionObject>() 
               .HasOne(co => co.ChildOrganizationEntity)  
               .WithMany(o => o.ConsumptionObjects)  
               .HasForeignKey(co => co.ChildOrganizationId);

            
            modelBuilder.Entity<MeasurementPoint>() 
               .HasOne(mp => mp.ConsumptionObjectEntity) 
               .WithMany(co => co.MeasurementPoints) 
               .HasForeignKey (mp => mp.ConsumptionObjectId);

            
            modelBuilder.Entity<SupplyPoint>() 
               .HasOne(sp => sp.ConsumptionObjectEntity)  
               .WithMany(co => co.SupplyPoints) 
               .HasForeignKey(sp => sp.ConsumptionObjectId);

            
            modelBuilder.Entity<ElectricityMeter>() 
               .HasOne(em => em.MeasurementPointEntity) 
               .WithOne(mp => mp.ElectricityMeterEntity) 
               .HasForeignKey<ElectricityMeter>(em => em.MeasurementPointId);

            
            modelBuilder.Entity<CurrentTransformer>() 
               .HasOne(ct => ct.MeasurementPointEntity) 
               .WithOne(mp => mp.CurrentTransformerEntity) 
               .HasForeignKey<CurrentTransformer>(ct => ct.MeasurementPointId); 

             
             modelBuilder.Entity<VoltageTransformer>()
                .HasOne(vt => vt.MeasurementPointEntity) 
                .WithOne(mp => mp.VoltageTransformerEntity) 
                .HasForeignKey<VoltageTransformer>(vt => vt.MeasurementPointId);
             
             
             modelBuilder.Entity<CalculationMeter>()
                .HasKey(cm => cm.Id); 
          
             
             modelBuilder.Entity<CalculationMeter>()
                .HasIndex(cm => new { cm.MeasurementPointId, cm.SupplyPointId, cm.StartTime, cm.EndTime }) 
                .IsUnique();

             
             modelBuilder.Entity<CalculationMeter>()
               .HasOne(cm => cm.MeasurementPointEntity)
               .WithMany(mp => mp.CalculationMeters)
               .HasForeignKey(cm => cm.MeasurementPointId);
             
             
             modelBuilder.Entity<CalculationMeter>()
               .HasOne(cm => cm.SupplyPointEntity)
               .WithMany(sp => sp.CalculationMeters)
               .HasForeignKey(cm => cm.SupplyPointId);
        }
    }
}
