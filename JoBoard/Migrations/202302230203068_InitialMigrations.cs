namespace JoBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
      "dbo.AppliedJobs",
      c => new
      {
          Id = c.Int(nullable: false),
          Name = c.String(nullable: false, maxLength: 200),
          Email = c.String(nullable: false, maxLength: 200),
          Phone_Number = c.Int(nullable: false),
          Resume = c.Binary(nullable: false),
          UserId = c.String(nullable: false, maxLength: 128),
          CondidaId = c.String(nullable: false, maxLength: 128),
          JobId = c.Int(nullable: false),
          ContentType = c.String(nullable: false, maxLength: 200),
          FileName = c.String(nullable: false, maxLength: 200),
      })
      .PrimaryKey(t => new { t.Id })
      .Index(t => t.UserId)
      .Index(t => t.JobId)
      .Index(t => t.CondidaId);
            AddForeignKey("dbo.AppliedJobs", "JobId", "dbo.Job", "Id");
            AddForeignKey("dbo.AppliedJobs", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AppliedJobs", "CondidaId", "dbo.AspNetUsers", "Id");
        }

        public override void Down()
        {
            DropTable("AppliedJobs");
        }
    }
}
