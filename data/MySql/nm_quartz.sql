/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80015
 Source Host           : localhost:3306
 Source Schema         : nm_quartz

 Target Server Type    : MySQL
 Target Server Version : 80015
 File Encoding         : 65001

 Date: 06/08/2019 18:17:51
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for group
-- ----------------------------
DROP TABLE IF EXISTS `group`;
CREATE TABLE `group`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '主键',
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '编码',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for job
-- ----------------------------
DROP TABLE IF EXISTS `job`;
CREATE TABLE `job`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '主键',
  `ModuleCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '所属模块',
  `JobKey` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '任务唯一键',
  `Group` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '任务组',
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '任务名称',
  `Code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '任务编码',
  `JobClass` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '任务类',
  `TriggerType` tinyint(3) NOT NULL COMMENT '触发器类型',
  `Interval` int(11) NOT NULL COMMENT '简单触发器时间间隔',
  `RepeatCount` int(11) NOT NULL COMMENT '简单触发器重复次数，0表示无限',
  `Cron` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Cron表达式',
  `BeginDate` datetime(0) NOT NULL COMMENT '开始日期',
  `EndDate` datetime(0) NOT NULL COMMENT '结束日期',
  `Status` tinyint(3) NOT NULL COMMENT '状态',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for job_log
-- ----------------------------
DROP TABLE IF EXISTS `job_log`;
CREATE TABLE `job_log`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `JobId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '任务编号',
  `Type` smallint(1) NOT NULL DEFAULT 0 COMMENT '类型',
  `Msg` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '日志信息',
  `CreatedTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of job_log
