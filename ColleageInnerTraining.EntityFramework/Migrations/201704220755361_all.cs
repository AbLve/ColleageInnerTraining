namespace ColleageInnerTraining.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class all : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.px_course_info", name: "cover_image_url", newName: "image_url");
            CreateTable(
                "dbo.px_course_bound_examinationl",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        course_id = c.Int(nullable: false),
                        course_name = c.String(maxLength: 255, storeType: "nvarchar"),
                        examination_id = c.Int(nullable: false),
                        examination_name = c.String(maxLength: 255, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CourseBoundExamination_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.px_course_bound_personnel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        course_id = c.Int(nullable: false),
                        course_name = c.String(maxLength: 255, storeType: "nvarchar"),
                        account_sys_no = c.Int(nullable: false),
                        account_user_name = c.String(maxLength: 255, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CourseBoundPersonnel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.px_class_training_info", "training_type", c => c.Int(nullable: false));
            AddColumn("dbo.px_class_training_info", "type_id", c => c.Int(nullable: false));
            AddColumn("dbo.px_course_info", "category_type", c => c.Int(nullable: false));
            AddColumn("dbo.px_course_info", "stage_name", c => c.String(maxLength: 1000, storeType: "nvarchar"));
            AddColumn("dbo.px_course_info", "display_position", c => c.Int(nullable: false));
            AddColumn("dbo.px_common_department", "department_id", c => c.Int(nullable: false));
            AlterColumn("dbo.px_class_training_info", "start_time", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.px_class_training_info", "end_time", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.px_class_user", "user_id", c => c.Int(nullable: false));
            DropColumn("dbo.px_class_training_info", "description");
            DropColumn("dbo.px_class_training_info", "pre_poll_id");
            DropColumn("dbo.px_class_training_info", "post_poll_id");
            DropColumn("dbo.px_class_training_info", "exam_id");
            DropColumn("dbo.px_course_info", "cover_image_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.px_course_info", "cover_image_id", c => c.Int(nullable: false));
            AddColumn("dbo.px_class_training_info", "exam_id", c => c.Int(nullable: false));
            AddColumn("dbo.px_class_training_info", "post_poll_id", c => c.Int(nullable: false));
            AddColumn("dbo.px_class_training_info", "pre_poll_id", c => c.Int(nullable: false));
            AddColumn("dbo.px_class_training_info", "description", c => c.String(maxLength: 255, storeType: "nvarchar"));
            AlterColumn("dbo.px_class_user", "user_id", c => c.Boolean(nullable: false));
            AlterColumn("dbo.px_class_training_info", "end_time", c => c.Int(nullable: false));
            AlterColumn("dbo.px_class_training_info", "start_time", c => c.Int(nullable: false));
            DropColumn("dbo.px_common_department", "department_id");
            DropColumn("dbo.px_course_info", "display_position");
            DropColumn("dbo.px_course_info", "stage_name");
            DropColumn("dbo.px_course_info", "category_type");
            DropColumn("dbo.px_class_training_info", "type_id");
            DropColumn("dbo.px_class_training_info", "training_type");
            DropTable("dbo.px_course_bound_personnel",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CourseBoundPersonnel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.px_course_bound_examinationl",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CourseBoundExamination_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            RenameColumn(table: "dbo.px_course_info", name: "image_url", newName: "cover_image_url");
        }
    }
}
