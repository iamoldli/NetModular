
-- ----------------------------
-- 标签
-- ----------------------------
CREATE TABLE `Tag`  (
 "Id" UNIQUEIDENTIFIER NOT NULL PRIMARY KEY ,
 "Name" text NOT NULL  ,
 "CreatedBy" UNIQUEIDENTIFIER NOT NULL  ,
 "CreatedTime" text NOT NULL  ,
 "ModifiedBy" UNIQUEIDENTIFIER NOT NULL  ,
 "ModifiedTime" text NOT NULL  
);

-- ----------------------------
-- 分类
-- ----------------------------
CREATE TABLE `Category`  (
 "Id" UNIQUEIDENTIFIER NOT NULL PRIMARY KEY ,
 "Name" text NOT NULL  ,
 "CreatedBy" UNIQUEIDENTIFIER NOT NULL  ,
 "CreatedTime" text NOT NULL  ,
 "ModifiedBy" UNIQUEIDENTIFIER NOT NULL  ,
 "ModifiedTime" text NOT NULL  
);

-- ----------------------------
-- 文章
-- ----------------------------
CREATE TABLE `Article`  (
 "Id" UNIQUEIDENTIFIER NOT NULL PRIMARY KEY ,
 "CategoryId" UNIQUEIDENTIFIER NOT NULL  ,
 "Title" text NOT NULL  ,
 "Summary" text NOT NULL  ,
 "Body" text NOT NULL  ,
 "Published" integer NOT NULL  ,
 "PublishTime" text NOT NULL  ,
 "CreatedBy" UNIQUEIDENTIFIER NOT NULL  ,
 "CreatedTime" text NOT NULL  ,
 "ModifiedBy" UNIQUEIDENTIFIER NOT NULL  ,
 "ModifiedTime" text NOT NULL  
);

