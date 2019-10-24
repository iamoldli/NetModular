/*
 Navicat Premium Data Transfer

 Source Server         : 129.211.40.240
 Source Server Type    : MySQL
 Source Server Version : 80016
 Source Host           : 129.211.40.240:13306
 Source Schema         : nm_admin

 Target Server Type    : MySQL
 Target Server Version : 80016
 File Encoding         : 65001

 Date: 12/10/2019 15:32:57
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
INSERT INTO `account` VALUES ('2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', 0, 'admin', 'E739279CB28CDAFD7373618313803524', '管理员', '', '', '2019-10-12 15:32:38', '115.236.71.156', 1, b'0', '2019-10-09 00:00:00', '00000000-0000-0000-0000-000000000000', '2019-10-09 00:00:00', '00000000-0000-0000-0000-000000000000', '2019-10-09 00:00:00', '00000000-0000-0000-0000-000000000000', b'0', '2019-10-09 00:00:00', '00000000-0000-0000-0000-000000000000');

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
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account_config
-- ----------------------------
INSERT INTO `account_config` VALUES (1, '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 'classics', 'default', 'default');
INSERT INTO `account_config` VALUES (2, '39f0d074-9e61-c099-ab57-d3539492bfc9', 'pretty', 'default', 'mini');

-- ----------------------------
-- Table structure for account_role
-- ----------------------------
DROP TABLE IF EXISTS `account_role`;
CREATE TABLE `account_role`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `RoleId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account_role
-- ----------------------------
INSERT INTO `account_role` VALUES (2, '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '39f08d5b-173b-1cbb-7381-407c87b2e8e2');

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
INSERT INTO `button` VALUES ('39f08dae-e147-307d-4b87-1742fa9e5885', 'common_attachment', '删除', 'delete', 'common_attachment_del', '2019-09-29 13:33:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 09:27:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39f08dae-e157-e486-3d68-06e5152d5e50', 'common_attachment', '导出', 'export', 'common_attachment_export', '2019-09-29 13:33:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 09:27:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
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
) ENGINE = InnoDB AUTO_INCREMENT = 95 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

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
INSERT INTO `button_permission` VALUES (93, 'common_attachment_del', 'common_attachment_delete_delete');
INSERT INTO `button_permission` VALUES (94, 'common_attachment_export', 'common_attachment_export_get');

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
INSERT INTO `config` VALUES (1, 'sys_title', '通用权限管理系统', '系统标题', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (2, 'sys_logo', '', '系统Logo', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (3, 'sys_home', '/admin/home', '系统首页', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (4, 'sys_userinfo_page', '', '个人信息页', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (5, 'sys_button_permission', 'True', '启用按钮权限', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (6, 'sys_permission_validate', 'True', '启用权限验证功能', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (7, 'sys_auditing', 'True', '启用审计日志', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (8, 'sys_verify_code', 'False', '启用登录验证码功能', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (9, 'sys_toolbar_fullscreen', 'True', '显示工具栏全屏按钮', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (10, 'sys_toolbar_skin', 'True', '显示工具栏皮肤按钮', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (11, 'sys_toolbar_logout', 'True', '显示工具栏退出按钮', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (12, 'sys_toolbar_userinfo', 'True', '显示工具栏用户信息按钮', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (13, 'sys_toolbar_customcss', '', '自定义CSS样式', '2019-09-29 12:02:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

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
INSERT INTO `menu` VALUES ('39f08dac-84e1-54e2-22ee-e5d7e3606f90', '', 0, '00000000-0000-0000-0000-000000000000', '系统设置', '', '', '', '', 'system', '', 0, b'1', 4, -1, '', '', b'1', '', '2019-09-29 13:30:28', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:32:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dac-9b84-9a56-20b9-26789223a74f', '', 0, '00000000-0000-0000-0000-000000000000', '权限管理', '', '', '', '', 'permission', '', 0, b'1', 3, -1, '', '', b'1', '', '2019-09-29 13:30:34', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:32:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dac-e585-f2d5-3037-58c6397135a5', '', 0, '00000000-0000-0000-0000-000000000000', '人事档案', '', '', '', '', 'user', '', 0, b'1', 0, -1, '', '', b'1', '', '2019-09-29 13:30:53', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:32:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dad-0641-fb8c-d3ab-b3bdcb803500', '', 0, '00000000-0000-0000-0000-000000000000', '基础数据', '', '', '', '', 'user', NULL, 0, b'1', 1, -1, '', '', b'1', '', '2019-09-29 13:31:01', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:32:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dad-3e6f-5955-ccdf-96b081a39bdd', '', 0, '00000000-0000-0000-0000-000000000000', '任务调度', '', '', '', '', 'timer', NULL, 0, b'1', 2, -1, '', '', b'1', '', '2019-09-29 13:31:15', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:32:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dad-6b9b-0fa4-767f-3b85eebbd534', '', 0, '00000000-0000-0000-0000-000000000000', '开发工具', '', '', '', '', 'develop', '', 0, b'1', 5, -1, '', '', b'1', '', '2019-09-29 13:31:27', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:32:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dad-aca0-7ddf-61d1-0b6ac0841b64', '', 2, '00000000-0000-0000-0000-000000000000', 'GitHub', '', '', '', 'https://github.com/iamoldli/NetModular', 'github', '', 0, b'1', 7, 4, '', '', b'1', '', '2019-09-29 13:31:44', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:32:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dae-46ba-04e4-a24b-346b81f8d936', 'PersonnelFiles', 1, '39f08dac-e585-f2d5-3037-58c6397135a5', '公司单位', 'personnelfiles_company', '', '', '', 'enterprise', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:32:23', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:23', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08dae-52eb-5c6d-1b13-b4afaa0da821', 'PersonnelFiles', 1, '39f08dac-e585-f2d5-3037-58c6397135a5', '部门列表', 'personnelfiles_department', '', '', '', 'department', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:26', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08dae-5e15-ecf9-fd83-439ed212a310', 'PersonnelFiles', 1, '39f08dac-e585-f2d5-3037-58c6397135a5', '岗位列表', 'personnelfiles_position', '', '', '', 'post', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:32:29', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:29', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08dae-8858-4e6a-b14d-9dc3537cb68b', 'PersonnelFiles', 1, '39f08dac-e585-f2d5-3037-58c6397135a5', '人员信息', 'personnelfiles_user', '', '', '', 'user', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:32:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08dae-d22c-4988-0dd9-098fe905e5bd', 'Common', 1, '39f08dad-0641-fb8c-d3ab-b3bdcb803500', '区划代码', 'common_area', '', '', '', 'area', '', 1, b'1', 1, 0, '', '', b'1', '', '2019-09-29 13:32:59', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 12:16:06', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08dae-e142-29fb-f4d4-4f8574d5d3b6', 'Common', 1, '39f08dad-0641-fb8c-d3ab-b3bdcb803500', '附件管理', 'common_attachment', '', '', '', 'attachment', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:03', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 12:16:06', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08daf-10e1-d0ef-f1be-27d6254133fe', 'Quartz', 1, '39f08dad-3e6f-5955-ccdf-96b081a39bdd', '任务组', 'quartz_group', '', '', '', 'basic-data', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:15', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:37', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08daf-4489-44fd-b2ca-06acd10db080', 'Quartz', 1, '39f08dad-3e6f-5955-ccdf-96b081a39bdd', '任务列表', 'quartz_job', '', '', '', 'tag', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:28', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-7355-6fb9-cfe6-b9804de226f9', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '模块管理', 'admin_moduleinfo', '', '', '', 'product', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:40', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:40', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-8081-2b67-5f77-3c4119b2ab66', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '权限列表', 'admin_permission', '', '', '', 'verifycode', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:43', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:43', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-8feb-d2ce-66dc-12fff451a969', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '菜单管理', 'admin_menu', '', '', '', 'menu', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:47', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-9b05-80d5-a7f5-21e450af9b70', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '角色管理', 'admin_role', '', '', '', 'role', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:50', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-a689-5e91-dce9-e6d0088d29dc', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '账户管理', 'admin_account', '', '', '', 'user', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:53', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-b44b-8fbf-3a88-629dc0922e84', 'Admin', 1, '39f08dac-9b84-9a56-20b9-26789223a74f', '审计日志', 'admin_auditinfo', '', '', '', 'log', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:33:57', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:33:57', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-e02b-bd7b-be06-12eeee0f4da4', 'Admin', 1, '39f08dac-84e1-54e2-22ee-e5d7e3606f90', '系统配置', 'admin_system', '', '', '', 'system', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:34:08', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:40:00', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08daf-f8f6-2dad-42c0-c73c787c6def', 'Admin', 1, '39f08dac-84e1-54e2-22ee-e5d7e3606f90', '配置项', 'admin_config', '', '', '', 'tag', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:34:14', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:07', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08db0-067d-fc37-84cb-b5283891fcce', 'Admin', 1, '39f08dac-84e1-54e2-22ee-e5d7e3606f90', '图标管理', 'admin_icon', '', '', '', 'photo', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:34:18', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-09 15:15:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f08db3-ea1d-1eab-35a7-70bf9342e718', 'CodeGenerator', 1, '39f08dad-6b9b-0fa4-767f-3b85eebbd534', '项目列表', 'codegenerator_project', '', '', '', 'project', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:38:33', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f08db3-f74c-72f6-f901-26a8a0677346', 'CodeGenerator', 1, '39f08dad-6b9b-0fa4-767f-3b85eebbd534', '枚举列表', 'codegenerator_enum', '', '', '', '', '', 1, b'1', 0, 0, '', '', b'1', '', '2019-09-29 13:38:36', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-09-29 13:38:36', '39f08d5b-177c-a334-34e7-408ef121c6e0');
INSERT INTO `menu` VALUES ('39f0c58b-1883-e583-ea02-bb7c9015c636', '', 0, '39f08dae-46ba-04e4-a24b-346b81f8d936', '公司回收站', '', '', '', '', '', '', 2, b'1', 0, -1, '', '', b'1', '', '2019-10-10 09:52:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-10-10 09:52:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f0c58c-5319-171e-6b51-468dd416c9e6', '', 2, '39f08dae-46ba-04e4-a24b-346b81f8d936', '百度链接', '', '', '', 'http://www.baidu.com', 'property', '#8F3D3D', 2, b'1', 1, 2, '', '', b'1', '', '2019-10-10 09:54:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-10-12 11:59:28', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f0c58d-4587-e80c-97c5-b863dad9cab8', '', 2, '39f08dac-e585-f2d5-3037-58c6397135a5', '百度', '', '', '', 'https://github.com', '', '', 1, b'1', 4, 3, '', '', b'1', '', '2019-10-10 09:55:04', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-10-10 10:17:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f0c5ba-1263-888c-80b4-2f5e3dcc40dc', '', 2, '39f08daf-8feb-d2ce-66dc-12fff451a969', 'X', '', '', '', 'https://www.baidu.com', '', '', 2, b'1', 0, 4, '', '', b'1', '', '2019-10-10 10:44:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-10-10 10:44:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39f0d10d-56b3-731b-f694-27dd6f5ac0a2', '', 2, '00000000-0000-0000-0000-000000000000', '文档说明', '', '', '', 'https://nm.iamoldli.com/docs', 'project', '', 0, b'1', 6, 0, '', '', b'1', '', '2019-10-12 15:30:47', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-10-12 15:32:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for menu_permission
-- ----------------------------
DROP TABLE IF EXISTS `menu_permission`;
CREATE TABLE `menu_permission`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MenuCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `PermissionCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 28 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menu_permission
-- ----------------------------
INSERT INTO `menu_permission` VALUES (1, 'personnelfiles_company', 'personnelfiles_company_query_get');
INSERT INTO `menu_permission` VALUES (2, 'personnelfiles_department', 'personnelfiles_department_query_get');
INSERT INTO `menu_permission` VALUES (3, 'personnelfiles_department', 'personnelfiles_department_tree_get');
INSERT INTO `menu_permission` VALUES (4, 'personnelfiles_user', 'personnelfiles_user_query_get');
INSERT INTO `menu_permission` VALUES (5, 'common_area', 'common_area_query_get');
INSERT INTO `menu_permission` VALUES (6, 'common_area', 'common_area_querychildren_get');
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
INSERT INTO `menu_permission` VALUES (27, 'common_attachment', 'common_attachment_query_get');

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
INSERT INTO `moduleinfo` VALUES ('39f08d5b-ba44-6817-6d57-fd07e0c9a482', '权限管理', 'Admin', '1.2.6', '', '2019-09-29 12:02:13', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:57', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('39f08d5b-ba7a-b9a9-eba5-99bc7035b59b', '代码生成', 'CodeGenerator', '1.2.2', '', '2019-09-29 12:02:13', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:57', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('39f08d5b-ba95-78b5-1bce-75ff8a247679', '通用模块', 'Common', '1.2.2', '', '2019-09-29 12:02:13', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:57', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('39f08d5b-baa9-01a1-49d2-d841bc8c1dc3', '人事档案', 'PersonnelFiles', '1.2.2', '', '2019-09-29 12:02:13', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:57', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('39f08d5b-bab9-54cd-1705-1312a597b6b3', '任务调度', 'Quartz', '1.0.4', '', '2019-09-29 12:02:13', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 15:29:57', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

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
INSERT INTO `permission` VALUES ('39f08d5b-d104-dad2-d611-3586643f34b7', '账户管理_绑定角色', 'Admin', 'Account', 'BindRole', 2, 'admin_account_bindrole_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d110-8a6f-5d0c-1acc4cd6e713', '账户管理_查询', 'Admin', 'Account', 'Query', 0, 'admin_account_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d11d-e539-b8e1-58967d8fd541', '账户管理_添加', 'Admin', 'Account', 'Add', 2, 'admin_account_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d131-df4d-fb47-2698f84c7cf6', '账户管理_编辑', 'Admin', 'Account', 'Edit', 0, 'admin_account_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d136-91f1-39c4-eec8eb28f490', '账户管理_更新', 'Admin', 'Account', 'Update', 2, 'admin_account_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d149-b5a9-3e89-d13f3cedb1c6', '账户管理_删除', 'Admin', 'Account', 'Delete', 3, 'admin_account_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d158-c047-51d7-509b0606fd96', '账户管理_重置密码', 'Admin', 'Account', 'ResetPassword', 2, 'admin_account_resetpassword_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d175-9882-0f7c-fb2bae67f6c8', '审计信息_查询', 'Admin', 'AuditInfo', 'Query', 0, 'admin_auditinfo_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d1ae-0b50-d74d-23ea07538398', '审计信息_详情', 'Admin', 'AuditInfo', 'Details', 0, 'admin_auditinfo_details_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d1cc-e9c7-bd4d-da331764b4a3', '按钮管理_查询', 'Admin', 'Button', 'Query', 0, 'admin_button_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d1d6-55c8-3580-cda4932af779', '配置项管理_查询', 'Admin', 'Config', 'Query', 0, 'admin_config_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d1e0-65f2-73d4-8424438c8567', '配置项管理_添加', 'Admin', 'Config', 'Add', 2, 'admin_config_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d1e5-9e0e-0c29-ddf2a73482b8', '配置项管理_删除', 'Admin', 'Config', 'Delete', 3, 'admin_config_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d1ec-13f3-a1f5-23a2bc892d15', '配置项管理_编辑', 'Admin', 'Config', 'Edit', 0, 'admin_config_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d1f1-f48b-9ebe-766fcb346c63', '配置项管理_修改', 'Admin', 'Config', 'Update', 2, 'admin_config_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d200-dee4-a526-d3422506a692', '菜单管理_菜单树', 'Admin', 'Menu', 'Tree', 0, 'admin_menu_tree_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d210-2150-33bc-f8b9b97d2b10', '菜单管理_查询菜单列表', 'Admin', 'Menu', 'Query', 0, 'admin_menu_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d21c-40fb-6de2-3bddb3a1682a', '菜单管理_添加', 'Admin', 'Menu', 'Add', 2, 'admin_menu_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d225-a13c-7b44-e8c3e7d3d128', '菜单管理_删除', 'Admin', 'Menu', 'Delete', 3, 'admin_menu_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d22e-ee56-933a-ed09cfb4a706', '菜单管理_编辑', 'Admin', 'Menu', 'Edit', 0, 'admin_menu_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d236-7004-f0ca-925788da03aa', '菜单管理_更新', 'Admin', 'Menu', 'Update', 2, 'admin_menu_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d243-8f1a-7c50-6feeb098432e', '菜单管理_更新排序信息', 'Admin', 'Menu', 'Sort', 0, 'admin_menu_sort_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d24b-cfa0-96e6-9242c9cdff31', '菜单管理_更新排序信息', 'Admin', 'Menu', 'Sort', 2, 'admin_menu_sort_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d259-2ac7-dfa6-1bf0ae78e4f0', '模块信息_查询', 'Admin', 'ModuleInfo', 'Query', 0, 'admin_moduleinfo_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d264-13cc-1cc2-a4c2639e7fda', '模块信息_同步模块数据', 'Admin', 'ModuleInfo', 'Sync', 2, 'admin_moduleinfo_sync_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d269-43b8-2d42-48a8cf8a3c38', '模块信息_删除', 'Admin', 'ModuleInfo', 'Delete', 3, 'admin_moduleinfo_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d27d-241c-b966-5a4ce019fe3e', '权限接口_查询', 'Admin', 'Permission', 'Query', 0, 'admin_permission_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d286-6b4b-82cf-b8f4c8052fef', '权限接口_同步', 'Admin', 'Permission', 'Sync', 2, 'admin_permission_sync_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d28c-73c8-d195-f2de0713cbf3', '角色管理_查询', 'Admin', 'Role', 'Query', 0, 'admin_role_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d298-0e81-694f-723d60c55bda', '角色管理_添加', 'Admin', 'Role', 'Add', 2, 'admin_role_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d29d-7195-e52b-5b20e346f898', '角色管理_删除', 'Admin', 'Role', 'Delete', 3, 'admin_role_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2a5-3475-50d1-d3984f82f5d3', '角色管理_编辑', 'Admin', 'Role', 'Edit', 0, 'admin_role_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2aa-77a2-7f52-72d9974f7020', '角色管理_修改', 'Admin', 'Role', 'Update', 2, 'admin_role_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2b5-55c5-995d-17557580db07', '角色管理_获取角色的关联菜单列表', 'Admin', 'Role', 'MenuList', 0, 'admin_role_menulist_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2b8-0db7-9575-bd6cd85f12b0', '角色管理_绑定菜单', 'Admin', 'Role', 'BindMenu', 2, 'admin_role_bindmenu_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2bc-3e4d-3993-a7a34ef134af', '角色管理_获取角色关联的菜单按钮列表', 'Admin', 'Role', 'MenuButtonList', 0, 'admin_role_menubuttonlist_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2c5-438b-194f-6b5f35cb587b', '角色管理_绑定菜单按钮', 'Admin', 'Role', 'BindMenuButton', 2, 'admin_role_bindmenubutton_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2cd-b7a0-c335-9b298cb1a765', '系统_修改系统配置', 'Admin', 'System', 'Config', 2, 'admin_system_config_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2d4-846e-7503-ee323474339d', '系统_上传Logo', 'Admin', 'System', 'UploadLogo', 2, 'admin_system_uploadlogo_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2da-2067-fedd-c1981f38df43', '实体管理_查询', 'CodeGenerator', 'Class', 'Query', 0, 'codegenerator_class_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2df-c717-3148-d83bafd4332d', '实体管理_添加', 'CodeGenerator', 'Class', 'Add', 2, 'codegenerator_class_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2eb-23f8-e1c4-dc922a8b790e', '实体管理_删除', 'CodeGenerator', 'Class', 'Delete', 3, 'codegenerator_class_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2f6-8a32-bce2-5e28a1d7be43', '实体管理_编辑', 'CodeGenerator', 'Class', 'Edit', 0, 'codegenerator_class_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d2fb-0602-bb66-88b07a28bb43', '实体管理_修改', 'CodeGenerator', 'Class', 'Update', 2, 'codegenerator_class_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d303-42c3-f5e9-0e5dd320aa3c', '实体管理_生成代码', 'CodeGenerator', 'Class', 'BuildCode', 0, 'codegenerator_class_buildcode_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d30d-9bfb-52a4-e3e9adb76294', '枚举管理_查询', 'CodeGenerator', 'Enum', 'Query', 0, 'codegenerator_enum_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d315-cf2e-6bca-1fc7ec8581f8', '枚举管理_添加', 'CodeGenerator', 'Enum', 'Add', 2, 'codegenerator_enum_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d31b-2433-ea5b-e5cd7145c0b7', '枚举管理_删除', 'CodeGenerator', 'Enum', 'Delete', 3, 'codegenerator_enum_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d31f-8b21-bb03-ac74fc419395', '枚举管理_编辑', 'CodeGenerator', 'Enum', 'Edit', 0, 'codegenerator_enum_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d32b-1bba-d4df-dce1ead4e5c6', '枚举管理_修改', 'CodeGenerator', 'Enum', 'Update', 2, 'codegenerator_enum_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d330-6ce3-27e0-e817c358a277', '枚举项管理_查询', 'CodeGenerator', 'EnumItem', 'Query', 0, 'codegenerator_enumitem_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d33a-0383-9b4d-b523cbf63a2f', '枚举项管理_添加', 'CodeGenerator', 'EnumItem', 'Add', 2, 'codegenerator_enumitem_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d33e-96ad-145f-7f994faa1903', '枚举项管理_删除', 'CodeGenerator', 'EnumItem', 'Delete', 3, 'codegenerator_enumitem_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d348-4947-0163-9178fd77b3b7', '枚举项管理_编辑', 'CodeGenerator', 'EnumItem', 'Edit', 0, 'codegenerator_enumitem_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d356-b5f3-b226-a10029cacfec', '枚举项管理_修改', 'CodeGenerator', 'EnumItem', 'Update', 2, 'codegenerator_enumitem_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d35b-8764-7535-052a29b44b08', '枚举项管理_更新排序信息', 'CodeGenerator', 'EnumItem', 'Sort', 0, 'codegenerator_enumitem_sort_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d360-18cb-98f5-e261b113ab04', '枚举项管理_更新排序信息', 'CodeGenerator', 'EnumItem', 'Sort', 2, 'codegenerator_enumitem_sort_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d367-f69a-57b3-ce8ded7f3de5', '模型属性管理_查询', 'CodeGenerator', 'ModelProperty', 'Query', 0, 'codegenerator_modelproperty_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d36f-53d1-f820-7a9c7c8c4b6d', '模型属性管理_添加', 'CodeGenerator', 'ModelProperty', 'Add', 2, 'codegenerator_modelproperty_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d374-88bb-a099-d97422e5cec0', '模型属性管理_删除', 'CodeGenerator', 'ModelProperty', 'Delete', 3, 'codegenerator_modelproperty_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d37b-3184-57e0-9604480a4960', '模型属性管理_编辑', 'CodeGenerator', 'ModelProperty', 'Edit', 0, 'codegenerator_modelproperty_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d37f-eace-978d-cfdea1abd89f', '模型属性管理_修改', 'CodeGenerator', 'ModelProperty', 'Update', 2, 'codegenerator_modelproperty_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d383-e095-add9-da0247912439', '模型属性管理_更新排序信息', 'CodeGenerator', 'ModelProperty', 'Sort', 0, 'codegenerator_modelproperty_sort_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d391-273a-ffdc-0a48c08b7f27', '模型属性管理_更新排序信息', 'CodeGenerator', 'ModelProperty', 'Sort', 2, 'codegenerator_modelproperty_sort_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d395-9c76-e512-f29226332659', '模型属性管理_修改可空状态', 'CodeGenerator', 'ModelProperty', 'UpdateNullable', 2, 'codegenerator_modelproperty_updatenullable_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d39f-2fff-d93c-cba8aa9f7df6', '模型属性管理_获取下拉列表', 'CodeGenerator', 'ModelProperty', 'Select', 0, 'codegenerator_modelproperty_select_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3a4-f9d0-4299-66546a7b29c9', '模型属性管理_从实体中导入属性', 'CodeGenerator', 'ModelProperty', 'ImportFromEntity', 2, 'codegenerator_modelproperty_importfromentity_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3ad-ba0a-25ec-778fe3502edf', '项目管理_查询', 'CodeGenerator', 'Project', 'Query', 0, 'codegenerator_project_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3b0-524c-6f42-574828bd090c', '项目管理_添加', 'CodeGenerator', 'Project', 'Add', 2, 'codegenerator_project_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3b4-64a3-f782-ffbe29288efd', '项目管理_删除', 'CodeGenerator', 'Project', 'Delete', 3, 'codegenerator_project_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3c6-023e-4950-4ea16561b8ac', '项目管理_编辑', 'CodeGenerator', 'Project', 'Edit', 0, 'codegenerator_project_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3cc-e1ac-e5d6-1764c3c1c3e6', '项目管理_修改', 'CodeGenerator', 'Project', 'Update', 2, 'codegenerator_project_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3d5-0bd7-b4e9-69a3a8af930a', '项目管理_生成代码', 'CodeGenerator', 'Project', 'BuildCode', 2, 'codegenerator_project_buildcode_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3de-9353-6bf0-dd9879f4ec95', '实体属性管理_查询', 'CodeGenerator', 'Property', 'Query', 0, 'codegenerator_property_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3e4-2ab7-6ca6-053aaee91ece', '实体属性管理_添加', 'CodeGenerator', 'Property', 'Add', 2, 'codegenerator_property_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3ed-a6c6-d5e6-9e85c28c9f65', '实体属性管理_删除', 'CodeGenerator', 'Property', 'Delete', 3, 'codegenerator_property_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3f5-2291-e063-03bab0dfcd6b', '实体属性管理_编辑', 'CodeGenerator', 'Property', 'Edit', 0, 'codegenerator_property_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d3f9-604e-569e-26fa8ac814d7', '实体属性管理_修改', 'CodeGenerator', 'Property', 'Update', 2, 'codegenerator_property_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d401-e1db-1b90-d59ab9892eda', '实体属性管理_获取属性类型下拉列表', 'CodeGenerator', 'Property', 'PropertyTypeSelect', 0, 'codegenerator_property_propertytypeselect_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d408-99fb-2545-81f711da2b63', '实体属性管理_更新排序信息', 'CodeGenerator', 'Property', 'Sort', 0, 'codegenerator_property_sort_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d411-7317-61fe-bc3599160088', '实体属性管理_更新排序信息', 'CodeGenerator', 'Property', 'Sort', 2, 'codegenerator_property_sort_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d415-0d9c-381f-0ccf3a83e7cc', '实体属性管理_修改可空状态', 'CodeGenerator', 'Property', 'UpdateNullable', 2, 'codegenerator_property_updatenullable_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d419-b6aa-d6ff-621d92fdf3b2', '实体属性管理_修改列表显示状态', 'CodeGenerator', 'Property', 'UpdateShowInList', 2, 'codegenerator_property_updateshowinlist_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d41e-cb35-4365-dd9d5d503109', '实体属性管理_获取下拉列表', 'CodeGenerator', 'Property', 'Select', 0, 'codegenerator_property_select_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d42a-26ca-f237-b54ae91aa7d0', '区划代码管理_查询', 'Common', 'Area', 'Query', 0, 'common_area_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d437-7718-e10b-ad340ba1d8eb', '区划代码管理_添加', 'Common', 'Area', 'Add', 2, 'common_area_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d43c-b6bf-6796-e43b7e47a879', '区划代码管理_删除', 'Common', 'Area', 'Delete', 3, 'common_area_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d44c-b50d-79ef-a7c468935861', '区划代码管理_编辑', 'Common', 'Area', 'Edit', 0, 'common_area_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d459-f5c1-ba9f-ef07ade74ad4', '区划代码管理_修改', 'Common', 'Area', 'Update', 2, 'common_area_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d468-26b8-ca6e-2d3358b041bb', '附件表管理_查询', 'Common', 'Attachment', 'Query', 0, 'common_attachment_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d47b-0354-f997-f26ed80caffb', '字典管理_查询', 'Common', 'Dict', 'Query', 0, 'common_dict_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d48e-43e4-daf0-729d5cedc2de', '字典管理_添加', 'Common', 'Dict', 'Add', 2, 'common_dict_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d4a3-3b4c-681a-ff99f584ac76', '字典管理_删除', 'Common', 'Dict', 'Delete', 3, 'common_dict_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d4c8-5bd2-1de3-d0e6ff088e9b', '字典管理_编辑', 'Common', 'Dict', 'Edit', 0, 'common_dict_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d4d1-d13c-0159-c76cd91999f9', '字典管理_修改', 'Common', 'Dict', 'Update', 2, 'common_dict_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d4d9-7c18-3422-03fed0d5180e', '多媒体管理_查询', 'Common', 'MediaType', 'Query', 0, 'common_mediatype_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d4e2-3298-6eda-6c19744563a1', '多媒体管理_添加', 'Common', 'MediaType', 'Add', 2, 'common_mediatype_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d4eb-f3a7-d8f9-6d138eb16fc7', '多媒体管理_删除', 'Common', 'MediaType', 'Delete', 3, 'common_mediatype_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d4f1-974c-575d-648729a3425c', '多媒体管理_编辑', 'Common', 'MediaType', 'Edit', 0, 'common_mediatype_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d4fa-3cfb-de7b-c4ad460f4282', '多媒体管理_修改', 'Common', 'MediaType', 'Update', 2, 'common_mediatype_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d4ff-5225-739a-cc4ba370746f', '公司单位管理_查询', 'PersonnelFiles', 'Company', 'Query', 0, 'personnelfiles_company_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d510-4e49-a621-0c99826ce308', '公司单位管理_添加', 'PersonnelFiles', 'Company', 'Add', 2, 'personnelfiles_company_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d51d-6869-59a2-f860f8e604b6', '公司单位管理_删除', 'PersonnelFiles', 'Company', 'Delete', 3, 'personnelfiles_company_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d523-c338-11c5-3fcd9c185316', '公司单位管理_编辑', 'PersonnelFiles', 'Company', 'Edit', 0, 'personnelfiles_company_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d52b-e65a-3c7c-f65dd7288d2e', '公司单位管理_修改', 'PersonnelFiles', 'Company', 'Update', 2, 'personnelfiles_company_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d52f-5f85-8e56-fc0838f2a994', '部门管理_部门树', 'PersonnelFiles', 'Department', 'Tree', 0, 'personnelfiles_department_tree_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d538-a08d-505f-42b19bd3653f', '部门管理_查询', 'PersonnelFiles', 'Department', 'Query', 0, 'personnelfiles_department_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d53f-8c0b-40da-7271ca3f1042', '部门管理_添加', 'PersonnelFiles', 'Department', 'Add', 2, 'personnelfiles_department_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d545-1a72-4eaf-44d80d3636d4', '部门管理_删除', 'PersonnelFiles', 'Department', 'Delete', 3, 'personnelfiles_department_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d54b-8ed3-0ad5-8f2ac5831a0f', '部门管理_编辑', 'PersonnelFiles', 'Department', 'Edit', 0, 'personnelfiles_department_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d550-0699-ff62-926d0566ac84', '部门管理_修改', 'PersonnelFiles', 'Department', 'Update', 2, 'personnelfiles_department_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d558-5e0a-cfea-73150fe2f841', '岗位管理_查询', 'PersonnelFiles', 'Position', 'Query', 0, 'personnelfiles_position_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d55f-0d60-8536-3fe365273484', '岗位管理_添加', 'PersonnelFiles', 'Position', 'Add', 2, 'personnelfiles_position_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d567-e00f-152f-09e0675916e5', '岗位管理_删除', 'PersonnelFiles', 'Position', 'Delete', 3, 'personnelfiles_position_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d56d-66e1-c701-844b494872a9', '岗位管理_编辑', 'PersonnelFiles', 'Position', 'Edit', 0, 'personnelfiles_position_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d574-87ea-3cf8-0c3008d57916', '岗位管理_修改', 'PersonnelFiles', 'Position', 'Update', 2, 'personnelfiles_position_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d587-1b85-810e-544e5e816644', '用户信息管理_查询', 'PersonnelFiles', 'User', 'Query', 0, 'personnelfiles_user_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d591-e2ff-ff3c-1469007b87ff', '用户信息管理_添加', 'PersonnelFiles', 'User', 'Add', 2, 'personnelfiles_user_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d597-b5e4-82ab-d0a505d20491', '用户信息管理_删除', 'PersonnelFiles', 'User', 'Delete', 3, 'personnelfiles_user_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5a2-7d6a-11f7-64f56fbd2633', '用户信息管理_编辑', 'PersonnelFiles', 'User', 'Edit', 0, 'personnelfiles_user_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5aa-c84f-1db3-8c01ac65842c', '用户信息管理_修改', 'PersonnelFiles', 'User', 'Update', 2, 'personnelfiles_user_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5b2-936a-0268-1f369650683e', '用户信息管理_上传照片', 'PersonnelFiles', 'User', 'UploadPicture', 2, 'personnelfiles_user_uploadpicture_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5b8-87cf-4d23-47db6d3ba0af', '用户信息管理_编辑联系信息', 'PersonnelFiles', 'User', 'EditContact', 0, 'personnelfiles_user_editcontact_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5c2-fb1f-f8d0-76178e88a034', '用户信息管理_修改联系信息', 'PersonnelFiles', 'User', 'UpdateContact', 2, 'personnelfiles_user_updatecontact_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5c6-4fd4-56e0-9c1b06f3ea67', '用户信息管理_联系信息详情', 'PersonnelFiles', 'User', 'ContactDetails', 0, 'personnelfiles_user_contactdetails_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5cd-ccda-60d7-2a45111cb78f', '用户教育经历管理_查询', 'PersonnelFiles', 'UserEducationHistory', 'Query', 0, 'personnelfiles_usereducationhistory_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5d6-ae89-f555-3fa38cf7ec24', '用户教育经历管理_添加', 'PersonnelFiles', 'UserEducationHistory', 'Add', 2, 'personnelfiles_usereducationhistory_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5e3-5851-dfca-67cae195f7b4', '用户教育经历管理_删除', 'PersonnelFiles', 'UserEducationHistory', 'Delete', 3, 'personnelfiles_usereducationhistory_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5eb-7995-b5c7-5f00f579860f', '用户教育经历管理_编辑', 'PersonnelFiles', 'UserEducationHistory', 'Edit', 0, 'personnelfiles_usereducationhistory_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5f3-6032-8d75-0aac643f86f1', '用户教育经历管理_修改', 'PersonnelFiles', 'UserEducationHistory', 'Update', 2, 'personnelfiles_usereducationhistory_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d5fa-6a31-832f-f8d2a0fd61a0', '用户工作经历管理_查询', 'PersonnelFiles', 'UserWorkHistory', 'Query', 0, 'personnelfiles_userworkhistory_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d602-65c9-b5a6-61858ae32293', '用户工作经历管理_添加', 'PersonnelFiles', 'UserWorkHistory', 'Add', 2, 'personnelfiles_userworkhistory_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d609-83b2-4594-94c8223413e6', '用户工作经历管理_删除', 'PersonnelFiles', 'UserWorkHistory', 'Delete', 3, 'personnelfiles_userworkhistory_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d60e-3295-fd55-b4a243c28dd1', '用户工作经历管理_编辑', 'PersonnelFiles', 'UserWorkHistory', 'Edit', 0, 'personnelfiles_userworkhistory_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d619-fb8c-d397-799d82f4e6f6', '用户工作经历管理_修改', 'PersonnelFiles', 'UserWorkHistory', 'Update', 2, 'personnelfiles_userworkhistory_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d61d-aebf-4fb2-0594718fca8e', '任务组管理_查询', 'Quartz', 'Group', 'Query', 0, 'quartz_group_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d624-4829-5a24-82e992a06708', '任务组管理_添加', 'Quartz', 'Group', 'Add', 2, 'quartz_group_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d628-a463-f055-61b6b32480d9', '任务组管理_删除', 'Quartz', 'Group', 'Delete', 3, 'quartz_group_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d62c-0b5b-e322-baa7ee0d227e', '任务管理_查询', 'Quartz', 'Job', 'Query', 0, 'quartz_job_query_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d633-3635-da2a-1ab40eeb7edc', '任务管理_添加', 'Quartz', 'Job', 'Add', 2, 'quartz_job_add_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d63d-5437-c1cd-9c4f1c7c4a1b', '任务管理_编辑', 'Quartz', 'Job', 'Edit', 0, 'quartz_job_edit_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d642-37aa-d6b3-3212db94b03d', '任务管理_修改', 'Quartz', 'Job', 'Update', 2, 'quartz_job_update_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d656-681b-82f9-76045707dfb4', '任务管理_删除', 'Quartz', 'Job', 'Delete', 3, 'quartz_job_delete_delete', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d660-aa20-ac4e-7185082235ce', '任务管理_暂停', 'Quartz', 'Job', 'Pause', 2, 'quartz_job_pause_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d669-d9ab-ea81-302e8a7dac4c', '任务管理_启动', 'Quartz', 'Job', 'Resume', 2, 'quartz_job_resume_post', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39f08d5b-d66d-4eed-dd76-d16f337504de', '任务管理_日志', 'Quartz', 'Job', 'Log', 0, 'quartz_job_log_get', '2019-09-29 12:02:19', '39f08d5b-177c-a334-34e7-408ef121c6e0', '2019-10-12 14:44:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

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
INSERT INTO `role` VALUES ('39f08d5b-173b-1cbb-7381-407c87b2e8e2', '超级管理员', '超级管理员0', b'0', '2019-09-29 12:01:31', '00000000-0000-0000-0000-000000000000', '2019-10-12 14:15:57', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', b'0', '2019-09-29 12:01:31', '00000000-0000-0000-0000-000000000000');
INSERT INTO `role` VALUES ('39f0d072-c8bc-598c-774a-595eddb928db', '普通管理员', '', b'0', '2019-10-12 12:41:58', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-10-12 12:41:58', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', b'0', '2019-10-12 12:41:58', '00000000-0000-0000-0000-000000000000');
INSERT INTO `role` VALUES ('39f0d08e-0c1d-ffa3-baf8-1384c8c4374b', '5', '55', b'0', '2019-10-12 13:11:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-10-12 13:12:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', b'1', '2019-10-12 13:17:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `role` VALUES ('39f0d0d7-8372-3bbe-6959-740dbe81ade2', '人事管理员', '', b'0', '2019-10-12 14:31:59', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-10-12 14:31:59', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', b'0', '2019-10-12 14:31:59', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for role_menu
-- ----------------------------
DROP TABLE IF EXISTS `role_menu`;
CREATE TABLE `role_menu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `MenuId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1122 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role_menu
-- ----------------------------
INSERT INTO `role_menu` VALUES (945, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dac-e585-f2d5-3037-58c6397135a5');
INSERT INTO `role_menu` VALUES (946, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-46ba-04e4-a24b-346b81f8d936');
INSERT INTO `role_menu` VALUES (947, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821');
INSERT INTO `role_menu` VALUES (948, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-5e15-ecf9-fd83-439ed212a310');
INSERT INTO `role_menu` VALUES (949, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b');
INSERT INTO `role_menu` VALUES (950, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f0c58d-4587-e80c-97c5-b863dad9cab8');
INSERT INTO `role_menu` VALUES (1035, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dad-0641-fb8c-d3ab-b3bdcb803500');
INSERT INTO `role_menu` VALUES (1036, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-e142-29fb-f4d4-4f8574d5d3b6');
INSERT INTO `role_menu` VALUES (1037, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-d22c-4988-0dd9-098fe905e5bd');
INSERT INTO `role_menu` VALUES (1038, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dad-3e6f-5955-ccdf-96b081a39bdd');
INSERT INTO `role_menu` VALUES (1039, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-10e1-d0ef-f1be-27d6254133fe');
INSERT INTO `role_menu` VALUES (1040, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-4489-44fd-b2ca-06acd10db080');
INSERT INTO `role_menu` VALUES (1041, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dac-e585-f2d5-3037-58c6397135a5');
INSERT INTO `role_menu` VALUES (1042, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-46ba-04e4-a24b-346b81f8d936');
INSERT INTO `role_menu` VALUES (1043, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821');
INSERT INTO `role_menu` VALUES (1044, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-5e15-ecf9-fd83-439ed212a310');
INSERT INTO `role_menu` VALUES (1045, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b');
INSERT INTO `role_menu` VALUES (1046, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f0c58d-4587-e80c-97c5-b863dad9cab8');
INSERT INTO `role_menu` VALUES (1047, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dad-6b9b-0fa4-767f-3b85eebbd534');
INSERT INTO `role_menu` VALUES (1048, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db3-ea1d-1eab-35a7-70bf9342e718');
INSERT INTO `role_menu` VALUES (1049, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db3-f74c-72f6-f901-26a8a0677346');
INSERT INTO `role_menu` VALUES (1050, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dac-84e1-54e2-22ee-e5d7e3606f90');
INSERT INTO `role_menu` VALUES (1051, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-e02b-bd7b-be06-12eeee0f4da4');
INSERT INTO `role_menu` VALUES (1052, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-f8f6-2dad-42c0-c73c787c6def');
INSERT INTO `role_menu` VALUES (1053, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db0-067d-fc37-84cb-b5283891fcce');
INSERT INTO `role_menu` VALUES (1054, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dac-9b84-9a56-20b9-26789223a74f');
INSERT INTO `role_menu` VALUES (1055, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-7355-6fb9-cfe6-b9804de226f9');
INSERT INTO `role_menu` VALUES (1056, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8081-2b67-5f77-3c4119b2ab66');
INSERT INTO `role_menu` VALUES (1057, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969');
INSERT INTO `role_menu` VALUES (1058, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70');
INSERT INTO `role_menu` VALUES (1059, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc');
INSERT INTO `role_menu` VALUES (1060, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-b44b-8fbf-3a88-629dc0922e84');
INSERT INTO `role_menu` VALUES (1061, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dad-aca0-7ddf-61d1-0b6ac0841b64');
INSERT INTO `role_menu` VALUES (1062, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f0d10d-56b3-731b-f694-27dd6f5ac0a2');
INSERT INTO `role_menu` VALUES (1216, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dad-0641-fb8c-d3ab-b3bdcb803500');
INSERT INTO `role_menu` VALUES (1217, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-e142-29fb-f4d4-4f8574d5d3b6');
INSERT INTO `role_menu` VALUES (1218, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-d22c-4988-0dd9-098fe905e5bd');
INSERT INTO `role_menu` VALUES (1219, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dad-3e6f-5955-ccdf-96b081a39bdd');
INSERT INTO `role_menu` VALUES (1220, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08daf-10e1-d0ef-f1be-27d6254133fe');
INSERT INTO `role_menu` VALUES (1221, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08daf-4489-44fd-b2ca-06acd10db080');
INSERT INTO `role_menu` VALUES (1222, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dac-e585-f2d5-3037-58c6397135a5');
INSERT INTO `role_menu` VALUES (1223, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-46ba-04e4-a24b-346b81f8d936');
INSERT INTO `role_menu` VALUES (1224, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-52eb-5c6d-1b13-b4afaa0da821');
INSERT INTO `role_menu` VALUES (1225, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-5e15-ecf9-fd83-439ed212a310');
INSERT INTO `role_menu` VALUES (1226, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-8858-4e6a-b14d-9dc3537cb68b');
INSERT INTO `role_menu` VALUES (1227, '39f0d072-c8bc-598c-774a-595eddb928db', '39f0c58d-4587-e80c-97c5-b863dad9cab8');
INSERT INTO `role_menu` VALUES (1228, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dad-6b9b-0fa4-767f-3b85eebbd534');
INSERT INTO `role_menu` VALUES (1229, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08db3-ea1d-1eab-35a7-70bf9342e718');
INSERT INTO `role_menu` VALUES (1230, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08db3-f74c-72f6-f901-26a8a0677346');
INSERT INTO `role_menu` VALUES (1231, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dac-84e1-54e2-22ee-e5d7e3606f90');
INSERT INTO `role_menu` VALUES (1232, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08daf-e02b-bd7b-be06-12eeee0f4da4');
INSERT INTO `role_menu` VALUES (1233, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08daf-f8f6-2dad-42c0-c73c787c6def');
INSERT INTO `role_menu` VALUES (1234, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08db0-067d-fc37-84cb-b5283891fcce');
INSERT INTO `role_menu` VALUES (1235, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dac-9b84-9a56-20b9-26789223a74f');
INSERT INTO `role_menu` VALUES (1236, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08daf-7355-6fb9-cfe6-b9804de226f9');
INSERT INTO `role_menu` VALUES (1237, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08daf-8081-2b67-5f77-3c4119b2ab66');

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
) ENGINE = InnoDB AUTO_INCREMENT = 164 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role_menu_button
-- ----------------------------
INSERT INTO `role_menu_button` VALUES (22, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-10e1-d0ef-f1be-27d6254133fe', '39f08daf-10f0-4af0-c82e-525f06a2f2c5');
INSERT INTO `role_menu_button` VALUES (23, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-10e1-d0ef-f1be-27d6254133fe', '39f08daf-10f7-7c83-9eec-048c3b1c9884');
INSERT INTO `role_menu_button` VALUES (30, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-7355-6fb9-cfe6-b9804de226f9', '39f08daf-7366-40d9-6116-510f50a9949b');
INSERT INTO `role_menu_button` VALUES (32, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8081-2b67-5f77-3c4119b2ab66', '39f08daf-8092-82f1-bf83-c74b15f5fae3');
INSERT INTO `role_menu_button` VALUES (34, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969', '39f08daf-8ffe-d189-3f53-f2174cb2e3d4');
INSERT INTO `role_menu_button` VALUES (39, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70', '39f08daf-9b28-8f39-eeb7-289ea4c44604');
INSERT INTO `role_menu_button` VALUES (40, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70', '39f08daf-9b38-d4f7-270d-5e8629f9601d');
INSERT INTO `role_menu_button` VALUES (45, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-b44b-8fbf-3a88-629dc0922e84', '39f08daf-b45e-0264-f197-063f969d3aa0');
INSERT INTO `role_menu_button` VALUES (46, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-f8f6-2dad-42c0-c73c787c6def', '39f08daf-f905-e02f-341a-bbc0b80cbf5e');
INSERT INTO `role_menu_button` VALUES (47, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-f8f6-2dad-42c0-c73c787c6def', '39f08daf-f909-628c-9a0a-2c23a57b9103');
INSERT INTO `role_menu_button` VALUES (48, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-f8f6-2dad-42c0-c73c787c6def', '39f08daf-f915-e1f1-d7d8-ebd21adbebe6');
INSERT INTO `role_menu_button` VALUES (49, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db3-ea1d-1eab-35a7-70bf9342e718', '39f08db3-ea6b-6af7-1ef5-a226af30fbe4');
INSERT INTO `role_menu_button` VALUES (50, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db3-ea1d-1eab-35a7-70bf9342e718', '39f08db3-ea80-e0e3-69ea-af5ab431ac6f');
INSERT INTO `role_menu_button` VALUES (51, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db3-ea1d-1eab-35a7-70bf9342e718', '39f08db3-eaa7-e42f-34fb-25baaaf55e87');
INSERT INTO `role_menu_button` VALUES (52, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db3-ea1d-1eab-35a7-70bf9342e718', '39f08db3-eada-892b-b250-c2d58279d388');
INSERT INTO `role_menu_button` VALUES (53, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db3-f74c-72f6-f901-26a8a0677346', '39f08db3-f788-9a1c-f227-f372acb73cea');
INSERT INTO `role_menu_button` VALUES (54, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db3-f74c-72f6-f901-26a8a0677346', '39f08db3-f7ba-4c00-d053-ca662ef2ad06');
INSERT INTO `role_menu_button` VALUES (55, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08db3-f74c-72f6-f901-26a8a0677346', '39f08db3-f7d6-7533-682f-739e51d10ae1');
INSERT INTO `role_menu_button` VALUES (61, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-46ba-04e4-a24b-346b81f8d936', '39f08dae-4709-666f-39e8-11cfb7db4594');
INSERT INTO `role_menu_button` VALUES (64, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70', '39f08daf-9b1f-b315-ccc8-4ed480ae2595');
INSERT INTO `role_menu_button` VALUES (65, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-9b05-80d5-a7f5-21e450af9b70', '39f08daf-9b13-0809-b6f5-64d2430a37c4');
INSERT INTO `role_menu_button` VALUES (66, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-46ba-04e4-a24b-346b81f8d936', '39f08dae-46e6-f6f2-cbff-fe925a9d9946');
INSERT INTO `role_menu_button` VALUES (68, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-5e15-ecf9-fd83-439ed212a310', '39f08dae-5e49-6a91-4661-55c6f645cc45');
INSERT INTO `role_menu_button` VALUES (70, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-e142-29fb-f4d4-4f8574d5d3b6', '39f08dae-e147-307d-4b87-1742fa9e5885');
INSERT INTO `role_menu_button` VALUES (71, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-8895-ce81-c6ec-723849dbc839');
INSERT INTO `role_menu_button` VALUES (72, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-88a0-823c-9da9-9fbe6d546612');
INSERT INTO `role_menu_button` VALUES (73, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-887d-fda0-3bf1-e76c7c490364');
INSERT INTO `role_menu_button` VALUES (74, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-8886-697c-73d1-0ab76ecaf2b9');
INSERT INTO `role_menu_button` VALUES (83, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5305-3dc4-ebe6-771d999a8e44');
INSERT INTO `role_menu_button` VALUES (84, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5315-b2cb-bd0a-65aaec059274');
INSERT INTO `role_menu_button` VALUES (85, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5323-c008-deeb-865f8b94be29');
INSERT INTO `role_menu_button` VALUES (86, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5327-e25c-ff40-0523a20e5d8c');
INSERT INTO `role_menu_button` VALUES (87, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-533a-604e-b0f7-a987eeb2f0de');
INSERT INTO `role_menu_button` VALUES (88, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-534a-fcdd-27dc-2f5b0f8eb80c');
INSERT INTO `role_menu_button` VALUES (89, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5358-b231-a8c5-0116ba761066');
INSERT INTO `role_menu_button` VALUES (92, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-886e-6a3e-9a33-4f95f1bed0ae');
INSERT INTO `role_menu_button` VALUES (93, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc', '39f08daf-a6a3-772f-7559-4a22f0614589');
INSERT INTO `role_menu_button` VALUES (94, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc', '39f08daf-a6b0-0ea0-58c4-918941c668b2');
INSERT INTO `role_menu_button` VALUES (95, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc', '39f08daf-a6b9-a4ac-5953-a4e4074ef3b0');
INSERT INTO `role_menu_button` VALUES (96, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-a689-5e91-dce9-e6d0088d29dc', '39f08daf-a6cc-8c2f-e31a-9c91f6c6afce');
INSERT INTO `role_menu_button` VALUES (100, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-7355-6fb9-cfe6-b9804de226f9', '39f08daf-7379-0830-a724-272048d335c4');
INSERT INTO `role_menu_button` VALUES (101, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-e142-29fb-f4d4-4f8574d5d3b6', '39f08dae-e157-e486-3d68-06e5152d5e50');
INSERT INTO `role_menu_button` VALUES (102, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-46ba-04e4-a24b-346b81f8d936', '39f08dae-46fc-9606-cae1-2c04803de11f');
INSERT INTO `role_menu_button` VALUES (107, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-d22c-4988-0dd9-098fe905e5bd', '39f08dae-d23c-c202-2cc8-d4ac00483563');
INSERT INTO `role_menu_button` VALUES (108, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-d22c-4988-0dd9-098fe905e5bd', '39f08dae-d240-6179-e176-bd044f544712');
INSERT INTO `role_menu_button` VALUES (109, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-4489-44fd-b2ca-06acd10db080', '39f08daf-4498-7ee7-797c-a2ab7871b505');
INSERT INTO `role_menu_button` VALUES (111, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-4489-44fd-b2ca-06acd10db080', '39f08daf-44ad-ac06-083a-c5a44b8e99dc');
INSERT INTO `role_menu_button` VALUES (112, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-4489-44fd-b2ca-06acd10db080', '39f08daf-44ba-da99-4a81-8dcc78efcf82');
INSERT INTO `role_menu_button` VALUES (113, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-4489-44fd-b2ca-06acd10db080', '39f08daf-44c9-7fe4-defd-0939a64171cd');
INSERT INTO `role_menu_button` VALUES (114, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-4489-44fd-b2ca-06acd10db080', '39f08daf-44cd-0154-5dcd-f428972e22f2');
INSERT INTO `role_menu_button` VALUES (115, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-46ba-04e4-a24b-346b81f8d936', '39f08dae-46e6-f6f2-cbff-fe925a9d9946');
INSERT INTO `role_menu_button` VALUES (116, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-46ba-04e4-a24b-346b81f8d936', '39f08dae-46fc-9606-cae1-2c04803de11f');
INSERT INTO `role_menu_button` VALUES (117, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-46ba-04e4-a24b-346b81f8d936', '39f08dae-4709-666f-39e8-11cfb7db4594');
INSERT INTO `role_menu_button` VALUES (118, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5305-3dc4-ebe6-771d999a8e44');
INSERT INTO `role_menu_button` VALUES (119, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5315-b2cb-bd0a-65aaec059274');
INSERT INTO `role_menu_button` VALUES (120, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5323-c008-deeb-865f8b94be29');
INSERT INTO `role_menu_button` VALUES (121, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5327-e25c-ff40-0523a20e5d8c');
INSERT INTO `role_menu_button` VALUES (122, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-533a-604e-b0f7-a987eeb2f0de');
INSERT INTO `role_menu_button` VALUES (123, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-534a-fcdd-27dc-2f5b0f8eb80c');
INSERT INTO `role_menu_button` VALUES (124, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5358-b231-a8c5-0116ba761066');
INSERT INTO `role_menu_button` VALUES (125, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-5e15-ecf9-fd83-439ed212a310', '39f08dae-5e49-6a91-4661-55c6f645cc45');
INSERT INTO `role_menu_button` VALUES (126, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-8895-ce81-c6ec-723849dbc839');
INSERT INTO `role_menu_button` VALUES (127, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969', '39f08daf-8ff9-b2e7-f6ec-bbc690f7568a');
INSERT INTO `role_menu_button` VALUES (128, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969', '39f08daf-9020-8ebb-2d86-bd0adcd9d9dd');
INSERT INTO `role_menu_button` VALUES (129, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-8feb-d2ce-66dc-12fff451a969', '39f08daf-901a-ea1c-6dbc-fd3d68fb9757');
INSERT INTO `role_menu_button` VALUES (137, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08dae-d22c-4988-0dd9-098fe905e5bd', '39f08dae-d230-1374-d024-1123543b6a9d');
INSERT INTO `role_menu_button` VALUES (142, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-46ba-04e4-a24b-346b81f8d936', '39f08dae-46e6-f6f2-cbff-fe925a9d9946');
INSERT INTO `role_menu_button` VALUES (143, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-46ba-04e4-a24b-346b81f8d936', '39f08dae-46fc-9606-cae1-2c04803de11f');
INSERT INTO `role_menu_button` VALUES (144, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-46ba-04e4-a24b-346b81f8d936', '39f08dae-4709-666f-39e8-11cfb7db4594');
INSERT INTO `role_menu_button` VALUES (145, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5305-3dc4-ebe6-771d999a8e44');
INSERT INTO `role_menu_button` VALUES (146, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5315-b2cb-bd0a-65aaec059274');
INSERT INTO `role_menu_button` VALUES (147, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5323-c008-deeb-865f8b94be29');
INSERT INTO `role_menu_button` VALUES (148, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5327-e25c-ff40-0523a20e5d8c');
INSERT INTO `role_menu_button` VALUES (149, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-533a-604e-b0f7-a987eeb2f0de');
INSERT INTO `role_menu_button` VALUES (150, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-534a-fcdd-27dc-2f5b0f8eb80c');
INSERT INTO `role_menu_button` VALUES (151, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-52eb-5c6d-1b13-b4afaa0da821', '39f08dae-5358-b231-a8c5-0116ba761066');
INSERT INTO `role_menu_button` VALUES (152, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-5e15-ecf9-fd83-439ed212a310', '39f08dae-5e49-6a91-4661-55c6f645cc45');
INSERT INTO `role_menu_button` VALUES (153, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-886e-6a3e-9a33-4f95f1bed0ae');
INSERT INTO `role_menu_button` VALUES (154, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-887d-fda0-3bf1-e76c7c490364');
INSERT INTO `role_menu_button` VALUES (155, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-8886-697c-73d1-0ab76ecaf2b9');
INSERT INTO `role_menu_button` VALUES (156, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-8895-ce81-c6ec-723849dbc839');
INSERT INTO `role_menu_button` VALUES (157, '39f0d0d7-8372-3bbe-6959-740dbe81ade2', '39f08dae-8858-4e6a-b14d-9dc3537cb68b', '39f08dae-88a0-823c-9da9-9fbe6d546612');
INSERT INTO `role_menu_button` VALUES (159, '39f08d5b-173b-1cbb-7381-407c87b2e8e2', '39f08daf-4489-44fd-b2ca-06acd10db080', '39f08daf-44a9-d1bc-755d-c3369034e10a');
INSERT INTO `role_menu_button` VALUES (160, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08db3-ea1d-1eab-35a7-70bf9342e718', '39f08db3-ea6b-6af7-1ef5-a226af30fbe4');
INSERT INTO `role_menu_button` VALUES (161, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08db3-ea1d-1eab-35a7-70bf9342e718', '39f08db3-ea80-e0e3-69ea-af5ab431ac6f');
INSERT INTO `role_menu_button` VALUES (162, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08db3-ea1d-1eab-35a7-70bf9342e718', '39f08db3-eaa7-e42f-34fb-25baaaf55e87');
INSERT INTO `role_menu_button` VALUES (163, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08db3-ea1d-1eab-35a7-70bf9342e718', '39f08db3-eada-892b-b250-c2d58279d388');
INSERT INTO `role_menu_button` VALUES (164, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08daf-f8f6-2dad-42c0-c73c787c6def', '39f08daf-f905-e02f-341a-bbc0b80cbf5e');
INSERT INTO `role_menu_button` VALUES (165, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08daf-f8f6-2dad-42c0-c73c787c6def', '39f08daf-f909-628c-9a0a-2c23a57b9103');
INSERT INTO `role_menu_button` VALUES (166, '39f0d072-c8bc-598c-774a-595eddb928db', '39f08daf-f8f6-2dad-42c0-c73c787c6def', '39f08daf-f915-e1f1-d7d8-ebd21adbebe6');

SET FOREIGN_KEY_CHECKS = 1;
