/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80015
 Source Host           : 127.0.0.1:3306
 Source Schema         : nm_admin

 Target Server Type    : MySQL
 Target Server Version : 80015
 File Encoding         : 65001

 Date: 24/10/2019 10:56:06
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for account
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Type` smallint(3) NOT NULL DEFAULT 0,
  `UserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Email` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LoginTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `LoginIP` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Status` smallint(3) NOT NULL DEFAULT 0,
  `IsLock` bit(1) NOT NULL DEFAULT b'0',
  `ClosedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `ClosedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Deleted` bit(1) NOT NULL DEFAULT b'0',
  `DeletedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `DeletedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account
-- ----------------------------
INSERT INTO `account` VALUES ('2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', 0, 'admin', 'E739279CB28CDAFD7373618313803524', '管理员', '', '', '2019-10-24 10:52:44', '0.0.0.1', 1, b'0', '2019-10-09 00:00:00', '00000000-0000-0000-0000-000000000000', '2019-10-09 00:00:00', '00000000-0000-0000-0000-000000000000', '2019-10-09 00:00:00', '00000000-0000-0000-0000-000000000000', b'0', '2019-10-09 00:00:00', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for account_config
-- ----------------------------
DROP TABLE IF EXISTS `account_config`;
CREATE TABLE `account_config`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Skin` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Theme` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `FontSize` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account_config
-- ----------------------------
INSERT INTO `account_config` VALUES (1, '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 'pretty', 'default', 'medium');

-- ----------------------------
-- Table structure for account_role
-- ----------------------------
DROP TABLE IF EXISTS `account_role`;
CREATE TABLE `account_role`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `RoleId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account_role
-- ----------------------------
INSERT INTO `account_role` VALUES (2, '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '39f08d5b-173b-1cbb-7381-407c87b2e8e2');

-- ----------------------------
-- Table structure for auditinfo
-- ----------------------------
DROP TABLE IF EXISTS `auditinfo`;
CREATE TABLE `auditinfo`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `AccountId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '账户编号',
  `AccountName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '账户名称',
  `Area` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '区域',
  `Module` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '模块名称',
  `Controller` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '控制器',
  `ControllerDesc` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '控制器说明',
  `Action` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '操作',
  `ActionDesc` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '操作说明',
  `Parameters` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '参数',
  `Result` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '返回数据',
  `ExecutionTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '方法开始执行时间',
  `ExecutionDuration` bigint(20) NOT NULL COMMENT '方法执行总用时(ms)',
  `BrowserInfo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '浏览器信息',
  `Platform` smallint(6) NOT NULL COMMENT '平台',
  `IP` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 0 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for button
-- ----------------------------
DROP TABLE IF EXISTS `button`;
CREATE TABLE `button`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `MenuCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Icon` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of button
-- ----------------------------
INSERT INTO `button` VALUES ('39f08dae-46e6-f6f2-cbff-fe925a9d9946', 'personnelfiles_company', '添加', 'add', 'personnelfiles_company_add', '2019-09-29 13:32:23', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:23', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-46fc-9606-cae1-2c04803de11f', 'personnelfiles_company', '编辑', 'edit', 'personnelfiles_company_edit', '2019-09-29 13:32:23', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:23', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-4709-666f-39e8-11cfb7db4594', 'personnelfiles_company', '删除', 'delete', 'personnelfiles_company_del', '2019-09-29 13:32:23', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:23', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-5305-3dc4-ebe6-771d999a8e44', 'personnelfiles_department', '添加', 'add', 'personnelfiles_department_add', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-5315-b2cb-bd0a-65aaec059274', 'personnelfiles_department', '编辑', 'edit', 'personnelfiles_department_edit', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-5323-c008-deeb-865f8b94be29', 'personnelfiles_department', '删除', 'delete', 'personnelfiles_department_del', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-5327-e25c-ff40-0523a20e5d8c', 'personnelfiles_department', '岗位', 'edit', 'personnelfiles_department_position', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-533a-604e-b0f7-a987eeb2f0de', 'personnelfiles_department', '岗位添加', 'add', 'personnelfiles_department_position_add', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-534a-fcdd-27dc-2f5b0f8eb80c', 'personnelfiles_department', '岗位编辑', 'edit', 'personnelfiles_department_position_edit', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-5358-b231-a8c5-0116ba761066', 'personnelfiles_department', '岗位删除', 'delete', 'personnelfiles_department_position_del', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-5e49-6a91-4661-55c6f645cc45', 'personnelfiles_position', '删除', 'delete', 'personnelfiles_position_del', '2019-09-29 13:32:29', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:29', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-886e-6a3e-9a33-4f95f1bed0ae', 'personnelfiles_user', '添加', 'add', 'personnelfiles_user_add', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-887d-fda0-3bf1-e76c7c490364', 'personnelfiles_user', '编辑', 'edit', 'personnelfiles_user_edit', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-8886-697c-73d1-0ab76ecaf2b9', 'personnelfiles_user', '删除', 'delete', 'personnelfiles_user_del', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-8895-ce81-c6ec-723849dbc839', 'personnelfiles_user', '工作经历', 'work', 'personnelfiles_user_work_history', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-88a0-823c-9da9-9fbe6d546612', 'personnelfiles_user', '教育经历', 'education', 'personnelfiles_user_education_history', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-d230-1374-d024-1123543b6a9d', 'common_area', '添加', 'add', 'common_area_add', '2019-09-29 13:32:59', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:59', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-d23c-c202-2cc8-d4ac00483563', 'common_area', '编辑', 'edit', 'common_area_edit', '2019-09-29 13:32:59', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:59', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-d240-6179-e176-bd044f544712', 'common_area', '删除', 'delete', 'common_area_del', '2019-09-29 13:32:59', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:59', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-e147-307d-4b87-1742fa9e5885', 'common_attachment', '删除', 'delete', 'common_attachment_del', '2019-09-29 13:33:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:03', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08dae-e157-e486-3d68-06e5152d5e50', 'common_attachment', '导出', 'export', 'common_attachment_export', '2019-09-29 13:33:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:03', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-10f0-4af0-c82e-525f06a2f2c5', 'quartz_group', '添加', 'add', 'quartz_group_add', '2019-09-29 13:33:15', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:37', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39f08daf-10f7-7c83-9eec-048c3b1c9884', 'quartz_group', '删除', 'delete', 'quartz_group_del', '2019-09-29 13:33:15', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:37', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39f08daf-4498-7ee7-797c-a2ab7871b505', 'quartz_job', '添加', 'add', 'quartz_job_add', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-44a9-d1bc-755d-c3369034e10a', 'quartz_job', '编辑', 'edit', 'quartz_job_edit', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-44ad-ac06-083a-c5a44b8e99dc', 'quartz_job', '暂停', 'pause', 'quartz_job_pause', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-44ba-da99-4a81-8dcc78efcf82', 'quartz_job', '启动', 'run', 'quartz_job_resume', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-44c9-7fe4-defd-0939a64171cd', 'quartz_job', '日志', 'log', 'quartz_job_log', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-44cd-0154-5dcd-f428972e22f2', 'quartz_job', '删除', 'delete', 'quartz_job_del', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-7366-40d9-6116-510f50a9949b', 'admin_moduleinfo', '同步', 'refresh', 'admin_moduleinfo_sync', '2019-09-29 13:33:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-7379-0830-a724-272048d335c4', 'admin_moduleinfo', '删除', 'delete', 'admin_moduleinfo_del', '2019-09-29 13:33:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-8092-82f1-bf83-c74b15f5fae3', 'admin_permission', '同步', 'refresh', 'admin_permission_sync', '2019-09-29 13:33:43', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:43', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-8ff9-b2e7-f6ec-bbc690f7568a', 'admin_menu', '添加', 'add', 'admin_menu_add', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-8ffe-d189-3f53-f2174cb2e3d4', 'admin_menu', '编辑', 'edit', 'admin_menu_edit', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-901a-ea1c-6dbc-fd3d68fb9757', 'admin_menu', '删除', 'delete', 'admin_menu_del', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-9020-8ebb-2d86-bd0adcd9d9dd', 'admin_menu', '排序', 'sort', 'admin_menu_sort', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-9b13-0809-b6f5-64d2430a37c4', 'admin_role', '添加', 'add', 'admin_role_add', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-9b1f-b315-ccc8-4ed480ae2595', 'admin_role', '编辑', 'edit', 'admin_role_edit', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-9b28-8f39-eeb7-289ea4c44604', 'admin_role', '删除', 'delete', 'admin_role_del', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-9b38-d4f7-270d-5e8629f9601d', 'admin_role', '绑定菜单', 'bind', 'admin_role_bind_menu', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-a6a3-772f-7559-4a22f0614589', 'admin_account', '添加', 'add', 'admin_account_add', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-a6b0-0ea0-58c4-918941c668b2', 'admin_account', '编辑', 'edit', 'admin_account_edit', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-a6b9-a4ac-5953-a4e4074ef3b0', 'admin_account', '删除', 'delete', 'admin_account_del', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-a6cc-8c2f-e31a-9c91f6c6afce', 'admin_account', '重置密码', 'refresh', 'admin_account_reset_password', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-b45e-0264-f197-063f969d3aa0', 'admin_auditinfo', '详情', 'detail', 'admin_auditinfo_details', '2019-09-29 13:33:57', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:57', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08daf-f905-e02f-341a-bbc0b80cbf5e', 'admin_config', '添加', 'add', 'admin_config_add', '2019-09-29 13:34:14', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:07', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39f08daf-f909-628c-9a0a-2c23a57b9103', 'admin_config', '编辑', 'edit', 'admin_config_edit', '2019-09-29 13:34:14', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:07', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39f08daf-f915-e1f1-d7d8-ebd21adbebe6', 'admin_config', '删除', 'delete', 'admin_config_del', '2019-09-29 13:34:14', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:07', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39f08db3-ea6b-6af7-1ef5-a226af30fbe4', 'codegenerator_project', '添加', 'add', 'codegenerator_project_add', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08db3-ea80-e0e3-69ea-af5ab431ac6f', 'codegenerator_project', '编辑', 'edit', 'codegenerator_project_edit', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08db3-eaa7-e42f-34fb-25baaaf55e87', 'codegenerator_project', '删除', 'delete', 'codegenerator_project_del', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08db3-eada-892b-b250-c2d58279d388', 'codegenerator_project', '生成', 'download', 'codegenerator_project_build_code', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08db3-f788-9a1c-f227-f372acb73cea', 'codegenerator_enum', '添加', 'add', 'codegenerator_enum_add', '2019-09-29 13:38:36', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:38:36', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08db3-f7ba-4c00-d053-ca662ef2ad06', 'codegenerator_enum', '编辑', 'edit', 'codegenerator_enum_edit', '2019-09-29 13:38:36', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:38:36', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `button` VALUES ('39f08db3-f7d6-7533-682f-739e51d10ae1', 'codegenerator_enum', '删除', 'delete', 'codegenerator_enum_del', '2019-09-29 13:38:36', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:38:36', '39f08d5b-177c-a334-34e7-408ef121c6e0');

-- ----------------------------
-- Table structure for button_permission
-- ----------------------------
DROP TABLE IF EXISTS `button_permission`;
CREATE TABLE `button_permission`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ButtonCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `PermissionCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 91 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of button_permission
-- ----------------------------
INSERT INTO `button_permission` VALUES (1, 'personnelfiles_company_add', 'personnelfiles_company_add_post');
INSERT INTO `button_permission` VALUES (2, 'personnelfiles_company_edit', 'personnelfiles_company_edit_get');
INSERT INTO `button_permission` VALUES (3, 'personnelfiles_company_edit', 'personnelfiles_company_update_post');
INSERT INTO `button_permission` VALUES (4, 'personnelfiles_company_del', 'personnelfiles_company_delete_delete');
INSERT INTO `button_permission` VALUES (5, 'personnelfiles_department_add', 'personnelfiles_department_add_post');
INSERT INTO `button_permission` VALUES (6, 'personnelfiles_department_edit', 'personnelfiles_department_edit_get');
INSERT INTO `button_permission` VALUES (7, 'personnelfiles_department_edit', 'personnelfiles_department_update_post');
INSERT INTO `button_permission` VALUES (8, 'personnelfiles_department_del', 'personnelfiles_department_delete_delete');
INSERT INTO `button_permission` VALUES (9, 'personnelfiles_department_position', 'personnelfiles_position_query_get');
INSERT INTO `button_permission` VALUES (10, 'personnelfiles_department_position_add', 'personnelfiles_position_add_post');
INSERT INTO `button_permission` VALUES (11, 'personnelfiles_department_position_edit', 'personnelfiles_position_edit_get');
INSERT INTO `button_permission` VALUES (12, 'personnelfiles_department_position_edit', 'personnelfiles_position_update_post');
INSERT INTO `button_permission` VALUES (13, 'personnelfiles_department_position_del', 'personnelfiles_position_delete_delete');
INSERT INTO `button_permission` VALUES (14, 'personnelfiles_position_del', 'personnelfiles_position_delete_delete');
INSERT INTO `button_permission` VALUES (15, 'personnelfiles_user_add', 'personnelfiles_user_add_post');
INSERT INTO `button_permission` VALUES (16, 'personnelfiles_user_add', 'personnelfiles_user_uploadpicture_post');
INSERT INTO `button_permission` VALUES (17, 'personnelfiles_user_edit', 'personnelfiles_user_edit_get');
INSERT INTO `button_permission` VALUES (18, 'personnelfiles_user_edit', 'personnelfiles_user_update_post');
INSERT INTO `button_permission` VALUES (19, 'personnelfiles_user_edit', 'personnelfiles_user_uploadpicture_post');
INSERT INTO `button_permission` VALUES (20, 'personnelfiles_user_edit', 'personnelfiles_user_editcontact_get');
INSERT INTO `button_permission` VALUES (21, 'personnelfiles_user_edit', 'personnelfiles_user_updatecontact_post');
INSERT INTO `button_permission` VALUES (22, 'personnelfiles_user_edit', 'personnelfiles_user_contactdetails_get');
INSERT INTO `button_permission` VALUES (23, 'personnelfiles_user_del', 'personnelfiles_user_delete_delete');
INSERT INTO `button_permission` VALUES (24, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_query_get');
INSERT INTO `button_permission` VALUES (25, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_add_post');
INSERT INTO `button_permission` VALUES (26, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_edit_get');
INSERT INTO `button_permission` VALUES (27, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_update_get');
INSERT INTO `button_permission` VALUES (28, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_delete_delete');
INSERT INTO `button_permission` VALUES (29, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_query_get');
INSERT INTO `button_permission` VALUES (30, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_add_post');
INSERT INTO `button_permission` VALUES (31, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_edit_get');
INSERT INTO `button_permission` VALUES (32, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_update_get');
INSERT INTO `button_permission` VALUES (33, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_delete_delete');
INSERT INTO `button_permission` VALUES (34, 'common_area_add', 'common_area_add_post');
INSERT INTO `button_permission` VALUES (35, 'common_area_edit', 'common_area_edit_get');
INSERT INTO `button_permission` VALUES (36, 'common_area_edit', 'common_area_update_post');
INSERT INTO `button_permission` VALUES (37, 'common_area_del', 'common_area_delete_delete');
INSERT INTO `button_permission` VALUES (38, 'common_attachment_del', 'common_attachment_delete_delete');
INSERT INTO `button_permission` VALUES (39, 'common_attachment_export', 'common_attachment_export_get');
INSERT INTO `button_permission` VALUES (42, 'quartz_job_add', 'quartz_job_add_post');
INSERT INTO `button_permission` VALUES (43, 'quartz_job_edit', 'quartz_job_edit_get');
INSERT INTO `button_permission` VALUES (44, 'quartz_job_edit', 'quartz_job_update_post');
INSERT INTO `button_permission` VALUES (45, 'quartz_job_pause', 'quartz_job_pause_post');
INSERT INTO `button_permission` VALUES (46, 'quartz_job_resume', 'quartz_job_resume_post');
INSERT INTO `button_permission` VALUES (47, 'quartz_job_log', 'quartz_job_log_get');
INSERT INTO `button_permission` VALUES (48, 'quartz_job_del', 'quartz_job_delete_delete');
INSERT INTO `button_permission` VALUES (49, 'admin_moduleinfo_sync', 'admin_moduleinfo_sync_post');
INSERT INTO `button_permission` VALUES (50, 'admin_moduleinfo_del', 'admin_moduleinfo_delete_delete');
INSERT INTO `button_permission` VALUES (51, 'admin_permission_sync', 'admin_permission_sync_post');
INSERT INTO `button_permission` VALUES (52, 'admin_menu_add', 'admin_menu_add_post');
INSERT INTO `button_permission` VALUES (53, 'admin_menu_edit', 'admin_menu_edit_get');
INSERT INTO `button_permission` VALUES (54, 'admin_menu_edit', 'admin_menu_update_post');
INSERT INTO `button_permission` VALUES (55, 'admin_menu_del', 'admin_menu_delete_delete');
INSERT INTO `button_permission` VALUES (56, 'admin_menu_sort', 'admin_menu_sort_get');
INSERT INTO `button_permission` VALUES (57, 'admin_menu_sort', 'admin_menu_sort_post');
INSERT INTO `button_permission` VALUES (58, 'admin_role_add', 'admin_role_add_post');
INSERT INTO `button_permission` VALUES (59, 'admin_role_edit', 'admin_role_edit_get');
INSERT INTO `button_permission` VALUES (60, 'admin_role_edit', 'admin_role_update_post');
INSERT INTO `button_permission` VALUES (61, 'admin_role_del', 'admin_role_delete_delete');
INSERT INTO `button_permission` VALUES (62, 'admin_role_bind_menu', 'admin_role_menulist_get');
INSERT INTO `button_permission` VALUES (63, 'admin_role_bind_menu', 'admin_role_bindmenu_post');
INSERT INTO `button_permission` VALUES (64, 'admin_role_bind_menu', 'admin_role_menubuttonlist_get');
INSERT INTO `button_permission` VALUES (65, 'admin_role_bind_menu', 'admin_role_bindmenubutton_post');
INSERT INTO `button_permission` VALUES (66, 'admin_account_add', 'admin_account_add_post');
INSERT INTO `button_permission` VALUES (67, 'admin_account_edit', 'admin_account_edit_get');
INSERT INTO `button_permission` VALUES (68, 'admin_account_edit', 'admin_account_update_post');
INSERT INTO `button_permission` VALUES (69, 'admin_account_del', 'admin_account_delete_delete');
INSERT INTO `button_permission` VALUES (70, 'admin_account_reset_password', 'admin_account_updatepassword_post');
INSERT INTO `button_permission` VALUES (71, 'admin_auditinfo_details', 'admin_auditinfo_details_get');
INSERT INTO `button_permission` VALUES (76, 'codegenerator_project_add', 'codegenerator_project_add_post');
INSERT INTO `button_permission` VALUES (77, 'codegenerator_project_edit', 'codegenerator_project_edit_get');
INSERT INTO `button_permission` VALUES (78, 'codegenerator_project_edit', 'codegenerator_project_update_post');
INSERT INTO `button_permission` VALUES (79, 'codegenerator_project_del', 'codegenerator_project_delete_delete');
INSERT INTO `button_permission` VALUES (80, 'codegenerator_project_build_code', 'codegenerator_project_buildcode_post');
INSERT INTO `button_permission` VALUES (81, 'codegenerator_enum_add', 'codegenerator_enum_add_post');
INSERT INTO `button_permission` VALUES (82, 'codegenerator_enum_edit', 'codegenerator_enum_edit_get');
INSERT INTO `button_permission` VALUES (83, 'codegenerator_enum_edit', 'codegenerator_enum_update_post');
INSERT INTO `button_permission` VALUES (84, 'codegenerator_enum_del', 'codegenerator_enum_delete_delete');
INSERT INTO `button_permission` VALUES (85, 'admin_config_add', 'admin_config_add_post');
INSERT INTO `button_permission` VALUES (86, 'admin_config_edit', 'admin_config_edit_get');
INSERT INTO `button_permission` VALUES (87, 'admin_config_edit', 'admin_config_update_post');
INSERT INTO `button_permission` VALUES (88, 'admin_config_del', 'admin_config_delete_delete');
INSERT INTO `button_permission` VALUES (89, 'quartz_group_add', 'quartz_group_add_post');
INSERT INTO `button_permission` VALUES (90, 'quartz_group_del', 'quartz_group_delete_delete');

-- ----------------------------
-- Table structure for config
-- ----------------------------
DROP TABLE IF EXISTS `config`;
CREATE TABLE `config`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Key` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Value` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Remarks` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of config
-- ----------------------------
INSERT INTO `config` VALUES (1, 'sys_title', '通用权限管理系统', '系统标题', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (2, 'sys_logo', '', '系统Logo', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (3, 'sys_home', '/admin/home', '系统首页', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (4, 'sys_userinfo_page', '', '个人信息页', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (5, 'sys_button_permission', 'True', '启用按钮权限', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (6, 'sys_permission_validate', 'True', '启用权限验证功能', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (7, 'sys_auditing', 'True', '启用审计日志', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (8, 'sys_verify_code', 'False', '启用登录验证码功能', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (9, 'sys_toolbar_fullscreen', 'True', '显示工具栏全屏按钮', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (10, 'sys_toolbar_skin', 'True', '显示工具栏皮肤按钮', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (11, 'sys_toolbar_logout', 'True', '显示工具栏退出按钮', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (12, 'sys_toolbar_userinfo', 'True', '显示工具栏用户信息按钮', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `config` VALUES (13, 'sys_toolbar_customcss', '', '自定义CSS样式', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:32', '39f08d5b-177c-a334-34e7-408ef121c6e0');

-- ----------------------------
-- Table structure for menu
-- ----------------------------
DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModuleCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Type` smallint(3) NOT NULL DEFAULT 0,
  `ParentId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `RouteName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `RouteParams` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `RouteQuery` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Url` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Icon` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `IconColor` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Level` int(11) NOT NULL DEFAULT 0,
  `Show` bit(1) NOT NULL DEFAULT b'0',
  `Sort` int(11) NOT NULL DEFAULT 0,
  `Target` smallint(3) NOT NULL DEFAULT 0,
  `DialogWidth` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `DialogHeight` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `DialogFullscreen` bit(1) NOT NULL DEFAULT b'0',
  `Remarks` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menu
-- ----------------------------
INSERT INTO `menu` VALUES ('39f08dac-84e1-54e2-22ee-e5d7e3606f90', '', 0, '00000000-0000-0000-0000-000000000000', '系统设置', '', '', '', '', 'system', '', 0, b'1', 1, -1, '', '', b'1', '', '2019-09-29 13:30:28', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-24 10:54:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dac-9b84-9a56-20b9-26789223a74f', '', 0, '00000000-0000-0000-0000-000000000000', '权限管理', '', '', '', '', 'permission', '', 0, b'1', 0, -1, '', '', b'1', '', '2019-09-29 13:30:34', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-24 10:54:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dad-aca0-7ddf-61d1-0b6ac0841b64', '', 2, '00000000-0000-0000-0000-000000000000', 'GitHub', '', '', '', 'https://github.com/iamoldli/NetModular', 'github', '', 0, b'1', 3, 0, '', '', b'1', '', '2019-09-29 13:31:44', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-24 10:54:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08daf-7355-6fb9-cfe6-b9804de226f9', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '模块管理', 'admin_moduleinfo', '', '', '', 'product', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-8081-2b67-5f77-3c4119b2ab66', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '权限列表', 'admin_permission', '', '', '', 'verifycode', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:43', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:43', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-8feb-d2ce-66dc-12fff451a969', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '菜单管理', 'admin_menu', '', '', '', 'menu', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-9b05-80d5-a7f5-21e450af9b70', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '角色管理', 'admin_role', '', '', '', 'role', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-a689-5e91-dce9-e6d0088d29dc', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '账户管理', 'admin_account', '', '', '', 'user', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-b44b-8fbf-3a88-629dc0922e84', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '审计日志', 'admin_auditinfo', '', '', '', 'log', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:57', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:57', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-e02b-bd7b-be06-12eeee0f4da4', 'Admin', 1, '39f08dac-84e1-54e2-22ee-e5d7e3606f90', '系统配置', 'admin_system', '', '', '', 'system', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:34:08', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:00', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-f8f6-2dad-42c0-c73c787c6def', 'Admin', 1, '39f08dac-84e1-54e2-22ee-e5d7e3606f90', '配置项', 'admin_config', '', '', '', 'tag', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:34:14', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:07', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08db0-067d-fc37-84cb-b5283891fcce', 'Admin', 1, '39f08dac-84e1-54e2-22ee-e5d7e3606f90', '图标管理', 'admin_icon', '', '', '', 'photo', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:34:18', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f10ddc-b762-96c9-58a3-9ce21ae7ebcd', '', 2, '00000000-0000-0000-0000-000000000000', '官方文档', '', '', '', 'https://nm.iamoldli.com/docs', 'archives', '', 0, b'1', 2, 0, '', '', b'1', '', '2019-10-24 10:54:30', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-10-24 10:54:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for menu_permission
-- ----------------------------
DROP TABLE IF EXISTS `menu_permission`;
CREATE TABLE `menu_permission`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MenuCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `PermissionCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menu_permission
-- ----------------------------
INSERT INTO `menu_permission` VALUES (1, 'personnelfiles_company', 'personnelfiles_company_query_get');
INSERT INTO `menu_permission` VALUES (2, 'personnelfiles_department', 'personnelfiles_department_query_get');
INSERT INTO `menu_permission` VALUES (3, 'personnelfiles_department', 'personnelfiles_department_tree_get');
INSERT INTO `menu_permission` VALUES (4, 'personnelfiles_user', 'personnelfiles_user_query_get');
INSERT INTO `menu_permission` VALUES (5, 'common_area', 'common_area_query_get');
INSERT INTO `menu_permission` VALUES (6, 'common_area', 'common_area_querychildren_get');
INSERT INTO `menu_permission` VALUES (7, 'common_attachment', 'common_attachment_query_get');
INSERT INTO `menu_permission` VALUES (9, 'quartz_job', 'quartz_job_query_get');
INSERT INTO `menu_permission` VALUES (10, 'admin_moduleinfo', 'admin_moduleinfo_query_get');
INSERT INTO `menu_permission` VALUES (11, 'admin_permission', 'admin_permission_query_get');
INSERT INTO `menu_permission` VALUES (12, 'admin_menu', 'admin_menu_query_get');
INSERT INTO `menu_permission` VALUES (13, 'admin_menu', 'admin_menu_tree_get');
INSERT INTO `menu_permission` VALUES (14, 'admin_role', 'admin_role_query_get');
INSERT INTO `menu_permission` VALUES (15, 'admin_account', 'admin_account_query_get');
INSERT INTO `menu_permission` VALUES (16, 'admin_auditinfo', 'admin_auditinfo_query_get');
INSERT INTO `menu_permission` VALUES (20, 'codegenerator_project', 'codegenerator_project_query_get');
INSERT INTO `menu_permission` VALUES (21, 'codegenerator_enum', 'codegenerator_enum_query_get');
INSERT INTO `menu_permission` VALUES (22, 'admin_system', 'admin_system_config_post');
INSERT INTO `menu_permission` VALUES (23, 'admin_system', 'admin_system_uploadlogo_post');
INSERT INTO `menu_permission` VALUES (24, 'admin_config', 'admin_config_query_get');
INSERT INTO `menu_permission` VALUES (25, 'quartz_group', 'quartz_group_query_get');

-- ----------------------------
-- Table structure for moduleinfo
-- ----------------------------
DROP TABLE IF EXISTS `moduleinfo`;
CREATE TABLE `moduleinfo`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Version` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Remarks` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of moduleinfo
-- ----------------------------
INSERT INTO `moduleinfo` VALUES ('39f08d5b-ba44-6817-6d57-fd07e0c9a482', '权限管理', 'Admin', '1.2.6', '', '2019-09-29 12:02:13', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:16:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for permission
-- ----------------------------
DROP TABLE IF EXISTS `permission`;
CREATE TABLE `permission`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModuleCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Controller` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Action` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `HttpMethod` smallint(3) NOT NULL DEFAULT 0,
  `Code` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of permission
-- ----------------------------
INSERT INTO `permission` VALUES ('39f08d5b-d104-dad2-d611-3586643f34b7', '账户管理_绑定角色', 'Admin', 'Account', 'BindRole', 2, 'admin_account_bindrole_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d110-8a6f-5d0c-1acc4cd6e713', '账户管理_查询', 'Admin', 'Account', 'Query', 0, 'admin_account_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d11d-e539-b8e1-58967d8fd541', '账户管理_添加', 'Admin', 'Account', 'Add', 2, 'admin_account_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d131-df4d-fb47-2698f84c7cf6', '账户管理_编辑', 'Admin', 'Account', 'Edit', 0, 'admin_account_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d136-91f1-39c4-eec8eb28f490', '账户管理_更新', 'Admin', 'Account', 'Update', 2, 'admin_account_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d149-b5a9-3e89-d13f3cedb1c6', '账户管理_删除', 'Admin', 'Account', 'Delete', 3, 'admin_account_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d158-c047-51d7-509b0606fd96', '账户管理_重置密码', 'Admin', 'Account', 'ResetPassword', 2, 'admin_account_resetpassword_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d175-9882-0f7c-fb2bae67f6c8', '审计信息_查询', 'Admin', 'AuditInfo', 'Query', 0, 'admin_auditinfo_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d1ae-0b50-d74d-23ea07538398', '审计信息_详情', 'Admin', 'AuditInfo', 'Details', 0, 'admin_auditinfo_details_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d1cc-e9c7-bd4d-da331764b4a3', '按钮管理_查询', 'Admin', 'Button', 'Query', 0, 'admin_button_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d1d6-55c8-3580-cda4932af779', '配置项管理_查询', 'Admin', 'Config', 'Query', 0, 'admin_config_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d1e0-65f2-73d4-8424438c8567', '配置项管理_添加', 'Admin', 'Config', 'Add', 2, 'admin_config_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d1e5-9e0e-0c29-ddf2a73482b8', '配置项管理_删除', 'Admin', 'Config', 'Delete', 3, 'admin_config_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d1ec-13f3-a1f5-23a2bc892d15', '配置项管理_编辑', 'Admin', 'Config', 'Edit', 0, 'admin_config_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d1f1-f48b-9ebe-766fcb346c63', '配置项管理_修改', 'Admin', 'Config', 'Update', 2, 'admin_config_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d200-dee4-a526-d3422506a692', '菜单管理_菜单树', 'Admin', 'Menu', 'Tree', 0, 'admin_menu_tree_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d210-2150-33bc-f8b9b97d2b10', '菜单管理_查询菜单列表', 'Admin', 'Menu', 'Query', 0, 'admin_menu_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d21c-40fb-6de2-3bddb3a1682a', '菜单管理_添加', 'Admin', 'Menu', 'Add', 2, 'admin_menu_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d225-a13c-7b44-e8c3e7d3d128', '菜单管理_删除', 'Admin', 'Menu', 'Delete', 3, 'admin_menu_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d22e-ee56-933a-ed09cfb4a706', '菜单管理_编辑', 'Admin', 'Menu', 'Edit', 0, 'admin_menu_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d236-7004-f0ca-925788da03aa', '菜单管理_更新', 'Admin', 'Menu', 'Update', 2, 'admin_menu_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d243-8f1a-7c50-6feeb098432e', '菜单管理_获取排序信息', 'Admin', 'Menu', 'Sort', 0, 'admin_menu_sort_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d24b-cfa0-96e6-9242c9cdff31', '菜单管理_更新排序信息', 'Admin', 'Menu', 'Sort', 2, 'admin_menu_sort_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d259-2ac7-dfa6-1bf0ae78e4f0', '模块信息_查询', 'Admin', 'ModuleInfo', 'Query', 0, 'admin_moduleinfo_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d264-13cc-1cc2-a4c2639e7fda', '模块信息_同步模块数据', 'Admin', 'ModuleInfo', 'Sync', 2, 'admin_moduleinfo_sync_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d269-43b8-2d42-48a8cf8a3c38', '模块信息_删除', 'Admin', 'ModuleInfo', 'Delete', 3, 'admin_moduleinfo_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d27d-241c-b966-5a4ce019fe3e', '权限接口_查询', 'Admin', 'Permission', 'Query', 0, 'admin_permission_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d286-6b4b-82cf-b8f4c8052fef', '权限接口_同步', 'Admin', 'Permission', 'Sync', 2, 'admin_permission_sync_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d28c-73c8-d195-f2de0713cbf3', '角色管理_查询', 'Admin', 'Role', 'Query', 0, 'admin_role_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d298-0e81-694f-723d60c55bda', '角色管理_添加', 'Admin', 'Role', 'Add', 2, 'admin_role_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d29d-7195-e52b-5b20e346f898', '角色管理_删除', 'Admin', 'Role', 'Delete', 3, 'admin_role_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d2a5-3475-50d1-d3984f82f5d3', '角色管理_编辑', 'Admin', 'Role', 'Edit', 0, 'admin_role_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d2aa-77a2-7f52-72d9974f7020', '角色管理_修改', 'Admin', 'Role', 'Update', 2, 'admin_role_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d2b5-55c5-995d-17557580db07', '角色管理_获取角色的关联菜单列表', 'Admin', 'Role', 'MenuList', 0, 'admin_role_menulist_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d2b8-0db7-9575-bd6cd85f12b0', '角色管理_绑定菜单', 'Admin', 'Role', 'BindMenu', 2, 'admin_role_bindmenu_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d2bc-3e4d-3993-a7a34ef134af', '角色管理_获取角色关联的菜单按钮列表', 'Admin', 'Role', 'MenuButtonList', 0, 'admin_role_menubuttonlist_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d2c5-438b-194f-6b5f35cb587b', '角色管理_绑定菜单按钮', 'Admin', 'Role', 'BindMenuButton', 2, 'admin_role_bindmenubutton_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d2cd-b7a0-c335-9b298cb1a765', '系统_修改系统配置', 'Admin', 'System', 'Config', 2, 'admin_system_config_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `permission` VALUES ('39f08d5b-d2d4-846e-7503-ee323474339d', '系统_上传Logo', 'Admin', 'System', 'UploadLogo', 2, 'admin_system_uploadlogo_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0');

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Remarks` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `IsSpecified` bit(1) NOT NULL DEFAULT b'0',
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Deleted` bit(1) NOT NULL DEFAULT b'0',
  `DeletedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `DeletedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES ('39f08d5b-173b-1cbb-7381-407c87b2e8e2', '超级管理员', '超级管理员', b'0', '2019-09-29 12:01:31', '00000000-0000-0000-0000-000000000000', '2019-09-29 12:01:31', '00000000-0000-0000-0000-000000000000', b'0', '2019-09-29 12:01:31', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for role_menu
-- ----------------------------
DROP TABLE IF EXISTS `role_menu`;
CREATE TABLE `role_menu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `MenuId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 27 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role_menu
-- ----------------------------
INSERT INTO `role_menu` VALUES (93, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dac-9b84-9a56-20b9-26789223a74f');
INSERT INTO `role_menu` VALUES (94, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-7355-6fb9-cfe6-b9804de226f9');
INSERT INTO `role_menu` VALUES (95, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8081-2b67-5f77-3c4119b2ab66');
INSERT INTO `role_menu` VALUES (96, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969');
INSERT INTO `role_menu` VALUES (97, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70');
INSERT INTO `role_menu` VALUES (98, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc');
INSERT INTO `role_menu` VALUES (99, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-b44b-8fbf-3a88-629dc0922e84');
INSERT INTO `role_menu` VALUES (100, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dac-84e1-54e2-22ee-e5d7e3606f90');
INSERT INTO `role_menu` VALUES (101, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-e02b-bd7b-be06-12eeee0f4da4');
INSERT INTO `role_menu` VALUES (102, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-f8f6-2dad-42c0-c73c787c6def');
INSERT INTO `role_menu` VALUES (103, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db0-067d-fc37-84cb-b5283891fcce');
INSERT INTO `role_menu` VALUES (104, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f10ddc-b762-96c9-58a3-9ce21ae7ebcd');
INSERT INTO `role_menu` VALUES (105, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dad-aca0-7ddf-61d1-0b6ac0841b64');

-- ----------------------------
-- Table structure for role_menu_button
-- ----------------------------
DROP TABLE IF EXISTS `role_menu_button`;
CREATE TABLE `role_menu_button`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `MenuId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ButtonId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 56 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role_menu_button
-- ----------------------------
INSERT INTO `role_menu_button` VALUES (30, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-7355-6fb9-cfe6-b9804de226f9', '39f08daf-7366-40d9-6116-510f50a9949b');
INSERT INTO `role_menu_button` VALUES (31, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-7355-6fb9-cfe6-b9804de226f9', '39f08daf-7379-0830-a724-272048d335c4');
INSERT INTO `role_menu_button` VALUES (32, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8081-2b67-5f77-3c4119b2ab66', '39f08daf-8092-82f1-bf83-c74b15f5fae3');
INSERT INTO `role_menu_button` VALUES (33, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969', '39f08daf-8ff9-b2e7-f6ec-bbc690f7568a');
INSERT INTO `role_menu_button` VALUES (34, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969', '39f08daf-8ffe-d189-3f53-f2174cb2e3d4');
INSERT INTO `role_menu_button` VALUES (35, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969', '39f08daf-901a-ea1c-6dbc-fd3d68fb9757');
INSERT INTO `role_menu_button` VALUES (36, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969', '39f08daf-9020-8ebb-2d86-bd0adcd9d9dd');
INSERT INTO `role_menu_button` VALUES (37, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70', '39f08daf-9b13-0809-b6f5-64d2430a37c4');
INSERT INTO `role_menu_button` VALUES (38, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70', '39f08daf-9b1f-b315-ccc8-4ed480ae2595');
INSERT INTO `role_menu_button` VALUES (39, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70', '39f08daf-9b28-8f39-eeb7-289ea4c44604');
INSERT INTO `role_menu_button` VALUES (40, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70', '39f08daf-9b38-d4f7-270d-5e8629f9601d');
INSERT INTO `role_menu_button` VALUES (41, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc', '39f08daf-a6a3-772f-7559-4a22f0614589');
INSERT INTO `role_menu_button` VALUES (42, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc', '39f08daf-a6b0-0ea0-58c4-918941c668b2');
INSERT INTO `role_menu_button` VALUES (43, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc', '39f08daf-a6b9-a4ac-5953-a4e4074ef3b0');
INSERT INTO `role_menu_button` VALUES (44, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc', '39f08daf-a6cc-8c2f-e31a-9c91f6c6afce');
INSERT INTO `role_menu_button` VALUES (45, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-b44b-8fbf-3a88-629dc0922e84', '39f08daf-b45e-0264-f197-063f969d3aa0');
INSERT INTO `role_menu_button` VALUES (46, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-f8f6-2dad-42c0-c73c787c6def', '39f08daf-f905-e02f-341a-bbc0b80cbf5e');
INSERT INTO `role_menu_button` VALUES (47, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-f8f6-2dad-42c0-c73c787c6def', '39f08daf-f909-628c-9a0a-2c23a57b9103');
INSERT INTO `role_menu_button` VALUES (48, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-f8f6-2dad-42c0-c73c787c6def', '39f08daf-f915-e1f1-d7d8-ebd21adbebe6');

SET FOREIGN_KEY_CHECKS = 1;