-- ----------------------------
INSERT INTO `job_log` VALUES (56, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务开始', '2019-08-06 16:04:52');
INSERT INTO `job_log` VALUES (57, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '当前任务编号：39ef7822-707e-5233-3095-f9ae31db9e1a,执行时间：2019/8/6 16:04:52', '2019-08-06 16:04:52');
INSERT INTO `job_log` VALUES (58, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务结束', '2019-08-06 16:04:52');
INSERT INTO `job_log` VALUES (59, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务开始', '2019-08-06 16:04:55');
INSERT INTO `job_log` VALUES (60, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '当前任务编号：39ef7822-707e-5233-3095-f9ae31db9e1a,执行时间：2019/8/6 16:04:54', '2019-08-06 16:04:55');
INSERT INTO `job_log` VALUES (61, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务结束', '2019-08-06 16:04:55');
INSERT INTO `job_log` VALUES (62, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务开始', '2019-08-06 16:05:00');
INSERT INTO `job_log` VALUES (63, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '当前任务编号：39ef7822-707e-5233-3095-f9ae31db9e1a,执行时间：2019/8/6 16:04:59', '2019-08-06 16:05:00');
INSERT INTO `job_log` VALUES (64, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务结束', '2019-08-06 16:05:00');
INSERT INTO `job_log` VALUES (65, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务开始', '2019-08-06 16:05:05');
INSERT INTO `job_log` VALUES (66, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '当前任务编号：39ef7822-707e-5233-3095-f9ae31db9e1a,执行时间：2019/8/6 16:05:04', '2019-08-06 16:05:05');
INSERT INTO `job_log` VALUES (67, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务结束', '2019-08-06 16:05:05');
INSERT INTO `job_log` VALUES (68, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务开始', '2019-08-06 16:05:10');
INSERT INTO `job_log` VALUES (69, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '当前任务编号：39ef7822-707e-5233-3095-f9ae31db9e1a,执行时间：2019/8/6 16:05:09', '2019-08-06 16:05:10');
INSERT INTO `job_log` VALUES (70, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务结束', '2019-08-06 16:05:10');
INSERT INTO `job_log` VALUES (71, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务开始', '2019-08-06 16:06:57');
INSERT INTO `job_log` VALUES (72, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '当前任务编号：39ef7822-707e-5233-3095-f9ae31db9e1a,执行时间：2019/8/6 16:06:57', '2019-08-06 16:06:57');
INSERT INTO `job_log` VALUES (73, '39ef7822-707e-5233-3095-f9ae31db9e1a', 0, '任务结束', '2019-08-06 16:06:57');
INSERT INTO `job_log` VALUES (74, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:12:32');
INSERT INTO `job_log` VALUES (75, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:12:31', '2019-08-06 16:12:32');
INSERT INTO `job_log` VALUES (76, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:12:32');
INSERT INTO `job_log` VALUES (77, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:12:34');
INSERT INTO `job_log` VALUES (78, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:12:34', '2019-08-06 16:12:34');
INSERT INTO `job_log` VALUES (79, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:12:34');
INSERT INTO `job_log` VALUES (80, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:12:39');
INSERT INTO `job_log` VALUES (81, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:12:39', '2019-08-06 16:12:39');
INSERT INTO `job_log` VALUES (82, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:12:39');
INSERT INTO `job_log` VALUES (83, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:12:44');
INSERT INTO `job_log` VALUES (84, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:12:44', '2019-08-06 16:12:44');
INSERT INTO `job_log` VALUES (85, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:12:44');
INSERT INTO `job_log` VALUES (86, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:12:49');
INSERT INTO `job_log` VALUES (87, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:12:49', '2019-08-06 16:12:49');
INSERT INTO `job_log` VALUES (88, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:12:49');
INSERT INTO `job_log` VALUES (89, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:13:03');
INSERT INTO `job_log` VALUES (90, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:13:02', '2019-08-06 16:13:03');
INSERT INTO `job_log` VALUES (91, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:13:03');
INSERT INTO `job_log` VALUES (92, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:14:14');
INSERT INTO `job_log` VALUES (93, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:14:13', '2019-08-06 16:14:14');
INSERT INTO `job_log` VALUES (94, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:14:14');
INSERT INTO `job_log` VALUES (95, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:14:14');
INSERT INTO `job_log` VALUES (96, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:14:13', '2019-08-06 16:14:14');
INSERT INTO `job_log` VALUES (97, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:14:14');
INSERT INTO `job_log` VALUES (98, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:15:03');
INSERT INTO `job_log` VALUES (99, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:15:03', '2019-08-06 16:15:03');
INSERT INTO `job_log` VALUES (100, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:15:03');
INSERT INTO `job_log` VALUES (101, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:15:34');
INSERT INTO `job_log` VALUES (102, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:15:34', '2019-08-06 16:15:34');
INSERT INTO `job_log` VALUES (103, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:15:34');
INSERT INTO `job_log` VALUES (104, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:15:34');
INSERT INTO `job_log` VALUES (105, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:15:34', '2019-08-06 16:15:34');
INSERT INTO `job_log` VALUES (106, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:15:34');
INSERT INTO `job_log` VALUES (107, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:15:39');
INSERT INTO `job_log` VALUES (108, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:15:38', '2019-08-06 16:15:39');
INSERT INTO `job_log` VALUES (109, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:15:39');
INSERT INTO `job_log` VALUES (110, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:15:44');
INSERT INTO `job_log` VALUES (111, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:15:43', '2019-08-06 16:15:44');
INSERT INTO `job_log` VALUES (112, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:15:44');
INSERT INTO `job_log` VALUES (113, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:15:49');
INSERT INTO `job_log` VALUES (114, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:15:48', '2019-08-06 16:15:49');
INSERT INTO `job_log` VALUES (115, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:15:49');
INSERT INTO `job_log` VALUES (116, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:15:54');
INSERT INTO `job_log` VALUES (117, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:15:53', '2019-08-06 16:15:54');
INSERT INTO `job_log` VALUES (118, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:15:54');
INSERT INTO `job_log` VALUES (119, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:15:59');
INSERT INTO `job_log` VALUES (120, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:15:58', '2019-08-06 16:15:59');
INSERT INTO `job_log` VALUES (121, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:15:59');
INSERT INTO `job_log` VALUES (122, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:16:04');
INSERT INTO `job_log` VALUES (123, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:16:03', '2019-08-06 16:16:04');
INSERT INTO `job_log` VALUES (124, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:16:04');
INSERT INTO `job_log` VALUES (125, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:16:09');
INSERT INTO `job_log` VALUES (126, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:16:08', '2019-08-06 16:16:09');
INSERT INTO `job_log` VALUES (127, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:16:09');
INSERT INTO `job_log` VALUES (128, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:16:14');
INSERT INTO `job_log` VALUES (129, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:16:13', '2019-08-06 16:16:14');
INSERT INTO `job_log` VALUES (130, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:16:14');
INSERT INTO `job_log` VALUES (131, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:16:19');
INSERT INTO `job_log` VALUES (132, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:16:18', '2019-08-06 16:16:19');
INSERT INTO `job_log` VALUES (133, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:16:19');
INSERT INTO `job_log` VALUES (134, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:16:24');
INSERT INTO `job_log` VALUES (135, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:16:23', '2019-08-06 16:16:24');
INSERT INTO `job_log` VALUES (136, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:16:24');
INSERT INTO `job_log` VALUES (137, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:16:29');
INSERT INTO `job_log` VALUES (138, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:16:28', '2019-08-06 16:16:29');
INSERT INTO `job_log` VALUES (139, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:16:29');
INSERT INTO `job_log` VALUES (140, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:16:31');
INSERT INTO `job_log` VALUES (141, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:16:31', '2019-08-06 16:16:31');
INSERT INTO `job_log` VALUES (142, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:16:31');
INSERT INTO `job_log` VALUES (143, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:16:36');
INSERT INTO `job_log` VALUES (144, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:16:36', '2019-08-06 16:16:36');
INSERT INTO `job_log` VALUES (145, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:16:36');
INSERT INTO `job_log` VALUES (146, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务开始', '2019-08-06 16:16:41');
INSERT INTO `job_log` VALUES (147, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '当前任务编号：39ef7829-7199-c2e7-3dca-c0018209e957,执行时间：2019/8/6 16:16:41', '2019-08-06 16:16:41');
INSERT INTO `job_log` VALUES (148, '39ef7829-7199-c2e7-3dca-c0018209e957', 0, '任务结束', '2019-08-06 16:16:41');

-- ----------------------------
-- Table structure for qrtz_blob_triggers
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_blob_triggers`;
CREATE TABLE `qrtz_blob_triggers`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `BLOB_DATA` blob NULL,
  PRIMARY KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) USING BTREE,
  INDEX `SCHED_NAME`(`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) USING BTREE,
  CONSTRAINT `qrtz_blob_triggers_ibfk_1` FOREIGN KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) REFERENCES `qrtz_triggers` (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_calendars
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_calendars`;
CREATE TABLE `qrtz_calendars`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CALENDAR_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CALENDAR` blob NOT NULL,
  PRIMARY KEY (`SCHED_NAME`, `CALENDAR_NAME`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_cron_triggers
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_cron_triggers`;
CREATE TABLE `qrtz_cron_triggers`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CRON_EXPRESSION` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TIME_ZONE_ID` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) USING BTREE,
  CONSTRAINT `qrtz_cron_triggers_ibfk_1` FOREIGN KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) REFERENCES `qrtz_triggers` (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_fired_triggers
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_fired_triggers`;
CREATE TABLE `qrtz_fired_triggers`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ENTRY_ID` varchar(140) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `INSTANCE_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `FIRED_TIME` bigint(19) NOT NULL,
  `SCHED_TIME` bigint(19) NOT NULL,
  `PRIORITY` int(11) NOT NULL,
  `STATE` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `JOB_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `JOB_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IS_NONCONCURRENT` tinyint(1) NULL DEFAULT NULL,
  `REQUESTS_RECOVERY` tinyint(1) NULL DEFAULT NULL,
  PRIMARY KEY (`SCHED_NAME`, `ENTRY_ID`) USING BTREE,
  INDEX `IDX_QRTZ_FT_TRIG_INST_NAME`(`SCHED_NAME`, `INSTANCE_NAME`) USING BTREE,
  INDEX `IDX_QRTZ_FT_INST_JOB_REQ_RCVRY`(`SCHED_NAME`, `INSTANCE_NAME`, `REQUESTS_RECOVERY`) USING BTREE,
  INDEX `IDX_QRTZ_FT_J_G`(`SCHED_NAME`, `JOB_NAME`, `JOB_GROUP`) USING BTREE,
  INDEX `IDX_QRTZ_FT_JG`(`SCHED_NAME`, `JOB_GROUP`) USING BTREE,
  INDEX `IDX_QRTZ_FT_T_G`(`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) USING BTREE,
  INDEX `IDX_QRTZ_FT_TG`(`SCHED_NAME`, `TRIGGER_GROUP`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_job_details
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_job_details`;
CREATE TABLE `qrtz_job_details`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `JOB_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `JOB_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `DESCRIPTION` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `JOB_CLASS_NAME` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `IS_DURABLE` tinyint(1) NOT NULL,
  `IS_NONCONCURRENT` tinyint(1) NOT NULL,
  `IS_UPDATE_DATA` tinyint(1) NOT NULL,
  `REQUESTS_RECOVERY` tinyint(1) NOT NULL,
  `JOB_DATA` blob NULL,
  PRIMARY KEY (`SCHED_NAME`, `JOB_NAME`, `JOB_GROUP`) USING BTREE,
  INDEX `IDX_QRTZ_J_REQ_RECOVERY`(`SCHED_NAME`, `REQUESTS_RECOVERY`) USING BTREE,
  INDEX `IDX_QRTZ_J_GRP`(`SCHED_NAME`, `JOB_GROUP`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_locks
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_locks`;
CREATE TABLE `qrtz_locks`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LOCK_NAME` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`SCHED_NAME`, `LOCK_NAME`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_paused_trigger_grps
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_paused_trigger_grps`;
CREATE TABLE `qrtz_paused_trigger_grps`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`SCHED_NAME`, `TRIGGER_GROUP`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_scheduler_state
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_scheduler_state`;
CREATE TABLE `qrtz_scheduler_state`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `INSTANCE_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LAST_CHECKIN_TIME` bigint(19) NOT NULL,
  `CHECKIN_INTERVAL` bigint(19) NOT NULL,
  PRIMARY KEY (`SCHED_NAME`, `INSTANCE_NAME`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_simple_triggers
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_simple_triggers`;
CREATE TABLE `qrtz_simple_triggers`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `REPEAT_COUNT` bigint(7) NOT NULL,
  `REPEAT_INTERVAL` bigint(12) NOT NULL,
  `TIMES_TRIGGERED` bigint(10) NOT NULL,
  PRIMARY KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) USING BTREE,
  CONSTRAINT `qrtz_simple_triggers_ibfk_1` FOREIGN KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) REFERENCES `qrtz_triggers` (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_simprop_triggers
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_simprop_triggers`;
CREATE TABLE `qrtz_simprop_triggers`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `STR_PROP_1` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `STR_PROP_2` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `STR_PROP_3` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `INT_PROP_1` int(11) NULL DEFAULT NULL,
  `INT_PROP_2` int(11) NULL DEFAULT NULL,
  `LONG_PROP_1` bigint(20) NULL DEFAULT NULL,
  `LONG_PROP_2` bigint(20) NULL DEFAULT NULL,
  `DEC_PROP_1` decimal(13, 4) NULL DEFAULT NULL,
  `DEC_PROP_2` decimal(13, 4) NULL DEFAULT NULL,
  `BOOL_PROP_1` tinyint(1) NULL DEFAULT NULL,
  `BOOL_PROP_2` tinyint(1) NULL DEFAULT NULL,
  `TIME_ZONE_ID` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) USING BTREE,
  CONSTRAINT `qrtz_simprop_triggers_ibfk_1` FOREIGN KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) REFERENCES `qrtz_triggers` (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qrtz_triggers
-- ----------------------------
DROP TABLE IF EXISTS `qrtz_triggers`;
CREATE TABLE `qrtz_triggers`  (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `JOB_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `JOB_GROUP` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `DESCRIPTION` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `NEXT_FIRE_TIME` bigint(19) NULL DEFAULT NULL,
  `PREV_FIRE_TIME` bigint(19) NULL DEFAULT NULL,
  `PRIORITY` int(11) NULL DEFAULT NULL,
  `TRIGGER_STATE` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TRIGGER_TYPE` varchar(8) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `START_TIME` bigint(19) NOT NULL,
  `END_TIME` bigint(19) NULL DEFAULT NULL,
  `CALENDAR_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MISFIRE_INSTR` smallint(2) NULL DEFAULT NULL,
  `JOB_DATA` blob NULL,
  PRIMARY KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) USING BTREE,
  INDEX `IDX_QRTZ_T_J`(`SCHED_NAME`, `JOB_NAME`, `JOB_GROUP`) USING BTREE,
  INDEX `IDX_QRTZ_T_JG`(`SCHED_NAME`, `JOB_GROUP`) USING BTREE,
  INDEX `IDX_QRTZ_T_C`(`SCHED_NAME`, `CALENDAR_NAME`) USING BTREE,
  INDEX `IDX_QRTZ_T_G`(`SCHED_NAME`, `TRIGGER_GROUP`) USING BTREE,
  INDEX `IDX_QRTZ_T_STATE`(`SCHED_NAME`, `TRIGGER_STATE`) USING BTREE,
  INDEX `IDX_QRTZ_T_N_STATE`(`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`, `TRIGGER_STATE`) USING BTREE,
  INDEX `IDX_QRTZ_T_N_G_STATE`(`SCHED_NAME`, `TRIGGER_GROUP`, `TRIGGER_STATE`) USING BTREE,
  INDEX `IDX_QRTZ_T_NEXT_FIRE_TIME`(`SCHED_NAME`, `NEXT_FIRE_TIME`) USING BTREE,
  INDEX `IDX_QRTZ_T_NFT_ST`(`SCHED_NAME`, `TRIGGER_STATE`, `NEXT_FIRE_TIME`) USING BTREE,
  INDEX `IDX_QRTZ_T_NFT_MISFIRE`(`SCHED_NAME`, `MISFIRE_INSTR`, `NEXT_FIRE_TIME`) USING BTREE,
  INDEX `IDX_QRTZ_T_NFT_ST_MISFIRE`(`SCHED_NAME`, `MISFIRE_INSTR`, `NEXT_FIRE_TIME`, `TRIGGER_STATE`) USING BTREE,
  INDEX `IDX_QRTZ_T_NFT_ST_MISFIRE_GRP`(`SCHED_NAME`, `MISFIRE_INSTR`, `NEXT_FIRE_TIME`, `TRIGGER_GROUP`, `TRIGGER_STATE`) USING BTREE,
  CONSTRAINT `qrtz_triggers_ibfk_1` FOREIGN KEY (`SCHED_NAME`, `JOB_NAME`, `JOB_GROUP`) REFERENCES `qrtz_job_details` (`SCHED_NAME`, `JOB_NAME`, `JOB_GROUP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
