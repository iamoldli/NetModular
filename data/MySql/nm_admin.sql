/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80015
 Source Host           : localhost:3306
 Source Schema         : nm_admin

 Target Server Type    : MySQL
 Target Server Version : 80015
 File Encoding         : 65001

 Date: 26/08/2019 15:20:46
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for account
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Type` int(11) NOT NULL DEFAULT 0 COMMENT '类型',
  `UserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户名',
  `Password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '密码',
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '手机号',
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '邮箱',
  `LoginTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '最后登录时间',
  `LoginIP` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '最后登录IP',
  `Status` smallint(6) NOT NULL COMMENT '状态：0、未激活 1、正常 2、禁用 3、注销',
  `IsLock` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否锁定角色',
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '最后修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '最后修改人',
  `ClosedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '注销时间',
  `ClosedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '注销人',
  `Deleted` bit(4) NOT NULL DEFAULT b'0' COMMENT '已删除',
  `DeletedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '删除时间',
  `DeletedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '删除人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account
-- ----------------------------
INSERT INTO `account` VALUES ('2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', 0, 'admin', 'E739279CB28CDAFD7373618313803524', '管理员', '', '', '2019-08-26 14:30:28', '0.0.0.1', 1, b'0', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-04-18 17:30:57', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', b'0000', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for account_role
-- ----------------------------
DROP TABLE IF EXISTS `account_role`;
CREATE TABLE `account_role`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '账户编号',
  `RoleId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色编号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of account_role
-- ----------------------------
INSERT INTO `account_role` VALUES (7, '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '39eeaf22-855c-9467-d478-06b8a4a085b2');

-- ----------------------------
-- Table structure for auditinfo
-- ----------------------------
DROP TABLE IF EXISTS `auditinfo`;
CREATE TABLE `auditinfo`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `AccountId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '账户编号',
  `Area` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '区域',
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
) ENGINE = InnoDB AUTO_INCREMENT = 894 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for button
-- ----------------------------
DROP TABLE IF EXISTS `button`;
CREATE TABLE `button`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `MenuCode` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '菜单编码',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Icon` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图标',
  `Code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '按钮编码',
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建日期',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of button
-- ----------------------------
INSERT INTO `button` VALUES ('39ef59aa-dd5f-95b8-b24f-7560f1e989a5', 'admin_moduleinfo', '同步', 'refresh', 'admin_moduleinfo_sync', '2019-07-31 18:05:37', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:05:37', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59aa-dd69-ce78-1f8e-18713fb581db', 'admin_moduleinfo', '删除', 'delete', 'admin_moduleinfo_del', '2019-07-31 18:05:37', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:05:37', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59ac-e8f8-d10a-d462-bf53de5eb381', 'admin_auditinfo', '详情', 'detail', 'admin_auditinfo_details', '2019-07-31 18:07:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:07:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59ad-7247-ad49-1a0f-bbe1ce5780d6', 'codegenerator_project', '添加', 'add', 'codegenerator_project_add', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59ad-724c-1205-5067-0b955db55169', 'codegenerator_project', '编辑', 'edit', 'codegenerator_project_edit', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59ad-7253-e5e7-8d94-e178089cec0f', 'codegenerator_project', '删除', 'delete', 'codegenerator_project_del', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59ad-7255-08dd-610d-f61b7a50a2d7', 'codegenerator_project', '生成', 'delete', 'codegenerator_project_build_code', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59ad-8516-cea4-728b-bdf350aa111e', 'codegenerator_enum', '添加', 'add', 'codegenerator_enum_add', '2019-07-31 18:08:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59ad-8525-b7ba-a300-495ff91e4075', 'codegenerator_enum', '编辑', 'edit', 'codegenerator_enum_edit', '2019-07-31 18:08:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59ad-8528-5821-4236-d95011a1e402', 'codegenerator_enum', '删除', 'delete', 'codegenerator_enum_del', '2019-07-31 18:08:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59b9-6cf9-a922-7053-07b39e66d2ac', 'admin_config', '添加', 'add', 'admin_config_add', '2019-07-31 18:21:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:21:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59b9-6d01-0f0e-b86a-30b358a8d86c', 'admin_config', '编辑', 'edit', 'admin_config_edit', '2019-07-31 18:21:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:21:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59b9-6d0a-f50e-6739-0bc1df7b73d1', 'admin_config', '删除', 'delete', 'admin_config_del', '2019-07-31 18:21:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:21:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59b9-bdd4-7aee-0ba3-cc1a2ee4593a', 'admin_permission', '同步', 'refresh', 'admin_permission_sync', '2019-07-31 18:21:52', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:21:52', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59b9-e0f6-2e7b-07da-c2d1167b50e2', 'admin_role', '添加', 'add', 'admin_role_add', '2019-07-31 18:22:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:22:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59b9-e0fd-34b9-300d-f57c170d17d7', 'admin_role', '编辑', 'edit', 'admin_role_edit', '2019-07-31 18:22:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:22:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59b9-e103-d474-f0bf-4598bc76e53f', 'admin_role', '删除', 'delete', 'admin_role_del', '2019-07-31 18:22:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:22:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59b9-e105-acc8-24d9-59f3b301cf8e', 'admin_role', '绑定菜单', 'bind', 'admin_role_bind_menu', '2019-07-31 18:22:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:22:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59bd-6012-a1fe-e8d3-35ec6a40b6c4', 'admin_account', '添加', 'add', 'admin_account_add', '2019-07-31 18:25:50', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:25:50', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59bd-6017-0310-363d-e2c63a5b28fb', 'admin_account', '编辑', 'edit', 'admin_account_edit', '2019-07-31 18:25:50', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:25:50', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59bd-601e-89d0-d3de-635df91054be', 'admin_account', '删除', 'delete', 'admin_account_del', '2019-07-31 18:25:50', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:25:50', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59bd-6021-8ec7-2d7a-2f1406388764', 'admin_account', '重置密码', 'refresh', 'admin_account_reset_password', '2019-07-31 18:25:50', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:25:50', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59be-6fab-9d9b-9368-27692403020d', 'admin_menu', '添加', 'add', 'admin_menu_add', '2019-07-31 18:27:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:27:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59be-6fb3-fdab-31fe-64f4c07f2b5b', 'admin_menu', '编辑', 'edit', 'admin_menu_edit', '2019-07-31 18:27:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:27:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59be-6fba-17c8-7660-8fbbc7458b11', 'admin_menu', '删除', 'delete', 'admin_menu_del', '2019-07-31 18:27:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:27:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef59be-6fbe-5a42-8652-4b6bd66d8aa7', 'admin_menu', '排序', 'sort', 'admin_menu_sort', '2019-07-31 18:27:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:27:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-0951-cc41-6079-e3dbe812d9b3', 'personnelfiles_company', '添加', 'add', 'personnelfiles_company_add', '2019-08-01 15:54:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:54:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-095b-ed9f-5520-5493684a60bb', 'personnelfiles_company', '编辑', 'edit', 'personnelfiles_company_edit', '2019-08-01 15:54:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:54:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-0960-5812-7cb2-459fb185a52e', 'personnelfiles_company', '删除', 'delete', 'personnelfiles_company_del', '2019-08-01 15:54:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:54:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-b8d1-289b-7fa1-e7b21f6bc53c', 'personnelfiles_department', '添加', 'add', 'personnelfiles_department_add', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-b8db-c4e1-a562-88770a69f3f6', 'personnelfiles_department', '编辑', 'edit', 'personnelfiles_department_edit', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-b8df-e961-4434-3120e646b510', 'personnelfiles_department', '删除', 'delete', 'personnelfiles_department_del', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-b8e1-f381-c7bc-b2115ac0cc45', 'personnelfiles_department', '岗位', 'edit', 'personnelfiles_department_position', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-b8e4-a1cb-01d1-1fe0e03f6488', 'personnelfiles_department', '岗位添加', 'add', 'personnelfiles_department_position_add', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-b8ed-6957-30bc-961ce7fdba97', 'personnelfiles_department', '岗位编辑', 'edit', 'personnelfiles_department_position_edit', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e59-b8f0-3a3f-8dae-455a251a8360', 'personnelfiles_department', '岗位删除', 'delete', 'personnelfiles_department_position_del', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e5a-38b3-333e-c7d9-95fbaad7d2d6', 'personnelfiles_position', '删除', 'delete', 'personnelfiles_position_del', '2019-08-01 15:55:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e5a-8de6-4038-2f99-6db4c8036645', 'personnelfiles_user', '添加', 'add', 'personnelfiles_user_add', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e5a-8dee-3f13-ba40-3420b4323799', 'personnelfiles_user', '编辑', 'edit', 'personnelfiles_user_edit', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e5a-8df6-29df-57ed-a4dc969f4e27', 'personnelfiles_user', '删除', 'delete', 'personnelfiles_user_del', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e5a-8df9-3e6f-2527-043fba5c643a', 'personnelfiles_user', '工作经历', 'work', 'personnelfiles_user_work_history', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef5e5a-8e05-7c17-ce12-84b366ab9e2c', 'personnelfiles_user', '教育经历', 'education', 'personnelfiles_user_education_history', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef66fd-b165-1e3e-52c9-19219ed962bf', 'common_area', '添加', 'add', 'common_area_add', '2019-08-03 08:11:09', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:27:52', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef66fd-b177-45dc-d79e-0802cd198fed', 'common_area', '编辑', 'edit', 'common_area_edit', '2019-08-03 08:11:09', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:27:52', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef66fd-b17f-68af-ce08-7f240e480ac0', 'common_area', '删除', 'delete', 'common_area_del', '2019-08-03 08:11:09', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:27:52', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef66fd-e397-ca10-df28-ec99046f7f11', 'common_attachment', '删除', 'delete', 'common_attachment_del', '2019-08-03 08:11:22', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:28:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef66fd-e3a3-5366-4e24-613e7ae5f4c7', 'common_attachment', '导出', 'export', 'common_attachment_export', '2019-08-03 08:11:22', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:28:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef6c7a-e661-5b1f-0b54-385ebc66e5de', 'quartz_group', '添加', 'add', 'quartz_group_add', '2019-08-04 09:46:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-04 09:47:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef6c7a-e670-993d-9b0f-92302d66d3f3', 'quartz_group', '编辑', 'edit', 'quartz_group_edit', '2019-08-04 09:46:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-04 09:47:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef6c7a-e674-9900-bbef-8b4e1d465ecd', 'quartz_group', '删除', 'delete', 'quartz_group_del', '2019-08-04 09:46:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-04 09:47:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef7205-e481-4691-d1a0-dfe80de18f93', 'quartz_job', '添加', 'add', 'quartz_job_add', '2019-08-05 11:35:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef7205-e48b-b3d8-f3bc-ef56b1c7dcab', 'quartz_job', '删除', 'delete', 'quartz_job_del', '2019-08-05 11:35:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef7349-e2bd-34b4-208d-582b863a9bb2', 'quartz_job', '暂停', 'pause', 'quartz_job_pause', '2019-08-05 17:29:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `button` VALUES ('39ef76e9-b458-f509-5bed-c799d499f937', 'quartz_job', '启动', 'run', 'quartz_job_resume', '2019-08-06 10:23:15', '00000000-0000-0000-0000-000000000000', '2019-08-06 10:23:15', '00000000-0000-0000-0000-000000000000');
INSERT INTO `button` VALUES ('39ef77d2-9623-1956-ca1d-572f5d537f76', 'quartz_job', '日志', 'log', 'quartz_job_log', '2019-08-06 14:37:37', '00000000-0000-0000-0000-000000000000', '2019-08-06 14:37:37', '00000000-0000-0000-0000-000000000000');
INSERT INTO `button` VALUES ('39ef77f5-9b56-ab8b-48cd-27977189c430', 'quartz_job', '编辑', 'edit', 'quartz_job_edit', '2019-08-06 15:15:52', '00000000-0000-0000-0000-000000000000', '2019-08-06 15:15:52', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for button_permission
-- ----------------------------
DROP TABLE IF EXISTS `button_permission`;
CREATE TABLE `button_permission`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ButtonCode` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '按钮编码',
  `PermissionCode` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '权限编码',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 451 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of button_permission
-- ----------------------------
INSERT INTO `button_permission` VALUES (286, 'admin_moduleinfo_sync', 'admin_moduleinfo_sync_post');
INSERT INTO `button_permission` VALUES (287, 'admin_moduleinfo_del', 'admin_moduleinfo_delete_delete');
INSERT INTO `button_permission` VALUES (312, 'admin_auditinfo_details', 'admin_auditinfo_details_get');
INSERT INTO `button_permission` VALUES (313, 'codegenerator_project_add', 'codegenerator_project_add_post');
INSERT INTO `button_permission` VALUES (314, 'codegenerator_project_edit', 'codegenerator_project_edit_get');
INSERT INTO `button_permission` VALUES (315, 'codegenerator_project_edit', 'codegenerator_project_update_post');
INSERT INTO `button_permission` VALUES (316, 'codegenerator_project_del', 'codegenerator_project_delete_delete');
INSERT INTO `button_permission` VALUES (317, 'codegenerator_project_build_code', 'codegenerator_project_buildcode');
INSERT INTO `button_permission` VALUES (318, 'codegenerator_enum_add', 'codegenerator_enum_add_post');
INSERT INTO `button_permission` VALUES (319, 'codegenerator_enum_edit', 'codegenerator_enum_edit_get');
INSERT INTO `button_permission` VALUES (320, 'codegenerator_enum_edit', 'codegenerator_enum_update_post');
INSERT INTO `button_permission` VALUES (321, 'codegenerator_enum_del', 'codegenerator_enum_delete_delete');
INSERT INTO `button_permission` VALUES (334, 'admin_config_add', 'admin_config_add_post');
INSERT INTO `button_permission` VALUES (335, 'admin_config_edit', 'admin_config_edit_get');
INSERT INTO `button_permission` VALUES (336, 'admin_config_edit', 'admin_config_update_post');
INSERT INTO `button_permission` VALUES (337, 'admin_config_del', 'admin_config_delete_delete');
INSERT INTO `button_permission` VALUES (338, 'admin_permission_sync', 'admin_permission_sync_post');
INSERT INTO `button_permission` VALUES (345, 'admin_role_add', 'admin_role_add_post');
INSERT INTO `button_permission` VALUES (346, 'admin_role_edit', 'admin_role_edit_get');
INSERT INTO `button_permission` VALUES (347, 'admin_role_edit', 'admin_role_update_post');
INSERT INTO `button_permission` VALUES (348, 'admin_role_del', 'admin_role_delete_delete');
INSERT INTO `button_permission` VALUES (349, 'admin_role_bind_menu', 'admin_role_menulist_get');
INSERT INTO `button_permission` VALUES (350, 'admin_role_bind_menu', 'admin_role_bindmenu_post');
INSERT INTO `button_permission` VALUES (351, 'admin_role_bind_menu', 'admin_role_menubuttonlist_get');
INSERT INTO `button_permission` VALUES (352, 'admin_role_bind_menu', 'admin_role_bindmenubutton_post');
INSERT INTO `button_permission` VALUES (365, 'admin_account_add', 'admin_account_add_post');
INSERT INTO `button_permission` VALUES (366, 'admin_account_edit', 'admin_account_edit_get');
INSERT INTO `button_permission` VALUES (367, 'admin_account_edit', 'admin_account_update_post');
INSERT INTO `button_permission` VALUES (368, 'admin_account_del', 'admin_account_delete_delete');
INSERT INTO `button_permission` VALUES (369, 'admin_account_reset_password', 'admin_account_updatepassword_post');
INSERT INTO `button_permission` VALUES (370, 'admin_menu_add', 'admin_menu_add_post');
INSERT INTO `button_permission` VALUES (371, 'admin_menu_edit', 'admin_menu_edit_get');
INSERT INTO `button_permission` VALUES (372, 'admin_menu_edit', 'admin_menu_update_post');
INSERT INTO `button_permission` VALUES (373, 'admin_menu_del', 'admin_menu_delete_delete');
INSERT INTO `button_permission` VALUES (374, 'admin_menu_sort', 'admin_menu_sort_get');
INSERT INTO `button_permission` VALUES (375, 'admin_menu_sort', 'admin_menu_sort_post');
INSERT INTO `button_permission` VALUES (376, 'personnelfiles_company_add', 'personnelfiles_company_add_post');
INSERT INTO `button_permission` VALUES (377, 'personnelfiles_company_edit', 'personnelfiles_company_edit_get');
INSERT INTO `button_permission` VALUES (378, 'personnelfiles_company_edit', 'personnelfiles_company_update_post');
INSERT INTO `button_permission` VALUES (379, 'personnelfiles_company_del', 'personnelfiles_company_delete_delete');
INSERT INTO `button_permission` VALUES (380, 'personnelfiles_department_add', 'personnelfiles_department_add_post');
INSERT INTO `button_permission` VALUES (381, 'personnelfiles_department_edit', 'personnelfiles_department_edit_get');
INSERT INTO `button_permission` VALUES (382, 'personnelfiles_department_edit', 'personnelfiles_department_update_post');
INSERT INTO `button_permission` VALUES (383, 'personnelfiles_department_del', 'personnelfiles_department_delete_delete');
INSERT INTO `button_permission` VALUES (384, 'personnelfiles_department_position', 'personnelfiles_position_query_get');
INSERT INTO `button_permission` VALUES (385, 'personnelfiles_department_position_add', 'personnelfiles_position_add_post');
INSERT INTO `button_permission` VALUES (386, 'personnelfiles_department_position_edit', 'personnelfiles_position_edit_get');
INSERT INTO `button_permission` VALUES (387, 'personnelfiles_department_position_edit', 'personnelfiles_position_update_post');
INSERT INTO `button_permission` VALUES (388, 'personnelfiles_department_position_del', 'personnelfiles_position_delete_delete');
INSERT INTO `button_permission` VALUES (389, 'personnelfiles_position_del', 'personnelfiles_position_delete_delete');
INSERT INTO `button_permission` VALUES (390, 'personnelfiles_user_add', 'personnelfiles_user_add_post');
INSERT INTO `button_permission` VALUES (391, 'personnelfiles_user_edit', 'personnelfiles_user_edit_get');
INSERT INTO `button_permission` VALUES (392, 'personnelfiles_user_edit', 'personnelfiles_user_update_post');
INSERT INTO `button_permission` VALUES (393, 'personnelfiles_user_del', 'personnelfiles_user_delete_delete');
INSERT INTO `button_permission` VALUES (394, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_query_get');
INSERT INTO `button_permission` VALUES (395, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_add_post');
INSERT INTO `button_permission` VALUES (396, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_edit_get');
INSERT INTO `button_permission` VALUES (397, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_update_get');
INSERT INTO `button_permission` VALUES (398, 'personnelfiles_user_work_history', 'personnelfiles_userworkhistory_delete_delete');
INSERT INTO `button_permission` VALUES (399, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_query_get');
INSERT INTO `button_permission` VALUES (400, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_add_post');
INSERT INTO `button_permission` VALUES (401, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_edit_get');
INSERT INTO `button_permission` VALUES (402, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_update_get');
INSERT INTO `button_permission` VALUES (403, 'personnelfiles_user_education_history', 'personnelfiles_usereducationhistory_delete_delete');
INSERT INTO `button_permission` VALUES (410, 'common_area_add', 'common_area_add_post');
INSERT INTO `button_permission` VALUES (411, 'common_area_edit', 'common_area_edit_get');
INSERT INTO `button_permission` VALUES (412, 'common_area_edit', 'common_area_update_post');
INSERT INTO `button_permission` VALUES (413, 'common_area_del', 'common_area_delete_delete');
INSERT INTO `button_permission` VALUES (414, 'common_attachment_del', 'common_attachment_delete_delete');
INSERT INTO `button_permission` VALUES (415, 'common_attachment_export', 'common_attachment_export_get');
INSERT INTO `button_permission` VALUES (420, 'quartz_group_add', 'quartz_group_add_post');
INSERT INTO `button_permission` VALUES (421, 'quartz_group_edit', 'quartz_group_edit_get');
INSERT INTO `button_permission` VALUES (422, 'quartz_group_edit', 'quartz_group_update_post');
INSERT INTO `button_permission` VALUES (423, 'quartz_group_del', 'quartz_group_delete_delete');
INSERT INTO `button_permission` VALUES (445, 'quartz_job_add', 'quartz_job_add_post');
INSERT INTO `button_permission` VALUES (446, 'quartz_job_edit', 'quartz_job_edit_get');
INSERT INTO `button_permission` VALUES (447, 'quartz_job_edit', 'quartz_job_update_post');
INSERT INTO `button_permission` VALUES (448, 'quartz_job_pause', 'quartz_job_pause_post');
INSERT INTO `button_permission` VALUES (449, 'quartz_job_resume', 'quartz_job_resume_post');
INSERT INTO `button_permission` VALUES (450, 'quartz_job_log', 'quartz_job_log_get');
INSERT INTO `button_permission` VALUES (451, 'quartz_job_del', 'quartz_job_delete_delete');

-- ----------------------------
-- Table structure for config
-- ----------------------------
DROP TABLE IF EXISTS `config`;
CREATE TABLE `config`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Key` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '键',
  `Value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '值',
  `Remarks` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '添加时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '添加人',
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of config
-- ----------------------------
INSERT INTO `config` VALUES (1, 'sys_button_permission', 'True', '启用按钮权限', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-08-01 15:58:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (3, 'sys_auditing', 'False', '启用审计日志', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-08-01 15:58:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (4, 'sys_toolbar_skin', 'False', '显示工具栏皮肤按钮', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-08-01 15:58:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (5, 'sys_title', '通用权限管理系统', '系统标题', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-08-01 15:58:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (6, 'sys_toolbar_fullscreen', 'True', '显示工具栏全屏按钮', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-08-01 15:58:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (7, 'sys_home', '/admin/home', '系统首页', '2019-04-12 11:36:52', '00000000-0000-0000-0000-000000000000', '2019-08-01 15:58:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (8, 'sys_verify_code', 'False', '启用登录验证码功能', '2019-05-06 09:11:35', '39ED5AB3-0C91-A26C-2F8A-A3B723EF422A', '2019-08-01 15:58:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (9, 'sys_toolbar_userinfo', 'True', '显示工具栏用户信息按钮', '2019-05-06 09:35:48', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-08-01 15:58:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (10, 'sys_toolbar_logout', 'True', '显示工具栏退出按钮', '2019-05-06 09:35:48', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-08-01 15:58:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (11, 'sys_userinfo_page', '', '个人信息页', '2019-06-14 13:47:55', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-08-01 15:58:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (12, 'sys_toolbar_customcss', '', '自定义CSS样式', '2019-06-20 19:20:09', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-08-01 15:58:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (13, 'sys_logo', '', '系统Logo', '2019-06-27 17:32:34', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-08-01 15:58:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `config` VALUES (14, 'sys_permission_validate', 'True', '启用权限验证功能', '2019-08-01 15:58:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:58:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

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
  `Icon` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图标',
  `IconColor` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图标颜色',
  `Url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '链接',
  `Level` int(11) NOT NULL COMMENT '等级',
  `Show` bit(4) NOT NULL COMMENT '是否显示',
  `Sort` int(11) NOT NULL COMMENT '排序',
  `Target` smallint(6) NOT NULL DEFAULT 0 COMMENT '打开方式',
  `DialogWidth` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '对话框宽度',
  `DialogHeight` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '对话框高度',
  `DialogFullscreen` tinyint(4) NOT NULL COMMENT '对话框可全屏',
  `Remarks` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menu
-- ----------------------------
INSERT INTO `menu` VALUES ('39ef59aa-4e53-26d4-d604-940d629d38c5', '', 0, '00000000-0000-0000-0000-000000000000', '权限管理', '', '', '', 'permission', '', '', 0, b'0001', 3, -1, '', '', 1, '', '2019-07-31 18:05:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-06 18:14:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59aa-7dda-ed60-475d-a8c29608812a', '', 0, '00000000-0000-0000-0000-000000000000', '系统管理', '', '', '', 'system', '', '', 0, b'0001', 4, -1, '', '', 1, '', '2019-07-31 18:05:13', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-06 18:14:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59aa-b114-d3dd-204a-910bd47e406d', '', 0, '00000000-0000-0000-0000-000000000000', '开发工具', '', '', '', 'develop', '', '', 0, b'0001', 5, -1, '', '', 1, '', '2019-07-31 18:05:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-06 18:14:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59aa-dd30-ba16-96a8-addb63f85f27', 'Admin', 1, '39ef59aa-4e53-26d4-d604-940d629d38c5', '模块管理', 'admin_moduleinfo', '', '', 'product', '', '', 1, b'0001', 0, 0, '', '', 1, '', '2019-07-31 18:05:37', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:24:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ab-3714-82b8-5bca-1650aab0d3a4', 'Admin', 1, '39ef59aa-4e53-26d4-d604-940d629d38c5', '权限列表', 'admin_permission', '', '', 'verifycode', '', '', 1, b'0001', 1, 0, '', '', 1, '', '2019-07-31 18:06:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:24:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ab-5b74-986c-5fee-b9daecf53951', 'Admin', 1, '39ef59aa-4e53-26d4-d604-940d629d38c5', '菜单管理', 'admin_menu', '', '', 'menu', '', '', 1, b'0001', 2, 0, '', '', 1, '', '2019-07-31 18:06:09', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:24:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ab-8bcd-bdce-1a20-b5bae35508e5', 'Admin', 1, '39ef59aa-4e53-26d4-d604-940d629d38c5', '角色管理', 'admin_role', '', '', 'role', '', '', 1, b'0001', 3, 0, '', '', 1, '', '2019-07-31 18:06:22', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:24:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ab-adba-82f7-47af-bda8fa2fe0bb', 'Admin', 1, '39ef59aa-4e53-26d4-d604-940d629d38c5', '账户管理', 'admin_account', '', '', 'user', '', '', 1, b'0001', 4, 0, '', '', 1, '', '2019-07-31 18:06:30', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:24:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ac-7771-b6ab-2ffd-d55d0627f7f3', 'Admin', 1, '39ef59aa-7dda-ed60-475d-a8c29608812a', '系统配置', 'admin_system', '', '', 'system', '', '', 1, b'0001', 0, 0, '', '', 1, '', '2019-07-31 18:07:22', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:25:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ac-bd76-459d-f146-3617a599cdf4', 'Admin', 1, '39ef59aa-7dda-ed60-475d-a8c29608812a', '配置项管理', 'admin_config', '', '', 'tag', '', '', 1, b'0001', 1, 0, '', '', 1, '', '2019-07-31 18:07:40', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:25:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ac-e8da-dff4-d41d-a2c334e75535', 'Admin', 1, '39ef59aa-7dda-ed60-475d-a8c29608812a', '审计日志', 'admin_auditinfo', '', '', 'log', '', '', 1, b'0001', 2, 0, '', '', 1, '', '2019-07-31 18:07:51', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:25:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ad-371a-50d1-096d-026cac03ce03', '', 0, '39ef59aa-b114-d3dd-204a-910bd47e406d', '代码生成', '', '', '', 'develop', '', '', 1, b'0001', 0, -1, '', '', 1, '', '2019-07-31 18:08:11', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:11', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ad-722e-47cf-b3a5-139de23f97a0', 'CodeGenerator', 1, '39ef59ad-371a-50d1-096d-026cac03ce03', '项目列表', 'codegenerator_project', '', '', 'project', '', '', 2, b'0001', 0, 0, '', '', 1, '', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef59ad-84f6-b1d2-9078-4a2654c4e9bc', 'CodeGenerator', 1, '39ef59ad-371a-50d1-096d-026cac03ce03', '枚举列表', 'codegenerator_enum', '', '', 'tag', '', '', 2, b'0001', 0, 0, '', '', 1, '', '2019-07-31 18:08:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 18:08:31', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef5a08-1a7b-0338-9dfb-472081085bb0', 'Admin', 1, '39ef59aa-7dda-ed60-475d-a8c29608812a', '图标管理', 'admin_icon', '', '', 'icon', '', '', 1, b'0001', 3, 0, '', '', 1, '', '2019-07-31 19:47:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-31 19:47:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef5a2f-7519-b0e1-5c47-3c3505921f72', '', 2, '00000000-0000-0000-0000-000000000000', 'GitHub', '', '', '', 'github', '', 'https://github.com/iamoldli/NetModular', 0, b'0001', 6, 0, '', '', 1, '', '2019-07-31 20:30:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-06 18:14:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef5e58-a4f9-9748-cadf-44779fc5f2be', '', 0, '00000000-0000-0000-0000-000000000000', '人事档案', '', '', '', 'user', '', '', 0, b'0001', 0, -1, '', '', 1, '', '2019-08-01 15:53:55', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-06 18:14:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef5e59-091a-7520-b5b2-33e6359e0ad7', 'PersonnelFiles', 1, '39ef5e58-a4f9-9748-cadf-44779fc5f2be', '公司单位', 'personnelfiles_company', '', '', 'enterprise', '', '', 1, b'0001', 0, 0, '', '', 1, '', '2019-08-01 15:54:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:54:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef5e59-b8b4-501e-7ea2-2c594f937ff6', 'PersonnelFiles', 1, '39ef5e58-a4f9-9748-cadf-44779fc5f2be', '部门列表', 'personnelfiles_department', '', '', 'department', '', '', 1, b'0001', 1, 0, '', '', 1, '', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:05', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef5e5a-388d-3e81-a486-ca9ff21b7a81', 'PersonnelFiles', 1, '39ef5e58-a4f9-9748-cadf-44779fc5f2be', '岗位列表', 'personnelfiles_position', '', '', 'post', '', '', 1, b'0001', 2, 0, '', '', 1, '', '2019-08-01 15:55:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:55:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef5e5a-8dc0-bcb5-daa8-5a9054e46ee0', 'PersonnelFiles', 1, '39ef5e58-a4f9-9748-cadf-44779fc5f2be', '用户信息', 'personnelfiles_user', '', '', 'user', '', '', 1, b'0001', 3, 0, '', '', 1, '', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:56:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef66fd-5d77-d191-38d9-c1f93284be8f', '', 0, '00000000-0000-0000-0000-000000000000', '基础数据', '', '', '', 'database', '', '', 0, b'0001', 2, -1, '', '', 1, '', '2019-08-03 08:10:48', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-06 18:14:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef670c-fe08-f104-04f8-9d810f9d9a9b', 'Common', 1, '39ef66fd-5d77-d191-38d9-c1f93284be8f', '区划代码', 'common_area', '', '', 'area', '', '', 1, b'0001', 0, 0, '', '', 1, '', '2019-08-03 08:27:52', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:27:52', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef670d-1edc-91b4-870f-989d4e7e22a0', 'Common', 1, '39ef66fd-5d77-d191-38d9-c1f93284be8f', '附件管理', 'common_attachment', '', '', 'attachment', '', '', 1, b'0001', 1, 0, '', '', 1, '', '2019-08-03 08:28:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:28:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef6c7a-75d3-fba0-5e0d-abf429acd1ab', '', 0, '00000000-0000-0000-0000-000000000000', '任务调度', '', '', '', 'timer', '', '', 0, b'0001', 1, -1, '', '', 1, '', '2019-08-04 09:45:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-06 18:14:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef6c7c-33e3-90e6-d6cb-1313e75df34b', 'Quartz', 1, '39ef6c7a-75d3-fba0-5e0d-abf429acd1ab', '任务组', 'quartz_group', '', '', 'entity', '', '', 1, b'0001', 0, 0, '', '', 1, '', '2019-08-04 09:47:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-04 09:47:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `menu` VALUES ('39ef7205-e44f-cb0e-554d-89587fa12e5c', 'Quartz', 1, '39ef6c7a-75d3-fba0-5e0d-abf429acd1ab', '任务列表', 'quartz_job', '', '', 'list', '', '', 1, b'0001', 1, 0, '', '', 1, '', '2019-08-05 11:35:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for menu_permission
-- ----------------------------
DROP TABLE IF EXISTS `menu_permission`;
CREATE TABLE `menu_permission`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MenuCode` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '菜单编码',
  `PermissionCode` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '权限编码',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 166 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menu_permission
-- ----------------------------
INSERT INTO `menu_permission` VALUES (128, 'admin_moduleinfo', 'admin_moduleinfo_query_get');
INSERT INTO `menu_permission` VALUES (134, 'admin_system', 'admin_system_config_post');
INSERT INTO `menu_permission` VALUES (136, 'admin_auditinfo', 'admin_auditinfo_query_get');
INSERT INTO `menu_permission` VALUES (137, 'codegenerator_project', 'codegenerator_project_query_get');
INSERT INTO `menu_permission` VALUES (138, 'codegenerator_enum', 'codegenerator_enum_query_get');
INSERT INTO `menu_permission` VALUES (141, 'admin_config', 'admin_config_query_get');
INSERT INTO `menu_permission` VALUES (142, 'admin_permission', 'admin_permission_query_get');
INSERT INTO `menu_permission` VALUES (145, 'admin_role', 'admin_role_query_get');
INSERT INTO `menu_permission` VALUES (150, 'admin_account', 'admin_account_query_get');
INSERT INTO `menu_permission` VALUES (151, 'admin_menu', 'admin_menu_query_get');
INSERT INTO `menu_permission` VALUES (152, 'admin_menu', 'admin_menu_tree_get');
INSERT INTO `menu_permission` VALUES (153, 'personnelfiles_company', 'personnelfiles_company_query_get');
INSERT INTO `menu_permission` VALUES (154, 'personnelfiles_department', 'personnelfiles_department_query_get');
INSERT INTO `menu_permission` VALUES (155, 'personnelfiles_user', 'personnelfiles_user_query_get');
INSERT INTO `menu_permission` VALUES (156, 'common_area', 'common_area_query_get');
INSERT INTO `menu_permission` VALUES (157, 'common_area', 'common_area_querychildren_get');
INSERT INTO `menu_permission` VALUES (158, 'common_attachment', 'common_attachment_query_get');
INSERT INTO `menu_permission` VALUES (160, 'quartz_group', 'quartz_group_query_get');
INSERT INTO `menu_permission` VALUES (166, 'quartz_job', 'quartz_job_query_get');

-- ----------------------------
-- Table structure for moduleinfo
-- ----------------------------
DROP TABLE IF EXISTS `moduleinfo`;
CREATE TABLE `moduleinfo`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '编码',
  `Version` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '版本号',
  `Remarks` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of moduleinfo
-- ----------------------------
INSERT INTO `moduleinfo` VALUES ('39ef66fc-fed1-c0b2-6d53-2503be7d5542', '任务调度', 'Quartz', '1.0.0', NULL, '2019-08-03 08:10:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:10:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('3B9B53E7-0CC5-0783-EABE-39EE9AE60DF4', '权限管理', 'Admin', '1.1.9', '', '2019-06-24 17:02:48', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-08-03 08:10:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('5038A392-1A28-3194-E808-39EE9C63DB60', '人事档案', 'PersonnelFiles', '1.1.7', '', '2019-06-24 23:59:50', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-08-03 08:10:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('5D2E02FB-F8AF-6013-58B5-39EE9C63DB58', '代码生成', 'CodeGenerator', '1.1.7', '', '2019-06-24 23:59:50', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-08-03 08:10:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `moduleinfo` VALUES ('9F83548C-9B25-4365-1B15-39EE9C63DB5E', '通用模块', 'Common', '1.1.7', '', '2019-06-24 23:59:50', '2E23D1D9-4A72-ACC2-F6FF-39ED21CB6A4A', '2019-08-03 08:10:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

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
  `HttpMethod` smallint(6) NOT NULL DEFAULT 0 COMMENT '请求方法',
  `Code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '唯一编码',
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of permission
-- ----------------------------
INSERT INTO `permission` VALUES ('39ef5e58-5322-ff47-9417-17e432ac5cd1', '账户管理_绑定角色', 'Admin', 'Account', 'BindRole', 2, 'admin_account_bindrole_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5373-9f9c-4bb6-6c3f7abbea6e', '账户管理_查询', 'Admin', 'Account', 'Query', 0, 'admin_account_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-537a-1c2a-2f08-6376fe5c92a0', '账户管理_添加', 'Admin', 'Account', 'Add', 2, 'admin_account_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5386-1fcd-0447-9709d4206141', '账户管理_编辑', 'Admin', 'Account', 'Edit', 0, 'admin_account_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5392-c10c-a79d-47bdec892b6d', '账户管理_更新', 'Admin', 'Account', 'Update', 2, 'admin_account_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5396-679d-74f4-e6e12d5f2bf0', '账户管理_删除', 'Admin', 'Account', 'Delete', 3, 'admin_account_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53a2-971f-beed-834988ce280f', '账户管理_重置密码', 'Admin', 'Account', 'ResetPassword', 2, 'admin_account_resetpassword_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53a7-40a8-5ac8-4d27497c79fc', '审计信息_查询', 'Admin', 'AuditInfo', 'Query', 0, 'admin_auditinfo_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53b0-0693-a550-7e348bf28755', '审计信息_详情', 'Admin', 'AuditInfo', 'Details', 0, 'admin_auditinfo_details_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53b5-00e3-6237-f06d7a87d3fe', '按钮管理_查询', 'Admin', 'Button', 'Query', 0, 'admin_button_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53b8-ccdf-192e-ade31335efc2', '配置项管理_查询', 'Admin', 'Config', 'Query', 0, 'admin_config_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53c4-7d5e-e497-04291b7e03eb', '配置项管理_添加', 'Admin', 'Config', 'Add', 2, 'admin_config_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53c7-d5a6-0c39-d6974538ae7f', '配置项管理_删除', 'Admin', 'Config', 'Delete', 3, 'admin_config_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53ce-2945-a633-412d64e92f9e', '配置项管理_编辑', 'Admin', 'Config', 'Edit', 0, 'admin_config_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53d6-218b-de94-d00f13a9f858', '配置项管理_修改', 'Admin', 'Config', 'Update', 2, 'admin_config_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53d9-57c2-7556-d390eec6747a', '菜单管理_菜单树', 'Admin', 'Menu', 'Tree', 0, 'admin_menu_tree_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53e6-85ae-6515-ce8fe3ea59c8', '菜单管理_查询菜单列表', 'Admin', 'Menu', 'Query', 0, 'admin_menu_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53e9-2755-1402-8411d03f2b9b', '菜单管理_添加', 'Admin', 'Menu', 'Add', 2, 'admin_menu_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53f2-8262-09c3-11d048d23d5c', '菜单管理_删除', 'Admin', 'Menu', 'Delete', 3, 'admin_menu_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53f6-1428-2eca-a5ac05cc2b7a', '菜单管理_编辑', 'Admin', 'Menu', 'Edit', 0, 'admin_menu_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-53fb-4999-f70d-55cd1cfc4c9a', '菜单管理_更新', 'Admin', 'Menu', 'Update', 2, 'admin_menu_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5403-df5b-1414-b9eb20af8e4f', '菜单管理_更新排序信息', 'Admin', 'Menu', 'Sort', 0, 'admin_menu_sort_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5406-3585-3e9a-8b91f8b8c2bb', '菜单管理_更新排序信息', 'Admin', 'Menu', 'Sort', 2, 'admin_menu_sort_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-540a-ee04-f38b-9e7d12778895', '模块信息_查询', 'Admin', 'ModuleInfo', 'Query', 0, 'admin_moduleinfo_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5411-b392-3551-126b4e8b54ba', '模块信息_同步模块数据', 'Admin', 'ModuleInfo', 'Sync', 2, 'admin_moduleinfo_sync_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5415-de44-8a4f-9feb94840c23', '模块信息_删除', 'Admin', 'ModuleInfo', 'Delete', 3, 'admin_moduleinfo_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5418-ea47-160c-c6a8fe951f69', '权限接口_查询', 'Admin', 'Permission', 'Query', 0, 'admin_permission_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-541c-effd-4433-cf068c5d911f', '权限接口_同步', 'Admin', 'Permission', 'Sync', 2, 'admin_permission_sync_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5421-01e7-7358-d4d0b2e12a83', '角色管理_查询', 'Admin', 'Role', 'Query', 0, 'admin_role_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5424-87b4-a871-007179fb2675', '角色管理_添加', 'Admin', 'Role', 'Add', 2, 'admin_role_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5427-a468-3019-ff3cbd8bf6eb', '角色管理_删除', 'Admin', 'Role', 'Delete', 3, 'admin_role_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-542b-b171-295c-7bfd23285a09', '角色管理_编辑', 'Admin', 'Role', 'Edit', 0, 'admin_role_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-542f-0a32-3cfe-45839ab5bb8a', '角色管理_修改', 'Admin', 'Role', 'Update', 2, 'admin_role_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5433-1556-b04c-b3cba7888069', '角色管理_获取角色的关联菜单列表', 'Admin', 'Role', 'MenuList', 0, 'admin_role_menulist_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5436-6988-6781-f334dcf63cdb', '角色管理_绑定菜单', 'Admin', 'Role', 'BindMenu', 2, 'admin_role_bindmenu_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-543a-f38a-1a91-8ea906b24866', '角色管理_获取角色关联的菜单按钮列表', 'Admin', 'Role', 'MenuButtonList', 0, 'admin_role_menubuttonlist_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-543d-c1b6-10e7-68232156ae5e', '角色管理_绑定菜单按钮', 'Admin', 'Role', 'BindMenuButton', 2, 'admin_role_bindmenubutton_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5440-af62-a0a8-59300e38fa09', '角色管理_下拉列表数据', 'Admin', 'Role', 'Select', 0, 'admin_role_select_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5443-baeb-5a1f-cd8962a97865', '系统_修改系统配置', 'Admin', 'System', 'Config', 2, 'admin_system_config_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5446-6d7d-c5e7-d910b7a1ea83', '系统_上传Logo', 'Admin', 'System', 'UploadLogo', 2, 'admin_system_uploadlogo_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-544a-7668-1dad-865bfe1489b5', '实体管理_查询', 'CodeGenerator', 'Class', 'Query', 0, 'codegenerator_class_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-544e-c62c-485e-d66a6a2110a2', '实体管理_添加', 'CodeGenerator', 'Class', 'Add', 2, 'codegenerator_class_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5451-04ae-5313-6cc153b7f9bb', '实体管理_删除', 'CodeGenerator', 'Class', 'Delete', 3, 'codegenerator_class_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5455-931e-6347-dc884156c40f', '实体管理_编辑', 'CodeGenerator', 'Class', 'Edit', 0, 'codegenerator_class_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5459-7728-f074-71e6a11f0f30', '实体管理_修改', 'CodeGenerator', 'Class', 'Update', 2, 'codegenerator_class_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-545c-4f0a-b7be-c494c83770fc', '实体管理_获取基类类型下拉列表', 'CodeGenerator', 'Class', 'BaseEntityTypeSelect', 0, 'codegenerator_class_baseentitytypeselect_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-545f-8905-c305-0ff107424a48', '枚举管理_查询', 'CodeGenerator', 'Enum', 'Query', 0, 'codegenerator_enum_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5462-dc12-373c-845fc81a23bd', '枚举管理_添加', 'CodeGenerator', 'Enum', 'Add', 2, 'codegenerator_enum_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5465-0409-227e-238fbad72f66', '枚举管理_删除', 'CodeGenerator', 'Enum', 'Delete', 3, 'codegenerator_enum_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5469-2f1b-f0c9-b8dc8e027497', '枚举管理_编辑', 'CodeGenerator', 'Enum', 'Edit', 0, 'codegenerator_enum_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-546c-5cf4-a6d9-382029185dfb', '枚举管理_修改', 'CodeGenerator', 'Enum', 'Update', 2, 'codegenerator_enum_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-546f-b7b3-3b9d-a12b45a4310c', '枚举管理_下拉列表', 'CodeGenerator', 'Enum', 'Select', 0, 'codegenerator_enum_select_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5473-b603-03ed-09ace692f2d6', '枚举项管理_查询', 'CodeGenerator', 'EnumItem', 'Query', 0, 'codegenerator_enumitem_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5476-5af5-3dbc-7c61b5ac88ba', '枚举项管理_添加', 'CodeGenerator', 'EnumItem', 'Add', 2, 'codegenerator_enumitem_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-547a-80e6-5a68-8b5479201299', '枚举项管理_删除', 'CodeGenerator', 'EnumItem', 'Delete', 3, 'codegenerator_enumitem_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-547d-e7dd-889b-c7def5c80fa1', '枚举项管理_编辑', 'CodeGenerator', 'EnumItem', 'Edit', 0, 'codegenerator_enumitem_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5481-a9f8-6093-2a7d6a765365', '枚举项管理_修改', 'CodeGenerator', 'EnumItem', 'Update', 2, 'codegenerator_enumitem_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5485-cc30-7f70-6a02fa4bf65e', '枚举项管理_更新排序信息', 'CodeGenerator', 'EnumItem', 'Sort', 0, 'codegenerator_enumitem_sort_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5489-9b01-5628-5806fabb2e4c', '枚举项管理_更新排序信息', 'CodeGenerator', 'EnumItem', 'Sort', 2, 'codegenerator_enumitem_sort_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-548d-8cf9-61e2-e1de320e3110', '模型属性管理_查询', 'CodeGenerator', 'ModelProperty', 'Query', 0, 'codegenerator_modelproperty_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5491-1957-aca8-017100fab54b', '模型属性管理_添加', 'CodeGenerator', 'ModelProperty', 'Add', 2, 'codegenerator_modelproperty_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5495-0dc4-413c-8ff322439c28', '模型属性管理_删除', 'CodeGenerator', 'ModelProperty', 'Delete', 3, 'codegenerator_modelproperty_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5499-9d66-2a83-6ba4e2d871ca', '模型属性管理_编辑', 'CodeGenerator', 'ModelProperty', 'Edit', 0, 'codegenerator_modelproperty_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-549c-7c17-e248-40905a1a29d9', '模型属性管理_修改', 'CodeGenerator', 'ModelProperty', 'Update', 2, 'codegenerator_modelproperty_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-549f-1f07-97c5-be3ce78a7741', '模型属性管理_更新排序信息', 'CodeGenerator', 'ModelProperty', 'Sort', 0, 'codegenerator_modelproperty_sort_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54a3-d302-b975-6ac96c3f419a', '模型属性管理_更新排序信息', 'CodeGenerator', 'ModelProperty', 'Sort', 2, 'codegenerator_modelproperty_sort_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54a7-f0db-4225-aaed3cef2bfd', '模型属性管理_修改可空状态', 'CodeGenerator', 'ModelProperty', 'UpdateNullable', 2, 'codegenerator_modelproperty_updatenullable_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54aa-54f4-448d-8645865c4f07', '模型属性管理_获取下拉列表', 'CodeGenerator', 'ModelProperty', 'Select', 0, 'codegenerator_modelproperty_select_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54ad-4d93-5bcc-0ff769cfb83a', '模型属性管理_从实体中导入属性', 'CodeGenerator', 'ModelProperty', 'ImportFromEntity', 2, 'codegenerator_modelproperty_importfromentity_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54b1-a5ac-6e9b-5f09a74b6045', '项目管理_查询', 'CodeGenerator', 'Project', 'Query', 0, 'codegenerator_project_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54b8-eb8c-df73-fbda03a70db0', '项目管理_添加', 'CodeGenerator', 'Project', 'Add', 2, 'codegenerator_project_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54bb-e900-4ca2-24864940aa42', '项目管理_删除', 'CodeGenerator', 'Project', 'Delete', 3, 'codegenerator_project_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54be-3949-6fde-d853eef12c35', '项目管理_编辑', 'CodeGenerator', 'Project', 'Edit', 0, 'codegenerator_project_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54c1-8ed4-067e-f4ef6720dd39', '项目管理_修改', 'CodeGenerator', 'Project', 'Update', 2, 'codegenerator_project_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54c4-c34d-d02a-ad2e0de88e30', '项目管理_生成代码', 'CodeGenerator', 'Project', 'BuildCode', 2, 'codegenerator_project_buildcode_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54c8-1a25-346f-0fc31a7db83e', '实体属性管理_查询', 'CodeGenerator', 'Property', 'Query', 0, 'codegenerator_property_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54cb-7899-dbd3-61a28a4ce21b', '实体属性管理_添加', 'CodeGenerator', 'Property', 'Add', 2, 'codegenerator_property_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54ce-32f3-faf3-30c5a6925c4e', '实体属性管理_删除', 'CodeGenerator', 'Property', 'Delete', 3, 'codegenerator_property_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54d1-c155-ccfe-ad31027e3d62', '实体属性管理_编辑', 'CodeGenerator', 'Property', 'Edit', 0, 'codegenerator_property_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54d5-f7d3-4fda-2afabd65c613', '实体属性管理_修改', 'CodeGenerator', 'Property', 'Update', 2, 'codegenerator_property_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54d8-8407-9f83-970c63117c08', '实体属性管理_获取属性类型下拉列表', 'CodeGenerator', 'Property', 'PropertyTypeSelect', 0, 'codegenerator_property_propertytypeselect_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54db-4b8f-88d7-d4d0bd693cad', '实体属性管理_更新排序信息', 'CodeGenerator', 'Property', 'Sort', 0, 'codegenerator_property_sort_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54e0-9c06-d77b-d2c5ffdf7d69', '实体属性管理_更新排序信息', 'CodeGenerator', 'Property', 'Sort', 2, 'codegenerator_property_sort_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54e4-0817-21f7-8e957b4bcb3e', '实体属性管理_修改可空状态', 'CodeGenerator', 'Property', 'UpdateNullable', 2, 'codegenerator_property_updatenullable_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54e9-16cc-02ba-ec4afa6fc79e', '实体属性管理_修改列表显示状态', 'CodeGenerator', 'Property', 'UpdateShowInList', 2, 'codegenerator_property_updateshowinlist_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54ed-9eca-75f8-e5b5b79b04a1', '实体属性管理_获取下拉列表', 'CodeGenerator', 'Property', 'Select', 0, 'codegenerator_property_select_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54f0-fe92-e4a3-7f068477c553', '区划代码管理_查询', 'Common', 'Area', 'Query', 0, 'common_area_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54f3-93bd-6f32-d4b1d8cf8c61', '区划代码管理_添加', 'Common', 'Area', 'Add', 2, 'common_area_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54f7-012b-307f-29b716829646', '区划代码管理_删除', 'Common', 'Area', 'Delete', 3, 'common_area_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54fa-3398-bcfb-c390b3410d6a', '区划代码管理_编辑', 'Common', 'Area', 'Edit', 0, 'common_area_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-54fd-c378-cbc3-66230643cd06', '区划代码管理_修改', 'Common', 'Area', 'Update', 2, 'common_area_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5500-f886-cfb0-8113061e64d4', '区划代码管理_查询子节点', 'Common', 'Area', 'QueryChildren', 0, 'common_area_querychildren_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5503-e592-0c80-6f17f2ef199f', '附件表管理_查询', 'Common', 'Attachment', 'Query', 0, 'common_attachment_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5507-9fe8-6b1e-4cf12f0ba3b9', '附件表管理_上传', 'Common', 'Attachment', 'Upload', 2, 'common_attachment_upload_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-550a-3c91-00ac-a2254c8f569b', '附件表管理_下载', 'Common', 'Attachment', 'Download', 0, 'common_attachment_download_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-550e-025f-643a-49371c3ea76c', '附件表管理_导出', 'Common', 'Attachment', 'Export', 0, 'common_attachment_export_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5511-20a1-6f0e-7db138cf28ad', '字典管理_查询', 'Common', 'Dict', 'Query', 0, 'common_dict_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5514-7c53-07c5-00302f2b519b', '字典管理_添加', 'Common', 'Dict', 'Add', 2, 'common_dict_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5517-821d-1f37-6612df77d901', '字典管理_删除', 'Common', 'Dict', 'Delete', 3, 'common_dict_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-551b-48fd-97d4-c4396b56a57d', '字典管理_编辑', 'Common', 'Dict', 'Edit', 0, 'common_dict_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-551f-252c-84e9-2a0f03b3b5f8', '字典管理_修改', 'Common', 'Dict', 'Update', 2, 'common_dict_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5522-126c-b0a5-78bf9589135a', '多媒体管理_查询', 'Common', 'MediaType', 'Query', 0, 'common_mediatype_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5526-2889-a0c5-821b6c9f7b79', '多媒体管理_添加', 'Common', 'MediaType', 'Add', 2, 'common_mediatype_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-552a-029f-bbc2-9056e1e77abb', '多媒体管理_删除', 'Common', 'MediaType', 'Delete', 3, 'common_mediatype_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-552d-6ffe-808e-15ebae2e7e12', '多媒体管理_编辑', 'Common', 'MediaType', 'Edit', 0, 'common_mediatype_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5530-0f89-2d66-82cbb58270ea', '多媒体管理_修改', 'Common', 'MediaType', 'Update', 2, 'common_mediatype_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5533-3ba7-4040-36a7d90fd553', '公司单位管理_查询', 'PersonnelFiles', 'Company', 'Query', 0, 'personnelfiles_company_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5536-04b1-29fb-82ba411ef409', '公司单位管理_添加', 'PersonnelFiles', 'Company', 'Add', 2, 'personnelfiles_company_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-553a-d34f-ed83-92559faf8b3e', '公司单位管理_删除', 'PersonnelFiles', 'Company', 'Delete', 3, 'personnelfiles_company_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-553e-e70b-0bd7-88f46d3f7ac7', '公司单位管理_编辑', 'PersonnelFiles', 'Company', 'Edit', 0, 'personnelfiles_company_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5542-7137-4dd3-50b21ef24130', '公司单位管理_修改', 'PersonnelFiles', 'Company', 'Update', 2, 'personnelfiles_company_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5545-5114-a815-e4dc15bdf101', '公司单位管理_下拉列表', 'PersonnelFiles', 'Company', 'Select', 0, 'personnelfiles_company_select_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5549-f632-3bc3-71090edf6586', '部门管理_部门树', 'PersonnelFiles', 'Department', 'Tree', 0, 'personnelfiles_department_tree_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-554e-1538-44ce-8caee21153ef', '部门管理_查询', 'PersonnelFiles', 'Department', 'Query', 0, 'personnelfiles_department_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5552-09cd-a009-a77b889dacb4', '部门管理_添加', 'PersonnelFiles', 'Department', 'Add', 2, 'personnelfiles_department_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5556-fae8-4519-b2c623cb1515', '部门管理_删除', 'PersonnelFiles', 'Department', 'Delete', 3, 'personnelfiles_department_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5559-6164-5458-26708a5f1fd1', '部门管理_编辑', 'PersonnelFiles', 'Department', 'Edit', 0, 'personnelfiles_department_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-555d-ca17-862f-8aa1df26149b', '部门管理_修改', 'PersonnelFiles', 'Department', 'Update', 2, 'personnelfiles_department_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5560-1020-0195-8251e02e3973', '岗位管理_查询', 'PersonnelFiles', 'Position', 'Query', 0, 'personnelfiles_position_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5563-e45c-6c06-5df65b817260', '岗位管理_添加', 'PersonnelFiles', 'Position', 'Add', 2, 'personnelfiles_position_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5567-9888-0b8e-f51981f5ad38', '岗位管理_删除', 'PersonnelFiles', 'Position', 'Delete', 3, 'personnelfiles_position_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5569-607d-ccb2-2a39d83c2593', '岗位管理_编辑', 'PersonnelFiles', 'Position', 'Edit', 0, 'personnelfiles_position_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-556d-9424-7b26-5374d2d5c23a', '岗位管理_修改', 'PersonnelFiles', 'Position', 'Update', 2, 'personnelfiles_position_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5570-ccf9-48e1-c578a91d9f81', '岗位管理_下拉列表', 'PersonnelFiles', 'Position', 'Select', 0, 'personnelfiles_position_select_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5573-dc14-d98f-99928ac1f32f', '用户信息管理_查询', 'PersonnelFiles', 'User', 'Query', 0, 'personnelfiles_user_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5576-601d-648b-72b9dd364f40', '用户信息管理_添加', 'PersonnelFiles', 'User', 'Add', 2, 'personnelfiles_user_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-557a-2bcf-6fbf-0ef0a78626e9', '用户信息管理_删除', 'PersonnelFiles', 'User', 'Delete', 3, 'personnelfiles_user_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-557d-1e71-c9fc-9a3dc912f35a', '用户信息管理_编辑', 'PersonnelFiles', 'User', 'Edit', 0, 'personnelfiles_user_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5580-3e4b-3433-4bc106e66ad3', '用户信息管理_修改', 'PersonnelFiles', 'User', 'Update', 2, 'personnelfiles_user_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5584-9e2e-2d34-22fb0804a3e0', '用户信息管理_上传照片', 'PersonnelFiles', 'User', 'UploadPicture', 2, 'personnelfiles_user_uploadpicture_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5587-cd02-2fbc-2d385138f0ec', '用户信息管理_编辑联系信息', 'PersonnelFiles', 'User', 'EditContact', 0, 'personnelfiles_user_editcontact_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-558a-32ce-b841-e4ac695231fc', '用户信息管理_修改联系信息', 'PersonnelFiles', 'User', 'UpdateContact', 2, 'personnelfiles_user_updatecontact_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-558e-551b-3990-794d50a7c21d', '用户信息管理_联系信息详情', 'PersonnelFiles', 'User', 'ContactDetails', 0, 'personnelfiles_user_contactdetails_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5592-ebe7-a684-c1153684a46f', '用户教育经历管理_查询', 'PersonnelFiles', 'UserEducationHistory', 'Query', 0, 'personnelfiles_usereducationhistory_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5595-595f-936a-ae52587228ba', '用户教育经历管理_添加', 'PersonnelFiles', 'UserEducationHistory', 'Add', 2, 'personnelfiles_usereducationhistory_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-5598-f8f5-7d1c-130c57e0318b', '用户教育经历管理_删除', 'PersonnelFiles', 'UserEducationHistory', 'Delete', 3, 'personnelfiles_usereducationhistory_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-559b-f9fe-d906-76fd6177d886', '用户教育经历管理_编辑', 'PersonnelFiles', 'UserEducationHistory', 'Edit', 0, 'personnelfiles_usereducationhistory_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-559f-b9fe-6636-dbc8f7bc51cd', '用户教育经历管理_修改', 'PersonnelFiles', 'UserEducationHistory', 'Update', 2, 'personnelfiles_usereducationhistory_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-55a2-3d66-86b4-ec4d4d58b44b', '用户工作经历管理_查询', 'PersonnelFiles', 'UserWorkHistory', 'Query', 0, 'personnelfiles_userworkhistory_query_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-55a5-fd1a-0b70-9c596118a131', '用户工作经历管理_添加', 'PersonnelFiles', 'UserWorkHistory', 'Add', 2, 'personnelfiles_userworkhistory_add_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-55a8-00fb-b8fa-2a09119029f8', '用户工作经历管理_删除', 'PersonnelFiles', 'UserWorkHistory', 'Delete', 3, 'personnelfiles_userworkhistory_delete_delete', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-55ab-2648-f65e-2f7c19615d3c', '用户工作经历管理_编辑', 'PersonnelFiles', 'UserWorkHistory', 'Edit', 0, 'personnelfiles_userworkhistory_edit_get', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef5e58-55af-fe0f-8c12-9a0aff7e1439', '用户工作经历管理_修改', 'PersonnelFiles', 'UserWorkHistory', 'Update', 2, 'personnelfiles_userworkhistory_update_post', '2019-08-01 15:53:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-24f8-2c7f-8c44-0b7106fd26bf', '任务组管理_查询', 'Quartz', 'Group', 'Query', 0, 'quartz_group_query_get', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-2505-df49-5bf5-f0e534f5b527', '任务组管理_添加', 'Quartz', 'Group', 'Add', 2, 'quartz_group_add_post', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-250c-8fa0-81c7-3af73eec1b33', '任务组管理_删除', 'Quartz', 'Group', 'Delete', 3, 'quartz_group_delete_delete', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-2512-602d-6d37-330078cb9a5e', '任务组管理_编辑', 'Quartz', 'Group', 'Edit', 0, 'quartz_group_edit_get', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-2518-1fea-8120-b9fb84eadbe7', '任务组管理_修改', 'Quartz', 'Group', 'Update', 2, 'quartz_group_update_post', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-251f-cdc6-f2ee-1966e94aca87', '任务类型管理_查询', 'Quartz', 'JobClass', 'Query', 0, 'quartz_jobclass_query_get', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-2524-0ba8-9e00-463a152fabd0', '任务类型管理_添加', 'Quartz', 'JobClass', 'Add', 2, 'quartz_jobclass_add_post', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-252a-4cdd-e4f4-6d09a521a737', '任务类型管理_删除', 'Quartz', 'JobClass', 'Delete', 3, 'quartz_jobclass_delete_delete', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-2530-6817-2995-37b99e1e6422', '任务类型管理_编辑', 'Quartz', 'JobClass', 'Edit', 0, 'quartz_jobclass_edit_get', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-2536-4634-7747-b3a435465f2e', '任务类型管理_修改', 'Quartz', 'JobClass', 'Update', 2, 'quartz_jobclass_update_post', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-253a-e1e0-27d3-017b05f1c2a5', '任务管理_查询', 'Quartz', 'Job', 'Query', 0, 'quartz_job_query_get', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-2541-da18-2677-ce1c0af66216', '任务管理_添加', 'Quartz', 'Job', 'Add', 2, 'quartz_job_add_post', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-2547-4ba9-011f-debb69887837', '任务管理_删除', 'Quartz', 'Job', 'Delete', 3, 'quartz_job_delete_delete', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-254d-5382-28b5-e5dfcc109d99', '任务管理_编辑', 'Quartz', 'Job', 'Edit', 0, 'quartz_job_edit_get', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-04 09:43:08', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef66fd-2554-4526-0a54-7eadf894da6a', '任务管理_修改', 'Quartz', 'Job', 'Update', 2, 'quartz_job_update_post', '2019-08-03 08:10:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-04 09:43:08', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef7349-c805-143d-1027-aff9e5898963', '任务组管理_Select', 'Quartz', 'Group', 'Select', 0, 'quartz_group_select_get', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef7349-c82d-292d-9589-c4d837f2939f', '任务管理_暂停', 'Quartz', 'Job', 'Pause', 2, 'quartz_job_pause_post', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-08-05 17:29:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `permission` VALUES ('39ef76e9-9a84-d3fa-b539-054856f6431b', '任务管理_启动', 'Quartz', 'Job', 'Resume', 2, 'quartz_job_resume_post', '2019-08-06 10:23:07', '00000000-0000-0000-0000-000000000000', '2019-08-06 10:23:07', '00000000-0000-0000-0000-000000000000');
INSERT INTO `permission` VALUES ('39ef77d2-79f8-aac4-a5c7-813371f2798e', '任务管理_日志', 'Quartz', 'Job', 'Log', 0, 'quartz_job_log_get', '2019-08-06 14:37:29', '00000000-0000-0000-0000-000000000000', '2019-08-06 14:37:29', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Remarks` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `IsSpecified` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否是指定的角色，如果是其它模块指定的，不允许删除修改',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  `Deleted` bit(4) NOT NULL COMMENT '已删除',
  `DeletedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '删除时间',
  `DeletedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '删除人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES ('39eeaf22-855c-9467-d478-06b8a4a085b2', '超级管理员', '超级管理员', b'0', '2019-06-28 15:21:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-28 15:21:15', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', b'0000', '2019-06-28 15:21:15', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for role_menu
-- ----------------------------
DROP TABLE IF EXISTS `role_menu`;
CREATE TABLE `role_menu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色编号',
  `MenuId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '菜单编号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 422 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role_menu
-- ----------------------------
INSERT INTO `role_menu` VALUES (395, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e58-a4f9-9748-cadf-44779fc5f2be');
INSERT INTO `role_menu` VALUES (396, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-091a-7520-b5b2-33e6359e0ad7');
INSERT INTO `role_menu` VALUES (397, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-b8b4-501e-7ea2-2c594f937ff6');
INSERT INTO `role_menu` VALUES (398, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e5a-388d-3e81-a486-ca9ff21b7a81');
INSERT INTO `role_menu` VALUES (399, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e5a-8dc0-bcb5-daa8-5a9054e46ee0');
INSERT INTO `role_menu` VALUES (400, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef66fd-5d77-d191-38d9-c1f93284be8f');
INSERT INTO `role_menu` VALUES (401, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef670c-fe08-f104-04f8-9d810f9d9a9b');
INSERT INTO `role_menu` VALUES (402, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef670d-1edc-91b4-870f-989d4e7e22a0');
INSERT INTO `role_menu` VALUES (403, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef6c7a-75d3-fba0-5e0d-abf429acd1ab');
INSERT INTO `role_menu` VALUES (404, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef6c7c-33e3-90e6-d6cb-1313e75df34b');
INSERT INTO `role_menu` VALUES (405, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef7205-e44f-cb0e-554d-89587fa12e5c');
INSERT INTO `role_menu` VALUES (406, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59aa-4e53-26d4-d604-940d629d38c5');
INSERT INTO `role_menu` VALUES (407, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59aa-dd30-ba16-96a8-addb63f85f27');
INSERT INTO `role_menu` VALUES (408, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-3714-82b8-5bca-1650aab0d3a4');
INSERT INTO `role_menu` VALUES (409, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-5b74-986c-5fee-b9daecf53951');
INSERT INTO `role_menu` VALUES (410, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-8bcd-bdce-1a20-b5bae35508e5');
INSERT INTO `role_menu` VALUES (411, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-adba-82f7-47af-bda8fa2fe0bb');
INSERT INTO `role_menu` VALUES (412, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59aa-7dda-ed60-475d-a8c29608812a');
INSERT INTO `role_menu` VALUES (413, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ac-7771-b6ab-2ffd-d55d0627f7f3');
INSERT INTO `role_menu` VALUES (414, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ac-bd76-459d-f146-3617a599cdf4');
INSERT INTO `role_menu` VALUES (415, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ac-e8da-dff4-d41d-a2c334e75535');
INSERT INTO `role_menu` VALUES (416, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5a08-1a7b-0338-9dfb-472081085bb0');
INSERT INTO `role_menu` VALUES (417, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59aa-b114-d3dd-204a-910bd47e406d');
INSERT INTO `role_menu` VALUES (418, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-371a-50d1-096d-026cac03ce03');
INSERT INTO `role_menu` VALUES (419, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-722e-47cf-b3a5-139de23f97a0');
INSERT INTO `role_menu` VALUES (420, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-84f6-b1d2-9078-4a2654c4e9bc');
INSERT INTO `role_menu` VALUES (421, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5a2f-7519-b0e1-5c47-3c3505921f72');

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
) ENGINE = InnoDB AUTO_INCREMENT = 183 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role_menu_button
-- ----------------------------
INSERT INTO `role_menu_button` VALUES (98, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59aa-dd30-ba16-96a8-addb63f85f27', '39ef59aa-dd5f-95b8-b24f-7560f1e989a5');
INSERT INTO `role_menu_button` VALUES (99, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59aa-dd30-ba16-96a8-addb63f85f27', '39ef59aa-dd69-ce78-1f8e-18713fb581db');
INSERT INTO `role_menu_button` VALUES (100, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-3714-82b8-5bca-1650aab0d3a4', '39ef59ab-3732-f4ca-b606-a92479a69b3b');
INSERT INTO `role_menu_button` VALUES (116, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ac-e8da-dff4-d41d-a2c334e75535', '39ef59ac-e8f8-d10a-d462-bf53de5eb381');
INSERT INTO `role_menu_button` VALUES (117, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-722e-47cf-b3a5-139de23f97a0', '39ef59ad-7247-ad49-1a0f-bbe1ce5780d6');
INSERT INTO `role_menu_button` VALUES (118, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-722e-47cf-b3a5-139de23f97a0', '39ef59ad-724c-1205-5067-0b955db55169');
INSERT INTO `role_menu_button` VALUES (119, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-722e-47cf-b3a5-139de23f97a0', '39ef59ad-7253-e5e7-8d94-e178089cec0f');
INSERT INTO `role_menu_button` VALUES (120, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-722e-47cf-b3a5-139de23f97a0', '39ef59ad-7255-08dd-610d-f61b7a50a2d7');
INSERT INTO `role_menu_button` VALUES (121, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-84f6-b1d2-9078-4a2654c4e9bc', '39ef59ad-8516-cea4-728b-bdf350aa111e');
INSERT INTO `role_menu_button` VALUES (122, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-84f6-b1d2-9078-4a2654c4e9bc', '39ef59ad-8525-b7ba-a300-495ff91e4075');
INSERT INTO `role_menu_button` VALUES (123, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ad-84f6-b1d2-9078-4a2654c4e9bc', '39ef59ad-8528-5821-4236-d95011a1e402');
INSERT INTO `role_menu_button` VALUES (124, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-3714-82b8-5bca-1650aab0d3a4', '39ef59b9-bdd4-7aee-0ba3-cc1a2ee4593a');
INSERT INTO `role_menu_button` VALUES (129, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-8bcd-bdce-1a20-b5bae35508e5', '39ef59b9-e0f6-2e7b-07da-c2d1167b50e2');
INSERT INTO `role_menu_button` VALUES (130, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-8bcd-bdce-1a20-b5bae35508e5', '39ef59b9-e0fd-34b9-300d-f57c170d17d7');
INSERT INTO `role_menu_button` VALUES (131, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-8bcd-bdce-1a20-b5bae35508e5', '39ef59b9-e103-d474-f0bf-4598bc76e53f');
INSERT INTO `role_menu_button` VALUES (132, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-8bcd-bdce-1a20-b5bae35508e5', '39ef59b9-e105-acc8-24d9-59f3b301cf8e');
INSERT INTO `role_menu_button` VALUES (133, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ac-bd76-459d-f146-3617a599cdf4', '39ef59b9-6cf9-a922-7053-07b39e66d2ac');
INSERT INTO `role_menu_button` VALUES (134, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ac-bd76-459d-f146-3617a599cdf4', '39ef59b9-6d01-0f0e-b86a-30b358a8d86c');
INSERT INTO `role_menu_button` VALUES (135, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ac-bd76-459d-f146-3617a599cdf4', '39ef59b9-6d0a-f50e-6739-0bc1df7b73d1');
INSERT INTO `role_menu_button` VALUES (144, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-5b74-986c-5fee-b9daecf53951', '39ef59be-6fab-9d9b-9368-27692403020d');
INSERT INTO `role_menu_button` VALUES (145, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-5b74-986c-5fee-b9daecf53951', '39ef59be-6fb3-fdab-31fe-64f4c07f2b5b');
INSERT INTO `role_menu_button` VALUES (146, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-5b74-986c-5fee-b9daecf53951', '39ef59be-6fba-17c8-7660-8fbbc7458b11');
INSERT INTO `role_menu_button` VALUES (147, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-5b74-986c-5fee-b9daecf53951', '39ef59be-6fbe-5a42-8652-4b6bd66d8aa7');
INSERT INTO `role_menu_button` VALUES (148, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-091a-7520-b5b2-33e6359e0ad7', '39ef5e59-0951-cc41-6079-e3dbe812d9b3');
INSERT INTO `role_menu_button` VALUES (149, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-091a-7520-b5b2-33e6359e0ad7', '39ef5e59-095b-ed9f-5520-5493684a60bb');
INSERT INTO `role_menu_button` VALUES (150, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-091a-7520-b5b2-33e6359e0ad7', '39ef5e59-0960-5812-7cb2-459fb185a52e');
INSERT INTO `role_menu_button` VALUES (151, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-b8b4-501e-7ea2-2c594f937ff6', '39ef5e59-b8d1-289b-7fa1-e7b21f6bc53c');
INSERT INTO `role_menu_button` VALUES (152, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-b8b4-501e-7ea2-2c594f937ff6', '39ef5e59-b8db-c4e1-a562-88770a69f3f6');
INSERT INTO `role_menu_button` VALUES (153, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-b8b4-501e-7ea2-2c594f937ff6', '39ef5e59-b8df-e961-4434-3120e646b510');
INSERT INTO `role_menu_button` VALUES (154, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-b8b4-501e-7ea2-2c594f937ff6', '39ef5e59-b8e1-f381-c7bc-b2115ac0cc45');
INSERT INTO `role_menu_button` VALUES (155, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-b8b4-501e-7ea2-2c594f937ff6', '39ef5e59-b8e4-a1cb-01d1-1fe0e03f6488');
INSERT INTO `role_menu_button` VALUES (156, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-b8b4-501e-7ea2-2c594f937ff6', '39ef5e59-b8ed-6957-30bc-961ce7fdba97');
INSERT INTO `role_menu_button` VALUES (157, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e59-b8b4-501e-7ea2-2c594f937ff6', '39ef5e59-b8f0-3a3f-8dae-455a251a8360');
INSERT INTO `role_menu_button` VALUES (158, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e5a-388d-3e81-a486-ca9ff21b7a81', '39ef5e5a-38b3-333e-c7d9-95fbaad7d2d6');
INSERT INTO `role_menu_button` VALUES (159, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e5a-8dc0-bcb5-daa8-5a9054e46ee0', '39ef5e5a-8de6-4038-2f99-6db4c8036645');
INSERT INTO `role_menu_button` VALUES (160, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e5a-8dc0-bcb5-daa8-5a9054e46ee0', '39ef5e5a-8dee-3f13-ba40-3420b4323799');
INSERT INTO `role_menu_button` VALUES (161, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e5a-8dc0-bcb5-daa8-5a9054e46ee0', '39ef5e5a-8df6-29df-57ed-a4dc969f4e27');
INSERT INTO `role_menu_button` VALUES (162, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e5a-8dc0-bcb5-daa8-5a9054e46ee0', '39ef5e5a-8df9-3e6f-2527-043fba5c643a');
INSERT INTO `role_menu_button` VALUES (163, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef5e5a-8dc0-bcb5-daa8-5a9054e46ee0', '39ef5e5a-8e05-7c17-ce12-84b366ab9e2c');
INSERT INTO `role_menu_button` VALUES (164, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-adba-82f7-47af-bda8fa2fe0bb', '39ef59bd-6012-a1fe-e8d3-35ec6a40b6c4');
INSERT INTO `role_menu_button` VALUES (165, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-adba-82f7-47af-bda8fa2fe0bb', '39ef59bd-6017-0310-363d-e2c63a5b28fb');
INSERT INTO `role_menu_button` VALUES (166, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-adba-82f7-47af-bda8fa2fe0bb', '39ef59bd-601e-89d0-d3de-635df91054be');
INSERT INTO `role_menu_button` VALUES (167, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef59ab-adba-82f7-47af-bda8fa2fe0bb', '39ef59bd-6021-8ec7-2d7a-2f1406388764');
INSERT INTO `role_menu_button` VALUES (168, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef670c-fe08-f104-04f8-9d810f9d9a9b', '39ef66fd-b165-1e3e-52c9-19219ed962bf');
INSERT INTO `role_menu_button` VALUES (169, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef670c-fe08-f104-04f8-9d810f9d9a9b', '39ef66fd-b177-45dc-d79e-0802cd198fed');
INSERT INTO `role_menu_button` VALUES (170, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef670c-fe08-f104-04f8-9d810f9d9a9b', '39ef66fd-b17f-68af-ce08-7f240e480ac0');
INSERT INTO `role_menu_button` VALUES (171, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef670d-1edc-91b4-870f-989d4e7e22a0', '39ef66fd-e397-ca10-df28-ec99046f7f11');
INSERT INTO `role_menu_button` VALUES (172, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef670d-1edc-91b4-870f-989d4e7e22a0', '39ef66fd-e3a3-5366-4e24-613e7ae5f4c7');
INSERT INTO `role_menu_button` VALUES (173, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef6c7c-33e3-90e6-d6cb-1313e75df34b', '39ef6c7a-e661-5b1f-0b54-385ebc66e5de');
INSERT INTO `role_menu_button` VALUES (174, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef6c7c-33e3-90e6-d6cb-1313e75df34b', '39ef6c7a-e670-993d-9b0f-92302d66d3f3');
INSERT INTO `role_menu_button` VALUES (175, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef6c7c-33e3-90e6-d6cb-1313e75df34b', '39ef6c7a-e674-9900-bbef-8b4e1d465ecd');
INSERT INTO `role_menu_button` VALUES (176, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef7205-e44f-cb0e-554d-89587fa12e5c', '39ef7205-e481-4691-d1a0-dfe80de18f93');
INSERT INTO `role_menu_button` VALUES (177, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef7205-e44f-cb0e-554d-89587fa12e5c', '39ef7205-e48a-e739-f3c1-f7cd28d3c5a7');
INSERT INTO `role_menu_button` VALUES (178, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef7205-e44f-cb0e-554d-89587fa12e5c', '39ef7205-e48b-b3d8-f3bc-ef56b1c7dcab');
INSERT INTO `role_menu_button` VALUES (179, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef7205-e44f-cb0e-554d-89587fa12e5c', '39ef7349-e2bd-34b4-208d-582b863a9bb2');
INSERT INTO `role_menu_button` VALUES (180, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef7205-e44f-cb0e-554d-89587fa12e5c', '39ef76e9-b458-f509-5bed-c799d499f937');
INSERT INTO `role_menu_button` VALUES (181, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef7205-e44f-cb0e-554d-89587fa12e5c', '39ef77d2-9623-1956-ca1d-572f5d537f76');
INSERT INTO `role_menu_button` VALUES (182, '39eeaf22-855c-9467-d478-06b8a4a085b2', '39ef7205-e44f-cb0e-554d-89587fa12e5c', '39ef77f5-9b56-ab8b-48cd-27977189c430');

SET FOREIGN_KEY_CHECKS = 1;
