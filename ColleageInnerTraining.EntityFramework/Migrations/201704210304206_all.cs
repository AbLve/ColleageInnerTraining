namespace ColleageInnerTraining.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class all : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.px_course_info", name: "cover_image_url", newName: "image_url");
            AddColumn("dbo.px_course_info", "category_type", c => c.Int(nullable: false));
            AddColumn("dbo.px_course_info", "stage_name", c => c.String(maxLength: 1000, storeType: "nvarchar"));
            AddColumn("dbo.px_course_info", "display_position", c => c.Int(nullable: false));
            AddColumn("dbo.px_common_department", "realId", c => c.Int(nullable: false));
            DropColumn("dbo.px_course_info", "cover_image_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.px_course_info", "cover_image_id", c => c.Int(nullable: false));
            DropColumn("dbo.px_common_department", "realId");
            DropColumn("dbo.px_course_info", "display_position");
            DropColumn("dbo.px_course_info", "stage_name");
            DropColumn("dbo.px_course_info", "category_type");
            RenameColumn(table: "dbo.px_course_info", name: "image_url", newName: "cover_image_url");
        }
    }
}
