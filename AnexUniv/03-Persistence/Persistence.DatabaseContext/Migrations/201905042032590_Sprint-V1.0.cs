namespace Persistence.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintV10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseLessonLearnedsPerStudents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LessonId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LessonPerCourses", t => t.LessonId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.LessonId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LessonPerCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.String(),
                        Video = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Slug = c.String(),
                        Icon = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image1 = c.String(),
                        Image2 = c.String(),
                        Vote = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Incomes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityType = c.Int(nullable: false),
                        IncomeType = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReviewsPerCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vote = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UsersPerCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Credit", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersPerCourses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersPerCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ReviewsPerCourses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReviewsPerCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseLessonLearnedsPerStudents", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseLessonLearnedsPerStudents", "LessonId", "dbo.LessonPerCourses");
            DropForeignKey("dbo.LessonPerCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Courses", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.UsersPerCourses", new[] { "UserId" });
            DropIndex("dbo.UsersPerCourses", new[] { "CourseId" });
            DropIndex("dbo.ReviewsPerCourses", new[] { "UserId" });
            DropIndex("dbo.ReviewsPerCourses", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "AuthorId" });
            DropIndex("dbo.Courses", new[] { "CategoryId" });
            DropIndex("dbo.LessonPerCourses", new[] { "CourseId" });
            DropIndex("dbo.CourseLessonLearnedsPerStudents", new[] { "UserId" });
            DropIndex("dbo.CourseLessonLearnedsPerStudents", new[] { "LessonId" });
            DropColumn("dbo.AspNetUsers", "Credit");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.UsersPerCourses");
            DropTable("dbo.ReviewsPerCourses");
            DropTable("dbo.Incomes");
            DropTable("dbo.Courses");
            DropTable("dbo.LessonPerCourses");
            DropTable("dbo.CourseLessonLearnedsPerStudents");
            DropTable("dbo.Categories");
        }
    }
}
