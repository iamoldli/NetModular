/*
 Navicat Premium Data Transfer

 Source Server         : NetModular
 Source Server Type    : MySQL
 Source Server Version : 80016
 Source Host           : localhost:3306
 Source Schema         : nm_admin

 Target Server Type    : MySQL
 Target Server Version : 80016
 File Encoding         : 65001

 Date: 19/06/2019 16:20:10
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for account
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Type` int(255) NOT NULL DEFAULT 0 COMMENT '类型',
  `UserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户名',
  `Password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '密码',
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '手机号',
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '邮箱',
  `LoginTime` datetime(0) NOT NULL COMMENT '最后登录时间',
  `LoginIP` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '最后登录IP',
  `Status` smallint(6) NOT NULL COMMENT '状态：0、未激活 1、正常 2、禁用 3、注销',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '最后修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '最后修改人',
  `ClosedTime` datetime(0) NOT NULL COMMENT '注销时间',
  `ClosedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '注销人',
  `Deleted` tinyint(4) NOT NULL COMMENT '已删除',
  `DeletedTime` datetime(0) NOT NULL COMMENT '删除时间',
  `DeletedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '删除人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account
-- ----------------------------
INSERT INTO `account` VALUES ('2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', 0, 'admin', 'E739279CB28CDAFD7373618313803524', '管理员', '', '', '2019-06-17 08:55:39', '0.0.0.1', 1, '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-04-18 17:30:57', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', 0, '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for account_role
-- ----------------------------
DROP TABLE IF EXISTS `account_role`;
CREATE TABLE `account_role`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '账户编号',
  `RoleId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色编号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account_role
-- ----------------------------
INSERT INTO `account_role` VALUES (2, '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D');

-- ----------------------------
-- Table structure for auditinfo
-- ----------------------------
DROP TABLE IF EXISTS `auditinfo`;
CREATE TABLE `auditinfo`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `AccountId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '账户编号',
  `Area` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '区域',
  `Controller` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '控制器',
  `Action` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '操作',
  `Parameters` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '参数',
  `Result` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '返回数据',
  `ExecutionTime` datetime(0) NOT NULL COMMENT '方法开始执行时间',
  `ExecutionDuration` bigint(20) NOT NULL COMMENT '方法执行总用时(ms)',
  `BrowserInfo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '浏览器信息',
  `Platform` smallint(6) NOT NULL COMMENT '平台',
  `IP` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for button
-- ----------------------------
DROP TABLE IF EXISTS `button`;
CREATE TABLE `button`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `MenuId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '菜单编号',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Icon` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图标',
  `Code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '按钮编码',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建日期',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of button
-- ----------------------------
INSERT INTO `button` VALUES ('02AA62D0-2944-EABE-ED49-39ED2238C652', '64BB88F0-AE8D-EFF9-C537-39ED22096F9B', '编辑', NULL, 'admin_account_edit', '2019-04-12 13:36:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 13:36:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('13EE6243-46B6-AE86-A1AA-39ED22360378', 'E38B0E46-A755-FF0F-012C-39ED2209462A', '添加', NULL, 'admin_role_add', '2019-04-12 13:33:18', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 13:33:18', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('161E3E65-A02E-8A59-8C3E-39EDD64A36E9', 'F2155B1A-D222-8786-E9D7-39EDD6492774', '添加', 'add', 'codegenerator_enum_add', '2019-05-17 12:47:00', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:47:00', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('181F6B49-4E36-8620-DC51-39ED220DF287', '0404F457-1C32-BA61-8FAD-39ED2208BCD9', '删除', NULL, 'admin_moduleinfo_del', '2019-04-12 12:49:32', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 12:49:32', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('1AE9F5E5-B972-375F-D680-39EDD64A3703', 'F2155B1A-D222-8786-E9D7-39EDD6492774', '编辑', NULL, 'codegenerator_enum_edit', '2019-05-17 12:47:00', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:47:00', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('1FC9841E-CCBA-4B85-41C6-39ED2213BE42', 'A9105CA0-722C-66F5-844B-39ED220926A0', '删除', '', 'admin_menu_del', '2019-04-12 12:55:52', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:05:21', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('318C6F27-604E-AFC5-22BF-39EDD64D6330', '90FE5CAC-952B-8919-59BE-39EDD648FAB5', '删除', NULL, 'codegenerator_project_del', '2019-05-17 12:50:28', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:50:28', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('39ee389d-2c10-4e60-1cc8-2bc3ec58ee13', '39ee3894-ab04-421c-a1f2-2abad6b24ca4', '添加', 'add', 'common_area_add', '2019-06-05 15:00:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-05 15:00:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee389d-2c46-d0db-4f3a-85eb9afa97eb', '39ee3894-ab04-421c-a1f2-2abad6b24ca4', '编辑', 'edit', 'common_area_edit', '2019-06-05 15:00:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-05 15:00:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee389d-2c4c-017a-e05b-f26b5c6ad6de', '39ee3894-ab04-421c-a1f2-2abad6b24ca4', '删除', 'delete', 'common_area_del', '2019-06-05 15:00:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-05 15:00:24', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee612f-a3c7-e53d-57c1-c8fce74e822b', '39ee612f-4910-0fc0-09d0-209d33fb74d0', '添加', 'add', 'personnelfiles_company_add', '2019-06-13 12:05:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:05:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee612f-a3ce-2755-58b5-9648d1051206', '39ee612f-4910-0fc0-09d0-209d33fb74d0', '编辑', 'edit', 'personnelfiles_company_edit', '2019-06-13 12:05:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:05:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee612f-a3d2-e549-66c1-46b36445dd2d', '39ee612f-4910-0fc0-09d0-209d33fb74d0', '删除', 'delete', 'personnelfiles_company_del', '2019-06-13 12:05:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:05:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee6131-c1f3-ed2b-70dd-030156cb2a67', '39ee6130-95cf-a276-2d63-abfad9b4c184', '添加', 'add', 'personnelfiles_department_add', '2019-06-13 12:07:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 14:11:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee6131-c1f6-06e1-803d-9e2a1f99b7e6', '39ee6130-95cf-a276-2d63-abfad9b4c184', '编辑', 'edit', 'personnelfiles_department_edit', '2019-06-13 12:07:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 14:11:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee6131-c1fc-8643-0b34-c01aa4db14f8', '39ee6130-95cf-a276-2d63-abfad9b4c184', '删除', 'delete', 'personnelfiles_department_del', '2019-06-13 12:07:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 14:11:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee66bb-5899-732f-a207-9e9f724b44ff', '39ee6130-95cf-a276-2d63-abfad9b4c184', '岗位', 'edit', 'personnelfiles_department_position', '2019-06-14 13:55:53', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 14:11:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee66c8-6c91-6a80-65cd-07be4b580101', '39ee6130-95cf-a276-2d63-abfad9b4c184', '岗位添加', 'add', 'personnelfiles_department_position_add', '2019-06-14 14:10:11', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 14:11:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee66c8-6c99-e097-ea62-6f7dc4ab46f7', '39ee6130-95cf-a276-2d63-abfad9b4c184', '岗位编辑', 'edit', 'personnelfiles_department_position_edit', '2019-06-14 14:10:11', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 14:11:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee66c8-6c9c-c5dc-ec91-31ce9e0060fe', '39ee6130-95cf-a276-2d63-abfad9b4c184', '岗位删除', 'delete', 'personnelfiles_department_position_del', '2019-06-14 14:10:11', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 14:11:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee6749-b559-330f-9801-fb96beabf49d', '39ee6131-1c10-5571-798e-b3f89501c151', '删除', 'delete', 'personnelfiles_position_del', '2019-06-14 16:31:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 16:31:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee6750-0088-44c0-ab18-ffd93a5c01c5', '39ee6131-5e71-db70-9458-33ab14dd32c3', '添加', 'add', 'personnelfiles_user_add', '2019-06-14 16:38:16', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 16:38:16', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee6750-00b9-c821-aa79-158304131906', '39ee6131-5e71-db70-9458-33ab14dd32c3', '编辑', 'edit', 'personnelfiles_user_edit', '2019-06-14 16:38:16', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 16:38:16', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ee6750-00bc-6b5b-c2ff-c9c5112be868', '39ee6131-5e71-db70-9458-33ab14dd32c3', '删除', 'delete', 'personnelfiles_user_del', '2019-06-14 16:38:16', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 16:38:16', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('40CCB97F-49B2-24AD-BFD7-39ED2238C653', '64BB88F0-AE8D-EFF9-C537-39ED22096F9B', '删除', NULL, 'admin_account_del', '2019-04-12 13:36:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 13:36:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('40F1BFDB-22BD-0DA1-3942-39ED220DF27F', '0404F457-1C32-BA61-8FAD-39ED2208BCD9', '同步', 'refresh', 'admin_moduleinfo_sync', '2019-04-12 12:49:32', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 12:49:32', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('577BEBF3-2408-9FA6-071A-39EDD64D6328', '90FE5CAC-952B-8919-59BE-39EDD648FAB5', '编辑', NULL, 'codegenerator_project_edit', '2019-05-17 12:50:28', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:50:28', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('62717E4F-D4DB-E8EC-2D4E-39EDD5B637BB', 'A9105CA0-722C-66F5-844B-39ED220926A0', '排序', NULL, 'admin_menu_sort', '2019-05-17 10:05:21', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:05:21', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('62C2CB73-EEC9-DEA5-C020-39ED223C4590', 'B09AAC3D-43B4-3327-9F05-39ED22098F00', '详情', NULL, 'admin_auditinfo_details', '2019-04-12 13:40:08', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 13:40:08', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('6309893F-188F-16DB-CA2E-39ED2213BE43', 'A9105CA0-722C-66F5-844B-39ED220926A0', '绑定权限', '', 'admin_menu_bind_permission', '2019-04-12 12:55:52', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:05:21', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('6443BABB-3005-3393-CFA3-39ED2236037E', 'E38B0E46-A755-FF0F-012C-39ED2209462A', '删除', NULL, 'admin_role_del', '2019-04-12 13:33:18', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 13:33:18', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('64A397A3-F185-F740-5FD8-39ED2213BE46', 'A9105CA0-722C-66F5-844B-39ED220926A0', '绑定按钮', '', 'admin_menu_bind_button', '2019-04-12 12:55:52', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:05:21', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('7823220C-7FAD-05CD-B3FF-39EDD64D6318', '90FE5CAC-952B-8919-59BE-39EDD648FAB5', '添加', 'add', 'codegenerator_project_add', '2019-05-17 12:50:28', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:50:28', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('7FCB3809-1893-BD91-CE50-39ED22360380', 'E38B0E46-A755-FF0F-012C-39ED2209462A', '绑定菜单', NULL, 'admin_role_bind_menu', '2019-04-12 13:33:18', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 13:33:18', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('8405FED8-E131-23AB-7740-39EDD64D6339', '90FE5CAC-952B-8919-59BE-39EDD648FAB5', '生成', NULL, 'codegenerator_project_build_code', '2019-05-17 12:50:28', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:50:28', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('97727921-F069-AFAB-CFF8-39ED2211C64D', 'A74578DA-7B3B-8A7C-E54C-39ED22090F48', '删除', NULL, 'admin_permission_del', '2019-04-12 12:53:43', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 12:53:43', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('99EB627E-7E79-B36A-1C3C-39ED2213BE3E', 'A9105CA0-722C-66F5-844B-39ED220926A0', '添加', '', 'admin_menu_add', '2019-04-12 12:55:52', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:05:21', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('A7D396FB-BC36-EE11-A857-39EDD64A370A', 'F2155B1A-D222-8786-E9D7-39EDD6492774', '删除', NULL, 'codegenerator_enum_del', '2019-05-17 12:47:00', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:47:00', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('A83923D1-7AA3-107B-C72D-39ED2238C64F', '64BB88F0-AE8D-EFF9-C537-39ED22096F9B', '添加', 'add', 'admin_account_add', '2019-04-12 13:36:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 13:36:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('B4EAAE9E-6D8D-4C28-F832-39ED2213BE41', 'A9105CA0-722C-66F5-844B-39ED220926A0', '编辑', '', 'admin_menu_edit', '2019-04-12 12:55:52', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:05:21', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('C8C65374-9DB0-98ED-9C41-39ED2236037B', 'E38B0E46-A755-FF0F-012C-39ED2209462A', '编辑', NULL, 'admin_role_edit', '2019-04-12 13:33:18', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 13:33:18', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('EF881C51-DE63-8572-250B-39ED2211C64A', 'A74578DA-7B3B-8A7C-E54C-39ED22090F48', '同步', 'refresh', 'admin_permission_sync', '2019-04-12 12:53:43', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 12:53:43', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `button` VALUES ('F92EE62D-1499-C296-E25C-39ED2238C655', '64BB88F0-AE8D-EFF9-C537-39ED22096F9B', '重置密码', NULL, 'admin_account_reset_password', '2019-04-12 13:36:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 13:36:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');

-- ----------------------------
-- Table structure for button_permission
-- ----------------------------
DROP TABLE IF EXISTS `button_permission`;
CREATE TABLE `button_permission`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ButtonId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '按钮编号',
  `PermissionId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '权限编号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 134 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of button_permission
-- ----------------------------
INSERT INTO `button_permission` VALUES (1, '40F1BFDB-22BD-0DA1-3942-39ED220DF27F', '70581745-E062-DCD0-C19C-39ED221095C5');
INSERT INTO `button_permission` VALUES (2, '181F6B49-4E36-8620-DC51-39ED220DF287', '753BC64A-A40C-E4C4-698D-39ED221095CC');
INSERT INTO `button_permission` VALUES (3, 'EF881C51-DE63-8572-250B-39ED2211C64A', 'B93930AE-B840-D1A1-B19E-39ED221095D5');
INSERT INTO `button_permission` VALUES (4, '97727921-F069-AFAB-CFF8-39ED2211C64D', '453FEF51-1D67-3C8D-10B5-39ED221095D6');
INSERT INTO `button_permission` VALUES (5, '99EB627E-7E79-B36A-1C3C-39ED2213BE3E', '6528A89E-55E0-55E3-10AE-39ED221095AD');
INSERT INTO `button_permission` VALUES (6, '99EB627E-7E79-B36A-1C3C-39ED2213BE3E', 'F9A16174-575B-6C3B-3F9D-39ED221095D1');
INSERT INTO `button_permission` VALUES (7, 'B4EAAE9E-6D8D-4C28-F832-39ED2213BE41', 'F9A16174-575B-6C3B-3F9D-39ED221095D1');
INSERT INTO `button_permission` VALUES (8, 'B4EAAE9E-6D8D-4C28-F832-39ED2213BE41', 'C1580441-4CC5-6733-E9DF-39ED221095B3');
INSERT INTO `button_permission` VALUES (9, 'B4EAAE9E-6D8D-4C28-F832-39ED2213BE41', '63237D43-54F2-5E06-2CEA-39ED221095B4');
INSERT INTO `button_permission` VALUES (10, '1FC9841E-CCBA-4B85-41C6-39ED2213BE42', 'F3959EB5-2CC2-AED6-67FC-39ED221095B0');
INSERT INTO `button_permission` VALUES (23, '6309893F-188F-16DB-CA2E-39ED2213BE43', 'F9A16174-575B-6C3B-3F9D-39ED221095D1');
INSERT INTO `button_permission` VALUES (24, '6309893F-188F-16DB-CA2E-39ED2213BE43', '2F075121-FF29-B379-88E0-39ED221095FE');
INSERT INTO `button_permission` VALUES (25, '6309893F-188F-16DB-CA2E-39ED2213BE43', 'C1699E04-4AD0-DF03-B375-39ED22109602');
INSERT INTO `button_permission` VALUES (26, '6309893F-188F-16DB-CA2E-39ED2213BE43', '369CA0D5-73B1-5DFF-0306-39ED221095D3');
INSERT INTO `button_permission` VALUES (27, '6309893F-188F-16DB-CA2E-39ED2213BE43', 'BD77531E-9146-AC03-B2D0-39ED221095C1');
INSERT INTO `button_permission` VALUES (28, '6309893F-188F-16DB-CA2E-39ED2213BE43', '3DE2E2DD-2E1A-BBCA-9162-39ED221095BF');
INSERT INTO `button_permission` VALUES (34, '64A397A3-F185-F740-5FD8-39ED2213BE46', 'C26BB465-8C89-51F9-A71F-39ED22109597');
INSERT INTO `button_permission` VALUES (35, '64A397A3-F185-F740-5FD8-39ED2213BE46', '131F5777-26DA-9A17-15B6-39ED2210959D');
INSERT INTO `button_permission` VALUES (36, '64A397A3-F185-F740-5FD8-39ED2213BE46', 'EB48579B-94A3-2DC8-AE5E-39ED22109593');
INSERT INTO `button_permission` VALUES (37, '64A397A3-F185-F740-5FD8-39ED2213BE46', '40A6A7CB-C7F2-3065-7640-39ED2210959F');
INSERT INTO `button_permission` VALUES (38, '64A397A3-F185-F740-5FD8-39ED2213BE46', 'CFD7E4EA-2592-3B9A-6780-39ED221095A1');
INSERT INTO `button_permission` VALUES (39, '64A397A3-F185-F740-5FD8-39ED2213BE46', '369CA0D5-73B1-5DFF-0306-39ED221095D3');
INSERT INTO `button_permission` VALUES (40, '64A397A3-F185-F740-5FD8-39ED2213BE46', 'F9A16174-575B-6C3B-3F9D-39ED221095D1');
INSERT INTO `button_permission` VALUES (41, '64A397A3-F185-F740-5FD8-39ED2213BE46', '2F075121-FF29-B379-88E0-39ED221095FE');
INSERT INTO `button_permission` VALUES (42, '64A397A3-F185-F740-5FD8-39ED2213BE46', 'C1699E04-4AD0-DF03-B375-39ED22109602');
INSERT INTO `button_permission` VALUES (43, '13EE6243-46B6-AE86-A1AA-39ED22360378', '21CB159F-DDDD-9AAE-90EE-39ED221095E1');
INSERT INTO `button_permission` VALUES (44, 'C8C65374-9DB0-98ED-9C41-39ED2236037B', '6E4BD745-6ADD-D321-03A3-39ED221095E4');
INSERT INTO `button_permission` VALUES (45, 'C8C65374-9DB0-98ED-9C41-39ED2236037B', '941B90CA-7BF7-FA66-21F4-39ED221095E5');
INSERT INTO `button_permission` VALUES (46, '6443BABB-3005-3393-CFA3-39ED2236037E', 'DAD12ACD-CC26-AC17-4FF6-39ED221095E3');
INSERT INTO `button_permission` VALUES (47, '7FCB3809-1893-BD91-CE50-39ED22360380', 'DC464A46-960C-DD10-0713-39ED221095E7');
INSERT INTO `button_permission` VALUES (48, '7FCB3809-1893-BD91-CE50-39ED22360380', '4B6EE44F-89F4-B657-2887-39ED221095ED');
INSERT INTO `button_permission` VALUES (49, '7FCB3809-1893-BD91-CE50-39ED22360380', '46F7F154-2B89-3F80-CC18-39ED221095F1');
INSERT INTO `button_permission` VALUES (50, '7FCB3809-1893-BD91-CE50-39ED22360380', 'E1F86178-2204-72BD-8C55-39ED221095F3');
INSERT INTO `button_permission` VALUES (51, '7FCB3809-1893-BD91-CE50-39ED22360380', '21194FEB-9C9F-3FC6-A27B-39ED221095A3');
INSERT INTO `button_permission` VALUES (52, '7FCB3809-1893-BD91-CE50-39ED22360380', '3C8918F7-15CC-8493-F61B-39ED221095C2');
INSERT INTO `button_permission` VALUES (54, '02AA62D0-2944-EABE-ED49-39ED2238C652', '2BAA7B7D-94A9-9F13-D009-39ED22109581');
INSERT INTO `button_permission` VALUES (55, '02AA62D0-2944-EABE-ED49-39ED2238C652', '60DE5DBA-7E79-658E-8A5F-39ED22109583');
INSERT INTO `button_permission` VALUES (56, '40CCB97F-49B2-24AD-BFD7-39ED2238C653', 'D4BD9D65-8540-9463-762F-39ED22109589');
INSERT INTO `button_permission` VALUES (57, 'F92EE62D-1499-C296-E25C-39ED2238C655', '5E13BBC7-40C0-78F0-9700-39ED2210958E');
INSERT INTO `button_permission` VALUES (58, '62C2CB73-EEC9-DEA5-C020-39ED223C4590', 'F6B17ECC-900F-0547-F1E6-39ED22109591');
INSERT INTO `button_permission` VALUES (59, 'A83923D1-7AA3-107B-C72D-39ED2238C64F', '479A4F27-08DB-C255-B993-39ED22109580');
INSERT INTO `button_permission` VALUES (60, 'A83923D1-7AA3-107B-C72D-39ED2238C64F', '024C22B5-DAC2-F91C-33D3-39ED221095F5');
INSERT INTO `button_permission` VALUES (78, '62717E4F-D4DB-E8EC-2D4E-39EDD5B637BB', '336CF783-A167-8AA0-5065-39EDD5B5EEE0');
INSERT INTO `button_permission` VALUES (79, '62717E4F-D4DB-E8EC-2D4E-39EDD5B637BB', 'BA54D018-0EB1-0683-8469-39EDD5B5EEBC');
INSERT INTO `button_permission` VALUES (80, '161E3E65-A02E-8A59-8C3E-39EDD64A36E9', '7E9A9372-B299-9F8E-155B-39EDD646F21F');
INSERT INTO `button_permission` VALUES (81, '1AE9F5E5-B972-375F-D680-39EDD64A3703', 'AC3C8201-D7D7-46D4-121B-39EDD646F22B');
INSERT INTO `button_permission` VALUES (82, '1AE9F5E5-B972-375F-D680-39EDD64A3703', 'DD546001-8F04-1EC4-8515-39EDD646F235');
INSERT INTO `button_permission` VALUES (83, 'A7D396FB-BC36-EE11-A857-39EDD64A370A', '7BD7741B-3653-BBA9-C13F-39EDD646F225');
INSERT INTO `button_permission` VALUES (84, '7823220C-7FAD-05CD-B3FF-39EDD64D6318', '103AD30B-F5CB-EC6B-2AAA-39EDD646F2C7');
INSERT INTO `button_permission` VALUES (85, '577BEBF3-2408-9FA6-071A-39EDD64D6328', 'A474FF49-B640-461A-E6FD-39EDD646F2D7');
INSERT INTO `button_permission` VALUES (86, '577BEBF3-2408-9FA6-071A-39EDD64D6328', '935B0BA6-4396-60EE-2AE7-39EDD646F2DB');
INSERT INTO `button_permission` VALUES (87, '318C6F27-604E-AFC5-22BF-39EDD64D6330', '938F6B86-B398-EED9-970C-39EDD646F2D0');
INSERT INTO `button_permission` VALUES (88, '8405FED8-E131-23AB-7740-39EDD64D6339', 'CDA73BA5-0ACA-C003-9E2E-39EDD64E7949');
INSERT INTO `button_permission` VALUES (106, '39ee389d-2c4f-e08c-eee2-872eeb58551c', '39ee3895-3f19-9ada-8848-87979b8c4ed5');
INSERT INTO `button_permission` VALUES (107, '39ee389d-2c10-4e60-1cc8-2bc3ec58ee13', '39ee3895-3ef0-a3a9-00a1-cfc9e8dc5467');
INSERT INTO `button_permission` VALUES (109, '39ee389d-2c4c-017a-e05b-f26b5c6ad6de', '39ee3895-3ef8-b047-2be4-ea2c45dce1a8');
INSERT INTO `button_permission` VALUES (110, '39ee389d-2c46-d0db-4f3a-85eb9afa97eb', '39ee3895-3f03-c7ee-5dff-2496ed3de5f4');
INSERT INTO `button_permission` VALUES (111, '39ee389d-2c46-d0db-4f3a-85eb9afa97eb', '39ee3895-3f0f-9a9d-2966-044a39d4d5de');
INSERT INTO `button_permission` VALUES (112, '39ee612f-a3c7-e53d-57c1-c8fce74e822b', '39ee6103-8b86-2761-42a8-cdcb5089848c');
INSERT INTO `button_permission` VALUES (113, '39ee612f-a3ce-2755-58b5-9648d1051206', '39ee6103-8b98-e168-c8a4-7fa9b96121d9');
INSERT INTO `button_permission` VALUES (114, '39ee612f-a3ce-2755-58b5-9648d1051206', '39ee6103-8b9f-7ca5-ef8f-954dd9be59c3');
INSERT INTO `button_permission` VALUES (115, '39ee612f-a3d2-e549-66c1-46b36445dd2d', '39ee6103-8b90-f3e3-0a36-0fb3ea8e326b');
INSERT INTO `button_permission` VALUES (116, '39ee6131-c1f3-ed2b-70dd-030156cb2a67', '39ee6103-8bae-16d9-a9e9-9a2b131638c8');
INSERT INTO `button_permission` VALUES (117, '39ee6131-c1f6-06e1-803d-9e2a1f99b7e6', '39ee6103-8bc2-2623-be91-5727e62dbe74');
INSERT INTO `button_permission` VALUES (118, '39ee6131-c1f6-06e1-803d-9e2a1f99b7e6', '39ee6103-8bc9-d41d-bdec-78b92d4b94b9');
INSERT INTO `button_permission` VALUES (119, '39ee6131-c1fc-8643-0b34-c01aa4db14f8', '39ee6103-8bb8-1ec4-e868-c2fdbc56cdd5');
INSERT INTO `button_permission` VALUES (120, '39ee66c8-6c91-6a80-65cd-07be4b580101', '39ee6103-8bda-0b6a-6f5b-815b6a3ab3ed');
INSERT INTO `button_permission` VALUES (121, '39ee66bb-5899-732f-a207-9e9f724b44ff', '39ee6103-8bd1-cf92-53e4-1e587e54e241');
INSERT INTO `button_permission` VALUES (122, '39ee66c8-6c99-e097-ea62-6f7dc4ab46f7', '39ee6103-8bec-6c72-f8a7-4c7dbd21f5ba');
INSERT INTO `button_permission` VALUES (123, '39ee66c8-6c99-e097-ea62-6f7dc4ab46f7', '39ee6103-8bf2-2f6f-01e0-c0ab6bacf6f3');
INSERT INTO `button_permission` VALUES (124, '39ee66c8-6c9c-c5dc-ec91-31ce9e0060fe', '39ee6103-8be2-050c-8631-2e39e6acced9');
INSERT INTO `button_permission` VALUES (125, '39ee6749-b559-330f-9801-fb96beabf49d', '39ee6103-8be2-050c-8631-2e39e6acced9');
INSERT INTO `button_permission` VALUES (129, '39ee6750-00bc-6b5b-c2ff-c9c5112be868', '39ee6103-8c3d-64a8-ba17-7507f9d16034');
INSERT INTO `button_permission` VALUES (130, '39ee6750-0088-44c0-ab18-ffd93a5c01c5', '39ee6103-8c33-8f1b-aad3-a316b3490d0a');
INSERT INTO `button_permission` VALUES (131, '39ee6750-0088-44c0-ab18-ffd93a5c01c5', '39ee6766-3f23-c539-3514-7f5609b369c6');
INSERT INTO `button_permission` VALUES (132, '39ee6750-00b9-c821-aa79-158304131906', '39ee6103-8c44-c0e7-a883-1ec0d8a52985');
INSERT INTO `button_permission` VALUES (133, '39ee6750-00b9-c821-aa79-158304131906', '39ee6103-8c4d-970f-b61c-e6637ac092ee');
INSERT INTO `button_permission` VALUES (134, '39ee6750-00b9-c821-aa79-158304131906', '39ee6766-3f23-c539-3514-7f5609b369c6');

-- ----------------------------
-- Table structure for config
-- ----------------------------
DROP TABLE IF EXISTS `config`;
CREATE TABLE `config`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Key` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '键',
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '值',
  `Remarks` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `CreatedTime` datetime(0) NOT NULL COMMENT '添加时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '添加人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of config
-- ----------------------------
INSERT INTO `config` VALUES (1, 'sys_button_permission', 'True', '启用按钮权限', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (2, 'sys_logo', 'Admin\\Logo\\2019\\06\\14', '系统Logo', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (3, 'sys_auditing', 'False', '启用审计日志', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (4, 'sys_toolbar_skin', 'True', '显示工具栏皮肤按钮', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (5, 'sys_title', '通用权限管理系统', '系统标题', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (6, 'sys_toolbar_fullscreen', 'True', '显示工具栏全屏按钮', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (7, 'sys_home', '', '系统首页', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (8, 'sys_verify_code', 'False', '启用登录验证码功能', '2019-05-06 09:11:35', '39ED5AB3-0C91-A26C-2F8A-A3B723EF422A', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (9, 'sys_toolbar_userinfo', 'True', '显示工具栏用户信息按钮', '2019-05-06 09:35:48', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (10, 'sys_toolbar_logout', 'True', '显示工具栏退出按钮', '2019-05-06 09:35:48', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (11, 'sys_userinfo_page', '', '个人信息页', '2019-06-14 13:47:55', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:09:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for department
-- ----------------------------
DROP TABLE IF EXISTS `department`;
CREATE TABLE `department`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '部门名称',
  `ParentId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '父级节点',
  `Leader` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '负责人',
  `Sort` int(255) NOT NULL DEFAULT 0 COMMENT '排序',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建日期',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for menu
-- ----------------------------
DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ModuleCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '所属模块',
  `Type` smallint(6) NOT NULL COMMENT '类型，0、节点 1、链接',
  `ParentId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '父菜单',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `RouteName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '路由名称',
  `RouteParams` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '路由参数',
  `RouteQuery` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '路由参数',
  `Icon` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '图标',
  `IconColor` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图标颜色',
  `Url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '链接',
  `Level` int(11) NOT NULL COMMENT '等级',
  `Show` tinyint(4) NOT NULL COMMENT '是否显示',
  `Sort` int(11) NOT NULL COMMENT '排序',
  `Target` smallint(6) NOT NULL COMMENT '打开方式',
  `DialogWidth` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '对话框宽度',
  `DialogHeight` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '对话框高度',
  `DialogFullscreen` tinyint(4) NOT NULL COMMENT '对话框可全屏',
  `Remarks` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menu
-- ----------------------------
INSERT INTO `menu` VALUES ('0404F457-1C32-BA61-8FAD-39ED2208BCD9', 'Admin', 1, '214E8C1A-6A87-C214-7FF9-39ED21E7CC27', '模块管理', 'admin_moduleinfo', '', '', 'app', '', '', 1, 1, 0, 0, '', '', 1, '', '2019-04-12 12:43:50', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:09:12', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `menu` VALUES ('214E8C1A-6A87-C214-7FF9-39ED21E7CC27', '', 0, '00000000-0000-0000-0000-000000000000', '权限管理', '', '', '', 'verifycode', '', '', 0, 1, 1, -1, '', '', 1, '', '2019-04-12 12:07:52', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:04:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39edef2c-6153-c935-1660-4823c692e142', '', 2, '00000000-0000-0000-0000-000000000000', '前端框架文档', '', '', '', 'attachment', '', 'http://progqx5cu.bkt.clouddn.com/skins/index.html', 0, 1, 4, 0, '', '', 1, '', '2019-05-22 08:44:55', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:04:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ee3891-d1bb-878e-698b-b2a9911c76e9', '', 0, '00000000-0000-0000-0000-000000000000', '通用模块', '', '', '', 'table', '', '', 0, 1, 2, -1, '', '', 1, '', '2019-06-05 14:48:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:04:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ee3894-ab04-421c-a1f2-2abad6b24ca4', 'Common', 1, '39ee3891-d1bb-878e-698b-b2a9911c76e9', '区划代码列表', 'Common_Area', '', '', 'app', '', '', 1, 1, 0, 0, '', '', 1, '', '2019-06-05 14:51:07', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-05 14:51:07', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ee5d50-9629-1e88-d03f-d74cf8497049', 'Common', 1, '39ee3891-d1bb-878e-698b-b2a9911c76e9', '区划代码组件示例', 'Common_Area_Demo', '', '', 'log', '', '', 1, 1, 1, 0, '', '', 1, '', '2019-06-12 18:02:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-12 18:02:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ee612e-ce4a-a2a7-8b18-71245ad6de18', '', 0, '00000000-0000-0000-0000-000000000000', '人事档案', '', '', '', 'basic-data', '', '', 0, 1, 0, -1, '', '', 1, '', '2019-06-13 12:04:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:04:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ee612f-4910-0fc0-09d0-209d33fb74d0', 'PersonnelFiles', 1, '39ee612e-ce4a-a2a7-8b18-71245ad6de18', '公司单位列表', 'PersonnelFiles_Company', '', '', 'enterprise', '', '', 1, 1, 0, 0, '', '', 1, '', '2019-06-13 12:04:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 13:38:46', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ee6130-95cf-a276-2d63-abfad9b4c184', 'PersonnelFiles', 1, '39ee612e-ce4a-a2a7-8b18-71245ad6de18', '部门列表', 'PersonnelFiles_Department', '', '', 'role', '', '', 1, 1, 1, 0, '', '', 1, '', '2019-06-13 12:06:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:06:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ee6131-1c10-5571-798e-b3f89501c151', 'PersonnelFiles', 1, '39ee612e-ce4a-a2a7-8b18-71245ad6de18', '岗位列表', 'PersonnelFiles_Position', '', '', 'tag', '', '', 1, 1, 1, 0, '', '', 1, '', '2019-06-13 12:06:48', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:06:48', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ee6131-5e71-db70-9458-33ab14dd32c3', 'PersonnelFiles', 1, '39ee612e-ce4a-a2a7-8b18-71245ad6de18', '用户信息列表', 'PersonnelFiles_User', '', '', 'user', '', '', 1, 1, 3, 0, '', '', 1, '', '2019-06-13 12:07:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:07:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('601D5926-CF2A-EA30-B3CC-39EDD647ECD8', '', 0, '00000000-0000-0000-0000-000000000000', '开发工具', '', '', '', 'develop', '', '', 0, 1, 3, -1, '', '', 1, '', '2019-05-17 12:44:30', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 12:04:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('64BB88F0-AE8D-EFF9-C537-39ED22096F9B', 'Admin', 1, '214E8C1A-6A87-C214-7FF9-39ED21E7CC27', '账户管理', 'admin_account', '', '', 'user', '', '', 1, 1, 4, 0, '', '', 1, '', '2019-04-12 12:44:36', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:09:12', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `menu` VALUES ('7F9DCB98-20B2-3556-A850-39ED2209AB84', 'Admin', 1, '214E8C1A-6A87-C214-7FF9-39ED21E7CC27', '系统配置', 'admin_system_config', '', '', 'system', '', '', 1, 1, 6, 0, '', '', 1, '', '2019-04-12 12:44:51', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:09:12', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `menu` VALUES ('90FE5CAC-952B-8919-59BE-39EDD648FAB5', 'CodeGenerator', 1, 'ACA98589-C672-4C78-8831-39EDD648ACBB', '项目列表', 'codegenerator_project', '', '', 'project', '', '', 2, 1, 0, 0, '', '', 1, '', '2019-05-17 12:45:39', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:45:39', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `menu` VALUES ('A74578DA-7B3B-8A7C-E54C-39ED22090F48', 'Admin', 1, '214E8C1A-6A87-C214-7FF9-39ED21E7CC27', '权限管理', 'admin_permission', '', '', 'permission', '', '', 1, 1, 1, 0, '', '', 1, '', '2019-04-12 12:44:11', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:09:12', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `menu` VALUES ('A9105CA0-722C-66F5-844B-39ED220926A0', 'Admin', 1, '214E8C1A-6A87-C214-7FF9-39ED21E7CC27', '菜单管理', 'admin_menu', '', '', 'menu', '', '', 1, 1, 2, 0, '', '', 1, '', '2019-04-12 12:44:17', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:09:12', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `menu` VALUES ('ACA98589-C672-4C78-8831-39EDD648ACBB', '', 0, '601D5926-CF2A-EA30-B3CC-39EDD647ECD8', '代码生成器', '', '', '', 'develop', '', '', 1, 1, 0, -1, '', '', 1, '', '2019-05-17 12:45:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:45:19', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `menu` VALUES ('B09AAC3D-43B4-3327-9F05-39ED22098F00', 'Admin', 1, '214E8C1A-6A87-C214-7FF9-39ED21E7CC27', '审计日志', 'admin_auditinfo', '', '', 'log', '', '', 1, 1, 5, 0, '', '', 1, '', '2019-04-12 12:44:44', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:09:12', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `menu` VALUES ('E38B0E46-A755-FF0F-012C-39ED2209462A', 'Admin', 1, '214E8C1A-6A87-C214-7FF9-39ED21E7CC27', '角色管理', 'admin_role', '', '', 'role', '', '', 1, 1, 3, 0, '', '', 1, '', '2019-04-12 12:44:26', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:09:12', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `menu` VALUES ('F2155B1A-D222-8786-E9D7-39EDD6492774', 'CodeGenerator', 1, 'ACA98589-C672-4C78-8831-39EDD648ACBB', '枚举列表', 'codegenerator_enum', '', '', 'tag', '', '', 2, 1, 0, 0, '', '', 1, '', '2019-05-17 12:45:51', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 12:45:51', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');

-- ----------------------------
-- Table structure for menu_permission
-- ----------------------------
DROP TABLE IF EXISTS `menu_permission`;
CREATE TABLE `menu_permission`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MenuId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '菜单编号',
  `PermissionId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '权限编号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 259 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menu_permission
-- ----------------------------
INSERT INTO `menu_permission` VALUES (1, '0404F457-1C32-BA61-8FAD-39ED2208BCD9', '0C882026-3B09-A715-698E-39ED221095C4');
INSERT INTO `menu_permission` VALUES (2, 'A74578DA-7B3B-8A7C-E54C-39ED22090F48', '369CA0D5-73B1-5DFF-0306-39ED221095D3');
INSERT INTO `menu_permission` VALUES (5, 'E38B0E46-A755-FF0F-012C-39ED2209462A', 'D533353C-A241-C4F5-82BD-39ED221095DD');
INSERT INTO `menu_permission` VALUES (6, '64BB88F0-AE8D-EFF9-C537-39ED22096F9B', '4888619B-5D4A-6430-AD3A-39ED2210957F');
INSERT INTO `menu_permission` VALUES (8, 'B09AAC3D-43B4-3327-9F05-39ED22098F00', '3D58A917-A9E6-8FB4-AEF0-39ED22109590');
INSERT INTO `menu_permission` VALUES (9, '7F9DCB98-20B2-3556-A850-39ED2209AB84', '07B2C882-D6F8-E251-E5EC-39ED221095F6');
INSERT INTO `menu_permission` VALUES (10, '7F9DCB98-20B2-3556-A850-39ED2209AB84', 'E8080A42-912A-23F7-474A-39ED221095F8');
INSERT INTO `menu_permission` VALUES (178, 'A9105CA0-722C-66F5-844B-39ED220926A0', '2073B60E-8AFD-4854-A206-39ED221095A4');
INSERT INTO `menu_permission` VALUES (179, 'A9105CA0-722C-66F5-844B-39ED220926A0', '21194FEB-9C9F-3FC6-A27B-39ED221095A3');
INSERT INTO `menu_permission` VALUES (180, 'A9105CA0-722C-66F5-844B-39ED220926A0', '79429274-CFCF-30F1-2B1E-39ED221095BA');
INSERT INTO `menu_permission` VALUES (181, 'F2155B1A-D222-8786-E9D7-39EDD6492774', '5A77EE39-3B26-088F-B0C4-39EDD646F216');
INSERT INTO `menu_permission` VALUES (182, 'F2155B1A-D222-8786-E9D7-39EDD6492774', 'DBE09A62-04EA-257F-4BBF-39EDD646F243');
INSERT INTO `menu_permission` VALUES (183, 'F2155B1A-D222-8786-E9D7-39EDD6492774', 'DE8FC51B-88E7-553B-7773-39EDD646F24C');
INSERT INTO `menu_permission` VALUES (184, 'F2155B1A-D222-8786-E9D7-39EDD6492774', 'EA79DEDB-099C-3132-92C4-39EDD646F252');
INSERT INTO `menu_permission` VALUES (185, 'F2155B1A-D222-8786-E9D7-39EDD6492774', 'AD112412-B385-A42E-9CB0-39EDD646F256');
INSERT INTO `menu_permission` VALUES (186, 'F2155B1A-D222-8786-E9D7-39EDD6492774', 'A8D12A71-6045-356A-56E1-39EDD646F25E');
INSERT INTO `menu_permission` VALUES (187, 'F2155B1A-D222-8786-E9D7-39EDD6492774', 'D327A892-61F1-5DBF-DA86-39EDD646F266');
INSERT INTO `menu_permission` VALUES (188, 'F2155B1A-D222-8786-E9D7-39EDD6492774', '821D172A-AADD-8516-CA9F-39EDD646F26E');
INSERT INTO `menu_permission` VALUES (219, '90fe5cac-952b-8919-59be-39edd648fab5', '70ea1fa1-1b84-c6b7-1c5b-39edd646f2be');
INSERT INTO `menu_permission` VALUES (220, '90fe5cac-952b-8919-59be-39edd648fab5', 'eec8c8d7-6f42-4cc3-1e27-39edd646f20f');
INSERT INTO `menu_permission` VALUES (221, '90fe5cac-952b-8919-59be-39edd648fab5', 'c0944193-d0c2-8040-91e5-39edd646f207');
INSERT INTO `menu_permission` VALUES (222, '90fe5cac-952b-8919-59be-39edd648fab5', '8563a927-bb00-0d98-5b42-39edd646f203');
INSERT INTO `menu_permission` VALUES (223, '90fe5cac-952b-8919-59be-39edd648fab5', '31f87f68-25d2-dd70-2821-39edd646f1fd');
INSERT INTO `menu_permission` VALUES (224, '90fe5cac-952b-8919-59be-39edd648fab5', '0a4360f5-7fde-6cf6-88f0-39edd646f1ef');
INSERT INTO `menu_permission` VALUES (225, '90fe5cac-952b-8919-59be-39edd648fab5', 'e181e108-3a6d-ebb5-8f53-39edd646f31d');
INSERT INTO `menu_permission` VALUES (226, '90fe5cac-952b-8919-59be-39edd648fab5', '92ba26a7-c033-58d3-6a10-39edd646f318');
INSERT INTO `menu_permission` VALUES (227, '90fe5cac-952b-8919-59be-39edd648fab5', '4e0ee58f-9bf6-14e4-0874-39edd646f312');
INSERT INTO `menu_permission` VALUES (228, '90fe5cac-952b-8919-59be-39edd648fab5', 'd7948bc2-c6d0-f501-8bf5-39edd646f30d');
INSERT INTO `menu_permission` VALUES (229, '90fe5cac-952b-8919-59be-39edd648fab5', 'cc325b48-b557-0635-d042-39edd646f306');
INSERT INTO `menu_permission` VALUES (230, '90fe5cac-952b-8919-59be-39edd648fab5', 'ed24644b-5857-c37f-4df3-39edd646f2fe');
INSERT INTO `menu_permission` VALUES (231, '90fe5cac-952b-8919-59be-39edd648fab5', '9ab67430-412e-0ca0-0635-39edd646f2f2');
INSERT INTO `menu_permission` VALUES (232, '90fe5cac-952b-8919-59be-39edd648fab5', '60dbf5ce-ff5f-88d6-ff9f-39edd646f2ee');
INSERT INTO `menu_permission` VALUES (233, '90fe5cac-952b-8919-59be-39edd648fab5', '3b70252c-0225-efdc-9f04-39edd646f2e8');
INSERT INTO `menu_permission` VALUES (234, '90fe5cac-952b-8919-59be-39edd648fab5', '837c460d-4a4b-c720-97ef-39edd646f2e1');
INSERT INTO `menu_permission` VALUES (235, '90fe5cac-952b-8919-59be-39edd648fab5', '45c2b872-d473-c1c7-43f0-39edd646f2b8');
INSERT INTO `menu_permission` VALUES (236, '90fe5cac-952b-8919-59be-39edd648fab5', 'db2e0a42-417d-98f3-9d03-39edd646f2b0');
INSERT INTO `menu_permission` VALUES (237, '90fe5cac-952b-8919-59be-39edd648fab5', '594a2ae3-56a8-d255-cb55-39edd646f2ab');
INSERT INTO `menu_permission` VALUES (238, '90fe5cac-952b-8919-59be-39edd648fab5', 'efbc6833-809e-9270-8898-39edd646f2a1');
INSERT INTO `menu_permission` VALUES (239, '90fe5cac-952b-8919-59be-39edd648fab5', '8dcec627-a20b-20ea-c4ef-39edd646f299');
INSERT INTO `menu_permission` VALUES (240, '90fe5cac-952b-8919-59be-39edd648fab5', 'ec812192-f175-6415-67e9-39edd646f292');
INSERT INTO `menu_permission` VALUES (241, '90fe5cac-952b-8919-59be-39edd648fab5', 'c87c85e6-e85e-ece1-cdb4-39edd646f28c');
INSERT INTO `menu_permission` VALUES (242, '90fe5cac-952b-8919-59be-39edd648fab5', '370d8236-6b26-a615-2d9f-39edd646f284');
INSERT INTO `menu_permission` VALUES (243, '90fe5cac-952b-8919-59be-39edd648fab5', 'e4d3cc7f-8017-3a34-d296-39edd646f27c');
INSERT INTO `menu_permission` VALUES (244, '90fe5cac-952b-8919-59be-39edd648fab5', 'e528d8ac-9704-607d-0d76-39edd646f275');
INSERT INTO `menu_permission` VALUES (245, '90fe5cac-952b-8919-59be-39edd648fab5', '0bbb7988-4e4b-073e-b94e-39edd646f23c');
INSERT INTO `menu_permission` VALUES (249, '39ee3894-ab04-421c-a1f2-2abad6b24ca4', '39ee3895-3ee8-6487-b58d-6dd60531970a');
INSERT INTO `menu_permission` VALUES (250, '39ee3894-ab04-421c-a1f2-2abad6b24ca4', '39ee5bf5-d134-5028-f3ea-45c65fa12650');
INSERT INTO `menu_permission` VALUES (251, '39ee612f-4910-0fc0-09d0-209d33fb74d0', '39ee6103-8b7a-113f-85de-0d1d35c29307');
INSERT INTO `menu_permission` VALUES (255, '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee6103-8ba9-7bef-ce60-4e193ae796fb');
INSERT INTO `menu_permission` VALUES (256, '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee61a6-6cf0-6e14-ec64-e96cba71437f');
INSERT INTO `menu_permission` VALUES (257, '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee61a6-6d0e-3101-1e30-f5f9fa6ab200');
INSERT INTO `menu_permission` VALUES (258, '39ee6131-1c10-5571-798e-b3f89501c151', '39ee6103-8bd1-cf92-53e4-1e587e54e241');
INSERT INTO `menu_permission` VALUES (259, '39ee6131-5e71-db70-9458-33ab14dd32c3', '39ee6103-8c2b-396a-7c15-7874fe0ccbfd');

-- ----------------------------
-- Table structure for moduleinfo
-- ----------------------------
DROP TABLE IF EXISTS `moduleinfo`;
CREATE TABLE `moduleinfo`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '编码',
  `Version` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '版本号',
  `Remarks` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '备注',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of moduleinfo
-- ----------------------------
INSERT INTO `moduleinfo` VALUES ('39ee3891-17c3-fbff-e0d9-7ccd77994e9a', '通用模块', 'Common', '1.0.0', '', '2019-06-05 14:47:13', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 11:16:10', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('39ee6102-bef8-38c8-e5bf-d99cf30ca5d5', '人事档案', 'PersonnelFiles', '1.0.0', NULL, '2019-06-13 11:16:10', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 11:16:10', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('4FE58AC7-506B-D100-C374-39EDD646CE70', '代码生成', 'CodeGenerator', '1.0.0', '', '2019-05-17 12:43:17', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-13 11:16:10', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('d4f5f231-f698-f224-d587-39ed9d56bf37', '权限管理', 'Admin', '1.0.0', '', '2019-05-06 11:22:20', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-13 11:16:10', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for permission
-- ----------------------------
DROP TABLE IF EXISTS `permission`;
CREATE TABLE `permission`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `ModuleCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '模块编码',
  `Controller` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '控制器',
  `Action` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '动作',
  `HttpMethod` smallint(6) NULL DEFAULT NULL COMMENT '请求方法',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of permission
-- ----------------------------
INSERT INTO `permission` VALUES ('024c22b5-dac2-f91c-33d3-39ed221095f5', '角色管理_下拉列表数据', 'Admin', 'Role', 'Select', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('07b2c882-d6f8-e251-e5ec-39ed221095f6', '系统_修改系统配置', 'Admin', 'System', 'Config', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('0a4360f5-7fde-6cf6-88f0-39edd646f1ef', '实体管理_查询', 'codegenerator', 'Class', 'Query', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('0bbb7988-4e4b-073e-b94e-39edd646f23c', '枚举管理_下拉列表', 'codegenerator', 'Enum', 'Select', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('0c882026-3b09-a715-698e-39ed221095c4', '模块信息_查询', 'Admin', 'ModuleInfo', 'Query', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('103ad30b-f5cb-ec6b-2aaa-39edd646f2c7', '项目管理_添加', 'codegenerator', 'Project', 'Add', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('131f5777-26da-9a17-15b6-39ed2210959d', '按钮管理_删除', 'Admin', 'Button', 'Delete', 4, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('16c7e2bc-a808-55d1-64d6-39ed22109572', '账户管理_绑定角色', 'Admin', 'Account', 'BindRole', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('2073b60e-8afd-4854-a206-39ed221095a4', '菜单管理_查询菜单列表', 'Admin', 'Menu', 'Query', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('21194feb-9c9f-3fc6-a27b-39ed221095a3', '菜单管理_菜单树', 'Admin', 'Menu', 'Tree', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('21cb159f-dddd-9aae-90ee-39ed221095e1', '角色管理_添加', 'Admin', 'Role', 'Add', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('2baa7b7d-94a9-9f13-d009-39ed22109581', '账户管理_编辑', 'Admin', 'Account', 'Edit', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('2f075121-ff29-b379-88e0-39ed221095fe', '系统_获取指定模块的Controller下拉列表', 'Admin', 'System', 'AllController', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('31f87f68-25d2-dd70-2821-39edd646f1fd', '实体管理_添加', 'codegenerator', 'Class', 'Add', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('336cf783-a167-8aa0-5065-39edd5b5eee0', '菜单管理_更新排序信息', 'Admin', 'Menu', 'Sort', 3, '2019-05-17 10:05:02', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('369ca0d5-73b1-5dff-0306-39ed221095d3', '权限接口_查询', 'Admin', 'Permission', 'Query', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('370d8236-6b26-a615-2d9f-39edd646f284', '模型属性管理_删除', 'codegenerator', 'ModelProperty', 'Delete', 4, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3ee8-6487-b58d-6dd60531970a', '区划代码管理_查询', 'common', 'Area', 'Query', 1, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3ef0-a3a9-00a1-cfc9e8dc5467', '区划代码管理_添加', 'common', 'Area', 'Add', 3, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3ef8-b047-2be4-ea2c45dce1a8', '区划代码管理_删除', 'common', 'Area', 'Delete', 4, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3f03-c7ee-5dff-2496ed3de5f4', '区划代码管理_编辑', 'common', 'Area', 'Edit', 1, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3f0f-9a9d-2966-044a39d4d5de', '区划代码管理_修改', 'common', 'Area', 'Update', 3, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3f19-9ada-8848-87979b8c4ed5', '区划代码管理_爬取数据', 'common', 'Area', 'Crawling', 3, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3f23-3247-35b0-61dc02dedbcf', '字典管理_查询', 'common', 'Dict', 'Query', 1, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3f2b-f57d-f420-e1c3735fa517', '字典管理_添加', 'common', 'Dict', 'Add', 3, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3f31-c102-287c-ae3516f807be', '字典管理_删除', 'common', 'Dict', 'Delete', 4, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3f43-a8c1-6b37-f0ec15bd05f4', '字典管理_编辑', 'common', 'Dict', 'Edit', 1, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee3895-3f4d-c48a-8400-d9996e4c3dad', '字典管理_修改', 'common', 'Dict', 'Update', 3, '2019-06-05 14:51:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee5bf5-d134-5028-f3ea-45c65fa12650', '区划代码管理_查询子节点', 'common', 'Area', 'QueryChildren', 1, '2019-06-12 11:43:55', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8b7a-113f-85de-0d1d35c29307', '公司单位管理_查询', 'PersonnelFiles', 'Company', 'Query', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8b86-2761-42a8-cdcb5089848c', '公司单位管理_添加', 'PersonnelFiles', 'Company', 'Add', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8b90-f3e3-0a36-0fb3ea8e326b', '公司单位管理_删除', 'PersonnelFiles', 'Company', 'Delete', 4, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8b98-e168-c8a4-7fa9b96121d9', '公司单位管理_编辑', 'PersonnelFiles', 'Company', 'Edit', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8b9f-7ca5-ef8f-954dd9be59c3', '公司单位管理_修改', 'PersonnelFiles', 'Company', 'Update', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8ba9-7bef-ce60-4e193ae796fb', '部门管理_查询', 'PersonnelFiles', 'Department', 'Query', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8bae-16d9-a9e9-9a2b131638c8', '部门管理_添加', 'PersonnelFiles', 'Department', 'Add', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8bb8-1ec4-e868-c2fdbc56cdd5', '部门管理_删除', 'PersonnelFiles', 'Department', 'Delete', 4, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8bc2-2623-be91-5727e62dbe74', '部门管理_编辑', 'PersonnelFiles', 'Department', 'Edit', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8bc9-d41d-bdec-78b92d4b94b9', '部门管理_修改', 'PersonnelFiles', 'Department', 'Update', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8bd1-cf92-53e4-1e587e54e241', '岗位管理_查询', 'PersonnelFiles', 'Position', 'Query', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8bda-0b6a-6f5b-815b6a3ab3ed', '岗位管理_添加', 'PersonnelFiles', 'Position', 'Add', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8be2-050c-8631-2e39e6acced9', '岗位管理_删除', 'PersonnelFiles', 'Position', 'Delete', 4, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8bec-6c72-f8a7-4c7dbd21f5ba', '岗位管理_编辑', 'PersonnelFiles', 'Position', 'Edit', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8bf2-2f6f-01e0-c0ab6bacf6f3', '岗位管理_修改', 'PersonnelFiles', 'Position', 'Update', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8bfe-0453-5cfb-1fe3b31849bf', '用户联系信息管理_查询', 'PersonnelFiles', 'UserContact', 'Query', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c08-9c91-a01c-1a9d025f98d1', '用户联系信息管理_添加', 'PersonnelFiles', 'UserContact', 'Add', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c11-e9b0-eedb-e9eb84668d78', '用户联系信息管理_删除', 'PersonnelFiles', 'UserContact', 'Delete', 4, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c1a-cd0c-f793-8fa155139b18', '用户联系信息管理_编辑', 'PersonnelFiles', 'UserContact', 'Edit', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c20-c611-37d0-a22b7dfb2a87', '用户联系信息管理_修改', 'PersonnelFiles', 'UserContact', 'Update', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c2b-396a-7c15-7874fe0ccbfd', '用户信息管理_查询', 'PersonnelFiles', 'User', 'Query', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c33-8f1b-aad3-a316b3490d0a', '用户信息管理_添加', 'PersonnelFiles', 'User', 'Add', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c3d-64a8-ba17-7507f9d16034', '用户信息管理_删除', 'PersonnelFiles', 'User', 'Delete', 4, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c44-c0e7-a883-1ec0d8a52985', '用户信息管理_编辑', 'PersonnelFiles', 'User', 'Edit', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c4d-970f-b61c-e6637ac092ee', '用户信息管理_修改', 'PersonnelFiles', 'User', 'Update', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c57-3bae-19f7-a4d713f24f36', '用户教育经历管理_查询', 'PersonnelFiles', 'UserEducationHistory', 'Query', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c5e-6eb7-0121-75168a1e8347', '用户教育经历管理_添加', 'PersonnelFiles', 'UserEducationHistory', 'Add', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c65-0cda-7b12-d3d8b0bb051d', '用户教育经历管理_删除', 'PersonnelFiles', 'UserEducationHistory', 'Delete', 4, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c75-56ab-9a33-1ba0c554046a', '用户教育经历管理_编辑', 'PersonnelFiles', 'UserEducationHistory', 'Edit', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c7d-040a-317b-40eabed00449', '用户教育经历管理_修改', 'PersonnelFiles', 'UserEducationHistory', 'Update', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c8e-1d6a-9914-7715d25abbbe', '用户工作经历管理_查询', 'PersonnelFiles', 'UserWorkHistory', 'Query', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8c97-5228-40f7-984fd36a304f', '用户工作经历管理_添加', 'PersonnelFiles', 'UserWorkHistory', 'Add', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8cb5-7594-c15a-58f833ff7a57', '用户工作经历管理_删除', 'PersonnelFiles', 'UserWorkHistory', 'Delete', 4, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8cc4-d9e7-b3ae-4c25b221144f', '用户工作经历管理_编辑', 'PersonnelFiles', 'UserWorkHistory', 'Edit', 1, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6103-8cd7-83c0-285d-37152658a660', '用户工作经历管理_修改', 'PersonnelFiles', 'UserWorkHistory', 'Update', 3, '2019-06-13 11:17:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee61a6-6cf0-6e14-ec64-e96cba71437f', '公司单位管理_下拉列表', 'PersonnelFiles', 'Company', 'Select', 1, '2019-06-13 14:14:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee61a6-6d0e-3101-1e30-f5f9fa6ab200', '部门管理_部门树', 'PersonnelFiles', 'Department', 'Tree', 1, '2019-06-13 14:14:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ee6766-3f23-c539-3514-7f5609b369c6', '用户信息管理_上传照片', 'PersonnelFiles', 'User', 'UploadPicture', 3, '2019-06-14 17:02:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('3b70252c-0225-efdc-9f04-39edd646f2e8', '实体属性管理_添加', 'codegenerator', 'Property', 'Add', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('3c8918f7-15cc-8493-f61b-39ed221095c2', '菜单管理_获取菜单的按钮列表', 'Admin', 'Menu', 'ButtonList', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('3d58a917-a9e6-8fb4-aef0-39ed22109590', '审计信息_查询', 'Admin', 'AuditInfo', 'Query', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('3de2e2dd-2e1a-bbca-9162-39ed221095bf', '菜单管理_获取菜单的权限列表', 'Admin', 'Menu', 'PermissionList', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('40a6a7cb-c7f2-3065-7640-39ed2210959f', '按钮管理_获取按钮绑定的权限列表', 'Admin', 'Button', 'PermissionList', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('453fef51-1d67-3c8d-10b5-39ed221095d6', '权限接口_删除', 'Admin', 'Permission', 'Delete', 4, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('45c2b872-d473-c1c7-43f0-39edd646f2b8', '模型属性管理_从实体中导入属性', 'codegenerator', 'ModelProperty', 'ImportFromEntity', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('46f7f154-2b89-3f80-cc18-39ed221095f1', '角色管理_获取角色关联的菜单按钮列表', 'Admin', 'Role', 'MenuButtonList', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('479a4f27-08db-c255-b993-39ed22109580', '账户管理_添加', 'Admin', 'Account', 'Add', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('4888619b-5d4a-6430-ad3a-39ed2210957f', '账户管理_查询', 'Admin', 'Account', 'Query', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('4b6ee44f-89f4-b657-2887-39ed221095ed', '角色管理_绑定菜单', 'Admin', 'Role', 'BindMenu', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('4e0ee58f-9bf6-14e4-0874-39edd646f312', '实体属性管理_修改可空状态', 'codegenerator', 'Property', 'UpdateNullable', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('594a2ae3-56a8-d255-cb55-39edd646f2ab', '模型属性管理_修改可空状态', 'codegenerator', 'ModelProperty', 'UpdateNullable', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('5a77ee39-3b26-088f-b0c4-39edd646f216', '枚举管理_查询', 'codegenerator', 'Enum', 'Query', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('5e13bbc7-40c0-78f0-9700-39ed2210958e', '账户管理_重置密码', 'Admin', 'Account', 'ResetPassword', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('60dbf5ce-ff5f-88d6-ff9f-39edd646f2ee', '实体属性管理_删除', 'codegenerator', 'Property', 'Delete', 4, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('60de5dba-7e79-658e-8a5f-39ed22109583', '账户管理_更新', 'Admin', 'Account', 'Update', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('63237d43-54f2-5e06-2cea-39ed221095b4', '菜单管理_更新', 'Admin', 'Menu', 'Update', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('6528a89e-55e0-55e3-10ae-39ed221095ad', '菜单管理_添加', 'Admin', 'Menu', 'Add', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('6e4bd745-6add-d321-03a3-39ed221095e4', '角色管理_编辑', 'Admin', 'Role', 'Edit', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('70581745-e062-dcd0-c19c-39ed221095c5', '模块信息_同步模块数据', 'Admin', 'ModuleInfo', 'Sync', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('70ea1fa1-1b84-c6b7-1c5b-39edd646f2be', '项目管理_查询', 'codegenerator', 'Project', 'Query', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('753bc64a-a40c-e4c4-698d-39ed221095cc', '模块信息_删除', 'Admin', 'ModuleInfo', 'Delete', 4, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('79429274-cfcf-30f1-2b1e-39ed221095ba', '菜单管理_详情', 'Admin', 'Menu', 'Details', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-05-17 10:05:02', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A');
INSERT INTO `permission` VALUES ('7bd7741b-3653-bba9-c13f-39edd646f225', '枚举管理_删除', 'codegenerator', 'Enum', 'Delete', 4, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('7e9a9372-b299-9f8e-155b-39edd646f21f', '枚举管理_添加', 'codegenerator', 'Enum', 'Add', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('821d172a-aadd-8516-ca9f-39edd646f26e', '枚举项管理_更新排序信息', 'codegenerator', 'EnumItem', 'Sort', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('837c460d-4a4b-c720-97ef-39edd646f2e1', '实体属性管理_查询', 'codegenerator', 'Property', 'Query', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('8563a927-bb00-0d98-5b42-39edd646f203', '实体管理_删除', 'codegenerator', 'Class', 'Delete', 4, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('8dcec627-a20b-20ea-c4ef-39edd646f299', '模型属性管理_更新排序信息', 'codegenerator', 'ModelProperty', 'Sort', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('92ba26a7-c033-58d3-6a10-39edd646f318', '实体属性管理_修改列表显示状态', 'codegenerator', 'Property', 'UpdateShowInList', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('935b0ba6-4396-60ee-2ae7-39edd646f2db', '项目管理_修改', 'codegenerator', 'Project', 'Update', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('938f6b86-b398-eed9-970c-39edd646f2d0', '项目管理_删除', 'codegenerator', 'Project', 'Delete', 4, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('941b90ca-7bf7-fa66-21f4-39ed221095e5', '角色管理_修改', 'Admin', 'Role', 'Update', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('9ab67430-412e-0ca0-0635-39edd646f2f2', '实体属性管理_编辑', 'codegenerator', 'Property', 'Edit', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('a474ff49-b640-461a-e6fd-39edd646f2d7', '项目管理_编辑', 'codegenerator', 'Project', 'Edit', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('a8d12a71-6045-356a-56e1-39edd646f25e', '枚举项管理_修改', 'codegenerator', 'EnumItem', 'Update', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('ac3c8201-d7d7-46d4-121b-39edd646f22b', '枚举管理_编辑', 'codegenerator', 'Enum', 'Edit', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('ad112412-b385-a42e-9cb0-39edd646f256', '枚举项管理_编辑', 'codegenerator', 'EnumItem', 'Edit', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('b93930ae-b840-d1a1-b19e-39ed221095d5', '权限接口_同步', 'Admin', 'Permission', 'Sync', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('ba54d018-0eb1-0683-8469-39edd5b5eebc', '菜单管理_更新排序信息', 'Admin', 'Menu', 'Sort', 1, '2019-05-17 10:05:02', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('bd77531e-9146-ac03-b2d0-39ed221095c1', '菜单管理_绑定权限', 'Admin', 'Menu', 'BindPermission', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('c0944193-d0c2-8040-91e5-39edd646f207', '实体管理_编辑', 'codegenerator', 'Class', 'Edit', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('c1580441-4cc5-6733-e9df-39ed221095b3', '菜单管理_编辑', 'Admin', 'Menu', 'Edit', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('c1699e04-4ad0-df03-b375-39ed22109602', '系统_获取指定模块和Controller的Action下拉列表', 'Admin', 'System', 'AllAction', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('c26bb465-8c89-51f9-a71f-39ed22109597', '按钮管理_同步', 'Admin', 'Button', 'Sync', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('c87c85e6-e85e-ece1-cdb4-39edd646f28c', '模型属性管理_编辑', 'codegenerator', 'ModelProperty', 'Edit', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('cc325b48-b557-0635-d042-39edd646f306', '实体属性管理_更新排序信息', 'codegenerator', 'Property', 'Sort', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('cda73ba5-0aca-c003-9e2e-39edd64e7949', '项目管理_生成代码', 'codegenerator', 'Project', 'BuildCode', 3, '2019-05-17 12:51:38', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('cfd7e4ea-2592-3b9a-6780-39ed221095a1', '按钮管理_绑定权限', 'Admin', 'Button', 'BindPermission', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('d327a892-61f1-5dbf-da86-39edd646f266', '枚举项管理_更新排序信息', 'codegenerator', 'EnumItem', 'Sort', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('d4bd9d65-8540-9463-762f-39ed22109589', '账户管理_删除', 'Admin', 'Account', 'Delete', 4, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('d533353c-a241-c4f5-82bd-39ed221095dd', '角色管理_查询', 'Admin', 'Role', 'Query', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('d7948bc2-c6d0-f501-8bf5-39edd646f30d', '实体属性管理_更新排序信息', 'codegenerator', 'Property', 'Sort', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('dad12acd-cc26-ac17-4ff6-39ed221095e3', '角色管理_删除', 'Admin', 'Role', 'Delete', 4, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('db2e0a42-417d-98f3-9d03-39edd646f2b0', '模型属性管理_获取下拉列表', 'codegenerator', 'ModelProperty', 'Select', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('dbe09a62-04ea-257f-4bbf-39edd646f243', '枚举项管理_查询', 'codegenerator', 'EnumItem', 'Query', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('dc464a46-960c-dd10-0713-39ed221095e7', '角色管理_获取角色的关联菜单列表', 'Admin', 'Role', 'MenuList', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('dd546001-8f04-1ec4-8515-39edd646f235', '枚举管理_修改', 'codegenerator', 'Enum', 'Update', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('de8fc51b-88e7-553b-7773-39edd646f24c', '枚举项管理_添加', 'codegenerator', 'EnumItem', 'Add', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('e181e108-3a6d-ebb5-8f53-39edd646f31d', '实体属性管理_获取下拉列表', 'codegenerator', 'Property', 'Select', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('e1f86178-2204-72bd-8c55-39ed221095f3', '角色管理_绑定菜单按钮', 'Admin', 'Role', 'BindMenuButton', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('e4d3cc7f-8017-3a34-d296-39edd646f27c', '模型属性管理_添加', 'codegenerator', 'ModelProperty', 'Add', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('e528d8ac-9704-607d-0d76-39edd646f275', '模型属性管理_查询', 'codegenerator', 'ModelProperty', 'Query', 1, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('e8080a42-912a-23f7-474a-39ed221095f8', '系统_上传Logo', 'Admin', 'System', 'UploadLogo', 3, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('ea79dedb-099c-3132-92c4-39edd646f252', '枚举项管理_删除', 'codegenerator', 'EnumItem', 'Delete', 4, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('eb48579b-94a3-2dc8-ae5e-39ed22109593', '按钮管理_查询', 'Admin', 'Button', 'Query', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('ec812192-f175-6415-67e9-39edd646f292', '模型属性管理_修改', 'codegenerator', 'ModelProperty', 'Update', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('ed24644b-5857-c37f-4df3-39edd646f2fe', '实体属性管理_修改', 'codegenerator', 'Property', 'Update', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('eec8c8d7-6f42-4cc3-1e27-39edd646f20f', '实体管理_修改', 'codegenerator', 'Class', 'Update', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('efbc6833-809e-9270-8898-39edd646f2a1', '模型属性管理_更新排序信息', 'codegenerator', 'ModelProperty', 'Sort', 3, '2019-05-17 12:43:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('f3959eb5-2cc2-aed6-67fc-39ed221095b0', '菜单管理_删除', 'Admin', 'Menu', 'Delete', 4, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('f6b17ecc-900f-0547-f1e6-39ed22109591', '审计信息_详情', 'Admin', 'AuditInfo', 'Details', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('f9a16174-575b-6c3b-3f9d-39ed221095d1', '模块信息_下拉列表数据', 'Admin', 'ModuleInfo', 'Select', 1, '2019-04-12 12:52:25', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-06-14 17:03:14', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Remarks` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  `Deleted` tinyint(4) NOT NULL COMMENT '已删除',
  `DeletedTime` datetime(0) NOT NULL COMMENT '删除时间',
  `DeletedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '删除人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES ('1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '系统管理员', NULL, '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', 0, '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for role_menu
-- ----------------------------
DROP TABLE IF EXISTS `role_menu`;
CREATE TABLE `role_menu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色编号',
  `MenuId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '菜单编号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 240 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role_menu
-- ----------------------------
INSERT INTO `role_menu` VALUES (220, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee612e-ce4a-a2a7-8b18-71245ad6de18');
INSERT INTO `role_menu` VALUES (221, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee612f-4910-0fc0-09d0-209d33fb74d0');
INSERT INTO `role_menu` VALUES (222, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6130-95cf-a276-2d63-abfad9b4c184');
INSERT INTO `role_menu` VALUES (223, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6131-1c10-5571-798e-b3f89501c151');
INSERT INTO `role_menu` VALUES (224, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6131-5e71-db70-9458-33ab14dd32c3');
INSERT INTO `role_menu` VALUES (225, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '214e8c1a-6a87-c214-7ff9-39ed21e7cc27');
INSERT INTO `role_menu` VALUES (226, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '0404f457-1c32-ba61-8fad-39ed2208bcd9');
INSERT INTO `role_menu` VALUES (227, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', 'a74578da-7b3b-8a7c-e54c-39ed22090f48');
INSERT INTO `role_menu` VALUES (228, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', 'a9105ca0-722c-66f5-844b-39ed220926a0');
INSERT INTO `role_menu` VALUES (229, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', 'e38b0e46-a755-ff0f-012c-39ed2209462a');
INSERT INTO `role_menu` VALUES (230, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '64bb88f0-ae8d-eff9-c537-39ed22096f9b');
INSERT INTO `role_menu` VALUES (231, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', 'b09aac3d-43b4-3327-9f05-39ed22098f00');
INSERT INTO `role_menu` VALUES (232, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '7f9dcb98-20b2-3556-a850-39ed2209ab84');
INSERT INTO `role_menu` VALUES (233, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee3891-d1bb-878e-698b-b2a9911c76e9');
INSERT INTO `role_menu` VALUES (234, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee3894-ab04-421c-a1f2-2abad6b24ca4');
INSERT INTO `role_menu` VALUES (235, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee5d50-9629-1e88-d03f-d74cf8497049');
INSERT INTO `role_menu` VALUES (236, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '601d5926-cf2a-ea30-b3cc-39edd647ecd8');
INSERT INTO `role_menu` VALUES (237, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', 'aca98589-c672-4c78-8831-39edd648acbb');
INSERT INTO `role_menu` VALUES (238, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '90fe5cac-952b-8919-59be-39edd648fab5');
INSERT INTO `role_menu` VALUES (239, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', 'f2155b1a-d222-8786-e9d7-39edd6492774');
INSERT INTO `role_menu` VALUES (240, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39edef2c-6153-c935-1660-4823c692e142');

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
) ENGINE = InnoDB AUTO_INCREMENT = 73 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role_menu_button
-- ----------------------------
INSERT INTO `role_menu_button` VALUES (1, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '0404F457-1C32-BA61-8FAD-39ED2208BCD9', '40F1BFDB-22BD-0DA1-3942-39ED220DF27F');
INSERT INTO `role_menu_button` VALUES (2, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '0404F457-1C32-BA61-8FAD-39ED2208BCD9', '181F6B49-4E36-8620-DC51-39ED220DF287');
INSERT INTO `role_menu_button` VALUES (3, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'A74578DA-7B3B-8A7C-E54C-39ED22090F48', 'EF881C51-DE63-8572-250B-39ED2211C64A');
INSERT INTO `role_menu_button` VALUES (4, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'A74578DA-7B3B-8A7C-E54C-39ED22090F48', '97727921-F069-AFAB-CFF8-39ED2211C64D');
INSERT INTO `role_menu_button` VALUES (5, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'A9105CA0-722C-66F5-844B-39ED220926A0', '99EB627E-7E79-B36A-1C3C-39ED2213BE3E');
INSERT INTO `role_menu_button` VALUES (6, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'A9105CA0-722C-66F5-844B-39ED220926A0', 'B4EAAE9E-6D8D-4C28-F832-39ED2213BE41');
INSERT INTO `role_menu_button` VALUES (7, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'A9105CA0-722C-66F5-844B-39ED220926A0', '1FC9841E-CCBA-4B85-41C6-39ED2213BE42');
INSERT INTO `role_menu_button` VALUES (8, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'A9105CA0-722C-66F5-844B-39ED220926A0', '6309893F-188F-16DB-CA2E-39ED2213BE43');
INSERT INTO `role_menu_button` VALUES (9, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'A9105CA0-722C-66F5-844B-39ED220926A0', '64A397A3-F185-F740-5FD8-39ED2213BE46');
INSERT INTO `role_menu_button` VALUES (10, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'E38B0E46-A755-FF0F-012C-39ED2209462A', '13EE6243-46B6-AE86-A1AA-39ED22360378');
INSERT INTO `role_menu_button` VALUES (11, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'E38B0E46-A755-FF0F-012C-39ED2209462A', 'C8C65374-9DB0-98ED-9C41-39ED2236037B');
INSERT INTO `role_menu_button` VALUES (12, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'E38B0E46-A755-FF0F-012C-39ED2209462A', '6443BABB-3005-3393-CFA3-39ED2236037E');
INSERT INTO `role_menu_button` VALUES (13, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'E38B0E46-A755-FF0F-012C-39ED2209462A', '7FCB3809-1893-BD91-CE50-39ED22360380');
INSERT INTO `role_menu_button` VALUES (14, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '64BB88F0-AE8D-EFF9-C537-39ED22096F9B', 'A83923D1-7AA3-107B-C72D-39ED2238C64F');
INSERT INTO `role_menu_button` VALUES (15, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '64BB88F0-AE8D-EFF9-C537-39ED22096F9B', '02AA62D0-2944-EABE-ED49-39ED2238C652');
INSERT INTO `role_menu_button` VALUES (16, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '64BB88F0-AE8D-EFF9-C537-39ED22096F9B', '40CCB97F-49B2-24AD-BFD7-39ED2238C653');
INSERT INTO `role_menu_button` VALUES (17, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '64BB88F0-AE8D-EFF9-C537-39ED22096F9B', 'F92EE62D-1499-C296-E25C-39ED2238C655');
INSERT INTO `role_menu_button` VALUES (18, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'B09AAC3D-43B4-3327-9F05-39ED22098F00', '62C2CB73-EEC9-DEA5-C020-39ED223C4590');
INSERT INTO `role_menu_button` VALUES (34, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'A9105CA0-722C-66F5-844B-39ED220926A0', '62717E4F-D4DB-E8EC-2D4E-39EDD5B637BB');
INSERT INTO `role_menu_button` VALUES (35, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '90FE5CAC-952B-8919-59BE-39EDD648FAB5', '7823220C-7FAD-05CD-B3FF-39EDD64D6318');
INSERT INTO `role_menu_button` VALUES (36, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '90FE5CAC-952B-8919-59BE-39EDD648FAB5', '577BEBF3-2408-9FA6-071A-39EDD64D6328');
INSERT INTO `role_menu_button` VALUES (37, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '90FE5CAC-952B-8919-59BE-39EDD648FAB5', '318C6F27-604E-AFC5-22BF-39EDD64D6330');
INSERT INTO `role_menu_button` VALUES (38, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', '90FE5CAC-952B-8919-59BE-39EDD648FAB5', '8405FED8-E131-23AB-7740-39EDD64D6339');
INSERT INTO `role_menu_button` VALUES (39, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'F2155B1A-D222-8786-E9D7-39EDD6492774', '161E3E65-A02E-8A59-8C3E-39EDD64A36E9');
INSERT INTO `role_menu_button` VALUES (40, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'F2155B1A-D222-8786-E9D7-39EDD6492774', '1AE9F5E5-B972-375F-D680-39EDD64A3703');
INSERT INTO `role_menu_button` VALUES (41, '1CB0D69C-6373-3B46-51B8-39ED21CB6A2D', 'F2155B1A-D222-8786-E9D7-39EDD6492774', 'A7D396FB-BC36-EE11-A857-39EDD64A370A');
INSERT INTO `role_menu_button` VALUES (52, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee3894-ab04-421c-a1f2-2abad6b24ca4', '39ee389d-2c10-4e60-1cc8-2bc3ec58ee13');
INSERT INTO `role_menu_button` VALUES (53, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee3894-ab04-421c-a1f2-2abad6b24ca4', '39ee389d-2c46-d0db-4f3a-85eb9afa97eb');
INSERT INTO `role_menu_button` VALUES (54, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee3894-ab04-421c-a1f2-2abad6b24ca4', '39ee389d-2c4c-017a-e05b-f26b5c6ad6de');
INSERT INTO `role_menu_button` VALUES (55, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee612f-4910-0fc0-09d0-209d33fb74d0', '39ee612f-a3c7-e53d-57c1-c8fce74e822b');
INSERT INTO `role_menu_button` VALUES (56, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee612f-4910-0fc0-09d0-209d33fb74d0', '39ee612f-a3ce-2755-58b5-9648d1051206');
INSERT INTO `role_menu_button` VALUES (57, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee612f-4910-0fc0-09d0-209d33fb74d0', '39ee612f-a3d2-e549-66c1-46b36445dd2d');
INSERT INTO `role_menu_button` VALUES (63, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee6131-c1f3-ed2b-70dd-030156cb2a67');
INSERT INTO `role_menu_button` VALUES (64, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee6131-c1f6-06e1-803d-9e2a1f99b7e6');
INSERT INTO `role_menu_button` VALUES (65, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee6131-c1fc-8643-0b34-c01aa4db14f8');
INSERT INTO `role_menu_button` VALUES (66, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee66bb-5899-732f-a207-9e9f724b44ff');
INSERT INTO `role_menu_button` VALUES (67, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee66c8-6c91-6a80-65cd-07be4b580101');
INSERT INTO `role_menu_button` VALUES (68, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee66c8-6c99-e097-ea62-6f7dc4ab46f7');
INSERT INTO `role_menu_button` VALUES (69, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6130-95cf-a276-2d63-abfad9b4c184', '39ee66c8-6c9c-c5dc-ec91-31ce9e0060fe');
INSERT INTO `role_menu_button` VALUES (70, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6131-1c10-5571-798e-b3f89501c151', '39ee6749-b559-330f-9801-fb96beabf49d');
INSERT INTO `role_menu_button` VALUES (71, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6131-5e71-db70-9458-33ab14dd32c3', '39ee6750-0088-44c0-ab18-ffd93a5c01c5');
INSERT INTO `role_menu_button` VALUES (72, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6131-5e71-db70-9458-33ab14dd32c3', '39ee6750-00b9-c821-aa79-158304131906');
INSERT INTO `role_menu_button` VALUES (73, '1cb0d69c-6373-3b46-51b8-39ed21cb6a2d', '39ee6131-5e71-db70-9458-33ab14dd32c3', '39ee6750-00bc-6b5b-c2ff-c9c5112be868');

SET FOREIGN_KEY_CHECKS = 1;
